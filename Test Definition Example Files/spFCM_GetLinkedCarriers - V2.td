{
  "Name": "spFCM_GetLinkedCarriers",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true
  },
  "NotificationOptions": {
    "NotifyOnFailure": false,
    "Email": ""
  },
  "Source": {
    "ConnectionString": "Data Source=devAccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
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
    "ConnectionString": "Data Source=devAccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
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
  "StoredProcedureTests": [
    {
      "Name": "Sample Stored Procedure Test 1 (1004)",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1004
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1004
        }
      ]
    },
    {
      "Name": "Sample Stored Procedure Test 2 (1007)",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1007
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1007
        }
      ]
    },
    {
      "Name": "Sample Stored Procedure Test 3 (1008)",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1008
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1008
        }
      ]
    },
    {
      "Name": "Sample Stored Procedure Test 4 (1006 vs 1007)",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1007
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1006
        }
      ]
    },
    {
      "Name": "Sample Stored Procedure Test 5 (1007 vs 1008)",
      "SourceTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1008
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@fcmid",
          "Value": 1007
        }
      ]
    }
  ]
}
