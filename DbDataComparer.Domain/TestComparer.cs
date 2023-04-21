using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;


namespace DbDataComparer.Domain
{
    /// <summary>
    /// This class is designed to compare test results from Source and Target.
    /// The Target must meet ** at the minimum ** the same results as the Source.
    /// This allows the Target to have additional data to support future needs
    /// </summary>
    public class TestComparer : ITestComparer
    {
        /// <summary>
        /// This will compare the Output Parameters.  This will compare Source => Target only, not vice-versa
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks>Output Parameter names must match (case sensitive)</remarks>
        public TestComparisonResult CompareParameterOutput(ExecutionResult source, ExecutionResult target)
        {
            TestComparisonResult tcr = new TestComparisonResult();
            IList<string> failureDescriptions = new List<string>();
            StringBuilder sb = new StringBuilder();

            if (source.ExecutionDefinition.Type == CommandType.StoredProcedure &&
                target.ExecutionDefinition.Type == CommandType.StoredProcedure)
            {
                // Default to Passed
                tcr.Result = ComparisonResultTypeEnum.Passed;

                foreach (KeyValuePair<string, object> srcKvp in source.OutputParameterResults)
                {
                    // Lookup Target information
                    KeyValuePair<string, object> tgtKvp = target.OutputParameterResults
                                                                .FirstOrDefault(x => x.Key == srcKvp.Key);

                    // Check Keys, DataTypes and Values
                    if (!srcKvp.Key.Equals(tgtKvp.Key) ||
                        !IsSameValue(srcKvp.Value, tgtKvp.Value))
                    {
                        // Marked as failed
                        tcr.Result = ComparisonResultTypeEnum.Failed;

                        // Output difference
                        sb.AppendFormat("Source output parameter [{0}] has a value of {1}", srcKvp.Key, FormatValue(srcKvp.Value));
                        sb.AppendLine();

                        if (tgtKvp.Equals(default(KeyValuePair<string, object>)))
                            sb.AppendLine("Target output parameter does not exist");
                        else
                        {
                            sb.AppendFormat("Target output parameter [{0}] has a value of {1}", tgtKvp.Key, FormatValue(tgtKvp.Value));
                            sb.AppendLine();
                        }

                        sb.AppendLine();
                    }
                }
            }
            else
            {
                sb.AppendFormat("Source command type is: {0}", source.ExecutionDefinition.TypeDescription);
                sb.AppendLine();
                sb.AppendFormat("Target command type is: {0}", target.ExecutionDefinition.TypeDescription);
                sb.AppendLine();
            }

            if (tcr.Result != ComparisonResultTypeEnum.Passed)
                tcr.ResultDescription = sb.ToString();

            return tcr;
        }

        /// <summary>
        /// This will compare the Return value of a stored procedure
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public TestComparisonResult CompareParameterReturn(ExecutionResult source, ExecutionResult target)
        {
            TestComparisonResult tcr = new TestComparisonResult();
            StringBuilder sb = new StringBuilder();

            if (source.ExecutionDefinition.Type == CommandType.StoredProcedure &&
                target.ExecutionDefinition.Type == CommandType.StoredProcedure)
            {
                // Default to Passed
                tcr.Result = ComparisonResultTypeEnum.Passed;

                if (source.ReturnResult != source.ReturnResult)
                {
                    // Marked as failed
                    tcr.Result = ComparisonResultTypeEnum.Failed;

                    // Output difference
                    sb.AppendFormat("Source has a value of {0}", source.ReturnResult);
                    sb.AppendLine();
                    sb.AppendFormat("Target has a value of {0}", target.ReturnResult);
                    sb.AppendLine();
                }
            }
            else
            {
                sb.AppendFormat("Source command type is: {0}", source.ExecutionDefinition.TypeDescription);
                sb.AppendLine();
                sb.AppendFormat("Target command type is: {0}", target.ExecutionDefinition.TypeDescription);
                sb.AppendLine();
            }

            if (tcr.Result != ComparisonResultTypeEnum.Passed)
                tcr.ResultDescription = sb.ToString();

            return tcr;
        }

