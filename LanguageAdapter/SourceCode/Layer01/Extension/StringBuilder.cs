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
using LanguageAdapter.CSharp.L0_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_StringBuilderExtensions
{
    /// <summary>
    /// StringBuilderExtensions
    /// </summary>
    public static class CStringBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNullOrEmpty(this StringBuilder ioSource)
        {
            return (ioSource.extIsNull() || (ioSource.Length == CConst.EMPTY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNotEmpty(this StringBuilder ioSource)
        {
            return !extIsNullOrEmpty(ioSource);
        }
    }
}
