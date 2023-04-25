namespace DbDataComparer.UI
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainTableLayoutPanel = new TableLayoutPanel();
            testDefinitionCreate = new Button();
            TestDefinitionModify = new Button();
            testDefinitionCompare = new Button();
            testDefinitionLabel = new Label();
            testDefinitionCreateControl = new TestDefinitionCreateControl();
            mainStatusBar = new StatusStrip();
            mainStatusTDStatusDescLabel = new ToolStripStatusLabel();
            mainStatusTDStatusLabel = new ToolStripStatusLabel();
            testDefinitionCompareControl = new TestDefinitionCompareControl();
            mainTableLayoutPanel.SuspendLayout();
            mainStatusBar.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(testDefinitionCreate, 0, 0);
            mainTableLayoutPanel.Controls.Add(TestDefinitionModify, 0, 1);
            mainTableLayoutPanel.Controls.Add(testDefinitionCompare, 0, 2);
            mainTableLayoutPanel.Location = new Point(12, 79);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 3;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.Size = new Size(150, 234);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // testDefinitionCreate
            // 
            testDefinitionCreate.Anchor = AnchorStyles.None;
            testDefinitionCreate.Location = new Point(15, 9);
            testDefinitionCreate.Name = "testDefinitionCreate";
            testDefinitionCreate.Size = new Size(120, 60);
            testDefinitionCreate.TabIndex = 0;
            testDefinitionCreate.Text = "Create";
            testDefinitionCreate.UseVisualStyleBackColor = true;
            testDefinitionCreate.Click += testDefinitionCreate_Click;
            // 
            // TestDefinitionModify
            // 
            TestDefinitionModify.Anchor = AnchorStyles.None;
            TestDefinitionModify.Location = new Point(15, 86);
            TestDefinitionModify.Name = "TestDefinitionModify";
            TestDefinitionModify.Size = new Size(120, 60);
            TestDefinitionModify.TabIndex = 1;
            TestDefinitionModify.Text = "Modifiy";
            TestDefinitionModify.UseVisualStyleBackColor = true;
            // 
            // testDefinitionCompare
            // 
            testDefinitionCompare.Anchor = AnchorStyles.None;
            testDefinitionCompare.Location = new Point(15, 164);
            testDefinitionCompare.Name = "testDefinitionCompare";
            testDefinitionCompare.Size = new Size(120, 60);
            testDefinitionCompare.TabIndex = 2;
            testDefinitionCompare.Text = "Compare";
            testDefinitionCompare.UseVisualStyleBackColor = true;
            testDefinitionCompare.Click += testDefinitionCompare_Click;
            // 
            // testDefinitionLabel
            // 
            testDefinitionLabel.AutoSize = true;
            testDefinitionLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            testDefinitionLabel.Location = new Point(12, 46);
            testDefinitionLabel.Name = "testDefinitionLabel";
            testDefinitionLabel.Size = new Size(150, 30);
            testDefinitionLabel.TabIndex = 1;
            testDefinitionLabel.Text = "Test Definition";
            // 
            // testDefinitionCreateControl
            // 
            testDefinitionCreateControl.BorderStyle = BorderStyle.FixedSingle;
            testDefinitionCreateControl.Location = new Point(229, 26);
            testDefinitionCreateControl.Name = "testDefinitionCreateControl";
            testDefinitionCreateControl.Size = new Size(442, 315);
            testDefinitionCreateControl.TabIndex = 2;
            // 
            // mainStatusBar
            // 
            mainStatusBar.Items.AddRange(new ToolStripItem[] { mainStatusTDStatusDescLabel, mainStatusTDStatusLabel });
            mainStatusBar.Location = new Point(0, 441);
            mainStatusBar.Name = "mainStatusBar";
            mainStatusBar.Size = new Size(720, 22);
            mainStatusBar.TabIndex = 3;
            // 
            // mainStatusTDStatusDescLabel
            // 
            mainStatusTDStatusDescLabel.Name = "mainStatusTDStatusDescLabel";
            mainStatusTDStatusDescLabel.Size = new Size(42, 17);
            mainStatusTDStatusDescLabel.Text = "Status:";
            // 
            // mainStatusTDStatusLabel
            // 
            mainStatusTDStatusLabel.Name = "mainStatusTDStatusLabel";
            mainStatusTDStatusLabel.Size = new Size(69, 17);
            mainStatusTDStatusLabel.Text = "Not Loaded";
            // 
            // testDefinitionCompareControl
            // 
            testDefinitionCompareControl.BorderStyle = BorderStyle.FixedSingle;
            testDefinitionCompareControl.Location = new Point(189, 16);
            testDefinitionCompareControl.Name = "testDefinitionCompareControl";
            testDefinitionCompareControl.Size = new Size(505, 404);
            testDefinitionCompareControl.TabIndex = 4;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 463);
            Controls.Add(testDefinitionCompareControl);
            Controls.Add(mainStatusBar);
            Controls.Add(testDefinitionCreateControl);
            Controls.Add(testDefinitionLabel);
            Controls.Add(mainTableLayoutPanel);
            Name = "Main";
            Text = "Database Data Comparer";
            Load += Main_Load;
            mainTableLayoutPanel.ResumeLayout(false);
            mainStatusBar.ResumeLayout(false);
            mainStatusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel;
        private Button testDefinitionCreate;
        private Button TestDefinitionModify;
        private Button testDefinitionCompare;
        private Label testDefinitionLabel;
        private TestDefinitionCreateControl testDefinitionCreateControl;
        private StatusStrip mainStatusBar;
        private ToolStripStatusLabel mainStatusTDStatusDescLabel;
        private ToolStripStatusLabel mainStatusTDStatusLabel;
        private TestDefinitionCompareControl testDefinitionTestControl1;
        private TestDefinitionCompareControl testDefinitionCompareControl;
    }
}