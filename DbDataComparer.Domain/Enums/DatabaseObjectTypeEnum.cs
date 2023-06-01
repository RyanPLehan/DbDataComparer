using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Enums
{
    /// <summary>
    /// Uniquely define the database object type that the user has choosen
    /// </summary>
    /// <remarks>
    /// 1. SQL Client/Data does not provide a unqiue type of the database object.
    /// 2. Originally I was using the System.Data.CommandTypeEnum to identify the database object
    ///    That was a mistake.  Unforunately, there was wide use of the Test Definition tool, so it got to be too complicated to change
    ///    The best option was to mimic the numeric values of the System.Data.CommandTypeEnum and add the two Functions
    ///    See https://learn.microsoft.com/en-us/dotnet/api/system.data.commandtype?view=net-7.0
    /// </remarks>
    public enum DatabaseObjectTypeEnum : int
    {
        Unknown = 0,
        View = 1,
        ScalarFunction = 2,
        TableFunction = 3,
        StoredProcedure = 4,
        Table = 512
    }
}
