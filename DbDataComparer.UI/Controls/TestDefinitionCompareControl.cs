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
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class TestDefinitionCompareControl : TestDefinitionUserControl
    {
        private const int OverallResultsTabPageIndex = 0;
        private const int ErrorsTabPageIndex = 1;


        public TestDefinitionCompareControl()
        {
            InitializeComponent();
        }



        private async void tdLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Raise event to load Test Definition for testing
                var loadEventArgs = new TestDefinitionLoadRequestedEventArgs();
                OnTestDefinitionLoadRequested(loadEventArgs);

                if (loadEventArgs.SuccessfullyLoaded)
                {
                    this.TestDefinition = loadEventArgs.TestDefinition;

                    var setEventArgs = new TestDefinitionSetRequestedEventArgs()
                    {
                        PathName = loadEventArgs.PathName,
                        TestDefinition = this.TestDefinition,
                    };
                    OnTestDefinitionSetRequested(setEventArgs);

                    var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Loaded" };
                    OnTestDefinitionStatusUpdated(statusEventArgs);
                }

            }

            catch (Exception ex)
            {
                RTLAwareMessageBox.ShowError("Test Definition Load", ex);

                var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Load Failed" };
                OnTestDefinitionStatusUpdated(statusEventArgs);
            }
        }

        private async void tdCompareButton_Click(object sender, EventArgs e)
        {
            // Let's query from main form for any loaded/created Test Definition object
            this.QueryTestDefinition();

            if (this.TestDefinition == null)
            {
                RTLAwareMessageBox.ShowMessage("Test Definition", "No Test Definition Loaded");
                return;
            }

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Executing Tests" };
                OnTestDefinitionStatusUpdated(statusEventArgs);
                ITestExecutioner testExecutioner = new TestExecutioner(new SqlDatabase());
                IEnumerable<TestExecutionResult> testResults = await testExecutioner.Execute(this.TestDefinition);

                statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Comparing Results" };
                OnTestDefinitionStatusUpdated(statusEventArgs);
                IEnumerable<ComparisonResult> comparisonResults = TestDefinitionComparer.Compare(this.TestDefinition, testResults);

                statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Formatting Results" };
                OnTestDefinitionStatusUpdated(statusEventArgs);
                await WriteOverallResults(this.TestDefinition, comparisonResults);

                if (TestDefinitionComparer.IsAny(comparisonResults, ComparisonResultTypeEnum.Failed))
                    await WriteDetailResults(this.TestDefinition, comparisonResults);

                statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Comparison Completed" };
                OnTestDefinitionStatusUpdated(statusEventArgs);

                Cursor.Current = currentCursor;
            }

            catch (Exception ex)
            {
                Cursor.Current = currentCursor;
                RTLAwareMessageBox.ShowError("Comparison", ex);
            }
        }


        private async Task WriteOverallResults(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                await TestDefinitionComparer.WriteOverallResults(sw, testDefinition, comparisonResults);
                sw.Flush();

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                Control control = this.tdTabControl.TabPages["overallResultsTabPage"].Controls["overallResultsTextBox"];
                ((TextBox)control).Text = sr.ReadToEnd();
            }
        }

        private async Task WriteDetailResults(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                await TestDefinitionComparer.WriteDetailResults(sw, testDefinition, comparisonResults);
                sw.Flush();

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                Control control = this.tdTabControl.TabPages["errorsTabPage"].Controls["errorsTextBox"];
                ((TextBox)control).Text = sr.ReadToEnd();
            }
        }
    }
}
