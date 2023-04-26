{
  "Name": "vw_LoadTypes",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true
  },
  "Source": {
    "ConnectionString": "Data Source=devAccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "lm.vw_LoadTypes",
    "Type": 1,
    "TypeDescription": "Text",
    "ExecutionTimeoutInSeconds": 30
  },
  "Target": {
    "ConnectionString": "Data Source=devAccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "lm.vw_LoadTypes",
    "Type": 1,
    "TypeDescription": "Text",
    "ExecutionTimeoutInSeconds": 30
  },
  "TableViewTests": [
    {
      "Name": "Sample Table/View Test 1",
      "SourceSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter = 1",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter = 1"
    },
    {
      "Name": "Sample Table/View Test 2",
      "SourceSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter > 5",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter > 5"
    },
    {
      "Name": "Sample Table/View Test 3",
      "SourceSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter IN (1,3,5,7,9)",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter IN (1,3,5,7,9)"
    }
  ]
}
