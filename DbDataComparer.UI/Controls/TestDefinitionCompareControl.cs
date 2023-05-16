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
using DbDataComparer.Domain.Interfaces;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class TestDefinitionCompareControl : TestDefinitionControl
    {
        private const int OverallResultsTabPageIndex = 0;
        private const int ErrorsTabPageIndex = 1;


        public TestDefinitionCompareControl()
        {
            InitializeComponent();
        }


        public override async void Activate()
        {
            base.Activate();

            // Make sure first tab is selected
            this.tdTabControl.SelectedIndex = OverallResultsTabPageIndex;

            // Clear out any previous values
            WriteOverallResults(null);
            WriteDetailResults(null);

            try
            {
                Application.UseWaitCursor = true;
                await ExecuteComparison();
                Application.UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                Application.UseWaitCursor = false;
                RTLAwareMessageBox.ShowError("Comparison", ex);
            }            
        }

        private async Task ExecuteComparison()
        {
            IEmailNotifier emailNotifier = new TestDefinitionNotifier(this.ConfigurationSettings.Notification);

            var statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Executing Tests" };
            OnTestDefinitionStatusUpdated(statusEventArgs);
            ITestExecutioner testExecutioner = new TestExecutioner(new SqlDatabase());
            IEnumerable<TestExecutionResult> testResults = await testExecutioner.Execute(this.TestDefinition);

            statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Comparing Results" };
            OnTestDefinitionStatusUpdated(statusEventArgs);
            IEnumerable<ComparisonResult> comparisonResults = TestDefinitionComparer.Compare(this.TestDefinition, testResults);

            statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Formatting Results" };
            OnTestDefinitionStatusUpdated(statusEventArgs);
            WriteOverallResults(await CreateOverallResults(this.TestDefinition, comparisonResults));

            if (TestDefinitionComparer.IsAny(comparisonResults, ComparisonResultTypeEnum.Failed))
                WriteDetailResults(await CreateDetailResults(this.TestDefinition, comparisonResults));

            if (emailNotifier.IsNotificationEnabled(this.TestDefinition, comparisonResults))
            {
                statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Emailing Results" };
                emailNotifier.AddNotification(this.TestDefinition, comparisonResults);
                emailNotifier.SendNotification("Data ComparerUI - Manual Comparison");
            }

            statusEventArgs = new TestDefinitionStatusUpdatedEventArgs() { Status = "Comparison Completed" };
            OnTestDefinitionStatusUpdated(statusEventArgs);
        }


        private async Task<string> CreateOverallResults(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            string ret = null;

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                await TestDefinitionComparer.WriteOverallResults(sw, testDefinition, comparisonResults);
                sw.Flush();

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);

                ret = sr.ReadToEnd();
            }

            return ret;
        }

        private async Task<string> CreateDetailResults(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            string ret = null;

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                await TestDefinitionComparer.WriteDetailResults(sw, testDefinition, comparisonResults);
                sw.Flush();

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);

                ret = sr.ReadToEnd();
            }

            return ret;
        }

        private void WriteOverallResults(string text)
        {
            Control control = this.tdTabControl.TabPages["overallResultsTabPage"].Controls["overallResultsTextBox"];
            ((TextBox)control).Text = text;
        }

        private void WriteDetailResults(string text)
        {
            Control control = this.tdTabControl.TabPages["errorsTabPage"].Controls["errorsTextBox"];
            ((TextBox)control).Text = text;
        }

    }
}
