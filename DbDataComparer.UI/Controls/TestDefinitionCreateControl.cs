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
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class TestDefinitionCreateControl : TestDefinitionUserControl
    {
        private const int SourceTabPageIndex = 0;
        private const int TargetTabPageIndex = 1;

        public TestDefinitionCreateControl()
        {
            InitializeComponent();
        }


        private TestDefinitionBuilderOptions CreateTestDefinitionBuilderOptions()
        {
            TestDefinitionBuilderOptions options = new TestDefinitionBuilderOptions();
            options.Name = this.tdNameTextBox.Text;

            // Source
            Control control = this.tdTabControl.TabPages["sourceTabPage"].Controls["sourceDataExplorerControl"];
            DataExplorerControl.DataExplorerResult deResult = GetDataExplorerResult(control);
            options.Source = new TestDefinitionBuilderOptions.DatabaseOptions()
            {
                ConnectionString = deResult.ConnectionString,
                DatabaseObjectName = deResult.DatabaseObjectName,
            };

            // Target
            control = this.tdTabControl.TabPages["targetTabPage"].Controls["targetDataExplorerControl"];
            deResult = GetDataExplorerResult(control);
            options.Target = new TestDefinitionBuilderOptions.DatabaseOptions()
            {
                ConnectionString = deResult.ConnectionString,
                DatabaseObjectName = deResult.DatabaseObjectName,
            };

            return options;
        }


        private DataExplorerControl.DataExplorerResult GetDataExplorerResult(Control c)
        {
            DataExplorerControl.DataExplorerResult result = null;

            if (c is DataExplorerControl)
                result = ((DataExplorerControl)c).GetDataExplorerResult();

            return result;
        }


        private void ValidateOptions(TestDefinitionBuilderOptions options)
        {
            if (String.IsNullOrWhiteSpace(options.Name))
                throw new Exception("Missing Name of Test Definition");

            // Validate data from Source and target Database Options
            ValidateOptions("Source", options.Source);
            ValidateOptions("Target", options.Target);
        }


        private void ValidateOptions(string description, TestDefinitionBuilderOptions.DatabaseOptions options)
        {
            if (String.IsNullOrWhiteSpace(options.ConnectionString))
                throw new Exception(String.Format("Missing {0} connection string", description));

            if (String.IsNullOrWhiteSpace(options.DatabaseObjectName))
                throw new Exception(String.Format("Missing {0} database object name", description));
        }


        private async void tdCreateButton_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var options = CreateTestDefinitionBuilderOptions();
                ValidateOptions(options);
                var builder = new TestDefinitionBuilder(new SqlDatabase());
                this.TestDefinition = await builder.Build(options);
                Cursor.Current = currentCursor;

                // Raise event for successful Test Definition Createion
                var saveEventArgs = new TestDefinitionSaveRequestedEventArgs() { TestDefinition = this.TestDefinition };
                OnTestDefinitionSaveRequested(saveEventArgs);

                if (saveEventArgs.SuccessfullySaved)
                {
                    var setEventArgs = new TestDefinitionSetRequestedEventArgs()
                    {
                        PathName = saveEventArgs.PathName,
                        TestDefinition = this.TestDefinition,
                    };
                    OnTestDefinitionSetRequested(setEventArgs);

                    var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Created" };
                    OnTestDefinitionStatusUpdated(statusEventArgs);
                }
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Test Definition Creation", ex);

                var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Creation Failed" };
                OnTestDefinitionStatusUpdated(statusEventArgs);
            }
        }
    }
}