namespace DbDataComparer.UI
{
    partial class DataExplorerControl
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
            connectionStringLabel = new Label();
            dataSourceTableLayoutPanel = new TableLayoutPanel();
            connectionStringBuild = new Button();
            connectionStringTextBox = new TextBox();
            dbObjectGroupBox = new GroupBox();
            dbObjectComboBox = new ComboBox();
            dbTypeTableLayoutPanel = new TableLayoutPanel();
            dbTableRadioButton = new RadioButton();
            dbSprocRadioButton = new RadioButton();
            dbViewRadioButton = new RadioButton();
            dataSourceTableLayoutPanel.SuspendLayout();
            dbObjectGroupBox.SuspendLayout();
            dbTypeTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // connectionStringLabel
            // 
            connectionStringLabel.AutoSize = true;
            connectionStringLabel.Location = new Point(11, 11);
            connectionStringLabel.Name = "connectionStringLabel";
            connectionStringLabel.Size = new Size(103, 15);
            connectionStringLabel.TabIndex = 0;
            connectionStringLabel.Text = "Connection String";
            // 
            // dataSourceTableLayoutPanel
            // 
            dataSourceTableLayoutPanel.ColumnCount = 2;
            dataSourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.71066F));
            dataSourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.289341F));
            dataSourceTableLayoutPanel.Controls.Add(connectionStringBuild, 0, 0);
            dataSourceTableLayoutPanel.Controls.Add(connectionStringTextBox, 0, 0);
            dataSourceTableLayoutPanel.Location = new Point(11, 29);
            dataSourceTableLayoutPanel.Name = "dataSourceTableLayoutPanel";
            dataSourceTableLayoutPanel.RowCount = 1;
            dataSourceTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            dataSourceTableLayoutPanel.Size = new Size(394, 28);
            dataSourceTableLayoutPanel.TabIndex = 1;
            // 
            // connectionStringBuild
            // 
            connectionStringBuild.Location = new Point(321, 3);
            connectionStringBuild.Name = "connectionStringBuild";
            connectionStringBuild.Size = new Size(67, 22);
            connectionStringBuild.TabIndex = 1;
            connectionStringBuild.Text = "Build";
            connectionStringBuild.UseVisualStyleBackColor = true;
            connectionStringBuild.Click += dataSourceBuild_Click;
            // 
            // connectionStringTextBox
            // 
            connectionStringTextBox.Location = new Point(3, 3);
            connectionStringTextBox.Name = "connectionStringTextBox";
            connectionStringTextBox.Size = new Size(312, 23);
            connectionStringTextBox.TabIndex = 0;
            connectionStringTextBox.WordWrap = false;
            connectionStringTextBox.TextChanged += dataSourceTextBox_TextChanged;
            // 
            // dbObjectGroupBox
            // 
            dbObjectGroupBox.Controls.Add(dbObjectComboBox);
            dbObjectGroupBox.Controls.Add(dbTypeTableLayoutPanel);
            dbObjectGroupBox.Location = new Point(14, 75);
            dbObjectGroupBox.Name = "dbObjectGroupBox";
            dbObjectGroupBox.Size = new Size(390, 93);
            dbObjectGroupBox.TabIndex = 2;
            dbObjectGroupBox.TabStop = false;
            dbObjectGroupBox.Text = "Choose Database Object";
            // 
            // dbObjectComboBox
            // 
            dbObjectComboBox.FormattingEnabled = true;
            dbObjectComboBox.Location = new Point(6, 55);
            dbObjectComboBox.Name = "dbObjectComboBox";
            dbObjectComboBox.Size = new Size(376, 23);
            dbObjectComboBox.TabIndex = 1;
            // 
            // dbTypeTableLayoutPanel
            // 
            dbTypeTableLayoutPanel.ColumnCount = 3;
            dbTypeTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            dbTypeTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            dbTypeTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            dbTypeTableLayoutPanel.Controls.Add(dbTableRadioButton, 0, 0);
            dbTypeTableLayoutPanel.Controls.Add(dbSprocRadioButton, 0, 0);
            dbTypeTableLayoutPanel.Controls.Add(dbViewRadioButton, 2, 0);
            dbTypeTableLayoutPanel.Location = new Point(6, 22);
            dbTypeTableLayoutPanel.Name = "dbTypeTableLayoutPanel";
            dbTypeTableLayoutPanel.RowCount = 1;
            dbTypeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTypeTableLayoutPanel.Size = new Size(377, 23);
            dbTypeTableLayoutPanel.TabIndex = 0;
            // 
            // dbTableRadioButton
            // 
            dbTableRadioButton.Anchor = AnchorStyles.None;
            dbTableRadioButton.AutoSize = true;
            dbTableRadioButton.Location = new Point(161, 3);
            dbTableRadioButton.Name = "dbTableRadioButton";
            dbTableRadioButton.Size = new Size(52, 17);
            dbTableRadioButton.TabIndex = 1;
            dbTableRadioButton.TabStop = true;
            dbTableRadioButton.Text = "Table";
            dbTableRadioButton.UseVisualStyleBackColor = true;
            dbTableRadioButton.CheckedChanged += dbTableRadioButton_CheckedChanged;
            // 
            // dbSprocRadioButton
            // 
            dbSprocRadioButton.Anchor = AnchorStyles.None;
            dbSprocRadioButton.AutoSize = true;
            dbSprocRadioButton.Location = new Point(4, 3);
            dbSprocRadioButton.Name = "dbSprocRadioButton";
            dbSprocRadioButton.Size = new Size(116, 17);
            dbSprocRadioButton.TabIndex = 0;
            dbSprocRadioButton.TabStop = true;
            dbSprocRadioButton.Text = "Stored Procedure";
            dbSprocRadioButton.UseVisualStyleBackColor = true;
            dbSprocRadioButton.CheckedChanged += dbSprocRadioButton_CheckedChanged;
            // 
            // dbViewRadioButton
            // 
            dbViewRadioButton.Anchor = AnchorStyles.None;
            dbViewRadioButton.AutoSize = true;
            dbViewRadioButton.Location = new Point(288, 3);
            dbViewRadioButton.Name = "dbViewRadioButton";
            dbViewRadioButton.Size = new Size(50, 17);
            dbViewRadioButton.TabIndex = 2;
            dbViewRadioButton.TabStop = true;
            dbViewRadioButton.Text = "View";
            dbViewRadioButton.UseVisualStyleBackColor = true;
            dbViewRadioButton.CheckedChanged += dbViewRadioButton_CheckedChanged;
            // 
            // DataExplorerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dbObjectGroupBox);
            Controls.Add(dataSourceTableLayoutPanel);
            Controls.Add(connectionStringLabel);
            Name = "DataExplorerControl";
            Size = new Size(419, 183);
            dataSourceTableLayoutPanel.ResumeLayout(false);
            dataSourceTableLayoutPanel.PerformLayout();
            dbObjectGroupBox.ResumeLayout(false);
            dbTypeTableLayoutPanel.ResumeLayout(false);
            dbTypeTableLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label connectionStringLabel;
        private TableLayoutPanel dataSourceTableLayoutPanel;
        private Button connectionStringBuild;
        private TextBox connectionStringTextBox;
        private GroupBox dbObjectGroupBox;
        private ComboBox dbObjectComboBox;
        private TableLayoutPanel dbTypeTableLayoutPanel;
        private RadioButton dbTableRadioButton;
        private RadioButton dbSprocRadioButton;
        private RadioButton dbViewRadioButton;
    }
}
