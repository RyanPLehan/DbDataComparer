{
  "Name": "spFCM_GetLinkedCarriers - V2",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true
  },
  "Source": {
    "ConnectionString": "Data Source = DEVACCTING; Initial Catalog = AcctWF; Integrated Security = SSPI;Application Name=DbDataComparer;TrustServerCertificate=true;",
    "Text": "dbo.spFCM_GetLinkedCarriers",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@fcmid",
        "DataType": 8,
        "DataTypeDescription": "Int",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      }
    ]
  },
  "Target": {
    "ConnectionString": "Data Source = DEVACCTING; Initial Catalog = AcctWF; Integrated Security = SSPI;Application Name=DbDataComparer;TrustServerCertificate=true;",
    "Text": "fin.spFCM_GetLinkedCarriers",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@fcmid",
        "DataType": 8,
        "DataTypeDescription": "Int",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      }
    ]
  },
  "Tests": [
    {
      "Name": "Sample Test 1",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ]
    },
    {
      "Name": "Sample Test 2",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ]
    },
    {
      "Name": "Sample Test 3",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 100
        }
      ]
    }
  ]
}
