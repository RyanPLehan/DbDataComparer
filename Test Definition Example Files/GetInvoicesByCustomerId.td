{
  "Name": "GetInvoicesByCustomerId",
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
    "ConnectionString": "Data Source=devaccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "dbo.spQPROC_GetInvoicesByCustomer",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@LMstartGPCustomerID",
        "DataType": 8,
        "DataTypeDescription": "Int",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      },
      {
        "Name": "@EDI",
        "DataType": 2,
        "DataTypeDescription": "Bit",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      }
    ]
  },
  "Target": {
    "ConnectionString": "Data Source=devaccting;Initial Catalog=AcctWF;Integrated Security=True;Pooling=True;Trust Server Certificate=True;Application Name=DbDataComparer",
    "Text": "fin.spQPROC_GetInvoicesByCustomer",
    "Type": 4,
    "TypeDescription": "StoredProcedure",
    "ExecutionTimeoutInSeconds": 30,
    "Parameters": [
      {
        "Name": "@LMstartGPCustomerID",
        "DataType": 8,
        "DataTypeDescription": "Int",
        "CharacterMaxLength": 0,
        "Direction": 1,
        "DirectionDescription": "Input",
        "IsNullable": true
      },
      {
        "Name": "@EDI",
        "DataType": 2,
        "DataTypeDescription": "Bit",
        "CharacterMaxLength": 0,
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
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "Name": "Sample Stored Procedure Test 1"
    },
    {
      "SourceTestValues": [
        {
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "Name": "Sample Stored Procedure Test 2"
    },
    {
      "SourceTestValues": [
        {
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "TargetTestValues": [
        {
          "ParameterName": "@LMstartGPCustomerID",
          "Value": 100
        },
        {
          "ParameterName": "@EDI",
          "Value": 1
        }
      ],
      "Name": "Sample Stored Procedure Test 3"
    }
  ]
}
