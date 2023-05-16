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
            tdTabControl.SuspendLayout();
            overallResultsTabPage.SuspendLayout();
            ErrorsTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // tdTabControl
            // 
            tdTabControl.Controls.Add(overallResultsTabPage);
            tdTabControl.Controls.Add(ErrorsTabPage);
            tdTabControl.Location = new Point(3, 3);
            tdTabControl.Name = "tdTabControl";
            tdTabControl.SelectedIndex = 0;
            tdTabControl.Size = new Size(734, 469);
            tdTabControl.TabIndex = 2;
            // 
            // overallResultsTabPage
            // 
            overallResultsTabPage.Controls.Add(overallResultsTextBox);
            overallResultsTabPage.Location = new Point(4, 24);
            overallResultsTabPage.Name = "overallResultsTabPage";
            overallResultsTabPage.Padding = new Padding(3);
            overallResultsTabPage.Size = new Size(726, 441);
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
            overallResultsTextBox.Size = new Size(720, 440);
            overallResultsTextBox.TabIndex = 0;
            overallResultsTextBox.WordWrap = false;
            // 
            // ErrorsTabPage
            // 
            ErrorsTabPage.Controls.Add(errorsTextBox);
            ErrorsTabPage.Location = new Point(4, 24);
            ErrorsTabPage.Name = "ErrorsTabPage";
            ErrorsTabPage.Padding = new Padding(3);
            ErrorsTabPage.Size = new Size(726, 441);
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
            errorsTextBox.Size = new Size(720, 440);
            errorsTextBox.TabIndex = 0;
            errorsTextBox.WordWrap = false;
            // 
            // TestDefinitionCompareControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tdTabControl);
            Name = "TestDefinitionCompareControl";
            Size = new Size(740, 475);
            tdTabControl.ResumeLayout(false);
            overallResultsTabPage.ResumeLayout(false);
            overallResultsTabPage.PerformLayout();
            ErrorsTabPage.ResumeLayout(false);
            ErrorsTabPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tdTabControl;
        private TabPage overallResultsTabPage;
        private TabPage ErrorsTabPage;
        private TextBox overallResultsTextBox;
        private TextBox errorsTextBox;
    }
}
