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
using LanguageAdapter.CSharp.L0_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_EnumerableExtensions
{
    /// <summary>
    /// IEnumerableExtensions
    /// </summary>
    public static class CIEnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool enumerableIsNullOrEmpty(this IEnumerable ioSource)
        {
            if (ioSource.extIsNull())
            {
                return true;
            }

            foreach (var mElement in ioSource)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool enumerableIsNotEmpty(this IEnumerable ioSource)
        {
            return enumerableIsNullOrEmpty(ioSource);
        }
    }
}
