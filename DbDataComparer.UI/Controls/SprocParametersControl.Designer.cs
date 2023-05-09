namespace DbDataComparer.UI
{
    partial class SprocParametersControl
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
            paramsTableLayoutPanel = new TableLayoutPanel();
            paramNameLabel = new Label();
            paramValueLabel = new Label();
            paramNullLabel = new Label();
            paramDataTypeLabel = new Label();
            paramsPanel = new Panel();
            paramsTableLayoutPanel.SuspendLayout();
            paramsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // paramsTableLayoutPanel
            // 
            paramsTableLayoutPanel.AutoSize = true;
            paramsTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            paramsTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            paramsTableLayoutPanel.ColumnCount = 4;
            paramsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            paramsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            paramsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            paramsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            paramsTableLayoutPanel.Controls.Add(paramNameLabel, 0, 0);
            paramsTableLayoutPanel.Controls.Add(paramValueLabel, 1, 0);
            paramsTableLayoutPanel.Controls.Add(paramNullLabel, 2, 0);
            paramsTableLayoutPanel.Controls.Add(paramDataTypeLabel, 3, 0);
            paramsTableLayoutPanel.Dock = DockStyle.Top;
            paramsTableLayoutPanel.Location = new Point(0, 0);
            paramsTableLayoutPanel.Name = "paramsTableLayoutPanel";
            paramsTableLayoutPanel.RowCount = 1;
            paramsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            paramsTableLayoutPanel.Size = new Size(650, 32);
            paramsTableLayoutPanel.TabIndex = 0;
            // 
            // paramNameLabel
            // 
            paramNameLabel.Anchor = AnchorStyles.None;
            paramNameLabel.AutoSize = true;
            paramNameLabel.Location = new Point(28, 8);
            paramNameLabel.Name = "paramNameLabel";
            paramNameLabel.Size = new Size(96, 15);
            paramNameLabel.TabIndex = 0;
            paramNameLabel.Text = "Parameter Name";
            // 
            // paramValueLabel
            // 
            paramValueLabel.Anchor = AnchorStyles.None;
            paramValueLabel.AutoSize = true;
            paramValueLabel.Location = new Point(256, 8);
            paramValueLabel.Name = "paramValueLabel";
            paramValueLabel.Size = new Size(92, 15);
            paramValueLabel.TabIndex = 1;
            paramValueLabel.Text = "Parameter Value";
            // 
            // paramNullLabel
            // 
            paramNullLabel.Anchor = AnchorStyles.None;
            paramNullLabel.AutoSize = true;
            paramNullLabel.Location = new Point(488, 8);
            paramNullLabel.Name = "paramNullLabel";
            paramNullLabel.Size = new Size(29, 15);
            paramNullLabel.TabIndex = 2;
            paramNullLabel.Text = "Null";
            // 
            // paramDataTypeLabel
            // 
            paramDataTypeLabel.Anchor = AnchorStyles.None;
            paramDataTypeLabel.AutoSize = true;
            paramDataTypeLabel.Location = new Point(571, 8);
            paramDataTypeLabel.Name = "paramDataTypeLabel";
            paramDataTypeLabel.Size = new Size(61, 15);
            paramDataTypeLabel.TabIndex = 3;
            paramDataTypeLabel.Text = " Data Type";
            // 
            // paramsPanel
            // 
            paramsPanel.AutoScroll = true;
            paramsPanel.Controls.Add(paramsTableLayoutPanel);
            paramsPanel.Dock = DockStyle.Fill;
            paramsPanel.Location = new Point(0, 0);
            paramsPanel.Name = "paramsPanel";
            paramsPanel.Size = new Size(650, 185);
            paramsPanel.TabIndex = 1;
            // 
            // SprocParametersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(paramsPanel);
            Name = "SprocParametersControl";
            Size = new Size(650, 185);
            paramsTableLayoutPanel.ResumeLayout(false);
            paramsTableLayoutPanel.PerformLayout();
            paramsPanel.ResumeLayout(false);
            paramsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel paramsTableLayoutPanel;
        private Label paramNameLabel;
        private Label paramValueLabel;
        private Label paramNullLabel;
        private Label paramDataTypeLabel;
        private Panel paramsPanel;
    }
}
