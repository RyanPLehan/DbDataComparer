{
  "Name": "spFCM_GetLinkedCarriers",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true,
    "EnforceResultSetData": false
  },
  "Source": {
    "Text": "dbo.spFCM_GetLinkedCarriers",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "TimeoutInSeconds": 30,
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
    "Text": "fin.spFCM_GetLinkedCarriers",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "TimeoutInSeconds": 30,
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
      "Name": "Test 1 - Factoring Company 1004",
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
      "Name": "Test 1 - Factoring Company 1007",
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
      "Name": "Test 1 - Factoring Company 1008",
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
    }
  ]
}