        /// <summary>
        /// This will iterate through all the result sets and compare the MetaData.  This will compare Source => Target only, not vice-versa
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IDictionary<int, TestComparisonResult> CompareResultSetMetaData(ExecutionResult source, ExecutionResult target)
        {
            IDictionary<int, TestComparisonResult> tcrDict = new Dictionary<int, TestComparisonResult>();

            foreach(KeyValuePair<int, ResultSet> srcKvp in source.ResultSets)
            {
                if (target.ResultSets.ContainsKey(srcKvp.Key))
                {
                    ResultSet tgtValue = target.ResultSets[srcKvp.Key];
                    tcrDict.Add(srcKvp.Key, CompareResultSetMetaData(srcKvp.Value, tgtValue));
                }
                else
                {
                    TestComparisonResult tcr = new TestComparisonResult() { Result = ComparisonResultTypeEnum.Failed };
                    tcr.ResultDescription = "Target does not contain a result set";
                    tcrDict.Add(srcKvp.Key, tcr);
                }
            }

            return tcrDict;
        }


        /// <summary>
        /// This will compare the Meta Data of the result set.  This will compare Source => Target only, not vice-versa
        /// The column name, ordinal position, data type, data length (if applicable) and nullablity of each field will be checked
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public TestComparisonResult CompareResultSetMetaData(ResultSet source, ResultSet target)
        {
            TestComparisonResult tcr = new TestComparisonResult();
            StringBuilder sb = new StringBuilder();

            if (source != null && target != null)
            {
                // Default to Passed
                tcr.Result = ComparisonResultTypeEnum.Passed;

                // Iterate through source meta data.  Output all differences
                foreach (ResultSetMetaData srcMD in source.MetaData)
                {
                    // Lookup Target information
                    ResultSetMetaData tgtMD = target.MetaData
                                                    .FirstOrDefault(x => x.Name.Equals(srcMD.Name));

                    // Check Data type, Length of datatype and ordinal position
                    if (tgtMD == null ||
                        srcMD.DataTypeName != tgtMD.DataTypeName ||
                        srcMD.Length > tgtMD.Length ||
                        srcMD.OrdinalPosition != tgtMD.OrdinalPosition ||
                        srcMD.IsNullable != tgtMD.IsNullable)
                    {
                        // Marked as failed
                        tcr.Result = ComparisonResultTypeEnum.Failed;

                        // Output difference
                        sb.Append("Source Attributes ::");
                        sb.AppendFormat(" Column Name: {0}", srcMD.Name);
                        sb.AppendFormat(" Ordinal Position: {0}", srcMD.OrdinalPosition);
                        sb.AppendFormat(" Data Type: {0}", srcMD.DataTypeName);
                        if (srcMD.Length > 0)
                            sb.AppendFormat(" Data Type Length: {0}", srcMD.Length);
                        sb.AppendFormat(" Nullable: {0}", srcMD.IsNullable);
                        sb.AppendLine();

                        if (tgtMD == null)
                            sb.AppendLine("\tTarget output parameter does not exist");
                        else
                        {
                            sb.Append("Target Attributes ::");
                            sb.AppendFormat(" Column Name: {0}", tgtMD.Name);
                            sb.AppendFormat(" Ordinal Position: {0}", tgtMD.OrdinalPosition);
                            sb.AppendFormat(" Data Type: {0}", tgtMD.DataTypeName);
                            if (tgtMD.Length > 0)
                                sb.AppendFormat(" Data Type Length: {0}", tgtMD.Length);
                            sb.AppendFormat(" Nullable: {0}", tgtMD.IsNullable);
                            sb.AppendLine();
                        }

                        sb.AppendLine();
                    }
                }
            }
            else
            {
                sb.AppendFormat("Source does {0}have a result set", (source == null) ? "NOT " : "");
                sb.AppendLine();
                sb.AppendFormat("Target does {0}have a result set", (target == null) ? "NOT " : "");
                sb.AppendLine();
            }

            if (tcr.Result != ComparisonResultTypeEnum.Passed)
                tcr.ResultDescription = sb.ToString();

            return tcr;
        }


        /// <summary>
        /// This will iterate through all the result sets and compare the MetaData.  This will compare Source => Target only, not vice-versa
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IDictionary<int, TestComparisonResult> CompareResultSetData(ExecutionResult source, ExecutionResult target)
        {
            IDictionary<int, TestComparisonResult> tcrDict = new Dictionary<int, TestComparisonResult>();

            foreach (KeyValuePair<int, ResultSet> srcKvp in source.ResultSets)
            {
                if (target.ResultSets.ContainsKey(srcKvp.Key))
                {
                    ResultSet tgtValue = target.ResultSets[srcKvp.Key];
                    tcrDict.Add(srcKvp.Key, CompareResultSetData(srcKvp.Value, tgtValue));
                }
                else
                {
                    TestComparisonResult tcr = new TestComparisonResult() { Result = ComparisonResultTypeEnum.Failed };
                    tcr.ResultDescription = "Target does not contain a result set";
                    tcrDict.Add(srcKvp.Key, tcr);
                }
            }

            return tcrDict;
        }


