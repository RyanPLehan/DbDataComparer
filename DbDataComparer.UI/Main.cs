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
        private TestDefinitionControl ActiveTestDefinitionControl { get; set; } = null;


        public Main(ConfigurationSettings settings)
        {
            this.Settings = settings ??
                throw new ArgumentNullException(nameof(settings));

            InitializeComponent();
            SetTestDefinition(null, null);
        }


        #region General routines

        private void SetTestDefinitionEventHandlers(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // A little bit of recursion here
                if (control.HasChildren)
                    this.SetTestDefinitionEventHandlers(control.Controls);

                if (control is TestDefinitionControl)
                {
                    TestDefinitionControl c = (TestDefinitionControl)control;
                    c.ConfigurationQueryRequested += this.ConfigurationQueryRequested;
                    c.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
                    c.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
                    c.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
                    c.TestDefinitionSetRequested += this.TestDefinitionSetRequested;
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

            SetButtonConfiguration();
        }

        private void SetButtonConfiguration()
        {
            bool enable = (this.TestDefinition != null);
            this.testDefinitionModifyButton.Enabled = enable;
            this.testDefinitionCompareButton.Enabled = enable;
        }

        private void SetStatus(string status)
        {
            this.mainStatusTDStatusLabel.Text = status;
        }


        private void ActivateUserControl(Control control)
        {
            if (control is TestDefinitionControl)
            {
                ((TestDefinitionControl)control).Activate();
                this.ActiveTestDefinitionControl = (TestDefinitionControl)control;
            }
        }

        private bool IsTestDefinitionLoaded(bool showMessage = true)
        {
            bool isLoaded = (this.TestDefinition != null);

            if (!isLoaded & showMessage)
                RTLAwareMessageBox.ShowMessage("Test Definition", "No Test Definition Loaded");

            return isLoaded;
        }
        #endregion


        #region Event Handler Routines for Test Definition operations
        private void ConfigurationQueryRequested(object sender, ConfigurationQueryRequestedEventArgs e)
        {
            e.ConfigurationSettings = this.Settings;
        }


        private void TestDefinitionLoadRequested(object sender, TestDefinitionLoadRequestedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = ApplicationIO.GetTestDefinitionPath(this.Settings.Location);
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
                        SetStatus("Loaded");
                    }
                }
                catch (Exception ex)
                {
                    e.SuccessfullyLoaded = false;
                    RTLAwareMessageBox.ShowError("Loading Test Definition", ex);
                    SetStatus("Load Failed");
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
            string path = ApplicationIO.GetTestDefinitionPath(this.Settings.Location);
            string pathName = TestDefinitionIO.CreatePathName(path, e.TestDefinition);
            string fileName = TestDefinitionIO.CreateFileName(e.TestDefinition);

            if (!e.ForceOverwrite &&
                File.Exists(pathName) &&
                RTLAwareMessageBox.ShowYesNo(fileName, "File already exists.  Overwrite", MessageBoxIcon.Question) == DialogResult.No)
            {
                e.SuccessfullySaved = false;
                SetStatus("File creation aborted!");
            }
            else
            {
                try
                {
                    TestDefinitionIO.CreateFile(pathName, e.TestDefinition);
                    e.SuccessfullySaved = true;
                    SetTestDefinition(e.TestDefinition, pathName);
                    SetStatus("File created");
                }
                catch (Exception ex)
                {
                    e.SuccessfullySaved = false;
                    RTLAwareMessageBox.ShowError("Test Definition File", ex);
                }
            }
        }


        private void TestDefinitionSetRequested(object sender, TestDefinitionSetRequestedEventArgs e)
        {
            this.PathName = null;
            this.TestDefinition = e.TestDefinition;
        }


        private void TestDefinitionStatusUpdated(object sender, TestDefinitionStatusUpdatedEventArgs e)
        {
            SetStatus(e.Status);
        }
        #endregion


        #region Form Control Events
        private void Main_Load(object sender, EventArgs e)
        {
            // Setup Event Handler for Test Definition operations
            this.SetTestDefinitionEventHandlers(this.Controls);

            /*
            this.testDefinitionCreateControl.ConfigurationQueryRequested += this.ConfigurationQueryRequested;
            this.testDefinitionCreateControl.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
            this.testDefinitionCreateControl.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
            this.testDefinitionCreateControl.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
            this.testDefinitionCreateControl.TestDefinitionSetRequested += this.TestDefinitionSetRequested;
            this.testDefinitionCreateControl.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;

            this.testDefinitionModifyControl.ConfigurationQueryRequested += this.ConfigurationQueryRequested;
            this.testDefinitionModifyControl.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
            this.testDefinitionModifyControl.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
            this.testDefinitionModifyControl.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
            this.testDefinitionModifyControl.TestDefinitionSetRequested += this.TestDefinitionSetRequested;
            this.testDefinitionModifyControl.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;

            this.testDefinitionCompareControl.ConfigurationQueryRequested += this.ConfigurationQueryRequested;
            this.testDefinitionCompareControl.TestDefinitionLoadRequested += this.TestDefinitionLoadRequested;
            this.testDefinitionCompareControl.TestDefinitionQueryRequested += this.TestDefinitionQueryRequested;
            this.testDefinitionCompareControl.TestDefinitionSaveRequested += this.TestDefinitionSaveRequested;
            this.testDefinitionCompareControl.TestDefinitionSetRequested += this.TestDefinitionSetRequested;
            this.testDefinitionCompareControl.TestDefinitionStatusUpdated += this.TestDefinitionStatusUpdated;
            */


            HideTestDefinitionControls();
            SetTestDefinition(null, null);
        }

        private void testDefinitionCreateButton_Click(object sender, EventArgs e)
        {
            HideTestDefinitionControls();
            this.createPanel.Visible = true;
        }


        private void testDefinitionCompareButton_Click(object sender, EventArgs e)
        {
            if (this.IsTestDefinitionLoaded())
            {
                HideTestDefinitionControls();
                this.comparePanel.Visible = true;
            }
        }

        private void testDefinitionModifyButton_Click(object sender, EventArgs e)
        {
            if (this.IsTestDefinitionLoaded())
            {
                HideTestDefinitionControls();
                this.modifyPanel.Visible = true;
            }
        }


        private void testDefinitionLoadButton_Click(object sender, EventArgs e)
        {
            var loadEventArgs = new TestDefinitionLoadRequestedEventArgs();
            TestDefinitionLoadRequested(sender, loadEventArgs);

            if (loadEventArgs.SuccessfullyLoaded &&
                this.ActiveTestDefinitionControl != null)
                ActivateUserControl(this.ActiveTestDefinitionControl);

        }
        #endregion

        private void Panel_VisibleChanged(object sender, EventArgs e)
        {
            if (sender is Panel)
            {
                Panel panel = (Panel)sender;
                if (panel.Visible)
                {
                    foreach (Control control in panel.Controls)
                        ActivateUserControl(control);
                }
            }
        }
    }
}