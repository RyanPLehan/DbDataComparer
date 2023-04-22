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
            tableLayoutPanel1 = new TableLayoutPanel();
            testDefinitionCreate = new Button();
            TestDefinitionModify = new Button();
            testDefinitionTest = new Button();
            testDefinitionLabel = new Label();
            createGroupBox = new GroupBox();
            testDefinitionCreateControl1 = new TestDefinitionCreateControl();
            tableLayoutPanel1.SuspendLayout();
            createGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(testDefinitionCreate, 0, 0);
            tableLayoutPanel1.Controls.Add(TestDefinitionModify, 0, 1);
            tableLayoutPanel1.Controls.Add(testDefinitionTest, 0, 2);
            tableLayoutPanel1.Location = new Point(35, 87);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Size = new Size(150, 234);
            tableLayoutPanel1.TabIndex = 0;
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
            TestDefinitionModify.Location = new Point(15, 87);
            TestDefinitionModify.Name = "TestDefinitionModify";
            TestDefinitionModify.Size = new Size(120, 60);
            TestDefinitionModify.TabIndex = 1;
            TestDefinitionModify.Text = "Modifiy";
            TestDefinitionModify.UseVisualStyleBackColor = true;
            // 
            // testDefinitionTest
            // 
            testDefinitionTest.Anchor = AnchorStyles.None;
            testDefinitionTest.Location = new Point(15, 165);
            testDefinitionTest.Name = "testDefinitionTest";
            testDefinitionTest.Size = new Size(120, 60);
            testDefinitionTest.TabIndex = 2;
            testDefinitionTest.Text = "Test";
            testDefinitionTest.UseVisualStyleBackColor = true;
            // 
            // testDefinitionLabel
            // 
            testDefinitionLabel.AutoSize = true;
            testDefinitionLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            testDefinitionLabel.Location = new Point(35, 54);
            testDefinitionLabel.Name = "testDefinitionLabel";
            testDefinitionLabel.Size = new Size(150, 30);
            testDefinitionLabel.TabIndex = 1;
            testDefinitionLabel.Text = "Test Definition";
            // 
            // createGroupBox
            // 
            createGroupBox.Controls.Add(testDefinitionCreateControl1);
            createGroupBox.Location = new Point(216, 12);
            createGroupBox.Name = "createGroupBox";
            createGroupBox.Size = new Size(507, 356);
            createGroupBox.TabIndex = 2;
            createGroupBox.TabStop = false;
            createGroupBox.Text = "Test Definition Create Options";
            // 
            // testDefinitionCreateControl1
            // 
            testDefinitionCreateControl1.Anchor = AnchorStyles.None;
            testDefinitionCreateControl1.Location = new Point(4, 17);
            testDefinitionCreateControl1.Name = "testDefinitionCreateControl1";
            testDefinitionCreateControl1.Size = new Size(485, 319);
            testDefinitionCreateControl1.TabIndex = 0;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 380);
            Controls.Add(createGroupBox);
            Controls.Add(testDefinitionLabel);
            Controls.Add(tableLayoutPanel1);
            Name = "Main";
            Text = "Database Data Comparer";
            Load += Main_Load;
            tableLayoutPanel1.ResumeLayout(false);
            createGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button testDefinitionCreate;
        private Button TestDefinitionModify;
        private Button testDefinitionTest;
        private Label testDefinitionLabel;
        private GroupBox createGroupBox;
        private TestDefinitionCreateControl testDefinitionCreateControl1;
    }
}