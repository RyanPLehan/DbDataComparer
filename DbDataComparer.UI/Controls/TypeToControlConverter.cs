using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI
{
    internal static class TypeToControlConverter
    {
        public static Control ToControl(SqlDbType sqlDbType)
        {
            Control control;

            switch (sqlDbType)
            {
                case SqlDbType.Bit:
                    control = new CheckBox() { Text = "True (checked)/False" };
                    break;

                case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                    control = new MaskedTextBox() { Mask = "999999999990" };
                    break;

                case SqlDbType.Float:
                case SqlDbType.Real:
                case SqlDbType.Decimal:
                    control = new MaskedTextBox() { Mask = "999999999990.9999999" };
                    break;

                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    control = new MaskedTextBox() { Mask = "999999999990.9999" };
                    break;


                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                    control = new DateTimePicker()
                    {
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "MM/dd/yyyy hh:mm:ss"
                    };
                    break;

                case SqlDbType.Time:
                    control = new DateTimePicker()
                    {
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "hh:mm:ss"
                    };
                    break;

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    control = new TextBox();
                    break;

                case SqlDbType.UniqueIdentifier:
                    control = new TextBox() { Text = "{00000000-0000-0000-0000-000000000000}" };
                    break;

                case SqlDbType.Structured:
                    control = new Button() { Text = "Edit Items" };
                    break;


                default:
                    control = new Label() { Text = "Unsupported Type" };
                    break;
            }

            return control;
        }
    }
}
