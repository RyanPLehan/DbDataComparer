﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class TestDefinitionBuilder
    {
        private readonly IDatabase Database;

        public TestDefinitionBuilder(IDatabase database)
        {
            this.Database = database;
        }

        public async Task<TestDefinition> Build(TestDefinitionBuilderOptions options)
        {
            ValidateBuildOptions(options);

            TestDefinition def = new TestDefinition() { Name = options.Name };

            // Configure source
            def.Source = await this.Database.Explore(options.Source.ConnectionString, 
                                                     options.Source.DatabaseObjectName);
            def.Source.ConnectionString = options.Source.ConnectionString;

            // Configure Target
            def.Target = await this.Database.Explore(options.Target.ConnectionString, 
                                                     options.Target.DatabaseObjectName);
            def.Target.ConnectionString = options.Target.ConnectionString;

            // Create Sample tests
            def.Tests = CreateSampleTests(def.Source, def.Target);
            
            return def;
        }

        private void ValidateBuildOptions(TestDefinitionBuilderOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (String.IsNullOrWhiteSpace(options.Name))
                throw new Exception("Test Definition Name must be supplied");

            if (options.Source == null)
                throw new Exception("Source Database Options must be supplied");
            else
            {
                if (String.IsNullOrWhiteSpace(options.Source.ConnectionString))
                    throw new Exception("Source Connection String must be supplied");

                if (String.IsNullOrWhiteSpace(options.Source.DatabaseObjectName))
                    throw new Exception("Source Database Object Name must be supplied");
            }

            if (options.Target == null)
                throw new Exception("Target Database Options must be supplied");
            else
            {
                if (String.IsNullOrWhiteSpace(options.Target.ConnectionString))
                    throw new Exception("Target Connection String must be supplied");

                if (String.IsNullOrWhiteSpace(options.Target.DatabaseObjectName))
                    throw new Exception("Target Database Object Name must be supplied");
            }
        }


        #region Sample Tests Creation
        private IEnumerable<Test> CreateSampleTests(ExecutionDefinition source, ExecutionDefinition target)
        {
            const int MAX_SAMPLE_TESTS = 3;
            IList<Test> tests = new List<Test>();

            for(int i = 1; i <= MAX_SAMPLE_TESTS; i++)
            {
                Test test = new Test { Name = $"Sample Test {i}" };

                if (source != null && source.Type == System.Data.CommandType.StoredProcedure)
                    test.SourceTestValues = CreateSampleTestValues(source);

                if (target != null && target.Type == System.Data.CommandType.StoredProcedure)
                    test.TargetTestValues = CreateSampleTestValues(target);

                tests.Add(test);
            }

            return tests;
        }

        private IEnumerable<ParameterTestValue> CreateSampleTestValues(ExecutionDefinition command)
        {
            IList<ParameterTestValue> testValues = new List<ParameterTestValue>();

            foreach(Parameter param in command.Parameters)
            {
                ParameterTestValue testValue = new ParameterTestValue() { ParameterName = param.Name };

                if (param.DataType == SqlDbType.Structured)
                    testValue.Values = CreateStructuredValues(param.UserDefinedType);
                else
                    testValue.Value = CreateSimpleValue(param.DataType);

                testValues.Add(testValue);
            }

            return testValues;
        }


        private IEnumerable<IDictionary<string, object>> CreateStructuredValues(UserDefinedType udt)
        {
            const int MAX_SAMPLE_ROW_SIZE = 4;
            IList<IDictionary<string, object>> rows = new List<IDictionary<string, object>>();

            for (int i = 0; i < MAX_SAMPLE_ROW_SIZE; i++)
            {
                IDictionary<string, object> kvp = new Dictionary<string, object>();
                foreach (UdtColumn col in udt.Columns)
                    kvp.Add(col.Name, CreateSimpleValue(col.DataType));

                rows.Add(kvp);
            }
            return rows;
        }


        private object CreateSimpleValue(SqlDbType dataType)
        {
            object value = null;

            switch (dataType)
            {
                case SqlDbType.Bit:
                    value = 1;
                    break;

                case SqlDbType.UniqueIdentifier:
                    value = Guid.Empty;
                    break;

                case SqlDbType.BigInt:
                case SqlDbType.Int:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                    value = 100;
                    break;

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.NText:
                    value = "Some Text Value";
                    break;

                case SqlDbType.Date:
                case SqlDbType.SmallDateTime:
                    value = DateTime.Today;
                    break;

                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.Time:
                    value = DateTime.Now;
                    break;

                case SqlDbType.Decimal:
                case SqlDbType.Float:
                case SqlDbType.Money:
                case SqlDbType.Real:
                case SqlDbType.SmallMoney:
                    value = 10.5;
                    break;

                default:
                    value = "<<< NOT SUPPORTED >>>>";
                    break;
            }

            return value;
        }
        #endregion
    }
}
