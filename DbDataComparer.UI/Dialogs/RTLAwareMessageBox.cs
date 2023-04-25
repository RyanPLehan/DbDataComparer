using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI
{
    internal sealed class RTLAwareMessageBox
    {
        private RTLAwareMessageBox()
        {
        }

        public static void ShowMessage(string title, string message)
        {
            Show(title, message, MessageBoxIcon.Information);
        }

        public static void ShowError(string title, Exception ex)
        {
            Show(title, ex.Message, MessageBoxIcon.Exclamation);
        }

        public static void ShowError(string title, string message)
        {
            Show(title, message, MessageBoxIcon.Exclamation);
        }

        public static DialogResult Show(string caption, string text, MessageBoxIcon icon)
        {
            MessageBoxOptions options = 0;
            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                options = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
            }
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, icon, MessageBoxDefaultButton.Button1, options);
        }

        public static DialogResult ShowYesNo(string caption, string text, MessageBoxIcon icon)
        {
            MessageBoxOptions options = 0;
            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                options = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
            }


            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, icon, MessageBoxDefaultButton.Button1, options);
        }

    }
}
