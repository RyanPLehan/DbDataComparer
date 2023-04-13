using System;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

namespace DbDataComparer.Domain.Utils
{
    public static class Reflection
    {
        public static T CreateInstance<T>(string fullyQualifiedName, object[] args)
        {
            Type type = Type.GetType(fullyQualifiedName);
            return CreateInstance<T>(type, args);
        }

        public static T CreateInstance<T>(object[] args)
        {
            return CreateInstance<T>(typeof(T), args);
        }

        public static T CreateInstance<T>(Type type, object[] args)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            return (T)Activator.CreateInstance(type, bindingFlags, null, args, cultureInfo);
        }

        /// <summary>
        /// This will return a list of Types/Classes that have the given attribute type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesByAttribute<T>(Assembly assembly) where T : Attribute
        {
            IList<Type> types = new List<Type>();
            
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                    types.Add(type);
            }

            return types;
        }
    }
}
