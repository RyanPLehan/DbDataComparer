using System;
using System.IO;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public partial class Main : Form
    {
        private const string FILE_EXTENSION = ".td";
        private readonly ConfigurationSettings Settings;


        public Main(ConfigurationSettings settings)
        {
            this.Settings = settings ??
                throw new ArgumentNullException(nameof(settings));

            InitializeComponent();
        }



        private void HideGroupBoxes()
        {
            this.createGroupBox.Visible = false;
            //this.ModifyGroupBox.Visible = false;
            //this.TestGroupBox.Visible = false;

        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Setup Event Handler for Test Definition operations

            HideGroupBoxes();
        }

        private void testDefinitionCreate_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            this.createGroupBox.Visible = true;
            

            /*
            var dialog = new DataConnectionDialog();
            dialog.ConnectionString = "Data Source = DEVACCTING; Initial Catalog = AcctWF; Integrated Security = SSPI;Application Name=DbDataComparer;TrustServerCertificate=true;";
            var result = DataConnectionDialog.Show(dialog, this);
            MessageBox.Show(result.ToString());
            */
        }

        // Event Handler Routines for Test Definition operations
        private void TestDefinitionCreated(object sender, TestDefinitionCreatedEventArgs e)
        {
            string pathName = CreatePathName(e.TestDefinition.Name);
            string fileName = Path.GetFileName(pathName);

            if (File.Exists(pathName) &&
                RTLAwareMessageBox.ShowYesNo(fileName, "File already exists.  Overwrite", MessageBoxIcon.Question) == DialogResult.No)
            {
                RTLAwareMessageBox.ShowMessage("Test Definition", "Aborting");
            }
            else
            {
                CreateFile(pathName, e.TestDefinition).GetAwaiter().GetResult();
            }
        }


        /// <summary>
        /// Clean, standardize file name and prepend directory path
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string CreatePathName(string fileName)
        {
            string cleansed = fileName.Replace(@"\", "_").Replace(@"/", "_").Replace(":", "_");
            if (!fileName.EndsWith(FILE_EXTENSION, StringComparison.OrdinalIgnoreCase))
                cleansed = cleansed + FILE_EXTENSION;

            return Path.Combine(this.Settings.Location.TestDefinitionsPath, cleansed);
        }


        /// <summary>
        /// Create Test Definition File stored in the path defined in the Location Settings
        /// </summary>
        /// <returns></returns>
        private async Task CreateFile(string pathName, TestDefinition testDefinition)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    try
                    {
                        await sw.WriteLineAsync(NSJson.Serialize(testDefinition));
                        RTLAwareMessageBox.ShowMessage("Test Definition File", "Successful");
                    }
                    catch (Exception ex)
                    {
                        RTLAwareMessageBox.ShowError("Saving Test Definition File", ex);
                    }
                }
            }
        }

    }
}