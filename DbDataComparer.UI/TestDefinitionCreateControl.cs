using System;
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
    public partial class TestDefinitionCreateControl : UserControl
    {
        public event EventHandler<TestDefinitionCreatedEventArgs> TestDefinitionCreated;

        public TestDefinitionCreateControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raise Event
        /// </summary>
        /// <param name=""></param>
        private void OnTestDefinitionCreated(TestDefinitionCreatedEventArgs e)
        {
            EventHandler<TestDefinitionCreatedEventArgs> handler = TestDefinitionCreated;
            if (handler != null)
                handler(this, e);
        }

        private TestDefinitionBuilderOptions CreateTestDefinitionBuilderOptions()
        {
            TestDefinitionBuilderOptions options = new TestDefinitionBuilderOptions();
            options.Name = this.tdNameTextBox.Text;

            // Source
            Control control = this.tdTabControl.TabPages["sourceTabPage"].Controls["sourceDataExplorer"];
            DataExplorerControl.DataExplorerResult deResult = GetDataExplorerResult(control);
            options.Source = new TestDefinitionBuilderOptions.DatabaseOptions()
            {
                ConnectionString = deResult.ConnectionString,
                DatabaseObjectName = deResult.DatabaseObjectName,
            };

            // Target
            control = this.tdTabControl.TabPages["targetTabPage"].Controls["targetDataExplorer"];
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

        private void tdCreateButton_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var options = CreateTestDefinitionBuilderOptions();
                ValidateOptions(options);
                var builder = new TestDefinitionBuilder(new SqlDatabase());
                TestDefinition td = builder.Build(options).GetAwaiter().GetResult();
                Cursor.Current = currentCursor;

                // Raise event for successful Test Definition Createion
                var eventArgs = new TestDefinitionCreatedEventArgs { TestDefinition = td };
                OnTestDefinitionCreated(eventArgs);
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Test Definition Creation", ex);
            }
        }
    }
}
