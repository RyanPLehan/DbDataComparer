using System;
using System.Reflection;
using System.Collections.Generic;


namespace DbDataComparer.Domain.Utils
{
    /// <summary>
    /// General routines regarding data entities
    /// </summary>
    public static class Entity
    {
        /// <summary>
        /// This will trim any fields that are of string data type.  This is due to the database having a char or nchar data type leaving trailing spaces
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>
        /// Using reflection, this will perform an in-place update, thus no return is needed
        /// </remarks>
        /// <returns></returns>
        public static void TrimStringFields<T>(IEnumerable<T> data)
        {
            foreach (T datum in data)
            {
                foreach (PropertyInfo prop in datum.GetType().GetProperties())
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (prop.CanWrite && type == typeof(string))
                    {
                        object value = prop.GetValue(datum);
                        prop.SetValue(datum, value?.ToString().Trim());
                    }
                }
            }
        }
    }
}
