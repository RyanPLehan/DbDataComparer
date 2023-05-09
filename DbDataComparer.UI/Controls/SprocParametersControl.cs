using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.UI
{
    public partial class SprocParametersControl : UserControl
    {
        private const int HEADER_ROW_INDEX = 0;

        private IEnumerable<Parameter> Parameters;

        public SprocParametersControl()
        {
            InitializeComponent();
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return this.Parameters;
        }

        public void SetParameters(IEnumerable<Parameter> parameters)
        {
            this.Parameters = parameters;
            Initialize();
            LoadParameters();
        }

        private void Initialize()
        {
            // Delete all rows with the exception of the first row
            for (int i = this.paramsTableLayoutPanel.RowCount - 1; i > HEADER_ROW_INDEX; i--)
            {
                if (this.paramsTableLayoutPanel.RowStyles.Count - 1 == i)
                    this.paramsTableLayoutPanel.RowStyles.RemoveAt(i);
            }

            this.paramsTableLayoutPanel.RowCount = HEADER_ROW_INDEX + 1;
        }

        private void LoadParameters()
        {
            int row = 0;
            foreach (Parameter param in this.Parameters)
            {
                // Do not create row based upon parameter direction
                if (param.Direction == ParameterDirection.ReturnValue ||
                    param.Direction == ParameterDirection.Output)
                    continue;

                // Increment panel row count before adding any thing
                this.paramsTableLayoutPanel.RowCount++;
                row = this.paramsTableLayoutPanel.RowCount - 1;
                LoadParameter(this.paramsTableLayoutPanel, row, param);
            }
        }

        private void LoadParameter(TableLayoutPanel table, int row, Parameter param)
        {
            // Parameter Name
            table.Controls.Add(CreateParamNameControl(param, String.Format("ParameterName_{0}", row)));

            // Parameter Value
            table.Controls.Add(CreateParamValueControl(param, String.Format("ParameterValue_{0}", row)));

            // Null Check box
            table.Controls.Add(CreateParamNullControl(param, String.Format("ParameterNull_{0}", row)));

            // Null Check box
            table.Controls.Add(CreateParamDataTypeControl(param, String.Format("ParameterDataType_{0}", row)));
        }

        private Control CreateParamNameControl(Parameter param, string controlName)
        {
            return new Label()
            {
                Text = param.Name,
                Name = controlName,
                Anchor = AnchorStyles.None,
                Tag = param.Name,
                Size = new Size(100, 15),
                TextAlign = ContentAlignment.MiddleCenter,
            };
        }

        private Control CreateParamValueControl(Parameter param, string controlName)
        {
            Control control = TypeToControlConverter.ToControl(param.DataType);
            control.Name = controlName;
            control.Anchor = AnchorStyles.None;
            control.Tag = param.Name;
            control.Size = new Size(300, 20);

            return control;
        }

        private Control CreateParamNullControl(Parameter param, string controlName)
        {
            return new CheckBox()
            {
                Text = "Set as Null",
                Name = controlName,
                Anchor = AnchorStyles.None,
                Tag = param.Name,
                Enabled = param.IsNullable,
                Size = new Size(90, 20),
            };
        }

        private Control CreateParamDataTypeControl(Parameter param, string controlName)
        {
            return new Label()
            {
                Text = param.DataTypeDescription,
                Name = controlName,
                Anchor = AnchorStyles.None,
                Tag = param.Name,
                Size = new Size(70, 15),
                TextAlign = ContentAlignment.MiddleCenter,
        };
    }

}

}
