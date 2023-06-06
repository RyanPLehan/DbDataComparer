using DbDataComparer.Domain;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Models;
using System.Text;
using DbDataComparer.Logger.Models;
using DbDataComparer.MSSql;
using DbDataComparer.Domain.Utils;

namespace DbDataComparer.Logger
{
    internal class Repository
    {
        private readonly SqlDatabase Database;
        private readonly string ConnectionString;
        private readonly IList<int> ProcessedTestDefinitionIds;
        private IList<TestDefinitionEntity> TestDefinitionEntities;

 
        public Repository(string connectionString)
        {
            this.ConnectionString = connectionString ??
                throw new ArgumentNullException(nameof(connectionString));

            this.Database = new SqlDatabase();
            this.ProcessedTestDefinitionIds = new List<int>();
        }


        public async Task Initialize()
        {
            this.TestDefinitionEntities = await LoadTestDefinitions();
        }

        public async Task Finalize(IEnumerable<TestDefinitionEntity> testDefinitionEntities)
        {
        }

        public TestDefinitionEntity GetTestDefinitionEntity(TestDefinition testDefinition)
        {
            IConnectionProperties srcConnProperties = ConnectionPropertiesBuilder.Parse(testDefinition.Source.ConnectionString);
            IConnectionProperties tgtConnProperties = ConnectionPropertiesBuilder.Parse(testDefinition.Target.ConnectionString);

            string srcSchema = FQNParser.GetSchema(testDefinition.Source.Text);
            string srcObject = FQNParser.GetDbObject(testDefinition.Source.Text);

            string tgtSchema = FQNParser.GetSchema(testDefinition.Target.Text);
            string tgtObject = FQNParser.GetDbObject(testDefinition.Target.Text);

            return this.TestDefinitionEntities
                       .Where(x => x.SourceServer.Equals(srcConnProperties.ConnectionBuilderOptions.Server, StringComparison.OrdinalIgnoreCase) &&
                                   x.SourceDatabase.Equals(srcConnProperties.ConnectionBuilderOptions.Database, StringComparison.OrdinalIgnoreCase) &&
                                   x.SourceSchema.Equals(srcSchema, StringComparison.OrdinalIgnoreCase) &&
                                   x.SourceObject.Equals(srcObject, StringComparison.OrdinalIgnoreCase) &&
                                   x.TargetServer.Equals(tgtConnProperties.ConnectionBuilderOptions.Server, StringComparison.OrdinalIgnoreCase) &&
                                   x.TargetDatabase.Equals(tgtConnProperties.ConnectionBuilderOptions.Database, StringComparison.OrdinalIgnoreCase) &&
                                   x.TargetSchema.Equals(tgtSchema, StringComparison.OrdinalIgnoreCase) &&
                                   x.TargetObject.Equals(tgtObject, StringComparison.OrdinalIgnoreCase))
                       .FirstOrDefault();
        }


        public async Task<TestDefinitionEntity> Insert(TestDefinition testDefinition)
        {
            TestDefinitionEntity entity = null;

            try
            {
                // Insert into database and update list
                var insertSql = $"{GenerateTestDefinitionInsertSql(testDefinition)} OUTPUT INSERTED.ID";
                var id = await this.Database.ExecuteScalar<int>(this.ConnectionString, insertSql);
                var selectSql = GenerateTestDefinitionSelectSql(id);
                var data = await this.Database.Execute(this.ConnectionString, selectSql);

                int rowCnt = (data == null) ? 0 : data.Length;
                if (rowCnt > 0)
                {
                    entity = CreateTestDefinitionEntity(data[0]);
                    this.TestDefinitionEntities.Add(entity);
                }
            }

            catch
            { }

            return entity;
        }

        public async Task Insert(int testDefinitionEntityId, DateTime comparisonDateTime, ComparisonResult comparisonResult)
        {
            try
            {
                var insertSql = GenerateTestResultInsertSql(testDefinitionEntityId, comparisonDateTime, comparisonResult);
                await this.Database.ExecuteNonQuery(this.ConnectionString, insertSql);
            }

            catch
            { }
        }


        #region Initialization and Lookup Routines
        private async Task<IList<TestDefinitionEntity>> LoadTestDefinitions()
        {
            IList<TestDefinitionEntity> ret = new List<TestDefinitionEntity>();
            var sql = GenerateTestDefinitionSelectSql();
            try
            {
                var resultSet = await this.Database.Execute(this.ConnectionString, sql);
                ret = CreateTestDefinitionEntities(resultSet);
            }
            catch { }

            return ret;
        }

        private string GenerateTestDefinitionSelectSql()
        {
            var select = "SELECT";
            var idColumn = "[GPDependencyTestDefinitionId] AS TestDefinitionId";
            var srcColumns = "[SourceServer], [SourceDatabase], [SourceSchema], [SourceObject]";
            var tgtColumns = "[TargetServer], [TargetDatabase], [TargetSchema], [TargetObject]";
            var from = "FROM";
            var tableName = "[dbo].[GPDependencyTestDefinitions]";

            return $"{select} {idColumn}, {srcColumns}, {tgtColumns} {from} {tableName}";
        }

        private string GenerateTestDefinitionSelectSql(int id)
        {
            var select = GenerateTestDefinitionSelectSql();
            var where = "WHERE";
            var whereClause = $"[GPDependencyTestDefinitionId] = {id}";

            return $"{select} {where} {whereClause}";
        }


        private IList<TestDefinitionEntity> CreateTestDefinitionEntities(object[][] data)
        {
            IList<TestDefinitionEntity> ret = new List<TestDefinitionEntity>();

            int rowCnt = (data == null) ? 0 : data.Length;
            for (int r = 0; r < rowCnt; r++)
                ret.Add(CreateTestDefinitionEntity(data[r]));

            return ret;
        }

