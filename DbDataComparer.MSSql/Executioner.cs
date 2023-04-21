using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using System.Collections.ObjectModel;

namespace DbDataComparer.MSSql
{
    internal class Executioner
    {
        private IEnumerable<SqlDbType> TextTypes = new SqlDbType[] { SqlDbType.Char, SqlDbType.NChar, SqlDbType.NVarChar, SqlDbType.VarChar };


        public async Task<ExecutionResult> Execute(SqlConnection connection, 
                                                   ExecutionDefinition executionDefinition, 
                                                   IEnumerable<ParameterTestValue> testValues)
        {
            ExecutionResult result = new ExecutionResult(executionDefinition);

            var sqlCmd = CreateCommand(connection, executionDefinition, testValues);
            SqlDataReader dataReader = await sqlCmd.ExecuteReaderAsync();
            result.ResultSets = await GetResultSets(dataReader);
            await dataReader.CloseAsync();

            result.OutputParameterResults = GetOutputValues(sqlCmd);     // Must come after reading through datareader & closing it
            result.ReturnResult = GetReturnResult(sqlCmd);               // Must come after reading through datareader & closing it

            return result;
        }

        #region Command Creation
        private SqlCommand CreateCommand(SqlConnection connection, 
                                         ExecutionDefinition command, 
                                         IEnumerable<ParameterTestValue> testValues)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = connection,
                CommandText = command.Text,
                CommandType = command.Type,
                CommandTimeout = command.ExecutionTimeoutInSeconds,
            };

            if (command.Type == CommandType.StoredProcedure)
                BuildParameters(sqlCommand.Parameters, command, testValues);

            return sqlCommand;
        }

        private void BuildParameters(SqlParameterCollection sqlParameterCollection, 
                                     ExecutionDefinition command, 
                                     IEnumerable<ParameterTestValue> testValues)
        {
            // Create and Populate Sql Parameters
            // Normally, I would separate the population of the Parameter Values, but do to UserDefined Table Types, makes it easier to do it here
            foreach (Parameter cmdParam in command.Parameters)
            {
                SqlParameter sqlParam = CreateParameter(cmdParam);


                // Lookup to see if we have any test value associated with the parameter
                var paramTestValue = testValues.FirstOrDefault(x => x.ParameterName.Equals(sqlParam.ParameterName, StringComparison.OrdinalIgnoreCase));

                if (paramTestValue != null)
                {
                    if (sqlParam.SqlDbType != SqlDbType.Structured)
                        sqlParam.Value = paramTestValue.Value;
                    else
                    {
                        DataTable dt = CreateDataTable(cmdParam.UserDefinedType);                        
                        sqlParam.Value = PopulateDataTable(dt, paramTestValue.Values);
                    }
                }

                sqlParameterCollection.Add(sqlParam);
            }
        }

        private SqlParameter CreateParameter(Parameter parameter)
        {
            SqlParameter sqlParameter = new SqlParameter()
            {
                ParameterName = parameter.Name,
                SqlDbType = parameter.DataType,
                Direction = parameter.Direction,
                IsNullable = parameter.IsNullable,
            };

            if (IsTextType(parameter.DataType))
                sqlParameter.Size = parameter.CharacterMaxLength;

            // Check for User Defined Table Type
            if (parameter.DataType == SqlDbType.Structured)
                sqlParameter.TypeName = BuildUdtTypeName(parameter.UserDefinedType);

            return sqlParameter;
        }

        private bool IsTextType(SqlDbType sqlDbType)
        {
            return this.TextTypes.Any(x => x == sqlDbType);
        }

        private string BuildUdtTypeName(UserDefinedType udt)
        {
            string schema = String.IsNullOrWhiteSpace(udt.Schema) ? FQNParser.DEFAULT_SCHEMA : udt.Schema;
            return $"{schema}.{udt.Name}";
        }

        private DataTable CreateDataTable(UserDefinedType udt)
        {
            DataTable dt = new DataTable();

            // Build Columns
            foreach(UdtColumn udtCol in udt.Columns)
            {
                // Use SqlParameter as a way to convert from SqlDbType to DbType
                SqlParameter param = new SqlParameter("name", udtCol.DataType);
                DataColumn col = new DataColumn()
                {
                    ColumnName = udtCol.Name,
                    DataType = param.DbType.GetType()
                };

                dt.Columns.Add(col);
            }

            return dt;
        }

        private DataTable PopulateDataTable(DataTable table, IEnumerable<IDictionary<string, object>> values)
        {
            foreach (IDictionary<string, object> row in values)
            {
                DataRow dataRow = table.NewRow();

                foreach (KeyValuePair<string, object> col in row)
                {
                    if (table.Columns.Contains(col.Key))
                        dataRow[col.Key] = col.Value;
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        #endregion


        #region Acquiring Results
        private async Task<IDictionary<int, ResultSet>> GetResultSets(SqlDataReader dataReader)
        {
            int key = 0;
            IDictionary<int, ResultSet> resultSets = new Dictionary<int, ResultSet>();

            // Iterate through all result sets
            do
            {
                resultSets.Add(key, await CreateResultSet(dataReader));
                key++;
            } while (await dataReader.NextResultAsync());

            return resultSets;
        }

        private async Task<ResultSet> CreateResultSet(SqlDataReader reader)
        {
            ResultSet rs = new ResultSet();
            rs.MetaData = CreateResultSetMetaData(await reader.GetColumnSchemaAsync());
            rs.Values = await CreateResultSetValues(reader);

            return rs;
        }

        private IEnumerable<ResultSetMetaData> CreateResultSetMetaData(ReadOnlyCollection<DbColumn> columns)
        {
            IList<ResultSetMetaData> list = new List<ResultSetMetaData>();

            foreach(DbColumn col in columns)
            {
                var item = new ResultSetMetaData()
                {
                    Name = col.ColumnName,
                    OrdinalPosition = col.ColumnOrdinal.GetValueOrDefault(),
                    DataTypeName = col.DataTypeName,
                    Length = col.ColumnSize.GetValueOrDefault(),
                    IsNullable = col.AllowDBNull.GetValueOrDefault(),
                };

                list.Add(item);
            }

            return list;
        }


        private async Task<object[][]> CreateResultSetValues(SqlDataReader reader)
        {
            IList<object[]> rows = new List<object[]>();

            while (await reader.ReadAsync())
            {
                object[] cols = new object[reader.FieldCount];
                reader.GetValues(cols);
                rows.Add(cols);
            }

            return rows.ToArray();
        }

        private IDictionary<string, object?> GetOutputValues(SqlCommand command)
        {
            IDictionary<string, object?> values = new Dictionary<string, object?>();

            // Guard Clause
            if (command.CommandType != CommandType.StoredProcedure)
                return values;

            foreach (SqlParameter parameter in command.Parameters)
            {
                switch (parameter.Direction)
                {
                    case ParameterDirection.Output:
                    case ParameterDirection.InputOutput:
                        values.Add(parameter.ParameterName, parameter.Value);
                        break;
                }
            }

            return values;
        }

        private int GetReturnResult(SqlCommand command)
        {
            int value = 0;

            // Guard Clause
            if (command.CommandType != CommandType.StoredProcedure)
                return value;

            foreach (SqlParameter parameter in command.Parameters)
            {
                switch (parameter.Direction)
                {
                    case ParameterDirection.ReturnValue:
                        value = (parameter.Value == null ? 0 : Convert.ToInt32(parameter.Value));
                        break;
                }
            }

            return value;
        }
        #endregion
    }
}
