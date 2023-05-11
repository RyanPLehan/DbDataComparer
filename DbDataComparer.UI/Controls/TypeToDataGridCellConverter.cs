using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI
{
    internal static class TypeToDataGridCellConverter
    {
        public static DataGridViewCell ToControl(SqlDbType sqlDbType)
        {
            DataGridViewCell control;

            switch (sqlDbType)
            {
                case SqlDbType.Bit:
                    control = new DataGridViewComboBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = true;
                    ((DataGridViewComboBoxCell)control).Items.AddRange(new bool[] { true, false });
                    break;

                case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "D" };
                    break;

                case SqlDbType.Float:
                case SqlDbType.Real:
                case SqlDbType.Decimal:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0.0;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "G" };
                    break;

                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0.00;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "C" };
                    break;


                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = DateTime.Now;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "MM/dd/yyyy HH:mm:ss" };
                    break;

                case SqlDbType.Time:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = DateTime.Now.TimeOfDay;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "HH:mm:ss" };
                    break;

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = "<Enter value>";
                    break;

                case SqlDbType.UniqueIdentifier:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = Guid.Empty;
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "B" };
                    break;

                case SqlDbType.Structured:
                    control = new DataGridViewButtonCell();
                    ((DataGridViewButtonCell)control).Value = "Click to Edit Items";
                    ((DataGridViewButtonCell)control).Style = new DataGridViewCellStyle() 
                    { 
                        //BackColor = Button.DefaultBackColor,
                        BackColor = Color.LightCoral,
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                    };
                    break;


                default:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Value = "<< Unsupported Type >>";
                    ((DataGridViewTextBoxCell)control).ReadOnly = true;
                    break;
            }

            return control;
        }
    }
}
