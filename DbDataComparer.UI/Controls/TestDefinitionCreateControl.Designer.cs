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
            tdCancelButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tdCreateButton = new Button();
            tdNameLabel = new Label();
            tdNameTextBox = new TextBox();
            tdTabControl.SuspendLayout();
            sourceTabPage.SuspendLayout();
            targetTabPage.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            tdCancelButton.Anchor = AnchorStyles.None;
            tdCancelButton.Location = new Point(104, 3);
            tdCancelButton.Name = "tdCancelButton";
            tdCancelButton.Size = new Size(80, 25);
            tdCancelButton.TabIndex = 2;
            tdCancelButton.Text = "Cancel";
            tdCancelButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tdCreateButton, 0, 0);
            tableLayoutPanel1.Controls.Add(tdCancelButton, 1, 0);
            tableLayoutPanel1.Location = new Point(237, 279);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(192, 31);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tdCreateButton
            // 
            tdCreateButton.Anchor = AnchorStyles.None;
            tdCreateButton.Location = new Point(8, 3);
            tdCreateButton.Name = "tdCreateButton";
            tdCreateButton.Size = new Size(80, 25);
            tdCreateButton.TabIndex = 2;
            tdCreateButton.Text = "Create";
            tdCreateButton.UseVisualStyleBackColor = true;
            tdCreateButton.Click += tdCreateButton_Click;
            // 
            // tdNameLabel
            // 
            tdNameLabel.AutoSize = true;
            tdNameLabel.Location = new Point(3, 6);
            tdNameLabel.Name = "tdNameLabel";
            tdNameLabel.Size = new Size(39, 15);
            tdNameLabel.TabIndex = 0;
            tdNameLabel.Text = "Name";
            // 
            // tdNameTextBox
            // 
            tdNameTextBox.Location = new Point(7, 24);
            tdNameTextBox.Name = "tdNameTextBox";
            tdNameTextBox.Size = new Size(422, 23);
            tdNameTextBox.TabIndex = 1;
            // 
            // TestDefinitionCreateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tdNameTextBox);
            Controls.Add(tdNameLabel);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tdTabControl);
            Name = "TestDefinitionCreateControl";
            Size = new Size(435, 316);
            tdTabControl.ResumeLayout(false);
            sourceTabPage.ResumeLayout(false);
            targetTabPage.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage sourceTabPage;
        private TabPage targetTabPage;
        private DataExplorerControl sourceDataExplorerControl;
        private DataExplorerControl targetDataExplorerControl;
        private Button tdCancelButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Button tdCreateButton;
        private Label tdNameLabel;
        private TextBox tdNameTextBox;
    }
}
