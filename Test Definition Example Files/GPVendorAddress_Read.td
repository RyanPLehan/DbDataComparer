{
  "Name": "GPVendorAddress_Read",
  "CompareOptions": {
    "ParameterOutput": true,
    "ParameterReturn": true,
    "ResultSetMetaData": true,
    "ResultSetData": true
  },
  "NotificationOptions": {
    "NotifyOnEveryCompare": false,
    "NotifyOnFailure": false,
    "Email": ""
  },
  "Source": {
    "ConnectionString": "Data Source=DEVACCTING;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "adi.pGPVendorAddress_R",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@ErpIds_i",
        "DataType": 30,
        "DataTypeDescription": "Structured",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true,
        "UserDefinedType": {
          "Name": "ListOfVARCHAR",
          "Schema": "dbo",
          "Columns": [
            {
              "Name": "Value",
              "DataType": 22,
              "DataTypeDescription": "VarChar"
            }
          ]
        }
      },
      {
        "Name": "@AddressCode_v",
        "DataType": 22,
        "DataTypeDescription": "VarChar",
        "CharacterMaxLength": 15,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      }
    ]
  },
  "Target": {
    "ConnectionString": "Data Source=DEVACCTING;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "fin.pGPVendorAddress_R",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@ErpIds_i",
        "DataType": 30,
        "DataTypeDescription": "Structured",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true,
        "UserDefinedType": {
          "Name": "ListOfVARCHAR",
          "Schema": "dbo",
          "Columns": [
            {
              "Name": "Value",
              "DataType": 22,
              "DataTypeDescription": "VarChar"
            }
          ]
        }
      },
      {
        "Name": "@AddressCode_v",
        "DataType": 22,
        "DataTypeDescription": "VarChar",
        "CharacterMaxLength": 15,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      }
    ]
  },
  "StoredProcedureTests": [
    {
      "SourceTestValues": [
        {
          "ParameterName": "@ErpIds_i",
          "Values": [
            {
              "Value": "00002602"
            },
            {
              "Value": "00017961"
            },
            {
              "Value": "00016185"
            },
            {
              "Value": "00018006"
            }
          ]
        },
        {
          "ParameterName": "@AddressCode_v"
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@ErpIds_i",
          "Values": [
            {
              "Value": "00002602"
            },
            {
              "Value": "00017961"
            },
            {
              "Value": "00016185"
            },
            {
              "Value": "00018006"
            }
          ]
        },
        {
          "ParameterName": "@AddressCode_v"
        }
      ],
      "Name": "4 erp ids"
    },
    {
      "SourceTestValues": [
        {
          "ParameterName": "@ErpIds_i",
          "Values": [
            {
              "Value": "00016185"
            }
          ]
        },
        {
          "ParameterName": "@AddressCode_v"
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@ErpIds_i",
          "Values": [
            {
              "Value": "00002602"
            }
          ]
        },
        {
          "ParameterName": "@AddressCode_v"
        }
      ],
      "Name": "should error"
    }
  ]
}
