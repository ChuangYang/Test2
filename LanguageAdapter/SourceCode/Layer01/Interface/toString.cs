using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_Create;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_toString
{
    /// <summary>
    /// toString
    /// </summary>
    public interface IToString : ICreate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        string[] toStrings(bool iWithFieldNames, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iSeparator"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        string toString(bool iWithFieldNames, string iSeparator = CConst.DEFAULT_SEPARATOR, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        string toString(Action<Exception> iExceptionHandler = null);
    }
}
