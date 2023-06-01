using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using DbDataComparer.Domain.Utils;
using System.Data.SqlTypes;
using DbDataComparer.Domain.Enums;
using System.Reflection.PortableExecutable;

namespace DbDataComparer.MSSql
{
    internal class Explorer
    {
        public async Task<ExecutionDefinition> Explore(SqlConnection connection, 
                                                       string databaseObjectName)
        {
            // Assume database object is a unknown.  Create default select
            ExecutionDefinition exeDefinition = new ExecutionDefinition() 
            { 
                Text = $"Database Object ({databaseObjectName}) not found", 
                Type = DatabaseObjectTypeEnum.Unknown,
            };

            var schema = FQNParser.GetSchema(databaseObjectName);
            var dbObject = FQNParser.GetDbObject(databaseObjectName);
            var dbObjectType = await GetDatabaseObjectType(connection, schema, dbObject);

            if (dbObjectType != DatabaseObjectTypeEnum.Unknown)
            {
                exeDefinition.Text = databaseObjectName;
                exeDefinition.Type = dbObjectType;

                if (dbObjectType == DatabaseObjectTypeEnum.StoredProcedure ||
                    dbObjectType == DatabaseObjectTypeEnum.ScalarFunction ||
                    dbObjectType == DatabaseObjectTypeEnum.TableFunction)
                {
                    exeDefinition.Parameters = await GetParameters(connection, schema, dbObject);
                }
            }

            return exeDefinition;
        }

        private async Task<DatabaseObjectTypeEnum> GetDatabaseObjectType(SqlConnection connection, string schema, string databaseObject)
        {
            DatabaseObjectTypeEnum ret = DatabaseObjectTypeEnum.Unknown;

            ret = (ret == DatabaseObjectTypeEnum.Unknown)
                ? await GetRoutineType(connection, schema, databaseObject)
                : DatabaseObjectTypeEnum.Unknown;

            ret = (ret == DatabaseObjectTypeEnum.Unknown)
                ? await GetTableViewType(connection, schema, databaseObject)
                : DatabaseObjectTypeEnum.Unknown;

            return ret;
        }

        #region Stored Procedure/Function Check/Generation
        private async Task<DatabaseObjectTypeEnum> GetRoutineType(SqlConnection connection, string schema, string databaseObject)
        {
            DatabaseObjectTypeEnum ret = DatabaseObjectTypeEnum.Unknown;
            var sql = GenerateRoutinesSql(schema, databaseObject);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                ret = (ret == DatabaseObjectTypeEnum.Unknown && IsStoredProcedure(reader)) 
                    ? DatabaseObjectTypeEnum.StoredProcedure 
                    : DatabaseObjectTypeEnum.Unknown;

                ret = (ret == DatabaseObjectTypeEnum.Unknown && IsFunction(reader) && IsScalarFunction(reader))
                    ? DatabaseObjectTypeEnum.ScalarFunction
                    : DatabaseObjectTypeEnum.Unknown;

                ret = (ret == DatabaseObjectTypeEnum.Unknown && IsFunction(reader) && IsTableFunction(reader))
                    ? DatabaseObjectTypeEnum.TableFunction
                    : DatabaseObjectTypeEnum.Unknown;
            }

            await reader.CloseAsync();

            return ret;
        }

        private bool IsStoredProcedure(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("ROUTINE_TYPE");
            return (!reader.IsDBNull(index) && reader.GetString(index).Equals("PROCEDURE", StringComparison.OrdinalIgnoreCase));
        }

        private bool IsFunction(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("ROUTINE_TYPE");
            return (!reader.IsDBNull(index) && reader.GetString(index).Equals("FUNCTION", StringComparison.OrdinalIgnoreCase));
        }

        private bool IsScalarFunction(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("DATA_TYPE");
            return (!reader.IsDBNull(index) && !reader.GetString(index).Equals("TABLE", StringComparison.OrdinalIgnoreCase));
        }

        private bool IsTableFunction(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("DATA_TYPE");
            return (!reader.IsDBNull(index) && reader.GetString(index).Equals("TABLE", StringComparison.OrdinalIgnoreCase));
        }


