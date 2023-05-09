using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace DbDataComparer.Domain
{
    public class DatabaseTypeConverter
    {
        /// <summary>
        /// Note: The majority of the code was taken from the following GitHub
        /// https://gist.github.com/tecmaverick/858392/53ddaaa6418b943fa3a230eac49a9efe05c2d0ba
        /// </summary>
        /// <remarks>
        /// I modified the code to take advantage of current SQL server types and overlaps
        /// </remarks>
        private struct DbTypeMapEntry
        {
            public Type Type;                   // .Net Type
            public DbType DbType;               // Common data type
            public SqlDbType SqlDbType;         // MS SQL Server data type
            public bool Preferred;              // Used when a single SqlDbType maps to multiple DbType or .Net types

            public DbTypeMapEntry(Type type, DbType dbType, SqlDbType sqlDbType, bool preferred = true)
            {
                this.Type = type;
                this.DbType = dbType;
                this.SqlDbType = sqlDbType;
                this.Preferred = preferred;
            }

        };

        private static ArrayList _DbTypeList = new ArrayList();

        #region Constructors
        private DatabaseTypeConverter()
        { }

        /// <summary>
        /// Static constructors are called only once for initialization 
        /// </summary>
        /// <remarks>
        /// See: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors
        /// </remarks>
        static DatabaseTypeConverter()
        {
            DbTypeMapEntry dbTypeMapEntry
            = new DbTypeMapEntry(typeof(bool), DbType.Boolean, SqlDbType.Bit);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(byte), DbType.Byte, SqlDbType.TinyInt);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(byte[]), DbType.Binary, SqlDbType.Image, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(byte[]), DbType.Binary, SqlDbType.Binary, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(byte[]), DbType.Binary, SqlDbType.VarBinary, true);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(byte[]), DbType.Binary, SqlDbType.Timestamp, false);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(DateTime), DbType.Date, SqlDbType.Date, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(DateTime), DbType.DateTime, SqlDbType.DateTime, true);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(DateTime), DbType.DateTime2, SqlDbType.DateTime2, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(DateTime), DbType.DateTimeOffset, SqlDbType.DateTimeOffset, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(DateTime), DbType.DateTime, SqlDbType.SmallDateTime, false);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Decimal), DbType.Decimal, SqlDbType.Decimal, true);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Decimal), DbType.Currency, SqlDbType.Money, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Decimal), DbType.Currency, SqlDbType.SmallMoney, false);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(double), DbType.Double, SqlDbType.Float, true);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(double), DbType.Double, SqlDbType.Real, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Guid), DbType.Guid, SqlDbType.UniqueIdentifier);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Int16), DbType.Int16, SqlDbType.SmallInt);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Int32), DbType.Int32, SqlDbType.Int);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(Int64), DbType.Int64, SqlDbType.BigInt);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(object), DbType.Object, SqlDbType.Variant);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.AnsiStringFixedLength, SqlDbType.Char, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.StringFixedLength, SqlDbType.NChar, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.AnsiString, SqlDbType.VarChar, true);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.String, SqlDbType.NVarChar, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.AnsiString, SqlDbType.Text, false);
            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(string), DbType.String, SqlDbType.NText, false);
            _DbTypeList.Add(dbTypeMapEntry);


            dbTypeMapEntry
            = new DbTypeMapEntry(typeof(TimeSpan), DbType.Time, SqlDbType.Time); 
            _DbTypeList.Add(dbTypeMapEntry);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Convert db type to .Net data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type ToNetType(DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.Type;
        }

        /// <summary>
        /// Convert TSQL type to .Net data type
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static Type ToNetType(SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.Type;
        }

        /// <summary>
        /// Convert .Net type to Db type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.DbType;
        }

        /// <summary>
        /// Convert TSQL data type to DbType
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static DbType ToDbType(SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.DbType;
        }

        /// <summary>
        /// Convert .Net type to TSQL data type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.SqlDbType;
        }

        /// <summary>
        /// Convert DbType type to TSQL data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.SqlDbType;
        }

        private static DbTypeMapEntry Find(Type type)
        {
            object retObj = null;
            for (int i = 0; i < _DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = (DbTypeMapEntry)_DbTypeList[i];
                if (entry.Type == type && entry.Preferred)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw
                new ApplicationException("Referenced an unsupported Type");
            }

            return (DbTypeMapEntry)retObj;
        }

        private static DbTypeMapEntry Find(DbType dbType)
        {
            object retObj = null;
            for (int i = 0; i < _DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = (DbTypeMapEntry)_DbTypeList[i];
                if (entry.DbType == dbType && entry.Preferred)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw
                new ApplicationException("Referenced an unsupported DbType");
            }

            return (DbTypeMapEntry)retObj;
        }

        private static DbTypeMapEntry Find(SqlDbType sqlDbType)
        {
            object retObj = null;
            for (int i = 0; i < _DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = (DbTypeMapEntry)_DbTypeList[i];
                if (entry.SqlDbType == sqlDbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw
                new ApplicationException("Referenced an unsupported SqlDbType");
            }

            return (DbTypeMapEntry)retObj;
        }

        #endregion
    }
}
