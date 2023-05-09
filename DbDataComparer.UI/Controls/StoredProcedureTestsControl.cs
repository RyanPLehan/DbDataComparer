using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.UI
{
    public partial class StoredProcedureTestsControl : UserControl
    {
        private enum AddUpdateActionEnum : int
        {
            Add,
            Update,
        }

        private const int NOT_SELECTED_INDEX = -1;
        private const int NEW_SELECTED_INDEX = 0;

        private const int SourceTabPageIndex = 0;
        private const int TargetTabPageIndex = 1;

        private TestDefinition TestDefinition;
        private IList<StoredProcedureTest> Tests;
        private StoredProcedureTest WorkingTest;

        public StoredProcedureTestsControl()
        {
            InitializeComponent();
        }

        #region General Routines
        public void LoadTestDefinition(TestDefinition testDefinition)
        {
            this.TestDefinition = testDefinition ??
                throw new ArgumentNullException(nameof(testDefinition));

            if (testDefinition.StoredProcedureTests != null)
                this.Tests = testDefinition.StoredProcedureTests.ToList();
            else
                this.Tests = new List<StoredProcedureTest>();

            SetAddUpdateButton(AddUpdateActionEnum.Add);
            LoadTests();
        }

        public TestDefinition SaveTestDefinition()
        {
            this.TestDefinition.StoredProcedureTests = this.Tests.ToArray();
            return this.TestDefinition;
        }


        private void SetAddUpdateButton(AddUpdateActionEnum addUpdateAction)
        {
            Button button = this.addUpdateButton;
            button.Tag = addUpdateAction;

            switch (addUpdateAction)
            {
                case AddUpdateActionEnum.Add:
                    button.Text = "Add";
                    break;

                case AddUpdateActionEnum.Update:
                    button.Text = "Update";
                    break;
            }
        }

        private void LoadTests()
        {
            this.addUpdateButton.Enabled = false;
            this.deleteButton.Enabled = false;

            LoadTestsComboBox();
            this.testsComboBox.SelectedIndex = NOT_SELECTED_INDEX;

            CreateWorkingTest();
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


        private void CreateWorkingTest()
        {
            this.WorkingTest = new StoredProcedureTest();
        }


        private void LoadTest()
        {
            Control control;

            this.testNameTextBox.Text = this.WorkingTest.Name;

            // Source
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["sourceSprocParametersControl"];
            ((SprocParametersControl)control).SetParameters(this.TestDefinition.Source.Parameters);


            // Target Sql
            control = this.testTabControl.TabPages["targetTabPage"].Controls["testTargetTextBox"];
            //((TextBox)control).Text = this.WorkingTest.TargetSql;

            // Set focus to first tab page
            this.testTabControl.SelectedIndex = SourceTabPageIndex;
        }


        private void SaveTest()
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
            Control control;

            if (String.IsNullOrWhiteSpace(this.testNameTextBox.Text))
                throw new Exception("Missing Test Name");


            // Source Sql
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["testSourceTextBox"];
            if (String.IsNullOrWhiteSpace(((TextBox)control).Text))
                throw new Exception("Missing Source SQL");


            // Target Sql
            control = this.testTabControl.TabPages["targetTabPage"].Controls["testTargetTextBox"];
            if (String.IsNullOrWhiteSpace(((TextBox)control).Text))
                throw new Exception("Missing Target SQL");
        }
        #endregion


        private void testsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addUpdateButton.Enabled = true;
            this.deleteButton.Enabled = false;

            int selectedIndex = this.testsComboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                    this.addUpdateButton.Enabled = false;
                    SetAddUpdateButton(AddUpdateActionEnum.Add);
                    break;

                case NEW_SELECTED_INDEX:
                    SetAddUpdateButton(AddUpdateActionEnum.Add);
                    CreateWorkingTest();
                    LoadTest();
                    break;

                default:
                    this.deleteButton.Enabled = true;
                    SetAddUpdateButton(AddUpdateActionEnum.Update);
                    object item = this.testsComboBox.Items[selectedIndex];
                    this.WorkingTest = (StoredProcedureTest)item;
                    LoadTest();
                    break;
            }
        }


        private void addUpdateButton_Click(object sender, EventArgs e)
        {
            AddUpdateActionEnum addUpdateAction = (AddUpdateActionEnum)addUpdateButton.Tag;

            try
            {
                ValidateTest();
                SaveTest();

                // Add to Tests list if not already in list
                if (addUpdateAction == AddUpdateActionEnum.Add &&
                    !this.Tests.Any(x => x == this.WorkingTest))
                    this.Tests.Add(this.WorkingTest);

                LoadTestsComboBox();
                FindInTestsComboBox(this.WorkingTest.Name);
            }
            catch (Exception ex)
            {
                string title = String.Format("{0} Test", addUpdateAction.ToString());
                RTLAwareMessageBox.ShowError(title, ex);
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
