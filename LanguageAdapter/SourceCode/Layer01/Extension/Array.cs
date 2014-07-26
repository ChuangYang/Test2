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

namespace LanguageAdapter.CSharp.L1_ArrayExtensions
{
    /// <summary>
    /// ArrayExtensions
    /// </summary>
    public static class CArrayExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNullOrEmpty(this Array ioSource)
        {
            return (ioSource.extIsNull() || (ioSource.Length == CConst.EMPTY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNotEmpty(this Array ioSource)
        {
            return !extIsNullOrEmpty(ioSource);
        }
    }
}
