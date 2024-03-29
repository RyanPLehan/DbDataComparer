﻿using System;
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
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace DbDataComparer.MSSql
{
    internal class Executioner
    {
        private IEnumerable<SqlDbType> TextTypes = new SqlDbType[] { SqlDbType.Char, SqlDbType.NChar, SqlDbType.NVarChar, SqlDbType.VarChar };

        /// <summary>
        /// Execute raw sql
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<object[][]> Execute(SqlConnection connection, 
                                              string sql)
        {
            var sqlCmd = CreateCommand(connection, sql);
            SqlDataReader dataReader = await sqlCmd.ExecuteReaderAsync();
            var result = await CreateResultSetValues(dataReader);
            await dataReader.CloseAsync();

            return result;
        }


        /// <summary>
        /// Execute Specific Sql statement
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="executionDefinition"></param>
        /// <param name="testValues"></param>
        /// <returns></returns>
        public async Task<ExecutionResult> Execute(SqlConnection connection,
                                                   ExecutionDefinition executionDefinition,
                                                   string sql)
        {
            var sqlCmd = CreateCommand(connection, executionDefinition, sql);
            return await Execute(sqlCmd, executionDefinition);
        }


        /// <summary>
        /// Execute Stored Procedure
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="executionDefinition"></param>
        /// <param name="testValues"></param>
        /// <returns></returns>
        public async Task<ExecutionResult> Execute(SqlConnection connection, 
                                                   ExecutionDefinition executionDefinition, 
                                                   IEnumerable<ParameterTestValue> testValues)
        {
            var sqlCmd = CreateCommand(connection, executionDefinition, testValues);
            return await Execute(sqlCmd, executionDefinition);
        }


        /// <summary>
        /// Execute raw sql
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalar<T>(SqlConnection connection,
                                              string sql)
        {
            T result = default(T);
            var sqlCmd = CreateCommand(connection, sql);
            var dbResult = await sqlCmd.ExecuteScalarAsync();

            if (dbResult != null)
                result = (T)dbResult;

            return result;
        }

        public async Task ExecuteNonQuery(SqlConnection connection,
                                          string sql)
        {
            var sqlCmd = CreateCommand(connection, sql);
            await sqlCmd.ExecuteNonQueryAsync();
        }

        private async Task<ExecutionResult> Execute(SqlCommand sqlCmd, ExecutionDefinition executionDefinition)
        {
            ExecutionResult result = new ExecutionResult(executionDefinition);

            SqlDataReader dataReader = await sqlCmd.ExecuteReaderAsync();
            result.ResultSets = await GetResultSets(dataReader);
            await dataReader.CloseAsync();

            result.OutputParameterResults = GetOutputValues(sqlCmd);     // Must come after reading through datareader & closing it
            result.ReturnResult = GetReturnResult(sqlCmd);               // Must come after reading through datareader & closing it

            return result;
        }

        #region Command Creation
        private SqlCommand CreateCommand(SqlConnection connection,
                                         string sql)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = connection,
                CommandText = sql,
                CommandType = CommandType.Text,
                CommandTimeout = 60,
            };

            return sqlCommand;
        }

        private SqlCommand CreateCommand(SqlConnection connection,
                                         ExecutionDefinition executionDefinition,
                                         string sql)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = connection,
                CommandText = sql,
                CommandType = executionDefinition.Type,
                CommandTimeout = executionDefinition.ExecutionTimeoutInSeconds,
            };

            return sqlCommand;
        }

        private SqlCommand CreateCommand(SqlConnection connection, 
                                         ExecutionDefinition executionDefinition, 
                                         IEnumerable<ParameterTestValue> testValues)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = connection,
                CommandText = executionDefinition.Text,
                CommandType = executionDefinition.Type,
                CommandTimeout = executionDefinition.ExecutionTimeoutInSeconds,
            };

            if (testValues.Any())
                BuildParameters(sqlCommand.Parameters, executionDefinition, testValues);

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
                DataColumn col = new DataColumn()
                {
                    ColumnName = udtCol.Name,
                    DataType = DatabaseTypeConverter.ToNetType(udtCol.DataType),
                };

                dt.Columns.Add(col);
            }

            return dt;
        }

        private DataTable PopulateDataTable(DataTable table, IEnumerable<IDictionary<string, object>> values)
        {
            if (values == null)
                return table;

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
