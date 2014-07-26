using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_CollectionExtensions
{
    /// <summary>
    /// ICollectionExtensions
    /// </summary>
    public static class CICollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNullOrEmpty(this ICollection ioSource)
        {
            return (ioSource.extIsNull() || (ioSource.Count == CConst.EMPTY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNotEmpty(this ICollection ioSource)
        {
            return !extIsNullOrEmpty(ioSource);
        }
    }
}