        private string GenerateRoutinesSql(string schema, string routineName)
        {
            var select = "SELECT ROUTINE_CATALOG, ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE, DATA_TYPE";
            var from = "FROM INFORMATION_SCHEMA.ROUTINES";
            var where = $"WHERE ROUTINE_SCHEMA = '{schema}' AND ROUTINE_NAME = '{routineName}'";

            return $"{select} {from} {where}";
        }
        #endregion

        #region Table/View Check/Generation
        private async Task<DatabaseObjectTypeEnum> GetTableViewType(SqlConnection connection, string schema, string databaseObject)
        {
            DatabaseObjectTypeEnum ret = DatabaseObjectTypeEnum.Unknown;
            var sql = GenerateTableViewSql(schema, databaseObject);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                ret = (ret == DatabaseObjectTypeEnum.Unknown && IsTable(reader))
                    ? DatabaseObjectTypeEnum.Table
                    : DatabaseObjectTypeEnum.Unknown;

                ret = (ret == DatabaseObjectTypeEnum.Unknown && IsView(reader))
                    ? DatabaseObjectTypeEnum.View
                    : DatabaseObjectTypeEnum.Unknown;
            }

            await reader.CloseAsync();

            return ret;
        }

        private bool IsTable(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("TABLE_TYPE");
            return (!reader.IsDBNull(index) && !reader.GetString(index).Equals("VIEW", StringComparison.OrdinalIgnoreCase));
        }

        private bool IsView(SqlDataReader reader)
        {
            int index = reader.GetOrdinal("TABLE_TYPE");
            return (!reader.IsDBNull(index) && reader.GetString(index).Equals("VIEW", StringComparison.OrdinalIgnoreCase));
        }

        private string GenerateTableViewSql(string schema, string routineName)
        {
            var select = "SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE";
            var from = "FROM INFORMATION_SCHEMA.TABLES";
            var where = $"WHERE TABLE_SCHEMA = '{schema}' AND TABLE_NAME = '{routineName}'";

            return $"{select} {from} {where}";
        }
        #endregion

        #region Parameter Generation
        private async Task<IEnumerable<Parameter>> GetParameters(SqlConnection connection, string schema, string routineName)
        {
            IList<Parameter> parameters = new List<Parameter>();
            var sql = GenerateParametersSql(schema, routineName);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                parameters.Add(CreateParameter(reader));

            await reader.CloseAsync();

            // Normally, this would be in the CreateParameter routine, but this requires a sql connection, so needs to be here
            foreach (Parameter param in parameters.Where(x => x.DataType == SqlDbType.Structured))
                param.UserDefinedType.Columns = await CreateUDTColumns(connection, param.UserDefinedType);

            return parameters;
        }

        private string GenerateParametersSql(string schema, string routineName)
        {
            var select = "SELECT ORDINAL_POSITION, PARAMETER_MODE, PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, USER_DEFINED_TYPE_SCHEMA, USER_DEFINED_TYPE_NAME";
            var from = "FROM INFORMATION_SCHEMA.PARAMETERS";

            // Ordinal Position 0 is a special output parameter type for Scalar functions only, therefore do not get as part of parameter list
            var where = $"WHERE SPECIFIC_SCHEMA = '{schema}' AND SPECIFIC_NAME = '{routineName}' AND ORDINAL_POSITION > 0";
            var orderBy = "ORDER BY ORDINAL_POSITION";

            return $"{select} {from} {where} {orderBy}";
        }


        private Parameter CreateParameter(IDataRecord record)
        {
            Parameter param = new Parameter()
            {
                Name = GetParameterName(record),
                DataType = GetParameterDataType(record),
                CharacterMaxLength = GetParameterCharacterMaxLength(record),
                Direction = GetParameterDirection(record),
                IsNullable = true,                              // Default to true b/c there is no way to determine actual value
            };

            // Create initial UDT object if it is a structure
            if (param.DataType == SqlDbType.Structured)
                param.UserDefinedType = CreateUDT(record);

            return param;
        }

        private string GetParameterName(IDataRecord record)
        {
            int index = record.GetOrdinal("PARAMETER_NAME");
            return record.GetString(index);
        }

        private SqlDbType GetParameterDataType(IDataRecord record)
        {
            SqlDbType ret;
            int index = record.GetOrdinal("DATA_TYPE");
            string value = record.GetString(index).ToUpper();

            if (value.Equals("table type", StringComparison.OrdinalIgnoreCase))
                ret = SqlDbType.Structured;
            else
                ret = Domain.Utils.Enum.ToEnum<SqlDbType>(value);

            return ret;
        }

        private int GetParameterCharacterMaxLength(IDataRecord record)
        {
            int index = record.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
            return record.IsDBNull(index) ? 0 : record.GetInt32(index);
        }

        private ParameterDirection GetParameterDirection(IDataRecord record)
        {
            ParameterDirection ret = ParameterDirection.Input;
            int index = record.GetOrdinal("PARAMETER_MODE");
            string value = record.GetString(index).ToUpper();

            switch (value)
            {
                case "IN":
                    ret = ParameterDirection.Input;
                    break;

                case "OUT":
                    ret = ParameterDirection.Output;
                    break;

                case "INOUT":
                    ret = ParameterDirection.InputOutput;
                    break;
            }

            return ret;
        }
        #endregion


        #region User Defined Type Generation
        private UserDefinedType CreateUDT(IDataRecord record)
        {
            int index = record.GetOrdinal("USER_DEFINED_TYPE_SCHEMA");
            string schema = record.GetString(index);

            index = record.GetOrdinal("USER_DEFINED_TYPE_NAME");
            string name = record.GetString(index);

            return new UserDefinedType()
            {
                Name = name,
                Schema = schema,
                Columns = Enumerable.Empty<UdtColumn>(),
            };
        }

        private async Task<IEnumerable<UdtColumn>> CreateUDTColumns(SqlConnection connection, UserDefinedType udt)
        {
            IList<UdtColumn> columns = new List<UdtColumn>();
            var sql = GenerateUserDefinedTableSql(udt.Schema, udt.Name);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                columns.Add(CreateUdtColumn(reader));

            await reader.CloseAsync();

            return columns;
        }

        private string GenerateUserDefinedTableSql(string schema, string userDefinedTypeName)
        {
            var select = "SELECT tt.[Name] AS [UDT_Name], c.[Name] AS [Column_Name], c.[Column_Id], c.[System_Type_Id], t.[Name] AS [DataType_Name], t.[max_length]";
            var from = "FROM sys.table_types tt";
            var join1 = "INNER JOIN sys.schemas s ON tt.[schema_id] = s.[schema_id]";
            var join2 = "INNER JOIN sys.columns c ON c.[object_id] = tt.[type_table_object_id]";
            var join3 = "INNER JOIN sys.types t ON c.[system_type_id] = t.[system_type_id]";
            var where = $"WHERE tt.[name] = '{userDefinedTypeName}' AND s.[name] = '{schema}'";
            var orderBy = "ORDER BY c.[column_id]";

            return $"{select} {from} {join1} {join2} {join3} {where} {orderBy}";
        }

        private UdtColumn CreateUdtColumn(IDataRecord record)
        {
            return new UdtColumn()
            {
                Name = GetUdtColumnName(record),
                DataType = GetUdtColumnDataType(record),
            };
        }

        private string GetUdtColumnName(IDataRecord record)
        {
            int index = record.GetOrdinal("Column_Name");
            return record.GetString(index);
        }

        private SqlDbType GetUdtColumnDataType(IDataRecord record)
        {
            int index = record.GetOrdinal("DataType_Name");
            string value = record.GetString(index).ToUpper();
            return Domain.Utils.Enum.ToEnum<SqlDbType>(value);
        }
        #endregion


        private SqlCommand CreateCommand(SqlConnection connection, string commandText)
        {
            return new SqlCommand()
            {
                Connection = connection,
                CommandText = commandText,
                CommandType = CommandType.Text,
            };
        }
    }
}
