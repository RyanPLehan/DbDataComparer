namespace DbDataComparer.UI
{
    partial class DataConnectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataConnectionDialog));
            separatorPanel = new Panel();
            testConnectionButton = new Button();
            buttonsTableLayoutPanel = new TableLayoutPanel();
            acceptButton = new Button();
            cancelButton = new Button();
            passwordLabel = new Label();
            userNameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            userNameLabel = new Label();
            databaseGroupBox = new GroupBox();
            databaseTableLayoutPanel = new TableLayoutPanel();
            databaseComboBox = new ComboBox();
            databaseRefreshButton = new Button();
            databaseLabel = new Label();
            sqlAuthRadioButton = new RadioButton();
            loginTableLayoutPanel = new TableLayoutPanel();
            loginSqlAuthLabel = new Label();
            logonGroupBox = new GroupBox();
            windowsAuthRadioButton = new RadioButton();
            serverComboBox = new ComboBox();
            serverRefreshButton = new Button();
            serverTableLayoutPanel = new TableLayoutPanel();
            serverLabel = new Label();
            buttonsTableLayoutPanel.SuspendLayout();
            databaseGroupBox.SuspendLayout();
            databaseTableLayoutPanel.SuspendLayout();
            loginTableLayoutPanel.SuspendLayout();
            logonGroupBox.SuspendLayout();
            serverTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // separatorPanel
            // 
            resources.ApplyResources(separatorPanel, "separatorPanel");
            separatorPanel.Name = "separatorPanel";
            // 
            // testConnectionButton
            // 
            resources.ApplyResources(testConnectionButton, "testConnectionButton");
            testConnectionButton.Name = "testConnectionButton";
            testConnectionButton.UseVisualStyleBackColor = true;
            testConnectionButton.Click += testConnectionButton_Click;
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
            // passwordLabel
            // 
            resources.ApplyResources(passwordLabel, "passwordLabel");
            passwordLabel.Name = "passwordLabel";
            // 
            // userNameTextBox
            // 
            resources.ApplyResources(userNameTextBox, "userNameTextBox");
            userNameTextBox.Name = "userNameTextBox";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(passwordTextBox, "passwordTextBox");
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // userNameLabel
            // 
            resources.ApplyResources(userNameLabel, "userNameLabel");
            userNameLabel.Name = "userNameLabel";
            // 
            // databaseGroupBox
            // 
            databaseGroupBox.Controls.Add(databaseTableLayoutPanel);
            databaseGroupBox.Controls.Add(databaseLabel);
            resources.ApplyResources(databaseGroupBox, "databaseGroupBox");
            databaseGroupBox.Name = "databaseGroupBox";
            databaseGroupBox.TabStop = false;
            // 
            // databaseTableLayoutPanel
            // 
            resources.ApplyResources(databaseTableLayoutPanel, "databaseTableLayoutPanel");
            databaseTableLayoutPanel.Controls.Add(databaseComboBox, 0, 0);
            databaseTableLayoutPanel.Controls.Add(databaseRefreshButton, 1, 0);
            databaseTableLayoutPanel.Name = "databaseTableLayoutPanel";
            // 
            // databaseComboBox
            // 
            databaseComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            databaseComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            databaseComboBox.FormattingEnabled = true;
            resources.ApplyResources(databaseComboBox, "databaseComboBox");
            databaseComboBox.Name = "databaseComboBox";
            // 
            // databaseRefreshButton
            // 
            resources.ApplyResources(databaseRefreshButton, "databaseRefreshButton");
            databaseRefreshButton.Name = "databaseRefreshButton";
            databaseRefreshButton.UseVisualStyleBackColor = true;
            databaseRefreshButton.Click += databaseRefreshButton_Click;
            // 
            // databaseLabel
            // 
            resources.ApplyResources(databaseLabel, "databaseLabel");
            databaseLabel.Name = "databaseLabel";
            // 
            // sqlAuthRadioButton
            // 
            resources.ApplyResources(sqlAuthRadioButton, "sqlAuthRadioButton");
            sqlAuthRadioButton.Name = "sqlAuthRadioButton";
            sqlAuthRadioButton.UseVisualStyleBackColor = true;
            // 
            // loginTableLayoutPanel
            // 
            resources.ApplyResources(loginTableLayoutPanel, "loginTableLayoutPanel");
            loginTableLayoutPanel.Controls.Add(passwordLabel, 0, 1);
            loginTableLayoutPanel.Controls.Add(userNameTextBox, 1, 0);
            loginTableLayoutPanel.Controls.Add(passwordTextBox, 1, 1);
            loginTableLayoutPanel.Controls.Add(userNameLabel, 0, 0);
            loginTableLayoutPanel.Controls.Add(loginSqlAuthLabel, 1, 2);
            loginTableLayoutPanel.Name = "loginTableLayoutPanel";
            // 
            // loginSqlAuthLabel
            // 
            resources.ApplyResources(loginSqlAuthLabel, "loginSqlAuthLabel");
            loginSqlAuthLabel.Name = "loginSqlAuthLabel";
            // 
            // logonGroupBox
            // 
            logonGroupBox.Controls.Add(loginTableLayoutPanel);
            logonGroupBox.Controls.Add(sqlAuthRadioButton);
            logonGroupBox.Controls.Add(windowsAuthRadioButton);
            resources.ApplyResources(logonGroupBox, "logonGroupBox");
            logonGroupBox.Name = "logonGroupBox";
            logonGroupBox.TabStop = false;
            // 
            // windowsAuthRadioButton
            // 
            resources.ApplyResources(windowsAuthRadioButton, "windowsAuthRadioButton");
            windowsAuthRadioButton.Checked = true;
            windowsAuthRadioButton.Name = "windowsAuthRadioButton";
            windowsAuthRadioButton.TabStop = true;
            windowsAuthRadioButton.UseVisualStyleBackColor = true;
            // 
            // serverComboBox
            // 
            resources.ApplyResources(serverComboBox, "serverComboBox");
            serverComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            serverComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            serverComboBox.FormattingEnabled = true;
            serverComboBox.Name = "serverComboBox";
            // 
            // serverRefreshButton
            // 
            resources.ApplyResources(serverRefreshButton, "serverRefreshButton");
            serverRefreshButton.Name = "serverRefreshButton";
            serverRefreshButton.UseVisualStyleBackColor = true;
            serverRefreshButton.Click += serverRefreshButton_Click;
            // 
            // serverTableLayoutPanel
            // 
            resources.ApplyResources(serverTableLayoutPanel, "serverTableLayoutPanel");
            serverTableLayoutPanel.Controls.Add(serverComboBox, 0, 0);
            serverTableLayoutPanel.Controls.Add(serverRefreshButton, 1, 0);
            serverTableLayoutPanel.Name = "serverTableLayoutPanel";
            // 
            // serverLabel
            // 
            resources.ApplyResources(serverLabel, "serverLabel");
            serverLabel.Name = "serverLabel";
            // 
            // DataConnectionDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(databaseGroupBox);
            Controls.Add(logonGroupBox);
            Controls.Add(serverTableLayoutPanel);
            Controls.Add(serverLabel);
            Controls.Add(buttonsTableLayoutPanel);
            Controls.Add(testConnectionButton);
            Controls.Add(separatorPanel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DataConnectionDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            buttonsTableLayoutPanel.ResumeLayout(false);
            databaseGroupBox.ResumeLayout(false);
            databaseGroupBox.PerformLayout();
            databaseTableLayoutPanel.ResumeLayout(false);
            loginTableLayoutPanel.ResumeLayout(false);
            loginTableLayoutPanel.PerformLayout();
            logonGroupBox.ResumeLayout(false);
            logonGroupBox.PerformLayout();
            serverTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel separatorPanel;
        private Button testConnectionButton;
        private TableLayoutPanel buttonsTableLayoutPanel;
        private Button acceptButton;
        private Button cancelButton;
        private Label passwordLabel;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private Label userNameLabel;
        private GroupBox databaseGroupBox;
        private ComboBox databaseComboBox;
        private RadioButton sqlAuthRadioButton;
        private TableLayoutPanel loginTableLayoutPanel;
        private GroupBox logonGroupBox;
        private RadioButton windowsAuthRadioButton;
        private ComboBox serverComboBox;
        private Button serverRefreshButton;
        private TableLayoutPanel serverTableLayoutPanel;
        private Label serverLabel;
        private Label databaseLabel;
        private TableLayoutPanel databaseTableLayoutPanel;
        private Button databaseRefreshButton;
        private Label loginSqlAuthLabel;
    }
}