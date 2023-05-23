using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class DataExplorerControl : UserControl
    {
        public event EventHandler<DataExplorerDataSourceChangedEventArgs> DataExplorerDataSourceChanged;


        public class DataExplorerResult
        {
            public string ConnectionString { get; set; }
            public string DatabaseObjectName { get; set; }
        }


        public DataExplorerControl()
        {
            InitializeComponent();
        }

        #region Event Raising Methods
        private void OnDataExplorerDataSourceChanged(DataExplorerDataSourceChangedEventArgs e)
        {
            EventHandler<DataExplorerDataSourceChangedEventArgs> handler = DataExplorerDataSourceChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion


        public DataExplorerResult GetDataExplorerResult()
        {
            return new DataExplorerResult()
            {
                ConnectionString = this.dataSourceTextBox.Text,
                DatabaseObjectName = this.dbObjectComboBox.Text,
            };
        }


        public void Reset()
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
            comboBox.AutoCompleteMode = AutoCompleteMode.None;
            comboBox.AutoCompleteSource = AutoCompleteSource.None;

            comboBox.ResetText();
            comboBox.SelectedIndex = -1;
            comboBox.Items.Clear();

            if (items.Any())
            {
                foreach (string item in items)
                    comboBox.Items.Add(item);

                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }


        private void dataSourceTextBox_TextChanged(object sender, EventArgs e)
        {
            Reset();
            this.dbObjectGroupBox.Enabled = !String.IsNullOrWhiteSpace(this.dataSourceTextBox.Text);

            var eventArgs = new DataExplorerDataSourceChangedEventArgs() { DataSource = this.dataSourceTextBox.Text };
            OnDataExplorerDataSourceChanged(eventArgs);
        }


        private async void dbSprocRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dbSprocRadioButton.Checked)
                return;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = await database.GetStoredProcedureNames(connStr);
                PopulateComboBox(this.dbObjectComboBox, items);
                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Populating Stored Procedures", ex);
            }
        }

        private async void dbTableRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dbTableRadioButton.Checked)
                return;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = await database.GetTableNames(connStr);
                PopulateComboBox(this.dbObjectComboBox, items);
                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Populating Tables", ex);
            }
        }

        private async void dbViewRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dbViewRadioButton.Checked)
                return;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string connStr = this.dataSourceTextBox.Text;
                IDatabase database = new SqlDatabase();
                IEnumerable<string> items = await database.GetViewNames(connStr);
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

            if (!String.IsNullOrWhiteSpace(this.dataSourceTextBox.Text))
                dialog.ConnectionString = this.dataSourceTextBox.Text;

            var result = DataConnectionDialog.Show(dialog, this);
            if (result == DialogResult.OK)
                this.dataSourceTextBox.Text = dialog.ConnectionString;
        }
    }
}
