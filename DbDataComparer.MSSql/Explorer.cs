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

namespace DbDataComparer.MSSql
{
    internal class Explorer
    {
        public async Task<Command> Explore(SqlConnection connection, 
                                           string databaseObject)
        {
            // Assume database object is a table/view.  Create default select
            Command command = new Command() 
            { 
                Text = $"SELECT TOP 10 * FROM {databaseObject}", 
                Type = CommandType.Text 
            };

            var schema = FQNParser.GetSchema(databaseObject);
            var dbObject = FQNParser.GetDbObject(databaseObject);

            if (await IsStoredProcedure(connection, schema, dbObject))
            {
                command.Text = databaseObject;
                command.Type = CommandType.StoredProcedure;
                command.Parameters = await GetParameters(connection, schema, dbObject);
            }

            return command;
        }


        #region Stored Procedure Check/Generation
        private async Task<bool> IsStoredProcedure(SqlConnection connection, string schema, string databaseObject)
        {
            bool ret = false;
            var sql = GenerateRoutinesSql(schema, databaseObject);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int index = reader.GetOrdinal("ROUTINE_TYPE");
                ret = (!reader.IsDBNull(index) && reader.GetString(index).Equals("PROCEDURE", StringComparison.OrdinalIgnoreCase));
            }

            await reader.CloseAsync();

            return ret;
        }

        private string GenerateRoutinesSql(string schema, string routineName)
        {
            var select = "SELECT ROUTINE_CATALOG, ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE";
            var from = "FROM INFORMATION_SCHEMA.ROUTINES";
            var where = $"WHERE ROUTINE_SCHEMA = '{schema}' AND ROUTINE_NAME = '{routineName}'";

            return $"{select} {from} {where}";
        }
        #endregion


        #region Parameter Generation
        private async Task<IEnumerable<Parameter>> GetParameters(SqlConnection connection, string schema, string sprocName)
        {
            IList<Parameter> parameters = new List<Parameter>();
            var sql = GenerateParametersSql(schema, sprocName);
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
            var where = $"WHERE SPECIFIC_SCHEMA = '{schema}' AND SPECIFIC_NAME = '{routineName}'";
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
            var sql = GenerateUserDefinedTableSql(udt.Name);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                columns.Add(CreateUdtColumn(reader));

            await reader.CloseAsync();

            return columns;
        }

        private string GenerateUserDefinedTableSql(string userDefinedTypeName)
        {
            var select = "SELECT tt.[Name] AS [UDT_Name], c.[Name] AS [Column_Name], c.[Column_Id], c.[System_Type_Id], t.[Name] AS [DataType_Name], t.[max_length]";
            var from = "FROM sys.table_types tt";
            var join1 = $"INNER JOIN sys.columns c ON tt.[name] = '{userDefinedTypeName}' AND c.[object_id] = tt.[type_table_object_id]";
            var join2 = "INNER JOIN sys.types t ON c.[system_type_id] = t.[system_type_id]";
            var orderBy = "ORDER BY c.[column_id]";

            return $"{select} {from} {join1} {join2} {orderBy}";
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
