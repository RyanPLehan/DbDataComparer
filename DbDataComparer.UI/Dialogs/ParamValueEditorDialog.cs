using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Extensions;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using static Azure.Core.HttpHeader;

namespace DbDataComparer.UI
{
    public partial class ParamValueEditorDialog : Form
    {
        private const int DATA_GRID_HEADER_ROW_INDEX = -1;

        private string Dialog_CannotModifyState = "The editor dialog state cannot be programmatically modified when the dialog is visible.";
        private string Dialog_ShowDialogNotSupported = "You cannot use the Form.ShowDialog() method to show the editor dialog.  Use ParamValueEditorDialog.Show() instead.";

        private bool ShowingDialog = false;
        private IEnumerable<IDictionary<string, object>> WorkingTestValues;

        public ParamValueEditorDialog()
        {
            InitializeComponent();

            UdtColumn[] columns = new UdtColumn[]
            {
                new UdtColumn() { Name = "Text Example", DataType = SqlDbType.VarChar },
                new UdtColumn() { Name = "Int Example", DataType = SqlDbType.Int },
            };
            InitializeDataGrid(this.testValuesDataGrid, columns);
        }

        public ParamValueEditorDialog(IEnumerable<UdtColumn> columns)
        {
            InitializeComponent();
            InitializeDataGrid(this.testValuesDataGrid, columns);
        }


        public static DialogResult Show(ParamValueEditorDialog dialog)
        {
            return Show(dialog, null);
        }

        public static DialogResult Show(ParamValueEditorDialog dialog, IWin32Window owner)
        {
            if (dialog == null)
                throw new ArgumentNullException(nameof(dialog));

            DialogResult result = DialogResult.Ignore;
            dialog.ShowingDialog = true;

            try
            {
                if (owner == null)
                    dialog.StartPosition = FormStartPosition.CenterScreen;

                result = dialog.ShowDialog(owner);
            }

            finally
            {
                dialog.ShowingDialog = false;
            }

            return result;
        }

        public IEnumerable<IDictionary<string, object>> TestValues
        {
            get { return (this.WorkingTestValues.Any() ? this.WorkingTestValues : null); }
            set
            {
                if (ShowingDialog)
                    throw new InvalidOperationException(Dialog_CannotModifyState);

                if (value != null)
                {
                    this.WorkingTestValues = value;
                    LoadDataGridValues(this.testValuesDataGrid, value);
                }
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            if (!ShowingDialog)
                throw new NotSupportedException(Dialog_ShowDialogNotSupported);

            base.OnLoad(e);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            ValidateDataGridValues(this.testValuesDataGrid);
            this.WorkingTestValues = SaveDataGridValues(this.testValuesDataGrid);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void testValuesDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount == DATA_GRID_HEADER_ROW_INDEX)
                return;

            DataGridView dgv = (DataGridView)sender;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            if (!row.IsNewRow)
                row.HeaderCell.Value = String.Format("{0}",e.RowIndex + 1);
        }

        #region Data Grid Initialization Routines
        private void InitializeDataGrid(DataGridView dataGridView, IEnumerable<UdtColumn> columns)
        {
            int columnWidth = dataGridView.Size.Width / columns.Count() - 50;   // Sub Row Indicator column

            foreach (UdtColumn column in columns)
            {
                CreateDataGridColumns(dataGridView, column, columnWidth);
            }

            dataGridView.ColumnHeadersVisible = true;
            dataGridView.AllowUserToAddRows = true;

            dataGridView.RowsAdded += testValuesDataGrid_RowsAdded;
        }

        private void CreateDataGridColumns(DataGridView dataGridView, UdtColumn column, int width)
        {
            DataGridViewColumn dgvColumn = TypeToDataGridControlConverter.ToColumn(column.DataType);
            dgvColumn.Name = column.Name;
            dgvColumn.HeaderText = String.Format("{0}{1}({2})", column.Name, Environment.NewLine, column.DataTypeDescription);
            dgvColumn.Tag = column.DataType;
            dgvColumn.ValueType = DatabaseTypeConverter.ToNetType(column.DataType);
            dgvColumn.Width = width;

            dgvColumn.HeaderCell.Style = new DataGridViewCellStyle()
            {
                // Font = new Font(Font, FontStyle.Bold | FontStyle.Underline),
                Font = new Font(Font, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dataGridView.Columns.Add(dgvColumn);
        }
        #endregion

        #region Grid Load/Save Routines
        private void LoadDataGridValues(DataGridView dataGridView, IEnumerable<IDictionary<string, object>> testValues)
        {
            DataGridViewRow row;

            dataGridView.Rows.Clear();

            // Iterate through each test value, lookup the appropriate ROW based upon the parameter name, set the value
            foreach (IDictionary<string, object> rowValues in testValues)
            {
                int rowIndex = dataGridView.Rows.Add();
                row = dataGridView.Rows[rowIndex];
                if (row != null)
                    SetDataGridRowValues(row, rowValues);
            }
        }


        private void SetDataGridRowValues(DataGridViewRow row, IDictionary<string, object> rowValues)
        {
            DataGridViewCell cell;

            foreach (KeyValuePair<string, object> kvp in rowValues)
            {
                cell = FindDataGridCell(row, kvp.Key);
                if (cell != null)
                    cell.Value = kvp.Value;
            }
        }

        private DataGridViewCell FindDataGridCell(DataGridViewRow row, string columnName)
        {
            DataGridViewCell retCell = null;

            // Iterate through all the rows and reset the values
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    retCell = cell;
                    break;
                }
            }

            return retCell;
        }

        private IEnumerable<IDictionary<string, object>> SaveDataGridValues(DataGridView dataGridView)
        {
            List<IDictionary<string, object>> testValues = new List<IDictionary<string, object>>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX ||
                    row.IsNewRow)
                    continue;

                var rowValues = GetDataGridRowValues(row);
                testValues.AddIfNotNull(rowValues);
            }

            return testValues;
        }

        private IDictionary<string, object> GetDataGridRowValues(DataGridViewRow row)
        {
            IDictionary<string, object> rowValues = new Dictionary<string, object>();

            foreach (DataGridViewCell cell in row.Cells)
            {
                SqlDbType sqlDbType = (SqlDbType)cell.OwningColumn.Tag;
                Type type = DatabaseTypeConverter.ToNetType(sqlDbType);
                rowValues.Add(cell.OwningColumn.Name, Convert.ChangeType(cell.Value, type));
            }

            return rowValues;
        }

        private void ValidateDataGridValues(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == DATA_GRID_HEADER_ROW_INDEX || 
                    row.IsNewRow)
                    continue;

                ValidateDataGridRowValues(row);
            }
        }

        private void ValidateDataGridRowValues(DataGridViewRow row)
        {
            object value = null;
            string columnName = null;

            foreach (DataGridViewCell cell in row.Cells)
            {
                columnName = cell.OwningColumn.Name;
                SqlDbType sqlDbType = (SqlDbType)cell.OwningColumn.Tag;
                Type type = DatabaseTypeConverter.ToNetType(sqlDbType);
                value = cell.Value;

                // Check if user entered value
                if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
                    throw new Exception(String.Format("Missing value in row {0} column {1}", row.Index, columnName));

                // Attempt to conversion
                try
                {
                    Convert.ChangeType(value, type);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to convert value to '{0}' for value in row {1} column {2}", sqlDbType.ToString(), row.Index, columnName));
                }
            }
        }
        #endregion


    }
}
