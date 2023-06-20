using System.Text;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Models;
using DbDataComparer.Logger.Models;
using DbDataComparer.MSSql;


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
            foreach (TestDefinitionEntity entity in testDefinitionEntities)
                await Finalize(entity);
        }


        public async Task Finalize(TestDefinitionEntity testDefinitionEntity)
        {
            try
            {
                var sprocSql = GenerateFinalizeSprocSql(testDefinitionEntity.Id);
                await this.Database.Execute(this.ConnectionString, sprocSql);
            }

            catch
            { }

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
                // Insert into database, query db entity and update list
                var insertSql = GenerateTestDefinitionInsertSql(testDefinition);
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
            var miscColumns = "[IsTable]";
            var from = "FROM";
            var tableName = "[dbo].[GPDependencyTestDefinitions]";

            return $"{select} {idColumn}, {srcColumns}, {tgtColumns}, {miscColumns} {from} {tableName}";
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
                IsTable = (data[9] == null ? false : Convert.ToBoolean(data[9])),
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
            var miscColumns = "[IsTable]";
            var outputId = "OUTPUT INSERTED.GPDependencyTestDefinitionId";
            var values = "VALUES";
            var srcValues = GenerateTestDefinitionInsertValuesSql(testDefinition.Source);
            var tgtValues = GenerateTestDefinitionInsertValuesSql(testDefinition.Target);
            var miscValues = GenerateTestDefinitionInsertValuesSql(testDefinition);

            return $"{insert} {tableName} ({srcColumns}, {tgtColumns}, {miscColumns}) {outputId} {values} ({srcValues}, {tgtValues}, {miscValues})";
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
        private string GenerateTestDefinitionInsertValuesSql(ExecutionDefinition def)
        {
            StringBuilder sb = new StringBuilder();
            IConnectionProperties connectionProperties = ConnectionPropertiesBuilder.Parse(def.ConnectionString);

            sb.AppendFormat("'{0}', ", connectionProperties.ConnectionBuilderOptions.Server);
            sb.AppendFormat("'{0}', ", connectionProperties.ConnectionBuilderOptions.Database);
            sb.AppendFormat("'{0}', ", FQNParser.GetSchema(def.Text));
            sb.AppendFormat("'{0}'", FQNParser.GetDbObject(def.Text));

            return sb.ToString();
        }


        /// <summary>
        /// Generate SQL for VALUES portion of the INSERT
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Return Values in the following format
        /// IsTable
        /// </remarks>
        private string GenerateTestDefinitionInsertValuesSql(TestDefinition testDefinition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("'{0}'", 0);            // TODO: Update to pull in value from testDefinition Source Type

            return sb.ToString();
        }


        private string GenerateTestResultInsertSql(int testDefinitionId, DateTime comparisonDateTime, ComparisonResult comparisonResult)
        {
            var insert = "INSERT INTO ";
            var tableName = "[dbo].[GPDependencyTestResults]";
            var fkColumns = "[GPDependencyTestDefinitionId]";
            var executionTimeColumns = "[SourceExecutionTime], [TargetExecutionTime]";
            var resultColumns = "[ParameterReturnResult], [ParameterOutputResult], [MetadataResult], [DataResult], [ExecutionResult]";
            var executionPropertyColumns = "[ExecutionTimeDifference], [StartDate]";
            var values = "VALUES";
            var executionTimeValues = GenerateTestResultInsertValuesSql(comparisonResult.TestResult.Source.ExecutionTime, comparisonResult.TestResult.Target.ExecutionTime);
            var resultValues = GenerateTestResultInsertValuesSql(comparisonResult);
            var executionPropertyValues = GenerateTestResultInsertValuesSql(comparisonResult.TestResult.Source.ExecutionTime, comparisonResult.TestResult.Target.ExecutionTime, comparisonDateTime);

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
        private string GenerateTestResultInsertValuesSql(TimeSpan source, TimeSpan target)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("'{0}', ", FormatTimeSpan(source));
            sb.AppendFormat("'{0}'", FormatTimeSpan(target));

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
        private string GenerateTestResultInsertValuesSql(ComparisonResult result)
        {
            StringBuilder sb = new StringBuilder();

            var metaDataResult = GetOverallComparisonResult(result.ResultsetMetaDataResults.Values);
            var dataResult = GetOverallComparisonResult(result.ResultsetDataResults.Values);

            sb.AppendFormat("'{0}', ", result.ParameterReturnResult.Result.ToString());
            sb.AppendFormat("'{0}', ", result.ParameterOutputResult.Result.ToString());
            sb.AppendFormat("'{0}', ", metaDataResult.ToString());
            sb.AppendFormat("'{0}', ", dataResult.ToString());
            sb.AppendFormat("'{0}'", result.ExecutionResult.Result.ToString());

            return sb.ToString();
        }

        private ComparisonResultTypeEnum GetOverallComparisonResult(IEnumerable<TestComparisonResult> comparisonResults)
        {
            // Assume all comparison results passed
            ComparisonResultTypeEnum result = ComparisonResultTypeEnum.Passed;

            result = (result == ComparisonResultTypeEnum.Passed && comparisonResults.Any(x => x.Result == ComparisonResultTypeEnum.Failed))
                      ? ComparisonResultTypeEnum.Failed
                      : ComparisonResultTypeEnum.Passed;

            result = (result == ComparisonResultTypeEnum.Passed && comparisonResults.Any(x => x.Result == ComparisonResultTypeEnum.NotCompared))
                      ? ComparisonResultTypeEnum.NotCompared
                      : ComparisonResultTypeEnum.Passed;

            return result;
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
        private string GenerateTestResultInsertValuesSql(TimeSpan source, TimeSpan target, DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan execTimeDiff = target - source;

            sb.AppendFormat("{0}, ", execTimeDiff.TotalMilliseconds);
            sb.AppendFormat("'{0:yyyy-MM-dd HH:mm:ss}'", dateTime);

            return sb.ToString();
        }


        private string GenerateFinalizeSprocSql(int testDefinitionEntityId)
        {
            return $"EXEC [dbo].[pGPDependencies_U] {testDefinitionEntityId}";
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