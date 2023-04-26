using System;
using System.IO;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class Main : Form
    {
        private readonly ConfigurationSettings Settings;

        private string PathName { get; set; }
        private TestDefinition TestDefinition { get; set; }


        public Main(ConfigurationSettings settings)
        {
            this.Settings = settings ??
                throw new ArgumentNullException(nameof(settings));

            InitializeComponent();
        }


        #region General routines

        private void SetTestDefinitionEventHandlers(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // A little bit of recursion here
                if (control.HasChildren)
                    this.SetTestDefinitionEventHandlers(control.Controls);

                if (control is TestDefinitionUserControl)
                {
                    TestDefinitionUserControl c = (TestDefinitionUserControl)control;
                    c.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
                    c.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
                    c.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
                    c.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;
                }
            }
        }


        private void HideTestDefinitionControls()
        {
            this.createPanel.Visible = false;
            this.modifyPanel.Visible = false;
            this.comparePanel.Visible = false;

        }

        private void SetTestDefinition(TestDefinition td, string pathName)
        {
            this.TestDefinition = td;
            this.PathName = pathName;
            this.Text = "Database Data Comparer";
            if (!String.IsNullOrWhiteSpace(td?.Name))
                this.Text += "  -  " + td.Name;
        }
        #endregion


        #region Event Handler Routines for Test Definition operations
        private void TestDefinitionLoadRequested(object sender, TestDefinitionLoadRequestedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Path.Combine(IOUtility.ExecutablePath(), this.Settings.Location.TestDefinitionsPath);
                dialog.Filter = "td files (*.td)|*.td|All files (*.*)|*.*";
                dialog.FilterIndex = 0;                         // default to first filter
                dialog.RestoreDirectory = true;                 // Restore directory to original setting

                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var pathName = dialog.FileName;
                        var testDefinition = TestDefinitionIO.LoadFile(pathName);
                        e.SuccessfullyLoaded = true;
                        SetTestDefinition(testDefinition, pathName);
                    }
                }
                catch (Exception ex)
                {
                    e.SuccessfullyLoaded = false;
                    RTLAwareMessageBox.ShowError("Loading Test Definition", ex);
                }
            }
        }


        private void TestDefinitionQueryRequested(object sender, TestDefinitionQueryRequestedEventArgs e)
        {
            e.PathName = this.PathName;
            e.TestDefinition = this.TestDefinition;
        }


        private void TestDefinitionSaveRequested(object sender, TestDefinitionSaveRequestedEventArgs e)
        {
            string rootPath = Path.Combine(IOUtility.ExecutablePath(), this.Settings.Location.TestDefinitionsPath);
            string pathName = IOUtility.CreatePathName(rootPath, e.TestDefinition);
            string fileName = Path.GetFileName(pathName);

            if (!e.ForceOverwrite &&
                File.Exists(pathName) &&
                RTLAwareMessageBox.ShowYesNo(fileName, "File already exists.  Overwrite", MessageBoxIcon.Question) == DialogResult.No)
            {
                e.SuccessfullySaved = false;
                RTLAwareMessageBox.ShowMessage("Test Definition", "Aborting");
            }
            else
            {
                try
                {
                    TestDefinitionIO.CreateFile(pathName, e.TestDefinition);
                    e.SuccessfullySaved = true;
                    SetTestDefinition(e.TestDefinition, pathName);
                }
                catch (Exception ex)
                {
                    e.SuccessfullySaved = false;
                    RTLAwareMessageBox.ShowError("Creating Test Definition", ex);
                }
            }
        }


        private void TestDefinitionStatusUpdated(object sender, TestDefinitionStatusUpdatedEventArgs e)
        {
            this.mainStatusTDStatusLabel.Text = e.Status;
        }
        #endregion


        #region Form Control Events
        private void Main_Load(object sender, EventArgs e)
        {
            // Setup Event Handler for Test Definition operations
            this.SetTestDefinitionEventHandlers(this.Controls);

            /*
            this.testDefinitionCreateControl.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
            this.testDefinitionCreateControl.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
            this.testDefinitionCreateControl.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
            this.testDefinitionCreateControl.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;

            this.testDefinitionCompareControl.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
            this.testDefinitionCompareControl.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
            this.testDefinitionCompareControl.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
            this.testDefinitionCompareControl.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;
            */


            HideTestDefinitionControls();
            SetTestDefinition(null, null);
        }

        private void testDefinitionCreate_Click(object sender, EventArgs e)
        {
            HideTestDefinitionControls();
            this.createPanel.Visible = true;
        }

        #endregion


        private void testDefinitionCompare_Click(object sender, EventArgs e)
        {
            HideTestDefinitionControls();
            this.comparePanel.Visible = true;
        }

        private void TestDefinitionModify_Click(object sender, EventArgs e)
        {
            HideTestDefinitionControls();
            this.modifyPanel.Visible = true;
        }
    }
}