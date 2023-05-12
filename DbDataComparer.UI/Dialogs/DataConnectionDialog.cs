using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using static Azure.Core.HttpHeader;

namespace DbDataComparer.UI
{
    public partial class DataConnectionDialog : Form
    {
        private string Dialog_TestResults = "Test results";
        private string Dialog_TestConnectionSucceeded = "Test connection succeeded.";
        private string Dialog_CannotModifyState = "The data connection dialog state cannot be programmatically modified when the dialog is visible.";
        private string Dialog_ShowDialogNotSupported = "You cannot use the Form.ShowDialog() method to show the data connection dialog.  Use DataConnectionDialog.Show() instead.";

        private bool ShowingDialog = false;
        private bool IsSuccessfulTest = false;
        private IConnectionProperties ConnectionProperties;


        public DataConnectionDialog()
        {
            InitializeComponent();
        }

        public static DialogResult Show(DataConnectionDialog dialog)
        {
            return Show(dialog, null);
        }

        public static DialogResult Show(DataConnectionDialog dialog, IWin32Window owner)
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


        public string ConnectionString
        {
            get { return ConnectionProperties?.ConnectionString ?? String.Empty; }
            set
            {
                if (ShowingDialog)
                    throw new InvalidOperationException(Dialog_CannotModifyState);

                Debug.Assert(!String.IsNullOrWhiteSpace(value));
                if (!String.IsNullOrWhiteSpace(value))
                {
                    this.ConnectionProperties = ConnectionPropertiesBuilder.Parse(value);
                    LoadConnectionBuilderOptions(this.ConnectionProperties.ConnectionBuilderOptions);
                }
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            if (!ShowingDialog)
                throw new NotSupportedException(Dialog_ShowDialogNotSupported);

            ConfigureGlobalControlChange(this.Controls);
            ControlChanged(this, EventArgs.Empty);
            base.OnLoad(e);
        }

        private void ConfigureGlobalControlChange(Form.ControlCollection controls)
        {
            foreach (Control c in controls)
                ConfigureControlChange(c);
        }

        private void ConfigureGlobalControlChange(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
                ConfigureControlChange(c);
        }

        private void ConfigureControlChange(Control c)
        {
            // check for container
            if (c.HasChildren)
            {
                ConfigureGlobalControlChange(c.Controls);
                return;
            }

            if (c is TextBox)
            {
                c.TextChanged += ControlChanged;
            }

            if (c is ComboBox)
            {
                ComboBox cb = (ComboBox)c;
                cb.TextChanged += ControlChanged;
                cb.SelectedIndexChanged += ControlChanged;
                cb.SelectedValueChanged += ControlChanged;
            }

            if (c is RadioButton)
            {
                ((RadioButton)c).CheckedChanged += ControlChanged;
            }
        }

        private void ControlChanged(object sender, EventArgs e)
        {
            this.IsSuccessfulTest = false;
            ConfigureAcceptButton();
            this.loginTableLayoutPanel.Enabled = sqlAuthRadioButton.Checked;
        }

        private void ConfigureAcceptButton()
        {
            acceptButton.Enabled = this.IsSuccessfulTest;
        }

        private ConnectionBuilderOptions CreateConnectionBuilderOptions()
        {
            return new ConnectionBuilderOptions()
            {
                Server = this.serverComboBox.Text,
                UseWindowsAuthentication = this.windowsAuthRadioButton.Checked,
                UserId = this.userNameTextBox.Text,
                Password = this.passwordTextBox.Text,
                Database = this.databaseComboBox.Text,
            };
        }


        private void LoadConnectionBuilderOptions(ConnectionBuilderOptions options)
        {
            if (options == null)
                return;

            this.serverComboBox.Items.Clear();
            this.serverComboBox.ResetText();
            this.serverComboBox.Text = options.Server;

            this.windowsAuthRadioButton.Checked = options.UseWindowsAuthentication;
            this.sqlAuthRadioButton.Checked = !options.UseWindowsAuthentication;
            this.userNameTextBox.Text = options.UserId;
            this.passwordTextBox.Text = options.Password;

            this.databaseComboBox.Items.Clear();
            this.databaseComboBox.ResetText();
            this.databaseComboBox.Text = options.Database;
        }


        private void serverRefreshButton_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var servers = ConnectionPropertiesBuilder.EnumerateServers();

                if (servers.Any())
                {
                    this.serverComboBox.ResetText();
                    this.serverComboBox.Items.Clear();

                    foreach (ConnectionDataSource server in servers)
                        this.serverComboBox.Items.Add(server.ServerName);

                    this.serverComboBox.SelectedIndex = 0;
                }

                Cursor.Current = currentCursor;
            }
            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError(Dialog_TestResults, ex);
            }
        }


        private void databaseRefreshButton_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var options = this.CreateConnectionBuilderOptions();
                var databases = ConnectionPropertiesBuilder.EnumerateDatabases(options);

                if (databases.Any())
                {
                    this.databaseComboBox.ResetText();
                    this.databaseComboBox.Items.Clear();

                    foreach (string db in databases)
                        this.databaseComboBox.Items.Add(db);

                    this.databaseComboBox.SelectedIndex = 0;
                }

                Cursor.Current = currentCursor;
            }
            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError(Dialog_TestResults, ex);
            }
        }


        private void testConnectionButton_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                IConnectionProperties connectionProperties = ConnectionPropertiesBuilder.Build(CreateConnectionBuilderOptions());
                connectionProperties.Test();
                this.ConnectionProperties = connectionProperties;
                this.IsSuccessfulTest = true;

                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowMessage(Dialog_TestResults, Dialog_TestConnectionSucceeded);
            }
            catch (Exception ex)
            {
                this.IsSuccessfulTest = false;
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError(Dialog_TestResults, ex);
            }
            finally
            {
                ConfigureAcceptButton();
            }
        }



        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