        /// <summary>
        /// This will compare the Data of the result set.  This will compare Source => Target only, not vice-versa
        /// The target cannot have more rows, but can have more columns (only at the end of the result set)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public TestComparisonResult CompareResultSetData(ResultSet source, ResultSet target)
        {
            TestComparisonResult tcr = new TestComparisonResult();
            bool failOperation = false;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            if (source != null && target != null)
            {
                // Default to Passed
                tcr.Result = ComparisonResultTypeEnum.Passed;

                int srcRowCnt = GetRowCount(source.Values);
                int tgtRowCnt = GetRowCount(target.Values);

                if (srcRowCnt != tgtRowCnt)
                {
                    // Marked as failed
                    tcr.Result = ComparisonResultTypeEnum.Failed;
                    failOperation = true;

                    // Output difference
                    sb.AppendLine("** Different row counts **");
                    sb.Append(Text.IndentChars);
                    sb.AppendFormat("Source row count: {0: #,##0}", srcRowCnt);
                    sb.AppendLine();
                    sb.Append(Text.IndentChars);
                    sb.AppendFormat("Target row count: {0: #,##0}", tgtRowCnt);
                    sb.AppendLine();
                }

                // Iterate through source data.  Output row difference and column name & value only
                for (int r = 0; r < srcRowCnt && !failOperation; r++)
                {
                    int srcColCnt = GetColumnCount(source.Values[r]);
                    int tgtColCnt = GetColumnCount(target.Values[r]);

                    if (srcColCnt > tgtColCnt)
                    {
                        // Marked as failed
                        tcr.Result = ComparisonResultTypeEnum.Failed;
                        failOperation = true;

                        // Output difference
                        sb.AppendLine("** Different column counts **");
                        sb.Append(Text.IndentChars);
                        sb.AppendFormat("Source column count: {0}", srcColCnt);
                        sb.AppendLine();
                        sb.Append(Text.IndentChars);
                        sb.AppendFormat("Target column count: {0}", tgtColCnt);
                        sb.AppendLine();
                    }

                    // Reset/re-initialize
                    sb2.Clear();

                    // Iterate through each column of data
                    for (int c = 0; c < srcColCnt && !failOperation; c++)
                    {
                        if (!IsSameValue(source.Values[r][c], target.Values[r][c]))
                        {
                            ResultSetMetaData srcMD = source.MetaData
                                                            .FirstOrDefault(x => x.OrdinalPosition == c);

                            // Marked as failed
                            tcr.Result = ComparisonResultTypeEnum.Failed;

                            // Output difference
                            sb2.AppendFormat("Column [{0}]", srcMD.Name);
                            sb2.AppendLine();
                            sb2.Append(Text.IndentChars);
                            sb2.AppendFormat("Source Value: {0}", FormatValue(source.Values[r][c]));
                            sb2.AppendLine();
                            sb2.Append(Text.IndentChars);
                            sb2.AppendFormat("Target Value: {0}", FormatValue(target.Values[r][c]));
                            sb2.AppendLine();
                        }
                    }

                    if (sb2.Length > 0)
                    {
                        sb.AppendFormat("Detected data difference at row: {0: #,##0}", r + 1);
                        sb.AppendLine();
                        sb.AppendLine(sb2.ToString());
                    }
                }
            }
            else
            {
                sb.AppendFormat("Source does {0}have a result set", (source == null) ? "NOT " : "");
                sb.AppendLine();
                sb.AppendFormat("Target does {0}have a result set", (target == null) ? "NOT " : "");
                sb.AppendLine();
            }


            if (tcr.Result != ComparisonResultTypeEnum.Passed)
                tcr.ResultDescription = sb.ToString();

            return tcr;
        }


        private int GetRowCount(object[][] values)
        {
            return (values == null) ? 0 : values.Length;
        }

        private int GetColumnCount(object[] values)
        {
            return (values == null) ? 0 : values.Length;
        }


        private bool IsSameValue(object source, object target)
        {
            // Rely on C# Short Circuiting
            bool result = false;

            // Start by check for both have null values
            result = result || (source == null && target == null);

            // Check actual value, but only if both are not null
            result = result || ((source != null && target != null) && (source.Equals(target)));

            return result;
        }

        private string FormatValue(object value) => (value == null) ? "{{NULL}}" : value.ToString();
            
    }
}
