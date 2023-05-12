using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Extensions;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.UI
{
    public partial class StoredProcedureTestsControl : TestsControl
    {
        private class ParameterStructure
        {
            public IEnumerable<UdtColumn> Columns { get; set; }
            public IEnumerable<IDictionary<string, object>> Values;
        }

        private const int DATA_GRID_HEADER_ROW_INDEX = -1;
        private const int DATA_GRID_PARAM_NAME_COL_INDEX = 0;
        private const int DATA_GRID_PARAM_VALUE_COL_INDEX = 1;
        private const int DATA_GRID_PARAM_NULL_COL_INDEX = 2;
        private const int DATA_GRID_PARAM_TYPE_COL_INDEX = 3;

        private IList<StoredProcedureTest> Tests;
        private StoredProcedureTest WorkingTest;

        public StoredProcedureTestsControl()
        {
            InitializeComponent();
            InitializeDataGrids();

            // Manually set Event Handlers
            this.addUpdateButton.Click += addUpdateButton_Click;
            this.deleteButton.Click += deleteButton_Click;
            this.testsComboBox.SelectedIndexChanged += testsComboBox_SelectedIndexChanged;
        }


        public override void LoadTestDefinition(TestDefinition testDefinition)
        {
            this.TestDefinition = testDefinition ??
                throw new ArgumentNullException(nameof(testDefinition));

            if (testDefinition.StoredProcedureTests != null)
                this.Tests = testDefinition.StoredProcedureTests.ToList();
            else
                this.Tests = new List<StoredProcedureTest>();

            SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
            LoadDataGridMetaData();
            LoadTests();
        }

        public override TestDefinition SaveTestDefinition()
        {
            this.TestDefinition.StoredProcedureTests = this.Tests.ToArray();
            return this.TestDefinition;
        }


        #region Data Grid Initialization Routines
        private void InitializeDataGrids()
        {
            InitializeDataGrid(GetSourceDataGrid());
            InitializeDataGrid(GetTargetDataGrid());
        }

        private void InitializeDataGrid(DataGridView dataGridView)
        {
            CreateDataGridColumns(dataGridView);
            dataGridView.AllowUserToAddRows = false;
            dataGridView.CellClick += dataGrid_CellClick;
            dataGridView.CellContentClick += dataGrid_CellContentClick;
        }

        private DataGridView GetSourceDataGrid()
        {
            Control control;
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["sourceDataGrid"];
            return (DataGridView)control;
        }

        private DataGridView GetTargetDataGrid()
        {
            Control control;
            control = this.testTabControl.TabPages["targetTabPage"].Controls["targetDataGrid"];
            return (DataGridView)control;
        }


        private void CreateDataGridColumns(DataGridView dataGridView)
        {
            DataGridViewColumn genericColumn;
            DataGridViewTextBoxColumn textBoxColumn;
            DataGridViewCheckBoxColumn checkBoxColumn;

            // Textbox - readonly
            textBoxColumn = new DataGridViewTextBoxColumn()
            {
                Name = "ParameterName",
                HeaderText = "Parameter Name",
                Width = 135,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.LightGray,
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                },
            };
            textBoxColumn.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font(Font, FontStyle.Bold | FontStyle.Underline),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView.Columns.Add(textBoxColumn);


            // Allow for dynamic cell types (textbox, combobox, button, etc) at each row
            genericColumn = new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "ParameterValue",
                HeaderText = "Parameter Value",
                Width = 325,
            };
            genericColumn.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font(Font, FontStyle.Bold | FontStyle.Underline),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView.Columns.Add(genericColumn);


            // Checkbox column
            checkBoxColumn = new DataGridViewCheckBoxColumn()
            {
                Name = "ParameterNull",
                HeaderText = "Null",
                Width = 50,
            };
            checkBoxColumn.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font(Font, FontStyle.Bold | FontStyle.Underline),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView.Columns.Add(checkBoxColumn);


            // Textbox readonly
            textBoxColumn = new DataGridViewTextBoxColumn()
            {
                Name = "ParameterDataType",
                HeaderText = "Data Type",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.LightGray,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
            };
            textBoxColumn.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font(Font, FontStyle.Bold | FontStyle.Underline),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView.Columns.Add(textBoxColumn);


            dataGridView.ColumnHeadersVisible = true;
        }


        private void LoadDataGridMetaData()
        {
            LoadDataGridMetaData(GetSourceDataGrid(), this.TestDefinition.Source);
            LoadDataGridMetaData(GetTargetDataGrid(), this.TestDefinition.Target);
        }


        private void LoadDataGridMetaData(DataGridView dataGridView, ExecutionDefinition definition)
        {
            DeleteDataGridRows(dataGridView);

            // Iterate through all parameters to build grid rows.  Need to match to test to get value
            DataGridViewRow row;
            foreach (Parameter param in definition.Parameters)
            {
                // Do not create row based upon parameter direction
                if (param.Direction == ParameterDirection.ReturnValue ||
                    param.Direction == ParameterDirection.Output)
                    continue;

                // Set Meta Data
                int rowIndex = dataGridView.Rows.Add();
                row = dataGridView.Rows[rowIndex];
                SetDataGridRowMetaData(row, param);

                // Set Cell Type
                dataGridView[DATA_GRID_PARAM_VALUE_COL_INDEX, rowIndex] = TypeToDataGridControlConverter.ToCell(param.DataType);

                // If database type is structure, then use Button Cell's tag property to hold structure definition and test values
                if (param.DataType == SqlDbType.Structured)
                {
                    dataGridView[DATA_GRID_PARAM_VALUE_COL_INDEX, rowIndex].Tag = new ParameterStructure()
                    {
                        Columns = param.UserDefinedType.Columns,
                        Values = null,
                    };
                }
            }
        }

        private void DeleteDataGridRows(DataGridView dataGridView)
        {
            for (int i = dataGridView.Rows.Count - 1; i > DATA_GRID_HEADER_ROW_INDEX; i--)
                dataGridView.Rows.RemoveAt(i);
        }

        private void SetDataGridRowMetaData(DataGridViewRow row, Parameter parameter)
        {
            DataGridViewCell cell;

            cell = row.Cells[DATA_GRID_PARAM_NAME_COL_INDEX];           // ParameterName
            cell.Value = parameter.Name;

            cell = row.Cells[DATA_GRID_PARAM_VALUE_COL_INDEX];          // ParameterValue

            cell = row.Cells[DATA_GRID_PARAM_NULL_COL_INDEX];           // ParameterNull
            if (!parameter.IsNullable)
            {
                cell.ReadOnly = true;
                cell.Style = new DataGridViewCellStyle() { BackColor = Color.LightGray };
            }

            cell = row.Cells[DATA_GRID_PARAM_TYPE_COL_INDEX];           // ParameterDataType
            cell.Value = parameter.DataTypeDescription;
            cell.Tag = parameter.DataType;                              // Use Tag to store Enum value
        }
        #endregion

        #region Grid Load/Save Routines
        private void LoadDataGridValues(DataGridView dataGridView, IEnumerable<ParameterTestValue> testValues)
        {
            DataGridViewRow row;
            ClearDataGridValues(dataGridView);

            // Iterate through each test value, lookup the appropriate ROW based upon the parameter name, set the value
            foreach (ParameterTestValue testValue in testValues)
            {
                row = FindDataGridRow(dataGridView, testValue.ParameterName);
                if (row != null)
                    SetDataGridRowValue(row, testValue);
            }
        }

        private void ClearDataGridValues(DataGridView dataGridView)
        {
            DataGridViewCell cell;

            // Iterate through all the rows and reset the values
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX)
                    continue;

                // Determine which field (Value or Values) to populate based upon data type stored in the tag field
                cell = row.Cells[DATA_GRID_PARAM_TYPE_COL_INDEX];
                SqlDbType sqlDbType = (SqlDbType)cell.Tag;

                cell = row.Cells[DATA_GRID_PARAM_VALUE_COL_INDEX];
                if (sqlDbType == SqlDbType.Structured)
                    ((ParameterStructure)cell.Tag).Values = null;
                else
                    cell.Value = null;

                // Null Check box
                cell = row.Cells[DATA_GRID_PARAM_NULL_COL_INDEX];
                cell.Value = true;
            }
        }

        private DataGridViewRow FindDataGridRow(DataGridView dataGridView, string paramName)
        {
            DataGridViewRow retRow = null;
            DataGridViewCell cell;

            // Iterate through all the rows and reset the values
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX)
                    continue;

                // Value Check box
                cell = row.Cells[DATA_GRID_PARAM_NAME_COL_INDEX];
                if (cell.Value != null &&
                    cell.Value.ToString().Equals(paramName, StringComparison.OrdinalIgnoreCase))
                {
                    retRow = row;
                    break;
                }
            }

            return retRow;
        }

        private void SetDataGridRowValue(DataGridViewRow row, ParameterTestValue testValue)
        {
            DataGridViewCell cell;
            bool isNull = false;

            // Determine which field (Value or Values) to populate based upon data type stored in the tag field
            cell = row.Cells[DATA_GRID_PARAM_TYPE_COL_INDEX];
            SqlDbType sqlDbType = (SqlDbType)cell.Tag;


            cell = row.Cells[DATA_GRID_PARAM_VALUE_COL_INDEX];
            if (sqlDbType == SqlDbType.Structured)
            {
                ((ParameterStructure)cell.Tag).Values = testValue.Values;
                isNull = testValue.Values == null;
            }
            else
            {
                cell.Value = testValue.Value;
                isNull = testValue.Value == null;
            }

            cell = row.Cells[DATA_GRID_PARAM_NULL_COL_INDEX];
            cell.Value = isNull;
        }


        private IEnumerable<ParameterTestValue> SaveDataGridValues(DataGridView dataGridView)
        {
            List<ParameterTestValue> testValues = new List<ParameterTestValue>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX)
                    continue;

                var testValue = GetDataGridRowValue(row);
                testValues.AddIfNotNull(testValue);
            }

            return testValues;
        }

        private ParameterTestValue GetDataGridRowValue(DataGridViewRow row)
        {
            DataGridViewCell cell;
            ParameterTestValue testValue = new ParameterTestValue()
            {
                Value = null,
                Values = null,
            };

            cell = row.Cells[DATA_GRID_PARAM_NAME_COL_INDEX];
            testValue.ParameterName = cell.Value.ToString();

            // 
            cell = row.Cells[DATA_GRID_PARAM_NULL_COL_INDEX];
            if (Convert.ToBoolean(cell.Value))
                return testValue;


            // Determine which field (Value or Values) to populate based upon data type stored in the tag field
            cell = row.Cells[DATA_GRID_PARAM_TYPE_COL_INDEX];
            SqlDbType sqlDbType = (SqlDbType)cell.Tag;


            cell = row.Cells[DATA_GRID_PARAM_VALUE_COL_INDEX];
            if (sqlDbType == SqlDbType.Structured)
            {
                ParameterStructure parameterStructure = (ParameterStructure)cell.Tag;
                testValue.Values = parameterStructure.Values;
            }
            else
            {
                Type type = DatabaseTypeConverter.ToNetType(sqlDbType);
                testValue.Value = Convert.ChangeType(cell.Value, type);
            }

            return testValue;
        }

        private void ValidateDataGridValues(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX)
                    continue;

                ValidateDataGridRowValue(row);
            }
        }

        private void ValidateDataGridRowValue(DataGridViewRow row)
        {
            DataGridViewCell cell;
            object value = null;
            string paramName;

            // If Null has been checked, the it is ok
            cell = row.Cells[DATA_GRID_PARAM_NULL_COL_INDEX];
            if (Convert.ToBoolean(cell.Value))
                return;

            cell = row.Cells[DATA_GRID_PARAM_NAME_COL_INDEX];
            paramName = cell.Value.ToString();


            // Determine which field (Value or Values) to valiidate based upon data type stored in the tag field
            cell = row.Cells[DATA_GRID_PARAM_TYPE_COL_INDEX];
            SqlDbType sqlDbType = (SqlDbType)cell.Tag;

            // Get data
            cell = row.Cells[DATA_GRID_PARAM_VALUE_COL_INDEX];
            if (sqlDbType == SqlDbType.Structured)
                value = ((ParameterStructure)cell.Tag).Values;
            else
                value = cell.Value;


            // Check if user entered value
            if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
                throw new Exception(String.Format("Missing value for parameter: {0}", paramName));

            // Attempt to conversion
            if (sqlDbType != SqlDbType.Structured)
            {
                try
                {
                    Type type = DatabaseTypeConverter.ToNetType(sqlDbType);
                    value = Convert.ChangeType(cell.Value, type);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to convert value to '{0}' for parameter: {1}", sqlDbType.ToString(), paramName));
                }
            }
        }
        #endregion


        #region Load Routines
        private void LoadTests()
        {
            this.addUpdateButton.Enabled = false;
            this.deleteButton.Enabled = false;

            LoadComboBox<StoredProcedureTest>(this.testsComboBox, this.Tests);
            this.testsComboBox.SelectedIndex = (this.testsComboBox.Items.Count > 1 ? NEW_SELECTED_INDEX + 1 : NEW_SELECTED_INDEX);
        }


        private void CreateWorkingTest()
        {
            this.WorkingTest = new StoredProcedureTest()
            {
                SourceTestValues = Enumerable.Empty<ParameterTestValue>(),
                TargetTestValues = Enumerable.Empty<ParameterTestValue>(),
            };
        }


        private void LoadTest()
        {
            Control control;

            this.testNameTextBox.Text = this.WorkingTest.Name;

            // Set focus to first tab page
            this.testTabControl.SelectedIndex = SourceTabPageIndex;

            LoadDataGridValues(GetSourceDataGrid(), this.WorkingTest.SourceTestValues);
            LoadDataGridValues(GetTargetDataGrid(), this.WorkingTest.TargetTestValues);
        }
        #endregion

        #region Save Routines
        private void SaveTest()
        {
            Control control;

            this.WorkingTest.Name = this.testNameTextBox.Text?.Trim();
            this.WorkingTest.SourceTestValues = SaveDataGridValues(GetSourceDataGrid());
            this.WorkingTest.TargetTestValues = SaveDataGridValues(GetTargetDataGrid());
        }


        private void ValidateTest()
        {
            Control control;

            if (String.IsNullOrWhiteSpace(this.testNameTextBox.Text))
                throw new Exception("Missing Test Name");

            ValidateDataGridValues(GetSourceDataGrid());
            ValidateDataGridValues(GetTargetDataGrid());
        }
        #endregion


        #region Control Event Handlers
        private void testsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addUpdateButton.Enabled = true;
            this.deleteButton.Enabled = false;

            ComboBox comboBox = (ComboBox)sender ??
                throw new ArgumentNullException(nameof(sender));

            int selectedIndex = comboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                    this.addUpdateButton.Enabled = false;
                    SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
                    break;

                case NEW_SELECTED_INDEX:
                    SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
                    CreateWorkingTest();
                    LoadTest();
                    break;

                default:
                    this.deleteButton.Enabled = true;
                    SetAddUpdateButton(AddUpdateActionEnum.Update, this.addUpdateButton);
                    object item = comboBox.Items[selectedIndex];
                    this.WorkingTest = (StoredProcedureTest)item;
                    LoadTest();
                    break;
            }
        }

        private void addUpdateButton_Click(object sender, EventArgs e)
        {
            AddUpdateActionEnum addUpdateAction = (AddUpdateActionEnum)addUpdateButton.Tag;

            try
            {
                ValidateTest();
                SaveTest();

                // Add to Tests list if not already in list
                if (addUpdateAction == AddUpdateActionEnum.Add &&
                    !this.Tests.Any(x => x == this.WorkingTest))
                    this.Tests.Add(this.WorkingTest);

                LoadComboBox<StoredProcedureTest>(this.testsComboBox, this.Tests);
                FindInComboBox(testsComboBox, this.WorkingTest.Name);
            }
            catch (Exception ex)
            {
                string title = String.Format("{0} Test", addUpdateAction.ToString());
                RTLAwareMessageBox.ShowError(title, ex);
            }
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.testsComboBox.SelectedIndex;

            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                case NEW_SELECTED_INDEX:
                    RTLAwareMessageBox.ShowMessage("Delete Test", "Please select a test from the drop down box");
                    break;

                default:
                    this.Tests = this.Tests
                                     .Where(x => x != this.WorkingTest)
                                     .ToList();
                    LoadTests();
                    break;
            }
        }


        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender ??
                throw new ArgumentNullException(nameof(sender));

            // Make sure user did not click on header row
            if (e.RowIndex == DATA_GRID_HEADER_ROW_INDEX)
                return;

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell;

            DataGridView dataGridView = (DataGridView)sender ??
                throw new ArgumentNullException(nameof(sender));

            // Make sure user did not click on header row
            if (e.RowIndex == DATA_GRID_HEADER_ROW_INDEX)
                return;

            // Make sure user checked only in the Parameter Value cell
            if (e.ColumnIndex != DATA_GRID_PARAM_VALUE_COL_INDEX)
                return;


            cell = dataGridView[DATA_GRID_PARAM_TYPE_COL_INDEX, e.RowIndex];
            SqlDbType sqlDbType = (SqlDbType)cell.Tag;

            // Check to see if user clicked on button by checking type
            if (sqlDbType == SqlDbType.Structured)
            {
                // MessageBox.Show("Not functional at the moment", "Edit Items");
                cell = dataGridView[DATA_GRID_PARAM_VALUE_COL_INDEX, e.RowIndex];
                ParameterStructure paramStruc = (ParameterStructure)cell.Tag;

                if (paramStruc != null)
                {
                    var dialog = new ParamValueEditorDialog(paramStruc.Columns);
                    dialog.TestValues = paramStruc.Values;

                    var result = ParamValueEditorDialog.Show(dialog, this);
                    if (result == DialogResult.OK)
                        paramStruc.Values = dialog.TestValues;
                }
            }

            // Check if user clicked on Null Value checkbox
            /*
            if (e.ColumnIndex == DATA_GRID_PARAM_NULL_COL_INDEX)
            {
                Color color = TextBox.DefaultBackColor;

                // *** 
                // * KEEP IN MIND this event is fired just before the actual value of the check box is changed
                // * Need to take the opposite value
                // * ***
                bool value = !Convert.ToBoolean(dataGridView[DATA_GRID_PARAM_NULL_COL_INDEX, e.RowIndex].Value);
                if (value)
                    color = Color.LightGray;

                // Alter parameter Value cell based upon click event
                DataGridViewCell cellParamValue = dataGridView[DATA_GRID_PARAM_VALUE_COL_INDEX, e.RowIndex];
                cellParamValue.ReadOnly = value;
                cellParamValue.Style = new DataGridViewCellStyle { BackColor = color };
            }
            */
        }

        #endregion
    }
}
