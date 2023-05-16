namespace DbDataComparer.UI
{
    partial class TestDefinitionCreateControl
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
            sourceTabPage = new TabPage();
            sourceDataExplorerControl = new DataExplorerControl();
            targetTabPage = new TabPage();
            targetDataExplorerControl = new DataExplorerControl();
            cancelButton = new Button();
            buttonTableLayoutPanel = new TableLayoutPanel();
            createButton = new Button();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            tdTabControl.SuspendLayout();
            sourceTabPage.SuspendLayout();
            targetTabPage.SuspendLayout();
            buttonTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(sourceTabPage);
            tdTabControl.Controls.Add(targetTabPage);
            tdTabControl.Location = new Point(3, 65);
            tdTabControl.Name = "tdTabControl";
            tdTabControl.SelectedIndex = 0;
            tdTabControl.Size = new Size(428, 211);
            tdTabControl.TabIndex = 2;
            // 
            // sourceTabPage
            // 
            sourceTabPage.Controls.Add(sourceDataExplorerControl);
            sourceTabPage.Location = new Point(4, 24);
            sourceTabPage.Name = "sourceTabPage";
            sourceTabPage.Padding = new Padding(3);
            sourceTabPage.Size = new Size(420, 183);
            sourceTabPage.TabIndex = 0;
            sourceTabPage.Text = "Source";
            sourceTabPage.UseVisualStyleBackColor = true;
            // 
            // sourceDataExplorerControl
            // 
            sourceDataExplorerControl.Location = new Point(0, 0);
            sourceDataExplorerControl.Name = "sourceDataExplorerControl";
            sourceDataExplorerControl.Size = new Size(410, 180);
            sourceDataExplorerControl.TabIndex = 0;
            // 
            // targetTabPage
            // 
            targetTabPage.Controls.Add(targetDataExplorerControl);
            targetTabPage.Location = new Point(4, 24);
            targetTabPage.Name = "targetTabPage";
            targetTabPage.Padding = new Padding(3);
            targetTabPage.Size = new Size(420, 183);
            targetTabPage.TabIndex = 1;
            targetTabPage.Text = "Target";
            targetTabPage.UseVisualStyleBackColor = true;
            // 
            // targetDataExplorerControl
            // 
            targetDataExplorerControl.Location = new Point(0, 0);
            targetDataExplorerControl.Name = "targetDataExplorerControl";
            targetDataExplorerControl.Size = new Size(410, 180);
            targetDataExplorerControl.TabIndex = 0;
            // 
            // tdCancelButton
            // 
            cancelButton.Anchor = AnchorStyles.None;
            cancelButton.Location = new Point(104, 3);
            cancelButton.Name = "tdCancelButton";
            cancelButton.Size = new Size(80, 25);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // tableLayoutPanel1
            // 
            buttonTableLayoutPanel.ColumnCount = 2;
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonTableLayoutPanel.Controls.Add(createButton, 0, 0);
            buttonTableLayoutPanel.Controls.Add(cancelButton, 1, 0);
            buttonTableLayoutPanel.Location = new Point(237, 279);
            buttonTableLayoutPanel.Name = "tableLayoutPanel1";
            buttonTableLayoutPanel.RowCount = 1;
            buttonTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonTableLayoutPanel.Size = new Size(192, 31);
            buttonTableLayoutPanel.TabIndex = 3;
            // 
            // tdCreateButton
            // 
            createButton.Anchor = AnchorStyles.None;
            createButton.Location = new Point(8, 3);
            createButton.Name = "tdCreateButton";
            createButton.Size = new Size(80, 25);
            createButton.TabIndex = 2;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // tdNameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(3, 6);
            nameLabel.Name = "tdNameLabel";
            nameLabel.Size = new Size(39, 15);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name";
            // 
            // tdNameTextBox
            // 
            nameTextBox.Location = new Point(7, 24);
            nameTextBox.Name = "tdNameTextBox";
            nameTextBox.Size = new Size(422, 23);
            nameTextBox.TabIndex = 1;
            // 
            // TestDefinitionCreateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(buttonTableLayoutPanel);
            Controls.Add(tdTabControl);
            Name = "TestDefinitionCreateControl";
            Size = new Size(435, 316);
            Load += TestDefinitionCreateControl_Load;
            tdTabControl.ResumeLayout(false);
            sourceTabPage.ResumeLayout(false);
            targetTabPage.ResumeLayout(false);
            buttonTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage sourceTabPage;
        private TabPage targetTabPage;
        private DataExplorerControl sourceDataExplorerControl;
        private DataExplorerControl targetDataExplorerControl;
        private Button cancelButton;
        private TableLayoutPanel buttonTableLayoutPanel;
        private Button createButton;
        private Label nameLabel;
        private TextBox nameTextBox;
    }
}
