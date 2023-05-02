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
            testDefinitionCreateButton = new Button();
            testDefinitionModifyButton = new Button();
            testDefinitionCompareButton = new Button();
            testDefinitionLabel = new Label();
            testDefinitionCreateControl = new TestDefinitionCreateControl();
            mainStatusBar = new StatusStrip();
            mainStatusTDStatusDescLabel = new ToolStripStatusLabel();
            mainStatusTDStatusLabel = new ToolStripStatusLabel();
            testDefinitionCompareControl = new TestDefinitionCompareControl();
            createPanel = new Panel();
            comparePanel = new Panel();
            modifyPanel = new Panel();
            testDefinitionModifyControl = new TestDefinitionModifyControl();
            mainTableLayoutPanel.SuspendLayout();
            mainStatusBar.SuspendLayout();
            createPanel.SuspendLayout();
            comparePanel.SuspendLayout();
            modifyPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(testDefinitionCreateButton, 0, 0);
            mainTableLayoutPanel.Controls.Add(testDefinitionModifyButton, 0, 1);
            mainTableLayoutPanel.Controls.Add(testDefinitionCompareButton, 0, 2);
            mainTableLayoutPanel.Location = new Point(12, 163);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 3;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            mainTableLayoutPanel.Size = new Size(150, 234);
            mainTableLayoutPanel.TabIndex = 1;
            // 
            // testDefinitionCreateButton
            // 
            testDefinitionCreateButton.Anchor = AnchorStyles.None;
            testDefinitionCreateButton.Location = new Point(15, 9);
            testDefinitionCreateButton.Name = "testDefinitionCreateButton";
            testDefinitionCreateButton.Size = new Size(120, 60);
            testDefinitionCreateButton.TabIndex = 0;
            testDefinitionCreateButton.Text = "Create";
            testDefinitionCreateButton.UseVisualStyleBackColor = true;
            testDefinitionCreateButton.Click += testDefinitionCreateButton_Click;
            // 
            // testDefinitionModifyButton
            // 
            testDefinitionModifyButton.Anchor = AnchorStyles.None;
            testDefinitionModifyButton.Location = new Point(15, 86);
            testDefinitionModifyButton.Name = "testDefinitionModifyButton";
            testDefinitionModifyButton.Size = new Size(120, 60);
            testDefinitionModifyButton.TabIndex = 1;
            testDefinitionModifyButton.Text = "Modifiy";
            testDefinitionModifyButton.UseVisualStyleBackColor = true;
            testDefinitionModifyButton.Click += TestDefinitionModifyButton_Click;
            // 
            // testDefinitionCompareButton
            // 
            testDefinitionCompareButton.Anchor = AnchorStyles.None;
            testDefinitionCompareButton.Location = new Point(15, 164);
            testDefinitionCompareButton.Name = "testDefinitionCompareButton";
            testDefinitionCompareButton.Size = new Size(120, 60);
            testDefinitionCompareButton.TabIndex = 2;
            testDefinitionCompareButton.Text = "Compare";
            testDefinitionCompareButton.UseVisualStyleBackColor = true;
            testDefinitionCompareButton.Click += testDefinitionCompareButton_Click;
            // 
            // testDefinitionLabel
            // 
            testDefinitionLabel.AutoSize = true;
            testDefinitionLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            testDefinitionLabel.Location = new Point(12, 130);
            testDefinitionLabel.Name = "testDefinitionLabel";
            testDefinitionLabel.Size = new Size(150, 30);
            testDefinitionLabel.TabIndex = 0;
            testDefinitionLabel.Text = "Test Definition";
            // 
            // testDefinitionCreateControl
            // 
            testDefinitionCreateControl.Anchor = AnchorStyles.None;
            testDefinitionCreateControl.BorderStyle = BorderStyle.FixedSingle;
            testDefinitionCreateControl.Location = new Point(149, 77);
            testDefinitionCreateControl.Name = "testDefinitionCreateControl";
            testDefinitionCreateControl.Size = new Size(442, 315);
            testDefinitionCreateControl.TabIndex = 2;
            // 
            // mainStatusBar
            // 
            mainStatusBar.Items.AddRange(new ToolStripItem[] { mainStatusTDStatusDescLabel, mainStatusTDStatusLabel });
            mainStatusBar.Location = new Point(0, 527);
            mainStatusBar.Name = "mainStatusBar";
            mainStatusBar.Size = new Size(926, 22);
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
            testDefinitionCompareControl.Location = new Point(5, 5);
            testDefinitionCompareControl.Name = "testDefinitionCompareControl";
            testDefinitionCompareControl.Size = new Size(740, 480);
            testDefinitionCompareControl.TabIndex = 0;
            // 
            // createPanel
            // 
            createPanel.BorderStyle = BorderStyle.FixedSingle;
            createPanel.Controls.Add(testDefinitionCreateControl);
            createPanel.Location = new Point(170, 25);
            createPanel.Name = "createPanel";
            createPanel.Size = new Size(750, 490);
            createPanel.TabIndex = 2;
            createPanel.VisibleChanged += Panel_VisibleChanged;
            // 
            // comparePanel
            // 
            comparePanel.BorderStyle = BorderStyle.FixedSingle;
            comparePanel.Controls.Add(testDefinitionCompareControl);
            comparePanel.Location = new Point(170, 25);
            comparePanel.Name = "comparePanel";
            comparePanel.Size = new Size(750, 490);
            comparePanel.TabIndex = 3;
            comparePanel.VisibleChanged += Panel_VisibleChanged;
            // 
            // modifyPanel
            // 
            modifyPanel.BorderStyle = BorderStyle.FixedSingle;
            modifyPanel.Controls.Add(testDefinitionModifyControl);
            modifyPanel.Location = new Point(170, 25);
            modifyPanel.Name = "modifyPanel";
            modifyPanel.Size = new Size(750, 490);
            modifyPanel.TabIndex = 4;
            modifyPanel.VisibleChanged += Panel_VisibleChanged;
            // 
            // testDefinitionModifyControl
            // 
            testDefinitionModifyControl.Location = new Point(5, 5);
            testDefinitionModifyControl.Name = "testDefinitionModifyControl";
            testDefinitionModifyControl.Size = new Size(745, 476);
            testDefinitionModifyControl.TabIndex = 0;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(926, 549);
            Controls.Add(modifyPanel);
            Controls.Add(createPanel);
            Controls.Add(mainStatusBar);
            Controls.Add(testDefinitionLabel);
            Controls.Add(mainTableLayoutPanel);
            Controls.Add(comparePanel);
            Name = "Main";
            Text = "Database Data Comparer";
            Load += Main_Load;
            mainTableLayoutPanel.ResumeLayout(false);
            mainStatusBar.ResumeLayout(false);
            mainStatusBar.PerformLayout();
            createPanel.ResumeLayout(false);
            comparePanel.ResumeLayout(false);
            modifyPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel;
        private Button testDefinitionCreateButton;
        private Button testDefinitionModifyButton;
        private Button testDefinitionCompareButton;
        private Label testDefinitionLabel;
        private StatusStrip mainStatusBar;
        private ToolStripStatusLabel mainStatusTDStatusDescLabel;
        private ToolStripStatusLabel mainStatusTDStatusLabel;
        private TestDefinitionCreateControl testDefinitionCreateControl;
        private TestDefinitionCompareControl testDefinitionCompareControl;
        private TestDefinitionModifyControl testDefinitionModifyControl;
        private Panel createPanel;
        private Panel comparePanel;
        private Panel modifyPanel;
    }
}