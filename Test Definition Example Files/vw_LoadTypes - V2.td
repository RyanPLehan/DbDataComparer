{
  "Name": "vw_LoadTypes",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true
  },
  "NotificationOptions": {
    "AlwaysSendOverallResults": false,
    "NotifyOnFailure": false,
    "Email": ""
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
      "Name": "Sample Table/View Test 1 - Pass",
      "SourceSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter < 5",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter < 5"
    },
    {
      "Name": "Sample Table/View Test 2 - Fail MetaData",
      "SourceSql": "SELECT LoadType FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter > 5",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter > 5"
    },
    {
      "Name": "Sample Table/View Test 3 - Fail Data Set",
      "SourceSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter IN (1,3,5,7,9)",
      "TargetSql": "SELECT * FROM lm.vw_LoadTypes WHERE LoadTypesRecordCounter IN (2,4,6,8)"
    }
  ]
}
