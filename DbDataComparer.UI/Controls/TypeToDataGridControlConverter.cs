using DbDataComparer.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI
{
    internal static class TypeToDataGridControlConverter
    {
        public static DataGridViewCell ToCell(SqlDbType sqlDbType)
        {
            DataGridViewCell control;

            switch (sqlDbType)
            {
                case SqlDbType.Bit:
                    // control = new DataGridViewComboBoxCell();
                    // ((DataGridViewComboBoxCell)control).Tag = true;
                    // ((DataGridViewComboBoxCell)control).Items.AddRange(new bool[] { true, false });

                    control = new DataGridViewCheckBoxCell();
                    ((DataGridViewCheckBoxCell)control).Tag = false;
                    ((DataGridViewCheckBoxCell)control).Style = new DataGridViewCellStyle() 
                    { 
                        Alignment = DataGridViewContentAlignment.MiddleCenter 
                    };
                    break;

                case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "D" };
                    break;

                case SqlDbType.Float:
                case SqlDbType.Real:
                case SqlDbType.Decimal:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0.0;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "G" };
                    break;

                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = 0.00;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "C" };
                    break;


                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = DateTime.Now;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "MM/dd/yyyy HH:mm:ss" };
                    break;

                case SqlDbType.Time:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = DateTime.Now.TimeOfDay;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    ((DataGridViewTextBoxCell)control).Style = new DataGridViewCellStyle() { Format = "HH:mm:ss" };
                    break;

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = "<Enter value>";
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
                    break;

                case SqlDbType.UniqueIdentifier:
                    control = new DataGridViewTextBoxCell();
                    ((DataGridViewTextBoxCell)control).Tag = Guid.Empty;
                    ((DataGridViewTextBoxCell)control).ValueType = DatabaseTypeConverter.ToNetType(sqlDbType);
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


        public static DataGridViewColumn ToColumn(SqlDbType sqlDbType)
        {
            DataGridViewColumn control;

            switch (sqlDbType)
            {
                case SqlDbType.Bit:                    
                    //control = new DataGridViewComboBoxColumn();
                    //((DataGridViewComboBoxColumn)control).Tag = true;
                    //((DataGridViewComboBoxColumn)control).Items.AddRange(new bool[] { true, false });

                    control = new DataGridViewCheckBoxColumn();
                    ((DataGridViewCheckBoxColumn)control).Tag = false;
                    break;

                case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = 0;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "D" };
                    break;

                case SqlDbType.Float:
                case SqlDbType.Real:
                case SqlDbType.Decimal:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = 0.0;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "G" };
                    break;

                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = 0.00;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "C" };
                    break;


                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = DateTime.Now;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "MM/dd/yyyy HH:mm:ss" };
                    break;

                case SqlDbType.Time:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = DateTime.Now.TimeOfDay;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "HH:mm:ss" };
                    break;

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = "<Enter value>";
                    break;

                case SqlDbType.UniqueIdentifier:
                    control = new DataGridViewTextBoxColumn();
                    ((DataGridViewTextBoxColumn)control).Tag = Guid.Empty;
                    ((DataGridViewTextBoxColumn)control).DefaultCellStyle = new DataGridViewCellStyle() { Format = "B" };
                    break;

                case SqlDbType.Structured:
                    control = new DataGridViewButtonColumn();
                    ((DataGridViewButtonColumn)control).DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        //BackColor = Button.DefaultBackColor,
                        BackColor = Color.LightCoral,
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                    };
                    break;


                default:
                    control = new DataGridViewColumn();
                    ((DataGridViewColumn)control).ReadOnly = true;
                    break;
            }

            return control;
        }
    }
}
