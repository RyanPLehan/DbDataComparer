namespace DbDataComparer.UI
{
    partial class TestDefinitionCompareControl
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
            overallResultsTabPage = new TabPage();
            overallResultsTextBox = new TextBox();
            ErrorsTabPage = new TabPage();
            errorsTextBox = new TextBox();
            tdCompareButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tdLoadButton = new Button();
            tdTabControl.SuspendLayout();
            overallResultsTabPage.SuspendLayout();
            ErrorsTabPage.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(overallResultsTabPage);
            tdTabControl.Controls.Add(ErrorsTabPage);
            tdTabControl.Location = new Point(3, 3);
            tdTabControl.Name = "tdTabControl";
            tdTabControl.SelectedIndex = 0;
            tdTabControl.Size = new Size(497, 362);
            tdTabControl.TabIndex = 2;
            // 
            // overallResultsTabPage
            // 
            overallResultsTabPage.Controls.Add(overallResultsTextBox);
            overallResultsTabPage.Location = new Point(4, 24);
            overallResultsTabPage.Name = "overallResultsTabPage";
            overallResultsTabPage.Padding = new Padding(3);
            overallResultsTabPage.Size = new Size(489, 334);
            overallResultsTabPage.TabIndex = 0;
            overallResultsTabPage.Text = "Overall Results";
            overallResultsTabPage.UseVisualStyleBackColor = true;
            // 
            // overallResultsTextBox
            // 
            overallResultsTextBox.Location = new Point(1, 1);
            overallResultsTextBox.Multiline = true;
            overallResultsTextBox.Name = "overallResultsTextBox";
            overallResultsTextBox.ScrollBars = ScrollBars.Both;
            overallResultsTextBox.Size = new Size(485, 330);
            overallResultsTextBox.TabIndex = 0;
            overallResultsTextBox.WordWrap = false;
            // 
            // ErrorsTabPage
            // 
            ErrorsTabPage.Controls.Add(errorsTextBox);
            ErrorsTabPage.Location = new Point(4, 24);
            ErrorsTabPage.Name = "ErrorsTabPage";
            ErrorsTabPage.Padding = new Padding(3);
            ErrorsTabPage.Size = new Size(489, 334);
            ErrorsTabPage.TabIndex = 1;
            ErrorsTabPage.Text = "Errors";
            ErrorsTabPage.UseVisualStyleBackColor = true;
            // 
            // errorsTextBox
            // 
            errorsTextBox.Location = new Point(1, 1);
            errorsTextBox.Multiline = true;
            errorsTextBox.Name = "errorsTextBox";
            errorsTextBox.ScrollBars = ScrollBars.Both;
            errorsTextBox.Size = new Size(485, 330);
            errorsTextBox.TabIndex = 0;
            errorsTextBox.WordWrap = false;
            // 
            // tdCompareButton
            // 
            tdCompareButton.Anchor = AnchorStyles.None;
            tdCompareButton.Location = new Point(104, 3);
            tdCompareButton.Name = "tdCompareButton";
            tdCompareButton.Size = new Size(80, 25);
            tdCompareButton.TabIndex = 2;
            tdCompareButton.Text = "Compare";
            tdCompareButton.UseVisualStyleBackColor = true;
            tdCompareButton.Click += tdCompareButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tdLoadButton, 0, 0);
            tableLayoutPanel1.Controls.Add(tdCompareButton, 1, 0);
            tableLayoutPanel1.Location = new Point(316, 371);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(192, 31);
            tableLayoutPanel1.TabIndex = 3;
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
            // TestDefinitionTestControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tdTabControl);
            Name = "TestDefinitionTestControl";
            Size = new Size(511, 405);
            tdTabControl.ResumeLayout(false);
            overallResultsTabPage.ResumeLayout(false);
            overallResultsTabPage.PerformLayout();
            ErrorsTabPage.ResumeLayout(false);
            ErrorsTabPage.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage overallResultsTabPage;
        private TabPage ErrorsTabPage;
        private Button tdCompareButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Button tdLoadButton;
        private TextBox overallResultsTextBox;
        private TextBox errorsTextBox;
    }
}
