using System;

namespace DbDataComparer.MSSql
{
    /// <summary>
    /// Fully Qualified Name parser routines
    /// </summary>
    internal static class FQNParser
    {
        private const char TOKEN_SEPARATOR = '.';
        public const string DEFAULT_SCHEMA = "dbo";

        /// <summary>
        /// Get Linked Server: Format: [linked server].[database].[schema].[db object]
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>Linked Server or null</returns>
        public static string GetLinkedServer(string commandName)
        {
            return GetToken(commandName, 0);
        }


        /// <summary>
        /// Get Database name: Format: [linked server].[database].[schema].[db object]
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>Database name or null</returns>
        public static string GetDatabase(string commandName)
        {
            return GetToken(commandName, 1);
        }


        /// <summary>
        /// Get Database name: Format: [linked server].[database].[schema].[db object]
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>Schema, if not available, then default schema of "dbo"</returns>
        public static string GetSchema(string commandName)
        {
            return GetToken(commandName, 2) ?? DEFAULT_SCHEMA;
        }


        /// <summary>
        /// Get Database name: Format: [linked server].[database].[schema].[db object]
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static string GetDbObject(string commandName)
        {
            return GetToken(commandName, 3);
        }

        /// <summary>
        /// Get token based upon the following format: [linked server].[database].[schema].[db object]
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="position">zero based position of token that is to be returned</param>
        /// <returns></returns>
        /// <remarks>
        /// Format: [linked server].[database].[schema].[db object]
        ///     OR: [database].[schema].[db object]
        ///     OR: [schema].[db object]
        ///     OR: [db object]
        ///     
        /// Overall Format: [optional].[optional].[optional].[mandatory]
        /// </remarks>
        private static string GetToken(string commandName, int position)
        {
            const int MAX_TOKEN_COUNT = 4;

            // Guard Clauses
            if (String.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException(nameof(commandName));

            if (position < 0 || position >= MAX_TOKEN_COUNT)
                throw new ArgumentOutOfRangeException($"{nameof(position)} must be between 0 and {MAX_TOKEN_COUNT - 1}");

            string[] defaultTokens = new string[MAX_TOKEN_COUNT];
            string[] tokens = commandName.Split(TOKEN_SEPARATOR, StringSplitOptions.None)
                                         .Reverse<string>()
                                         .ToArray();

            // Copy actual tokens into a known array size that supports all options
            // Need to ensure that we are in the mandatory object is in the last position of the default token array
            // Thus, the reason for reversing the array to make it easier to copy
            for (int i = 0; i < tokens.Length && i < MAX_TOKEN_COUNT; i++)
                defaultTokens[MAX_TOKEN_COUNT - 1 - i] = tokens[i];

            return defaultTokens[position];
        }
    }
}
