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
using LanguageAdapter.CSharp.L3_EnumerableExtensions;
using LanguageAdapter.CSharp.L8_4_EnumerableTExtensions;
#endregion

#region Set the aliases.
using CL3IEnumerableTExtensions = LanguageAdapter.CSharp.L3_EnumerableTExtensions.CIEnumerableTExtensions;
#endregion

namespace LanguageAdapter.CSharp.L8_5_EnumerableIExtensions
{
    /// <summary>
    /// IEnumerableExtensions
    /// </summary>
    public static class CIEnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="iShufflingTimes"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] enumerableShuffleItems<T>(this IEnumerable ioBucket, int iShufflingTimes, Action<Exception> iExceptionHandler = null)
        {
            return ioBucket.enumerableSkipThanTake<T>(iExceptionHandler).extShuffleItems(iShufflingTimes, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iShufflingTimes"></param>
        /// <returns></returns>
        public static T[] enumerableShuffleItems<T>(this IEnumerable ioBucket, Action<Exception> iExceptionHandler = null, int iShufflingTimes = CL3IEnumerableTExtensions.DEFAULT_SHUFFLING_TIMES)
        {
            return enumerableShuffleItems<T>(ioBucket, iShufflingTimes, iExceptionHandler);
        }
    }
}
