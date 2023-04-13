using System;
using System.Collections.Generic;
using System.Linq;


namespace DbDataComparer.Domain.Utils
{
    public class Enum
    {
        private static string ARG_EXCEPTION_MSG = "T must be an enumerated type";

        private static void CheckType<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(ARG_EXCEPTION_MSG);
        }

        public static KeyValuePair<int, string> ToKeyValuePair<T>(T enumValue) where T : struct, IConvertible
        {
            CheckType<T>();
            return new KeyValuePair<int, string>(Convert.ToInt32(enumValue), enumValue.ToString());
        }


        public static IEnumerable<T> ToList<T>() where T : struct, IConvertible
        {
            CheckType<T>();
            return System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }


        public static IEnumerable<KeyValuePair<int, string>> ToKeyValuePairList<T>() where T : struct, IConvertible
        {
            IEnumerable<T> enumList = Enum.ToList<T>();
            IList<KeyValuePair<int, string>> ret = new List<KeyValuePair<int, string>>();

            ret = (
                    from x in enumList
                    orderby Convert.ToInt32(x)
                    select ToKeyValuePair<T>(x)
                  ).ToList();

            return ret;
        }


        /// <summary>
        /// Covert string to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(string value) where T : struct, IConvertible
        {
            CheckType<T>();
            T ret;
            if (!System.Enum.TryParse<T>(value, true, out ret))
                throw new InvalidOperationException();
            return ret;
        }


        /// <summary>
        /// Covert int to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kvp"></param>
        /// <returns></returns>
        public static T ToEnum<T>(int key) where T : struct, IConvertible
        {
            CheckType<T>();

            T ret;
            if (System.Enum.IsDefined(typeof(T), key))
                ret = (T)System.Enum.ToObject(typeof(T), key);
            else
                throw new InvalidOperationException();

            return ret;
        }


        /// <summary>
        /// Covert int to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kvp"></param>
        /// <returns></returns>
        public static T ToEnum<T>(int key, T defaultValue) where T : struct, IConvertible
        {
            CheckType<T>();

            T ret = defaultValue;
            if (System.Enum.IsDefined(typeof(T), key))
                ret = (T)System.Enum.ToObject(typeof(T), key);

            return ret;
        }


        /// <summary>
        /// Covert KeyValuePair to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kvp"></param>
        /// <returns></returns>
        public static T ToEnum<T>(KeyValuePair<int, string> keyValuePair) where T : struct, IConvertible
        {
            return ToEnum<T>(keyValuePair.Key);
        }


        /// <summary>
        /// Covert KeyValuePair to Enum
        /// </summary>
        /// <remarks>
        /// Does not work if Enum has Flags Attribute
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="kvp"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(KeyValuePair<int, string> keyValuePair, T defaultValue) where T : struct, IConvertible
        {
            return ToEnum(keyValuePair.Key, defaultValue);
        }
    }
}
