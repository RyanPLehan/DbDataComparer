namespace DbDataComparer.UI.Controls
{
    partial class StoredProcedureTestsControl
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
            testNamesLabel = new Label();
            testsComboBox = new ComboBox();
            testGroupBox = new GroupBox();
            testNameTextBox = new TextBox();
            testNameLabel = new Label();
            testTabControl = new TabControl();
            sourceTabPage = new TabPage();
            testSourceTextBox = new TextBox();
            targetTabPage = new TabPage();
            testTargetTextBox = new TextBox();
            buttonTableLayoutPanel = new TableLayoutPanel();
            addButton = new Button();
            deleteButton = new Button();
            testGroupBox.SuspendLayout();
            testTabControl.SuspendLayout();
            sourceTabPage.SuspendLayout();
            targetTabPage.SuspendLayout();
            buttonTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // testNamesLabel
            // 
            testNamesLabel.AutoSize = true;
            testNamesLabel.Location = new Point(13, 10);
            testNamesLabel.Name = "testNamesLabel";
            testNamesLabel.Size = new Size(32, 15);
            testNamesLabel.TabIndex = 0;
            testNamesLabel.Text = "Tests";
            // 
            // testsComboBox
            // 
            testsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            testsComboBox.FormattingEnabled = true;
            testsComboBox.Location = new Point(13, 28);
            testsComboBox.Name = "testsComboBox";
            testsComboBox.Size = new Size(496, 23);
            testsComboBox.TabIndex = 1;
            testsComboBox.SelectedIndexChanged += testsComboBox_SelectedIndexChanged;
            // 
            // testGroupBox
            // 
            testGroupBox.Controls.Add(testNameTextBox);
            testGroupBox.Controls.Add(testNameLabel);
            testGroupBox.Controls.Add(testTabControl);
            testGroupBox.Location = new Point(13, 57);
            testGroupBox.Name = "testGroupBox";
            testGroupBox.Size = new Size(683, 289);
            testGroupBox.TabIndex = 4;
            testGroupBox.TabStop = false;
            testGroupBox.Text = "Test";
            // 
            // testNameTextBox
            // 
            testNameTextBox.Location = new Point(6, 34);
            testNameTextBox.Name = "testNameTextBox";
            testNameTextBox.Size = new Size(490, 23);
            testNameTextBox.TabIndex = 5;
            // 
            // testNameLabel
            // 
            testNameLabel.AutoSize = true;
            testNameLabel.Location = new Point(6, 16);
            testNameLabel.Name = "testNameLabel";
            testNameLabel.Size = new Size(39, 15);
            testNameLabel.TabIndex = 4;
            testNameLabel.Text = "Name";
            // 
            // testTabControl
            // 
            testTabControl.Controls.Add(sourceTabPage);
            testTabControl.Controls.Add(targetTabPage);
            testTabControl.Location = new Point(5, 63);
            testTabControl.Name = "testTabControl";
            testTabControl.SelectedIndex = 0;
            testTabControl.Size = new Size(672, 221);
            testTabControl.TabIndex = 0;
            // 
            // sourceTabPage
            // 
            sourceTabPage.Controls.Add(testSourceTextBox);
            sourceTabPage.Location = new Point(4, 24);
            sourceTabPage.Name = "sourceTabPage";
            sourceTabPage.Padding = new Padding(3);
            sourceTabPage.Size = new Size(664, 193);
            sourceTabPage.TabIndex = 0;
            sourceTabPage.Text = "Source";
            sourceTabPage.UseVisualStyleBackColor = true;
            // 
            // testSourceTextBox
            // 
            testSourceTextBox.Location = new Point(5, 5);
            testSourceTextBox.Multiline = true;
            testSourceTextBox.Name = "testSourceTextBox";
            testSourceTextBox.ScrollBars = ScrollBars.Vertical;
            testSourceTextBox.Size = new Size(653, 185);
            testSourceTextBox.TabIndex = 0;
            // 
            // targetTabPage
            // 
            targetTabPage.Controls.Add(testTargetTextBox);
            targetTabPage.Location = new Point(4, 24);
            targetTabPage.Name = "targetTabPage";
            targetTabPage.Padding = new Padding(3);
            targetTabPage.Size = new Size(664, 193);
            targetTabPage.TabIndex = 1;
            targetTabPage.Text = "Target";
            targetTabPage.UseVisualStyleBackColor = true;
            // 
            // testTargetTextBox
            // 
            testTargetTextBox.Location = new Point(5, 5);
            testTargetTextBox.Multiline = true;
            testTargetTextBox.Name = "testTargetTextBox";
            testTargetTextBox.ScrollBars = ScrollBars.Vertical;
            testTargetTextBox.Size = new Size(653, 185);
            testTargetTextBox.TabIndex = 1;
            // 
            // buttonTableLayoutPanel
            // 
            buttonTableLayoutPanel.ColumnCount = 2;
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.Controls.Add(addButton, 0, 0);
            buttonTableLayoutPanel.Controls.Add(deleteButton, 1, 0);
            buttonTableLayoutPanel.Location = new Point(560, 347);
            buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            buttonTableLayoutPanel.RowCount = 1;
            buttonTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonTableLayoutPanel.Size = new Size(142, 35);
            buttonTableLayoutPanel.TabIndex = 5;
            // 
            // addButton
            // 
            addButton.Anchor = AnchorStyles.None;
            addButton.Location = new Point(5, 5);
            addButton.Name = "addButton";
            addButton.Size = new Size(60, 25);
            addButton.TabIndex = 0;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.None;
            deleteButton.Location = new Point(76, 5);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(60, 25);
            deleteButton.TabIndex = 1;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // StoredProcedureTestsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonTableLayoutPanel);
            Controls.Add(testGroupBox);
            Controls.Add(testsComboBox);
            Controls.Add(testNamesLabel);
            Name = "StoredProcedureTestsControl";
            Size = new Size(705, 385);
            testGroupBox.ResumeLayout(false);
            testGroupBox.PerformLayout();
            testTabControl.ResumeLayout(false);
            sourceTabPage.ResumeLayout(false);
            sourceTabPage.PerformLayout();
            targetTabPage.ResumeLayout(false);
            targetTabPage.PerformLayout();
            buttonTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label testNamesLabel;
        private ComboBox testsComboBox;
        private GroupBox testGroupBox;
        private TableLayoutPanel buttonTableLayoutPanel;
        private Button addButton;
        private Button deleteButton;
        private TabControl testTabControl;
        private TabPage sourceTabPage;
        private TextBox testSourceTextBox;
        private TabPage targetTabPage;
        private TextBox testTargetTextBox;
        private TextBox testNameTextBox;
        private Label testNameLabel;
    }
}
