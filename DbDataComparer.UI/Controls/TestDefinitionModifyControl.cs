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
    public partial class TestDefinitionModifyControl : TestDefinitionUserControl
    {
        private TestDefinition WorkingTestDefinition = null;

        private const string EMAIL_DOMAIN = "@tql.com";

        private const int CompareOptionsTabPageIndex = 0;
        private const int notificationsTabPageIndex = 1;
        private const int testsTabPageIndex = 2;

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


        #region Load Form Field Value Routines
        private void LoadTestDefinitionValues()
        {
            if (this.WorkingTestDefinition == null)
                return;

            LoadTestDefintionCompareOptions();
            LoadTestDefintionNotificationOptions();
        }

        private void LoadTestDefintionCompareOptions()
        {
            Control control;
            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["parameterOutputCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ParameterOutput);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["parameterReturnCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ParameterReturn);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["resultSetMetaDataCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ResultSetMetaData);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["resultSetDataCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.CompareOptions.ResultSetData);
        }

        private void LoadTestDefintionNotificationOptions()
        {
            Control control;
            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["everyCompareCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.NotificationOptions.NotifyOnEveryCompare);

            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["failureCheckBox"];
            SetCheckBox(control, this.WorkingTestDefinition.NotificationOptions.NotifyOnFailure);

            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["emailTextBox"];
            SetTextBox(control, this.WorkingTestDefinition.NotificationOptions.Email);
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
        }

        private void SaveTestDefintionCompareOptions()
        {
            Control control;
            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["parameterOutputCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ParameterOutput = GetCheckBox(control);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["parameterReturnCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ParameterReturn = GetCheckBox(control);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["resultSetMetaDataCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ResultSetMetaData = GetCheckBox(control);

            control = this.tdTabControl.TabPages["compareOptionsTabPage"].Controls["resultSetDataCheckBox"];
            this.WorkingTestDefinition.CompareOptions.ResultSetData = GetCheckBox(control);
        }


        private void SaveTestDefintionNotificationOptions()
        {
            Control control;
            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["everyCompareCheckBox"];
            this.WorkingTestDefinition.NotificationOptions.NotifyOnEveryCompare = GetCheckBox(control);

            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["failureCheckBox"];
            this.WorkingTestDefinition.NotificationOptions.NotifyOnFailure = GetCheckBox(control);

            control = this.tdTabControl.TabPages["notificationsTabPage"].Controls["emailTextBox"];
            this.WorkingTestDefinition.NotificationOptions.Email = GetTextBox(control)?.Trim();
            if (!String.IsNullOrWhiteSpace(this.WorkingTestDefinition.NotificationOptions.Email) &&
                !this.WorkingTestDefinition.NotificationOptions.Email.EndsWith(EMAIL_DOMAIN, StringComparison.OrdinalIgnoreCase))
                this.WorkingTestDefinition.NotificationOptions.Email += EMAIL_DOMAIN;
        }


        private void Validate()
        {
            ValidateTestDefintionNotificationOptions();
        }

        private void ValidateTestDefintionNotificationOptions()
        {
            
            Control compareControl = this.tdTabControl.TabPages["notificationsTabPage"].Controls["everyCompareCheckBox"];
            Control failureControl = this.tdTabControl.TabPages["notificationsTabPage"].Controls["failureCheckBox"];
            Control emailControl = this.tdTabControl.TabPages["notificationsTabPage"].Controls["emailTextBox"];

            if ((GetCheckBox(compareControl) || GetCheckBox(failureControl)) &&
                String.IsNullOrWhiteSpace(GetTextBox(emailControl)))
            {
                throw new Exception("Email is required if at least one notification option is selected");
            }
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


        private void TestDefinitionModifyControl_Load(object sender, EventArgs e)
        {
            saveButtonContextMenuStrip.ItemClicked += saveButtonContextMenuStrip_ItemClicked;
        }


        private async void tdLoadButton_Click(object sender, EventArgs e)
        {
            // Raise event to load Test Definition for testing
            var loadEventArgs = new TestDefinitionLoadRequestedEventArgs();
            OnTestDefinitionLoadRequested(loadEventArgs);

            if (loadEventArgs.SuccessfullyLoaded)
                this.QueryTestDefinition();
        }

        private async void tdSaveButton_Click(object sender, EventArgs e)
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

                // Overlay saveButtonContextMenuStrip to give user a choice of save options
                Point screenPoint = tdSaveButton.PointToScreen(new Point(tdSaveButton.Left, tdSaveButton.Bottom));
                if (screenPoint.Y + saveButtonContextMenuStrip.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    saveButtonContextMenuStrip.Show(tdSaveButton, new Point(0, -saveButtonContextMenuStrip.Size.Height));
                }
                else
                {
                    saveButtonContextMenuStrip.Show(tdSaveButton, new Point(0, tdSaveButton.Height));
                }
            }

            catch(Exception ex)
            {
                RTLAwareMessageBox.ShowError("Save", ex);
            }
        }


        private async void saveButtonContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (item == this.saveForComparisonToolStripMenuItem)
            {
                this.SetTestDefinition(this.WorkingTestDefinition);
            }

            if (item == this.saveToFileToolStripMenuItem)
            {
                var saveEventArgs = new TestDefinitionSaveRequestedEventArgs() { TestDefinition = this.WorkingTestDefinition };
                OnTestDefinitionSaveRequested(saveEventArgs);

                if (saveEventArgs.SuccessfullySaved)
                    this.QueryTestDefinition();
            }
        }
    }
}
