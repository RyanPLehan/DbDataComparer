namespace DbDataComparer.UI
{
    partial class TestDefinitionModifyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tdTabControl = new TabControl();
            compareOptionsTabPage = new TabPage();
            resultSetDataCheckBox = new CheckBox();
            resultSetMetaDataCheckBox = new CheckBox();
            parameterReturnCheckBox = new CheckBox();
            parameterOutputCheckBox = new CheckBox();
            testsTabPage = new TabPage();
            tdSaveButton = new Button();
            buttonTableLayoutPanel = new TableLayoutPanel();
            tdLoadButton = new Button();
            saveButtonContextMenuStrip = new ContextMenuStrip(components);
            saveToFileToolStripMenuItem = new ToolStripMenuItem();
            saveForComparisonToolStripMenuItem = new ToolStripMenuItem();
            tdTabControl.SuspendLayout();
            compareOptionsTabPage.SuspendLayout();
            buttonTableLayoutPanel.SuspendLayout();
            saveButtonContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(compareOptionsTabPage);
            tdTabControl.Controls.Add(testsTabPage);
            tdTabControl.Location = new Point(10, 10);
            tdTabControl.Name = "tdTabControl";
            tdTabControl.SelectedIndex = 0;
            tdTabControl.Size = new Size(554, 415);
            tdTabControl.TabIndex = 2;
            // 
            // compareOptionsTabPage
            // 
            compareOptionsTabPage.Controls.Add(resultSetDataCheckBox);
            compareOptionsTabPage.Controls.Add(resultSetMetaDataCheckBox);
            compareOptionsTabPage.Controls.Add(parameterReturnCheckBox);
            compareOptionsTabPage.Controls.Add(parameterOutputCheckBox);
            compareOptionsTabPage.Location = new Point(4, 24);
            compareOptionsTabPage.Name = "compareOptionsTabPage";
            compareOptionsTabPage.Padding = new Padding(3);
            compareOptionsTabPage.Size = new Size(546, 387);
            compareOptionsTabPage.TabIndex = 0;
            compareOptionsTabPage.Text = "Compare Options";
            compareOptionsTabPage.UseVisualStyleBackColor = true;
            // 
            // resultSetDataCheckBox
            // 
            resultSetDataCheckBox.AutoSize = true;
            resultSetDataCheckBox.Location = new Point(20, 150);
            resultSetDataCheckBox.Name = "resultSetDataCheckBox";
            resultSetDataCheckBox.Size = new Size(104, 19);
            resultSetDataCheckBox.TabIndex = 3;
            resultSetDataCheckBox.Text = "Result Set Data";
            resultSetDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // resultSetMetaDataCheckBox
            // 
            resultSetMetaDataCheckBox.AutoSize = true;
            resultSetMetaDataCheckBox.Location = new Point(20, 110);
            resultSetMetaDataCheckBox.Name = "resultSetMetaDataCheckBox";
            resultSetMetaDataCheckBox.Size = new Size(134, 19);
            resultSetMetaDataCheckBox.TabIndex = 2;
            resultSetMetaDataCheckBox.Text = "Result Set Meta Data";
            resultSetMetaDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // parameterReturnCheckBox
            // 
            parameterReturnCheckBox.AutoSize = true;
            parameterReturnCheckBox.Location = new Point(20, 70);
            parameterReturnCheckBox.Name = "parameterReturnCheckBox";
            parameterReturnCheckBox.Size = new Size(186, 19);
            parameterReturnCheckBox.TabIndex = 1;
            parameterReturnCheckBox.Text = "Stored Procedure Return Value";
            parameterReturnCheckBox.UseVisualStyleBackColor = true;
            // 
            // parameterOutputCheckBox
            // 
            parameterOutputCheckBox.AutoSize = true;
            parameterOutputCheckBox.Location = new Point(20, 30);
            parameterOutputCheckBox.Name = "parameterOutputCheckBox";
            parameterOutputCheckBox.Size = new Size(228, 19);
            parameterOutputCheckBox.TabIndex = 0;
            parameterOutputCheckBox.Text = "Stored Procedure Output Parameter(s)";
            parameterOutputCheckBox.UseVisualStyleBackColor = true;
            // 
            // testsTabPage
            // 
            testsTabPage.Location = new Point(4, 24);
            testsTabPage.Name = "testsTabPage";
            testsTabPage.Padding = new Padding(3);
            testsTabPage.Size = new Size(546, 387);
            testsTabPage.TabIndex = 1;
            testsTabPage.Text = "Tests";
            testsTabPage.UseVisualStyleBackColor = true;
            // 
            // tdSaveButton
            // 
            tdSaveButton.Anchor = AnchorStyles.None;
            tdSaveButton.Location = new Point(104, 3);
            tdSaveButton.Name = "tdSaveButton";
            tdSaveButton.Size = new Size(80, 25);
            tdSaveButton.TabIndex = 2;
            tdSaveButton.Text = "Save";
            tdSaveButton.UseVisualStyleBackColor = true;
            tdSaveButton.Click += tdSaveButton_Click;
            // 
            // buttonTableLayoutPanel
            // 
            buttonTableLayoutPanel.ColumnCount = 2;
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.Controls.Add(tdLoadButton, 0, 0);
            buttonTableLayoutPanel.Controls.Add(tdSaveButton, 1, 0);
            buttonTableLayoutPanel.Location = new Point(380, 441);
            buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            buttonTableLayoutPanel.RowCount = 1;
            buttonTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonTableLayoutPanel.Size = new Size(192, 31);
            buttonTableLayoutPanel.TabIndex = 3;
            // 
            // tdLoadButton
            // 
            tdLoadButton.Anchor = AnchorStyles.None;
            tdLoadButton.Location = new Point(8, 3);
            tdLoadButton.Name = "tdLoadButton";
            tdLoadButton.Size = new Size(80, 25);
            tdLoadButton.TabIndex = 2;
            tdLoadButton.Text = "Load";
            tdLoadButton.UseVisualStyleBackColor = true;
            tdLoadButton.Click += tdLoadButton_Click;
            // 
            // saveButtonContextMenuStrip
            // 
            saveButtonContextMenuStrip.Items.AddRange(new ToolStripItem[] { saveToFileToolStripMenuItem, saveForComparisonToolStripMenuItem });
            saveButtonContextMenuStrip.Name = "saveButtonContextMenuStrip";
            saveButtonContextMenuStrip.Size = new Size(183, 48);
            // 
            // saveToFileToolStripMenuItem
            // 
            saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            saveToFileToolStripMenuItem.Size = new Size(182, 22);
            saveToFileToolStripMenuItem.Text = "Save to file...";
            // 
            // saveForComparisonToolStripMenuItem
            // 
            saveForComparisonToolStripMenuItem.Name = "saveForComparisonToolStripMenuItem";
            saveForComparisonToolStripMenuItem.Size = new Size(182, 22);
            saveForComparisonToolStripMenuItem.Text = "Save for comparison";
            // 
            // TestDefinitionModifyControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(buttonTableLayoutPanel);
            Controls.Add(tdTabControl);
            Name = "TestDefinitionModifyControl";
            Size = new Size(575, 475);
            Load += TestDefinitionModifyControl_Load;
            tdTabControl.ResumeLayout(false);
            compareOptionsTabPage.ResumeLayout(false);
            compareOptionsTabPage.PerformLayout();
            buttonTableLayoutPanel.ResumeLayout(false);
            saveButtonContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage overallResultsTabPage;
        private TabPage testsTabPage;
        private Button tdSaveButton;
        private TableLayoutPanel buttonTableLayoutPanel;
        private Button tdLoadButton;
        private TabPage compareOptionsTabPage;
        private TextBox overallResultsTextBox;
        private TextBox errorsTextBox;
        private CheckBox resultSetDataCheckBox;
        private CheckBox resultSetMetaDataCheckBox;
        private CheckBox parameterReturnCheckBox;
        private CheckBox parameterOutputCheckBox;
        private ContextMenuStrip saveButtonContextMenuStrip;
        private ToolStripMenuItem saveToFileToolStripMenuItem;
        private ToolStripMenuItem saveForComparisonToolStripMenuItem;
    }
}
