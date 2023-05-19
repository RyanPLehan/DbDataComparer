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
            tdTabControl = new TabControl();
            compareOptionsTabPage = new TabPage();
            resultSetDataCheckBox = new CheckBox();
            resultSetMetaDataCheckBox = new CheckBox();
            parameterReturnCheckBox = new CheckBox();
            parameterOutputCheckBox = new CheckBox();
            notificationsTabPage = new TabPage();
            emailDomainLabel = new Label();
            emailTextBox = new TextBox();
            emailLabel = new Label();
            failureCheckBox = new CheckBox();
            everyCompareCheckBox = new CheckBox();
            databaseTabPage = new TabPage();
            tgtGroupBox = new GroupBox();
            tgtBuildButton = new Button();
            tgtConnectionStringTextBox = new TextBox();
            tgtConnectionStringLabel = new Label();
            tgtExecutionTimeOutTextBox = new TextBox();
            tgtExecutionTimeOutLabel = new Label();
            srcGroupBox = new GroupBox();
            srcBuildButton = new Button();
            srcConnectionStringTextBox = new TextBox();
            srcConnectionStringLabel = new Label();
            srcExecutionTimeOutTextBox = new TextBox();
            srcExecutionTimeOutLabel = new Label();
            testsTabPage = new TabPage();
            tableViewTestsControl = new TableViewTestsControl();
            storedProcedureTestsControl = new StoredProcedureTestsControl();
            saveButton = new Button();
            buttonTableLayoutPanel = new TableLayoutPanel();
            cancelButton = new Button();
            srcDatabaseObjectLabel = new Label();
            srcDatabaseObjectTextBox = new TextBox();
            tgtDatabaseObjectTextBox = new TextBox();
            tgtDatabaseObjectLabel = new Label();
            tdTabControl.SuspendLayout();
            compareOptionsTabPage.SuspendLayout();
            notificationsTabPage.SuspendLayout();
            databaseTabPage.SuspendLayout();
            tgtGroupBox.SuspendLayout();
            srcGroupBox.SuspendLayout();
            testsTabPage.SuspendLayout();
            buttonTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(compareOptionsTabPage);
            tdTabControl.Controls.Add(notificationsTabPage);
            tdTabControl.Controls.Add(databaseTabPage);
            tdTabControl.Controls.Add(testsTabPage);
            tdTabControl.Location = new Point(10, 10);
            tdTabControl.Name = "tdTabControl";
            tdTabControl.SelectedIndex = 0;
            tdTabControl.Size = new Size(728, 428);
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
            compareOptionsTabPage.Size = new Size(720, 400);
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
            notificationsTabPage.Controls.Add(emailDomainLabel);
            notificationsTabPage.Controls.Add(emailTextBox);
            notificationsTabPage.Controls.Add(emailLabel);
            notificationsTabPage.Controls.Add(failureCheckBox);
            notificationsTabPage.Controls.Add(everyCompareCheckBox);
            notificationsTabPage.Location = new Point(4, 24);
            notificationsTabPage.Name = "notificationsTabPage";
            notificationsTabPage.Size = new Size(720, 400);
            notificationsTabPage.TabIndex = 2;
            notificationsTabPage.Text = "Notifications";
            notificationsTabPage.UseVisualStyleBackColor = true;
            // 
            // emailDomainLabel
            // 
            emailDomainLabel.AutoSize = true;
            emailDomainLabel.Location = new Point(360, 133);
            emailDomainLabel.Name = "emailDomainLabel";
            emailDomainLabel.Size = new Size(59, 15);
            emailDomainLabel.TabIndex = 4;
            emailDomainLabel.Text = "@tql.com";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(20, 130);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(334, 23);
            emailTextBox.TabIndex = 3;
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
            // databaseTabPage
            // 
            databaseTabPage.Controls.Add(tgtGroupBox);
            databaseTabPage.Controls.Add(srcGroupBox);
            databaseTabPage.Location = new Point(4, 24);
            databaseTabPage.Name = "databaseTabPage";
            databaseTabPage.Size = new Size(720, 400);
            databaseTabPage.TabIndex = 4;
            databaseTabPage.Text = "Database";
            databaseTabPage.UseVisualStyleBackColor = true;
            // 
            // tgtGroupBox
            // 
            tgtGroupBox.Controls.Add(tgtDatabaseObjectTextBox);
            tgtGroupBox.Controls.Add(tgtDatabaseObjectLabel);
            tgtGroupBox.Controls.Add(tgtBuildButton);
            tgtGroupBox.Controls.Add(tgtConnectionStringTextBox);
            tgtGroupBox.Controls.Add(tgtConnectionStringLabel);
            tgtGroupBox.Controls.Add(tgtExecutionTimeOutTextBox);
            tgtGroupBox.Controls.Add(tgtExecutionTimeOutLabel);
            tgtGroupBox.Location = new Point(14, 207);
            tgtGroupBox.Name = "tgtGroupBox";
            tgtGroupBox.Size = new Size(693, 185);
            tgtGroupBox.TabIndex = 1;
            tgtGroupBox.TabStop = false;
            tgtGroupBox.Text = "Target";
            // 
            // tgtBuildButton
            // 
            tgtBuildButton.Location = new Point(612, 143);
            tgtBuildButton.Name = "tgtBuildButton";
            tgtBuildButton.Size = new Size(77, 24);
            tgtBuildButton.TabIndex = 6;
            tgtBuildButton.Text = "Build";
            tgtBuildButton.UseVisualStyleBackColor = true;
            tgtBuildButton.Click += tgtBuildButton_Click;
            // 
            // tgtConnectionStringTextBox
            // 
            tgtConnectionStringTextBox.Location = new Point(20, 144);
            tgtConnectionStringTextBox.Name = "tgtConnectionStringTextBox";
            tgtConnectionStringTextBox.Size = new Size(580, 23);
            tgtConnectionStringTextBox.TabIndex = 5;
            // 
            // tgtConnectionStringLabel
            // 
            tgtConnectionStringLabel.AutoSize = true;
            tgtConnectionStringLabel.Location = new Point(18, 127);
            tgtConnectionStringLabel.Name = "tgtConnectionStringLabel";
            tgtConnectionStringLabel.Size = new Size(103, 15);
            tgtConnectionStringLabel.TabIndex = 4;
            tgtConnectionStringLabel.Text = "Connection String";
            // 
            // tgtExecutionTimeOutTextBox
            // 
            tgtExecutionTimeOutTextBox.Location = new Point(18, 91);
            tgtExecutionTimeOutTextBox.Name = "tgtExecutionTimeOutTextBox";
            tgtExecutionTimeOutTextBox.Size = new Size(49, 23);
            tgtExecutionTimeOutTextBox.TabIndex = 3;
            // 
            // tgtExecutionTimeOutLabel
            // 
            tgtExecutionTimeOutLabel.AutoSize = true;
            tgtExecutionTimeOutLabel.Location = new Point(18, 73);
            tgtExecutionTimeOutLabel.Name = "tgtExecutionTimeOutLabel";
            tgtExecutionTimeOutLabel.Size = new Size(163, 15);
            tgtExecutionTimeOutLabel.TabIndex = 2;
            tgtExecutionTimeOutLabel.Text = "Execution timeout in seconds";
            // 
            // srcGroupBox
            // 
            srcGroupBox.Controls.Add(srcDatabaseObjectTextBox);
            srcGroupBox.Controls.Add(srcDatabaseObjectLabel);
            srcGroupBox.Controls.Add(srcBuildButton);
            srcGroupBox.Controls.Add(srcConnectionStringTextBox);
            srcGroupBox.Controls.Add(srcConnectionStringLabel);
            srcGroupBox.Controls.Add(srcExecutionTimeOutTextBox);
            srcGroupBox.Controls.Add(srcExecutionTimeOutLabel);
            srcGroupBox.Location = new Point(14, 16);
            srcGroupBox.Name = "srcGroupBox";
            srcGroupBox.Size = new Size(693, 185);
            srcGroupBox.TabIndex = 0;
            srcGroupBox.TabStop = false;
            srcGroupBox.Text = "Source";
            // 
            // srcBuildButton
            // 
            srcBuildButton.Location = new Point(610, 141);
            srcBuildButton.Name = "srcBuildButton";
            srcBuildButton.Size = new Size(77, 24);
            srcBuildButton.TabIndex = 6;
            srcBuildButton.Text = "Build";
            srcBuildButton.UseVisualStyleBackColor = true;
            srcBuildButton.Click += srcBuildButton_Click;
            // 
            // srcConnectionStringTextBox
            // 
            srcConnectionStringTextBox.Location = new Point(18, 142);
            srcConnectionStringTextBox.Name = "srcConnectionStringTextBox";
            srcConnectionStringTextBox.Size = new Size(580, 23);
            srcConnectionStringTextBox.TabIndex = 5;
            // 
            // srcConnectionStringLabel
            // 
            srcConnectionStringLabel.AutoSize = true;
            srcConnectionStringLabel.Location = new Point(16, 125);
            srcConnectionStringLabel.Name = "srcConnectionStringLabel";
            srcConnectionStringLabel.Size = new Size(103, 15);
            srcConnectionStringLabel.TabIndex = 4;
            srcConnectionStringLabel.Text = "Connection String";
            // 
            // srcExecutionTimeOutTextBox
            // 
            srcExecutionTimeOutTextBox.Location = new Point(16, 90);
            srcExecutionTimeOutTextBox.Name = "srcExecutionTimeOutTextBox";
            srcExecutionTimeOutTextBox.Size = new Size(49, 23);
            srcExecutionTimeOutTextBox.TabIndex = 3;
            // 
            // srcExecutionTimeOutLabel
            // 
            srcExecutionTimeOutLabel.AutoSize = true;
            srcExecutionTimeOutLabel.Location = new Point(16, 72);
            srcExecutionTimeOutLabel.Name = "srcExecutionTimeOutLabel";
            srcExecutionTimeOutLabel.Size = new Size(163, 15);
            srcExecutionTimeOutLabel.TabIndex = 2;
            srcExecutionTimeOutLabel.Text = "Execution timeout in seconds";
            // 
            // testsTabPage
            // 
            testsTabPage.Controls.Add(tableViewTestsControl);
            testsTabPage.Controls.Add(storedProcedureTestsControl);
            testsTabPage.Location = new Point(4, 24);
            testsTabPage.Name = "testsTabPage";
            testsTabPage.Size = new Size(720, 400);
            testsTabPage.TabIndex = 3;
            testsTabPage.Text = "Tests";
            testsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableViewTestsControl
            // 
            tableViewTestsControl.Location = new Point(3, 3);
            tableViewTestsControl.Name = "tableViewTestsControl";
            tableViewTestsControl.Size = new Size(710, 390);
            tableViewTestsControl.TabIndex = 0;
            // 
            // storedProcedureTestsControl
            // 
            storedProcedureTestsControl.Location = new Point(3, 3);
            storedProcedureTestsControl.Name = "storedProcedureTestsControl";
            storedProcedureTestsControl.Size = new Size(710, 390);
            storedProcedureTestsControl.TabIndex = 0;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.None;
            saveButton.Location = new Point(8, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(80, 25);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // buttonTableLayoutPanel
            // 
            buttonTableLayoutPanel.ColumnCount = 2;
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.Controls.Add(cancelButton, 1, 0);
            buttonTableLayoutPanel.Controls.Add(saveButton, 0, 0);
            buttonTableLayoutPanel.Location = new Point(546, 440);
            buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            buttonTableLayoutPanel.RowCount = 1;
            buttonTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonTableLayoutPanel.Size = new Size(192, 31);
            buttonTableLayoutPanel.TabIndex = 3;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.None;
            cancelButton.Location = new Point(104, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // srcDatabaseObjectLabel
            // 
            srcDatabaseObjectLabel.AutoSize = true;
            srcDatabaseObjectLabel.Location = new Point(18, 19);
            srcDatabaseObjectLabel.Name = "srcDatabaseObjectLabel";
            srcDatabaseObjectLabel.Size = new Size(93, 15);
            srcDatabaseObjectLabel.TabIndex = 0;
            srcDatabaseObjectLabel.Text = "Database Object";
            // 
            // srcDatabaseObjectTextBox
            // 
            srcDatabaseObjectTextBox.Enabled = false;
            srcDatabaseObjectTextBox.Location = new Point(18, 37);
            srcDatabaseObjectTextBox.Name = "srcDatabaseObjectTextBox";
            srcDatabaseObjectTextBox.Size = new Size(420, 23);
            srcDatabaseObjectTextBox.TabIndex = 1;
            // 
            // tgtDatabaseObjectTextBox
            // 
            tgtDatabaseObjectTextBox.Enabled = false;
            tgtDatabaseObjectTextBox.Location = new Point(20, 37);
            tgtDatabaseObjectTextBox.Name = "tgtDatabaseObjectTextBox";
            tgtDatabaseObjectTextBox.Size = new Size(420, 23);
            tgtDatabaseObjectTextBox.TabIndex = 1;
            // 
            // tgtDatabaseObjectLabel
            // 
            tgtDatabaseObjectLabel.AutoSize = true;
            tgtDatabaseObjectLabel.Location = new Point(20, 19);
            tgtDatabaseObjectLabel.Name = "tgtDatabaseObjectLabel";
            tgtDatabaseObjectLabel.Size = new Size(93, 15);
            tgtDatabaseObjectLabel.TabIndex = 0;
            tgtDatabaseObjectLabel.Text = "Database Object";
            // 
            // TestDefinitionModifyControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonTableLayoutPanel);
            Controls.Add(tdTabControl);
            Name = "TestDefinitionModifyControl";
            Size = new Size(742, 477);
            tdTabControl.ResumeLayout(false);
            compareOptionsTabPage.ResumeLayout(false);
            compareOptionsTabPage.PerformLayout();
            notificationsTabPage.ResumeLayout(false);
            notificationsTabPage.PerformLayout();
            databaseTabPage.ResumeLayout(false);
            tgtGroupBox.ResumeLayout(false);
            tgtGroupBox.PerformLayout();
            srcGroupBox.ResumeLayout(false);
            srcGroupBox.PerformLayout();
            testsTabPage.ResumeLayout(false);
            buttonTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage overallResultsTabPage;
        private Button saveButton;
        private TableLayoutPanel buttonTableLayoutPanel;
        private Button cancelButton;
        private TabPage compareOptionsTabPage;
        private TextBox overallResultsTextBox;
        private TextBox errorsTextBox;
        private CheckBox resultSetDataCheckBox;
        private CheckBox resultSetMetaDataCheckBox;
        private CheckBox parameterReturnCheckBox;
        private CheckBox parameterOutputCheckBox;
        private TabPage notificationsTabPage;
        private TextBox emailTextBox;
        private Label emailLabel;
        private CheckBox failureCheckBox;
        private CheckBox everyCompareCheckBox;
        private Label emailDomainLabel;
        private TabPage testsTabPage;
        private TableViewTestsControl tableViewTestsControl;
        private StoredProcedureTestsControl storedProcedureTestsControl;
        private TabPage databaseTabPage;
        private GroupBox tgtGroupBox;
        private GroupBox srcGroupBox;
        private TextBox srcExecutionTimeOutTextBox;
        private Label srcExecutionTimeOutLabel;
        private Button srcBuildButton;
        private TextBox srcConnectionStringTextBox;
        private Label srcConnectionStringLabel;
        private Button tgtBuildButton;
        private TextBox tgtConnectionStringTextBox;
        private Label tgtConnectionStringLabel;
        private TextBox tgtExecutionTimeOutTextBox;
        private Label tgtExecutionTimeOutLabel;
        private TextBox srcDatabaseObjectTextBox;
        private Label srcDatabaseObjectLabel;
        private TextBox tgtDatabaseObjectTextBox;
        private Label tgtDatabaseObjectLabel;
    }
}
