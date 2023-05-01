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
            notificationsTabPage = new TabPage();
            tdSaveButton = new Button();
            buttonTableLayoutPanel = new TableLayoutPanel();
            tdLoadButton = new Button();
            saveButtonContextMenuStrip = new ContextMenuStrip(components);
            saveToFileToolStripMenuItem = new ToolStripMenuItem();
            saveForComparisonToolStripMenuItem = new ToolStripMenuItem();
            everyCompareCheckBox = new CheckBox();
            failureCheckBox = new CheckBox();
            emailLabel = new Label();
            emailTextBox = new TextBox();
            emailDomainLable = new Label();
            tdTabControl.SuspendLayout();
            compareOptionsTabPage.SuspendLayout();
            notificationsTabPage.SuspendLayout();
            buttonTableLayoutPanel.SuspendLayout();
            saveButtonContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(compareOptionsTabPage);
            tdTabControl.Controls.Add(notificationsTabPage);
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
            // notificationsTabPage
            // 
            notificationsTabPage.Controls.Add(emailDomainLable);
            notificationsTabPage.Controls.Add(emailTextBox);
            notificationsTabPage.Controls.Add(emailLabel);
            notificationsTabPage.Controls.Add(failureCheckBox);
            notificationsTabPage.Controls.Add(everyCompareCheckBox);
            notificationsTabPage.Location = new Point(4, 24);
            notificationsTabPage.Name = "notificationsTabPage";
            notificationsTabPage.Size = new Size(546, 387);
            notificationsTabPage.TabIndex = 2;
            notificationsTabPage.Text = "Notifications";
            notificationsTabPage.UseVisualStyleBackColor = true;
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
            // everyCompareCheckBox
            // 
            everyCompareCheckBox.AutoSize = true;
            everyCompareCheckBox.Location = new Point(20, 30);
            everyCompareCheckBox.Name = "everyCompareCheckBox";
            everyCompareCheckBox.Size = new Size(157, 19);
            everyCompareCheckBox.TabIndex = 0;
            everyCompareCheckBox.Text = "Notify on every compare";
            everyCompareCheckBox.UseVisualStyleBackColor = true;
            // 
            // failureCheckBox
            // 
            failureCheckBox.AutoSize = true;
            failureCheckBox.Location = new Point(20, 70);
            failureCheckBox.Name = "failureCheckBox";
            failureCheckBox.Size = new Size(112, 19);
            failureCheckBox.TabIndex = 1;
            failureCheckBox.Text = "Notify on failure";
            failureCheckBox.UseVisualStyleBackColor = true;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(20, 110);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(81, 15);
            emailLabel.TabIndex = 2;
            emailLabel.Text = "Email Address";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(20, 130);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(334, 23);
            emailTextBox.TabIndex = 3;
            // 
            // emailDomainLable
            // 
            emailDomainLable.AutoSize = true;
            emailDomainLable.Location = new Point(360, 133);
            emailDomainLable.Name = "emailDomainLable";
            emailDomainLable.Size = new Size(59, 15);
            emailDomainLable.TabIndex = 4;
            emailDomainLable.Text = "@tql.com";
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
            notificationsTabPage.ResumeLayout(false);
            notificationsTabPage.PerformLayout();
            buttonTableLayoutPanel.ResumeLayout(false);
            saveButtonContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage overallResultsTabPage;
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
        private TabPage notificationsTabPage;
        private TextBox emailTextBox;
        private Label emailLabel;
        private CheckBox failureCheckBox;
        private CheckBox everyCompareCheckBox;
        private Label emailDomainLable;
    }
}
