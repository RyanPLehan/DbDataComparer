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
    public partial class TableViewTestsControl : TestsControl
    {
        private IList<TableViewTest> Tests;
        private TableViewTest WorkingTest;

        public TableViewTestsControl()
        {
            InitializeComponent();

            // Manually set Event Handlers
            this.addUpdateButton.Click += addUpdateButton_Click;
            this.deleteButton.Click += deleteButton_Click;
            this.testsComboBox.SelectedIndexChanged += testsComboBox_SelectedIndexChanged;
        }


        #region General Routines
        public override void LoadTestDefinition(TestDefinition testDefinition)
        {
            this.TestDefinition = testDefinition ??
                throw new ArgumentNullException(nameof(testDefinition));

            if (testDefinition.TableViewTests != null)
                this.Tests = testDefinition.TableViewTests.ToList();
            else
                this.Tests = new List<TableViewTest>();

            SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
            LoadTests();
        }

        public override TestDefinition SaveTestDefinition()
        {
            this.TestDefinition.TableViewTests = this.Tests.ToArray();
            return this.TestDefinition;
        }


        private void LoadTests()
        {
            this.addUpdateButton.Enabled = false;
            this.deleteButton.Enabled = false;

            LoadComboBox<TableViewTest>(this.testsComboBox, this.Tests);
            this.testsComboBox.SelectedIndex = (this.testsComboBox.Items.Count > 1 ? NEW_SELECTED_INDEX + 1 : NEW_SELECTED_INDEX);
        }


        private void CreateWorkingTest()
        {
            this.WorkingTest = new TableViewTest();
        }

        private TextBox GetSourceTextBox()
        {
            Control control;
            control = this.testTabControl.TabPages["sourceTabPage"].Controls["testSourceTextBox"];
            return (TextBox)control;
        }

        private TextBox GetTargetTextBox()
        {
            Control control;
            control = this.testTabControl.TabPages["targetTabPage"].Controls["testTargetTextBox"];
            return (TextBox)control;
        }


        private void LoadTest()
        {
            Control control;

            this.testNameTextBox.Text = this.WorkingTest.Name;

            // Set focus to first tab page
            this.testTabControl.SelectedIndex = SourceTabPageIndex;

            // SQL
            GetSourceTextBox().Text = this.WorkingTest.SourceSql;
            GetTargetTextBox().Text = this.WorkingTest.TargetSql;
        }


        private void SaveTest()
        {
            Control control;

            this.WorkingTest.Name = this.testNameTextBox.Text?.Trim();

            // SQL
            this.WorkingTest.SourceSql = GetSourceTextBox().Text?.Trim();
            this.WorkingTest.TargetSql = GetTargetTextBox().Text?.Trim();

        }


        private void ValidateTest()
        {
            Control control;

            if (String.IsNullOrWhiteSpace(this.testNameTextBox.Text))
                throw new Exception("Missing Test Name");

            // Sql
            if (String.IsNullOrWhiteSpace(GetSourceTextBox().Text))
                throw new Exception("Missing Source SQL");

            if (String.IsNullOrWhiteSpace(GetTargetTextBox().Text))
                throw new Exception("Missing Target SQL");
        }
        #endregion


        private void testsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addUpdateButton.Enabled = true;
            this.deleteButton.Enabled = false;

            ComboBox comboBox = (ComboBox)sender ??
                throw new ArgumentNullException(nameof(sender));

            int selectedIndex = comboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case NOT_SELECTED_INDEX:
                    this.addUpdateButton.Enabled = false;
                    SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
                    break;

                case NEW_SELECTED_INDEX:
                    SetAddUpdateButton(AddUpdateActionEnum.Add, this.addUpdateButton);
                    CreateWorkingTest();
                    LoadTest();
                    break;

                default:
                    this.deleteButton.Enabled = true;
                    SetAddUpdateButton(AddUpdateActionEnum.Update, this.addUpdateButton);
                    object item = comboBox.Items[selectedIndex];
                    this.WorkingTest = (TableViewTest)item;
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

                LoadComboBox<TableViewTest>(this.testsComboBox, this.Tests);
                FindInComboBox(testsComboBox, this.WorkingTest.Name);
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
