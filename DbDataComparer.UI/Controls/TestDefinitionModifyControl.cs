﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class TestDefinitionModifyControl : TestDefinitionControl
    {
        private TestDefinition WorkingTestDefinition = null;

        private const string EMAIL_DOMAIN = "@tql.com";

        private const int CompareOptionsTabPageIndex = 0;
        private const int NotificationsTabPageIndex = 1;
        private const int DatabaseTabPageIndex = 2;
        private const int TestsTabPageIndex = 3;

        public TestDefinitionModifyControl()
        {
            InitializeComponent();
        }

        public override void Activate()
        {
            base.Activate();
            this.QueryTestDefinition();
        }

        protected override void QueryTestDefinition()
        {
            base.QueryTestDefinition();
            SetWorkingTestDefinition(this.TestDefinition);
            InitializeTestControls();
            this.LoadTestDefinitionValues();
        }

        private void SetWorkingTestDefinition(TestDefinition testDefinition)
        {
            this.WorkingTestDefinition = Clone(testDefinition);
        }

        private TestDefinition Clone(TestDefinition testDefinition)
        {
            if (testDefinition == null)
                return null;
            else
                return NSJson.Deserialize<TestDefinition>(NSJson.Serialize(testDefinition));
        }

        private void InitializeTestControls()
        {
            Control tvControl = GetTableViewTestsControl();
            Control spControl = GetStoredProcedureTestsControl();

            if (tvControl != null)
                tvControl.Visible = false;

            if (spControl != null)
                spControl.Visible = false;
        }

        private TestsControl GetTestsControl()
        {
            TestsControl control = null;

            if (this.WorkingTestDefinition.TableViewTests != null)
                control = GetTableViewTestsControl();

            if (this.WorkingTestDefinition.StoredProcedureTests != null)
                control = GetStoredProcedureTestsControl();

            return control;
        }

        private TableViewTestsControl GetTableViewTestsControl()
        {
            Control control = this.tdTabControl
                                  .TabPages["testsTabPage"]
                                  .Controls["tableViewTestsControl"];
            return (TableViewTestsControl)control;
        }

        private StoredProcedureTestsControl GetStoredProcedureTestsControl()
        {
            Control control = this.tdTabControl
                                  .TabPages["testsTabPage"]
                                  .Controls["storedProcedureTestsControl"];
            return (StoredProcedureTestsControl)control;
        }


        #region Load Form Field Value Routines
        private void LoadTestDefinitionValues()
        {
            if (this.WorkingTestDefinition == null)
                return;

            LoadTestDefintionCompareOptions();
            LoadTestDefintionNotificationOptions();
            LoadTestDefintionDatabaseOptions();
            LoadTests();
        }

        private void LoadTestDefintionCompareOptions()
        {
            Control control;
            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["parameterOutputCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ParameterOutput);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["parameterReturnCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ParameterReturn);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["resultSetMetaDataCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ResultSetMetaData);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["resultSetDataCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ResultSetData);
        }

        private void LoadTestDefintionNotificationOptions()
        {
            Control control;
            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["everyCompareCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.NotificationOptions.NotifyOnEveryCompare);

            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["failureCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.NotificationOptions.NotifyOnFailure);

            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["emailTextBox"];
            SetTextBox(control, this.WorkingTestDefinition.NotificationOptions.Email);
        }

        private void LoadTestDefintionDatabaseOptions()
        {
            Control control;

            // Source
            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcDatabaseObjectLabel"];
            control.Text = this.WorkingTestDefinition.Source.Type == CommandType.Text ? "Table / View" : "Stored Procedure";

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcDatabaseObjectTextBox"];
            control.Text = this.WorkingTestDefinition.Source.Text;

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcExecutionTimeOutTextBox"];
            control.Text = this.WorkingTestDefinition.Source.ExecutionTimeoutInSeconds.ToString();

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcConnectionStringTextBox"];
            control.Text = this.WorkingTestDefinition.Source.ConnectionString;


            // Target
            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtDatabaseObjectLabel"];
            control.Text = this.WorkingTestDefinition.Target.Type == CommandType.Text ? "Table / View" : "Stored Procedure";

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtDatabaseObjectTextBox"];
            control.Text = this.WorkingTestDefinition.Target.Text;

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtExecutionTimeOutTextBox"];
            control.Text = this.WorkingTestDefinition.Target.ExecutionTimeoutInSeconds.ToString();

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtConnectionStringTextBox"];
            control.Text = this.WorkingTestDefinition.Target.ConnectionString;
        }

        private void LoadTests()
        {
            TestsControl control = GetTestsControl();
            control.Visible = true;
            control.LoadTestDefinition(this.WorkingTestDefinition);
        }


        private void SetCheckBox(Control control, bool value)
        {
            if (control is CheckBox)
                ((CheckBox)control).Checked = value;
        }

        private void SetTextBox(Control control, string value)
        {
            if (control is TextBox)
                ((TextBox)control).Text = value;
        }

        #endregion


        #region Save Form Field Value Routines
        private void SaveTestDefinitionValues()
        {
            if (this.WorkingTestDefinition == null)
                return;

            SaveTestDefintionCompareOptions();
            SaveTestDefintionNotificationOptions();
            SaveTestDefintionDatabaseOptions();
            SaveTests();
        }

        private void SaveTestDefintionCompareOptions()
        {
            Control control;
            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["parameterOutputCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ParameterOutput = GetCheckBox(control);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["parameterReturnCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ParameterReturn = GetCheckBox(control);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["resultSetMetaDataCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ResultSetMetaData = GetCheckBox(control);

            control = this.tdTabControl
                          .TabPages["compareOptionsTabPage"]
                          .Controls["resultSetDataCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ResultSetData = GetCheckBox(control);
        }


        private void SaveTestDefintionNotificationOptions()
        {
            Control control;
            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["everyCompareCheckBox"];
            this.WorkingTestDefinition.NotificationOptions.NotifyOnEveryCompare = GetCheckBox(control);

            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["failureCheckBox"];
            this.WorkingTestDefinition.NotificationOptions.NotifyOnFailure = GetCheckBox(control);

            control = this.tdTabControl
                          .TabPages["notificationsTabPage"]
                          .Controls["emailTextBox"];
            this.WorkingTestDefinition.NotificationOptions.Email = GetTextBox(control)?.Trim();
            if (!String.IsNullOrWhiteSpace(this.WorkingTestDefinition.NotificationOptions.Email) &&
                !this.WorkingTestDefinition.NotificationOptions.Email.EndsWith(EMAIL_DOMAIN, StringComparison.OrdinalIgnoreCase))
                this.WorkingTestDefinition.NotificationOptions.Email += EMAIL_DOMAIN;
        }

        private void SaveTestDefintionDatabaseOptions()
        {
            Control control;
            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcExecutionTimeOutTextBox"];
            this.WorkingTestDefinition.Source.ExecutionTimeoutInSeconds = Int32.Parse(control.Text);

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcConnectionStringTextBox"];
            this.WorkingTestDefinition.Source.ConnectionString = control.Text;


            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtExecutionTimeOutTextBox"];
            this.WorkingTestDefinition.Target.ExecutionTimeoutInSeconds = Int32.Parse(control.Text);

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtConnectionStringTextBox"];
            this.WorkingTestDefinition.Target.ConnectionString = control.Text;
        }


        private void SaveTests()
        {
            if (this.WorkingTestDefinition.TableViewTests != null)
            {
                TableViewTestsControl control = GetTableViewTestsControl();
                this.WorkingTestDefinition.TableViewTests = control.SaveTestDefinition().TableViewTests;
            }

            if (this.WorkingTestDefinition.StoredProcedureTests != null)
            {
                StoredProcedureTestsControl control = GetStoredProcedureTestsControl();
                this.WorkingTestDefinition.StoredProcedureTests = control.SaveTestDefinition().StoredProcedureTests;
            }
        }

        private void Validate()
        {
            ValidateTestDefintionNotificationOptions();
            ValidateTestDefintionDatabaseOptions();
        }

        private void ValidateTestDefintionNotificationOptions()
        {

            Control compareControl = this.tdTabControl
                                         .TabPages["notificationsTabPage"]
                                         .Controls["everyCompareCheckBox"];
            Control failureControl = this.tdTabControl
                                         .TabPages["notificationsTabPage"]
                                         .Controls["failureCheckBox"];
            Control emailControl = this.tdTabControl
                                       .TabPages["notificationsTabPage"]
                                       .Controls["emailTextBox"];

            if ((GetCheckBox(compareControl) || GetCheckBox(failureControl)) &&
                String.IsNullOrWhiteSpace(GetTextBox(emailControl)))
            {
                throw new Exception("Email is required if at least one notification option is selected");
            }
        }

        private void ValidateTestDefintionDatabaseOptions()
        {
            int temp;
            Control control;
            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcExecutionTimeOutTextBox"];
            if (!Int32.TryParse(control.Text, out temp))
                throw new Exception("Unable to parse Source Execution Timeout");

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcConnectionStringTextBox"];
            if (String.IsNullOrEmpty(control.Text))
                throw new Exception("Source Connection String cannot be null or empty");


            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtExecutionTimeOutTextBox"];
            if (!Int32.TryParse(control.Text, out temp))
                throw new Exception("Unable to parse Target Execution Timeout");

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtConnectionStringTextBox"];
            if (String.IsNullOrEmpty(control.Text))
                throw new Exception("Target Connection String cannot be null or empty");
        }


        private bool GetCheckBox(Control control)
        {
            bool ret = false;
            if (control is CheckBox)
                ret = ((CheckBox)control).Checked;

            return ret;
        }

        private string GetTextBox(Control control)
        {
            string ret = null;
            if (control is TextBox)
                ret = ((TextBox)control).Text;

            return ret;
        }

        #endregion


        #region Control Event handlers
        private async void cancelButton_Click(object sender, EventArgs e)
        {
            this.tdTabControl.SelectedIndex = CompareOptionsTabPageIndex;
            this.QueryTestDefinition();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (this.WorkingTestDefinition == null)
            {
                RTLAwareMessageBox.ShowMessage("Test Definition", "No Test Definition Loaded");
                return;
            }

            try
            {
                Validate();
                SaveTestDefinitionValues();

                var saveEventArgs = new TestDefinitionSaveRequestedEventArgs()
                {
                    TestDefinition = this.WorkingTestDefinition,
                    ForceOverwrite = true,
                };
                OnTestDefinitionSaveRequested(saveEventArgs);

                if (saveEventArgs.SuccessfullySaved)
                    this.QueryTestDefinition();
            }

            catch (Exception ex)
            {
                RTLAwareMessageBox.ShowError("Save", ex);
            }
        }
        #endregion

        private void srcBuildButton_Click(object sender, EventArgs e)
        {
            Control control;
            var dialog = new DataConnectionDialog();

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["srcGroupBox"]
                          .Controls["srcConnectionStringTextBox"];

            if (!String.IsNullOrWhiteSpace(control.Text))
                dialog.ConnectionString = control.Text;

            var result = DataConnectionDialog.Show(dialog, this);
            if (result == DialogResult.OK)
                control.Text = dialog.ConnectionString;
        }

        private void tgtBuildButton_Click(object sender, EventArgs e)
        {
            Control control;
            var dialog = new DataConnectionDialog();

            control = this.tdTabControl
                          .TabPages["databaseTabPage"]
                          .Controls["tgtGroupBox"]
                          .Controls["tgtConnectionStringTextBox"];

            if (!String.IsNullOrWhiteSpace(control.Text))
                dialog.ConnectionString = control.Text;

            var result = DataConnectionDialog.Show(dialog, this);
            if (result == DialogResult.OK)
                control.Text = dialog.ConnectionString;

        }
    }
}
