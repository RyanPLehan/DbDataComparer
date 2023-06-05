using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Extensions
{
    public static class StringExtension
    {
        public static string CamelCaseToSpaces(this string value)
        {
            return Regex.Replace(value, "(\\B[A-Z])", " $1");
        }

        public static string ToListViewText(this Enum value)
        {
            return Regex.Replace(value.ToString(), "(\\B[A-Z])", " $1");
        }
    }
}
