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

namespace LanguageAdapter.CSharp.L1_CollectionTExtensions
{
    /// <summary>
    /// ICollectionTExtensions
    /// </summary>
    public static class CICollectionTExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNullOrEmpty<T>(this ICollection<T> ioSource)
        {
            return (ioSource.extIsNull() || (ioSource.Count == CConst.EMPTY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNotEmpty<T>(this ICollection<T> ioSource)
        {
            return !extIsNullOrEmpty(ioSource);
        }
    }
}
