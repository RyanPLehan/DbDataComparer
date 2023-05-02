using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbDataComparer.UI.Controls
{
    public partial class StoredProcedureTestsControl : UserControl
    {
        private const int NOT_SELECTED_INDEX = -1;
        private const int NEW_SELECTED_INDEX = 0;

        private IList<StoredProcedureTest> Tests = new List<StoredProcedureTest>();
        private StoredProcedureTest WorkingTest = new StoredProcedureTest();

        public StoredProcedureTestsControl()
        {
            InitializeComponent();
        }

        #region General Routines
        public IEnumerable<StoredProcedureTest> GetTests()
        {
            return this.Tests.ToArray();
        }


        public void SetTests(IEnumerable<StoredProcedureTest> tests)
        {
            if (tests != null)
                this.Tests = tests.ToList();

            LoadTests();
        }


        private void LoadTests()
        {
            LoadTestsComboBox();
            this.testsComboBox.SelectedIndex = NOT_SELECTED_INDEX;

            this.WorkingTest = new StoredProcedureTest();
            LoadTest();
        }


        private void LoadTestsComboBox()
        {
            this.testsComboBox.Items.Clear();

            this.testsComboBox.Items.Add("<< New Test >>");
            foreach (StoredProcedureTest test in this.Tests.OrderBy(x => x.Name))
                this.testsComboBox.Items.Add(test);
        }


        private void FindInTestsComboBox(string testName)
        {
            int selectedIndex = NOT_SELECTED_INDEX;

            for (int i = 1; i < this.testsComboBox.Items.Count; i++)
            {
                if (this.testsComboBox.Items[i].ToString().Equals(testName))
                {
                    selectedIndex = i;
                    break;
                }
            }

            this.testsComboBox.SelectedIndex = selectedIndex;
        }


        private void LoadTest()
        {
            Control control;

            this.testNameTextBox.Text = this.WorkingTest.Name;

            // Source Sql
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["testSourceTextBox"];
            //((TextBox)control).Text = this.WorkingTest.SourceSql;

            // Target Sql
            control = this.testTabControl.TabPages["targetTabPage"].Controls["testTargetTextBox"];
            //((TextBox)control).Text = this.WorkingTest.TargetSql;

            // Set focus to first tab page
            this.testTabControl.TabPages[0].Focus();
        }


        private void SetWorkingTest()
        {
            Control control;

            this.WorkingTest.Name = this.testNameTextBox.Text?.Trim();

            // Source Sql
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["testSourceTextBox"];
            //this.WorkingTest.SourceSql = ((TextBox)control).Text?.Trim();

            // Target Sql
            control = this.testTabControl.TabPages["targetTabPage"].Controls["testTargetTextBox"];
            //this.WorkingTest.TargetSql = ((TextBox)control).Text?.Trim();

        }


        private void ValidateTest()
        {
            if (String.IsNullOrWhiteSpace(this.WorkingTest.Name))
                throw new Exception("Missing Test Name");

            /*
            if (String.IsNullOrWhiteSpace(this.WorkingTest.SourceSql))
                throw new Exception("Missing Source SQL");

            if (String.IsNullOrWhiteSpace(this.WorkingTest.TargetSql))
                throw new Exception("Missing Target SQL");
            */
        }
        #endregion


        private void testsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.testsComboBox.SelectedIndex;

            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                    break;

                case NEW_SELECTED_INDEX:
                    this.WorkingTest = new StoredProcedureTest();
                    LoadTest();
                    break;

                default:
                    object item = this.testsComboBox.Items[selectedIndex];
                    this.WorkingTest = (StoredProcedureTest)item;
                    LoadTest();
                    break;
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Check to see if test already exists in base list
                if (!this.Tests.Any(x => x == this.WorkingTest))
                {
                    SetWorkingTest();
                    ValidateTest();
                    this.Tests.Add(this.WorkingTest);
                    LoadTestsComboBox();
                    FindInTestsComboBox(this.WorkingTest.Name);
                }
            }
            catch (Exception ex)
            {
                RTLAwareMessageBox.ShowError("Add Test", ex);
            }
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.testsComboBox.SelectedIndex;

            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                case NEW_SELECTED_INDEX:
                    RTLAwareMessageBox.ShowMessage("Delete Test", "Please select a test from the drop down box");
                    break;

                default:
                    this.Tests = this.Tests
                                     .Where(x => x != this.WorkingTest)
                                     .ToList();
                    LoadTests();
                    break;
            }
        }
    }
}