        private TestDefinitionEntity CreateTestDefinitionEntity(object[] data)
        {            
            return new TestDefinitionEntity()
            {
                Id = Convert.ToInt32(data[0]),
                SourceServer = Convert.ToString(data[1]),
                SourceDatabase = Convert.ToString(data[2]),
                SourceSchema = Convert.ToString(data[3]),
                SourceObject = Convert.ToString(data[4]),
                TargetServer = Convert.ToString(data[5]),
                TargetDatabase = Convert.ToString(data[6]),
                TargetSchema = Convert.ToString(data[7]),
                TargetObject = Convert.ToString(data[8]),
            };
        }
        #endregion


        #region Insert Routines
        private string GenerateTestDefinitionInsertSql(TestDefinition testDefinition)
        {
            var insert = "INSERT INTO ";
            var tableName = "[dbo].[GPDependencyTestDefinitions]";
            var srcColumns = "[SourceServer], [SourceDatabase], [SourceSchema], [SourceObject]";
            var tgtColumns = "[TargetServer], [TargetDatabase], [TargetSchema], [TargetObject]";
            var values = "VALUES";
            var srcValues = GenerateInsertValuesSql(testDefinition.Source);
            var tgtValues = GenerateInsertValuesSql(testDefinition.Target);

            return $"{insert} {tableName} ({srcColumns}, {tgtColumns}) {values} ({srcValues}, {tgtValues})";
        }


        /// <summary>
        /// Generate SQL for VALUES portion of the INSERT
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        /// <remarks>
        /// Return Values in the following format
        /// Server, Database, Schema, Object
        /// </remarks>
        private string GenerateInsertValuesSql(ExecutionDefinition def)
        {
            StringBuilder sb = new StringBuilder();
            IConnectionProperties connectionProperties = ConnectionPropertiesBuilder.Parse(def.ConnectionString);

            sb.AppendFormat("{0}, ", connectionProperties.ConnectionBuilderOptions.Server);
            sb.AppendFormat("{0}, ", connectionProperties.ConnectionBuilderOptions.Database);
            sb.AppendFormat("{0}, ", FQNParser.GetSchema(def.Text));
            sb.AppendFormat("{0}", FQNParser.GetDbObject(def.Text));

            return sb.ToString();
        }


        private string GenerateTestResultInsertSql(int testDefinitionId, DateTime comparisonDateTime, ComparisonResult comparisonResult)
        {
            var insert = "INSERT INTO ";
            var tableName = "[dbo].[GPDependencyTestResult]";
            var fkColumns = "[GPDependencyTestDefinitionId]";
            var executionTimeColumns = "[SourceExecutionTime], [TargetExecutionTime]";
            var resultColumns = "[ParameterReturnResult], [ParameterOutputResult], [MetadataResult], [DataResult], [ExecutionResult]";
            var executionPropertyColumns = "[ExecutionTimeDifference], [StartDate]";
            var values = "VALUES";
            var executionTimeValues = GenerateInsertValuesSql(comparisonResult.TestResult.Source.ExecutionTime, comparisonResult.TestResult.Target.ExecutionTime);
            var resultValues = GenerateInsertValuesSql(comparisonResult);
            var executionPropertyValues = GenerateInsertValuesSql(comparisonResult.TestResult.Source.ExecutionTime, comparisonResult.TestResult.Target.ExecutionTime, comparisonDateTime);

            return $"{insert} {tableName} ({fkColumns}, {executionTimeColumns}, {resultColumns}, {executionPropertyColumns}) {values} ({testDefinitionId}, {executionTimeValues}, {resultValues}, {executionPropertyValues})";
        }


        /// <summary>
        /// Generate SQL for VALUES portion of the INSERT
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        /// <remarks>
        /// Return Values in the following format
        /// SourceExecutionTime, TargetExecutionTime
        /// </remarks>
        private string GenerateInsertValuesSql(TimeSpan source, TimeSpan target)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("'{0}', ", FormatTimeSpan(source));
            sb.AppendFormat("'{0}' ", FormatTimeSpan(target));

            return sb.ToString();
        }



        /// <summary>
        /// Generate SQL for VALUES portion of the INSERT
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        /// <remarks>
        /// Return Values in the following format
        /// ParameterReturnResult, ParameterOutputResult, MetadataResult, DataResult, ExecutionResult
        /// </remarks>
        private string GenerateInsertValuesSql(ComparisonResult result)
        {
            StringBuilder sb = new StringBuilder();

            var metaDataResult = result.ResultsetMetaDataResults.FirstOrDefault();
            var dataResult = result.ResultsetDataResults.FirstOrDefault();

            sb.AppendFormat("'{0}', ", result.ParameterReturnResult);
            sb.AppendFormat("'{0}', ", result.ParameterOutputResult);
            sb.AppendFormat("'{0}', ", metaDataResult);
            sb.AppendFormat("'{0}'", dataResult);

            return sb.ToString();
        }


        /// <summary>
        /// Generate SQL for VALUES portion of the INSERT
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        /// <remarks>
        /// Return Values in the following format
        /// SourceExecutionTime, TargetExecutionTime
        /// </remarks>
        private string GenerateInsertValuesSql(TimeSpan source, TimeSpan target, DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan execTimeDiff = target - source;

            sb.AppendFormat("{0}, ", execTimeDiff.TotalMilliseconds);
            sb.AppendFormat("'{0:yyyy-MM-dd HH:mm:ss}' ", dateTime);

            return sb.ToString();
        }


        private string FormatTimeSpan(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours,
                                                                 ts.Minutes,
                                                                 ts.Seconds,
                                                                 ts.Milliseconds);
        }
        #endregion
    }
}