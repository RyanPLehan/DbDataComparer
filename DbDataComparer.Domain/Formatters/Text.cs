using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;

namespace DbDataComparer.Domain.Formatters
{
    public static class Text
    {
        private const string DEFAULT_INDENT = "   ";

        public static string IndentChars { get => DEFAULT_INDENT; }

        /// <summary>
        /// This will indent each line within text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string Indent(string text, string indent)
        {
            StringBuilder sb = new StringBuilder();

            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                    sb.Append(indent);

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public static string ToPascalCase(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return text;

            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            return ti.ToTitleCase(text);
        }
    }
}
