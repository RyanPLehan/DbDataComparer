namespace DbDataComparer.UI
{
    partial class ParamValueEditorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamValueEditorDialog));
            testValuesDataGrid = new DataGridView();
            buttonsTableLayoutPanel = new TableLayoutPanel();
            acceptButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)testValuesDataGrid).BeginInit();
            buttonsTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // testValuesDataGrid
            // 
            testValuesDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(testValuesDataGrid, "testValuesDataGrid");
            testValuesDataGrid.Name = "testValuesDataGrid";
            // 
            // buttonsTableLayoutPanel
            // 
            resources.ApplyResources(buttonsTableLayoutPanel, "buttonsTableLayoutPanel");
            buttonsTableLayoutPanel.Controls.Add(acceptButton, 0, 0);
            buttonsTableLayoutPanel.Controls.Add(cancelButton, 1, 0);
            buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.Name = "acceptButton";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // ParamValueEditorDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(testValuesDataGrid);
            Controls.Add(buttonsTableLayoutPanel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ParamValueEditorDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)testValuesDataGrid).EndInit();
            buttonsTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel buttonsTableLayoutPanel;
        private Button acceptButton;
        private Button cancelButton;
        private DataGridView testValuesDataGrid;
    }
}