using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;

namespace DbDataComparer.UI
{
    public partial class DataExplorerControl : UserControl
    {
        public class DataExplorerResult
        {
            public string ConnectionString { get; set; }
            public string DatabaseObjectName { get; set; }
        }


        public DataExplorerControl()
        {
            InitializeComponent();
        }

        public DataExplorerResult GetDataExplorerResult()
        {
            return new DataExplorerResult()
            {
                ConnectionString = this.dataSourceTextBox.Text,
                DatabaseObjectName = this.dbObjectComboBox.Text,
            };
        }


        private void Reset()
        {
            this.dbSprocRadioButton.Checked = false;
            this.dbTableRadioButton.Checked = false;
            this.dbViewRadioButton.Checked = false;
            this.dbObjectComboBox.Text = null;
            this.dbObjectComboBox.Items.Clear();
            this.dbObjectComboBox.SelectedIndex = -1;
        }


        private void PopulateComboBox(ComboBox comboBox, IEnumerable<string> items)
        {
            comboBox.Text = null;
            comboBox.SelectedIndex = -1;
            comboBox.Items.Clear();

            foreach (string item in items)
                comboBox.Items.Add(item);
        }


        private void dataSourceTextBox_TextChanged(object sender, EventArgs e)
        {
            Reset();
            this.dbObjectGroupBox.Enabled = !String.IsNullOrWhiteSpace(this.dataSourceTextBox.Text);
        }


        private void dbSprocRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = database.GetStoredProcedureNames(connStr).GetAwaiter().GetResult();
                PopulateComboBox(this.dbObjectComboBox, items);
                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Populating Stored Procedures", ex);
            }
        }

        private void dbTableRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = database.GetTableNames(connStr).GetAwaiter().GetResult();
                PopulateComboBox(this.dbObjectComboBox, items);
                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Populating Tables", ex);
            }
        }

        private void dbViewRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = database.GetViewNames(connStr).GetAwaiter().GetResult();
                PopulateComboBox(this.dbObjectComboBox, items);
                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Populating Views", ex);
            }
        }

        private void dataSourceBuild_Click(object sender, EventArgs e)
        {
            var dialog = new DataConnectionDialog();
            var result = DataConnectionDialog.Show(dialog, this);
            if (result == DialogResult.OK)
                this.dataSourceTextBox.Text = dialog.ConnectionString;
        }
    }
}
