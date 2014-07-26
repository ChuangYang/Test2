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
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_EnumerableExtensions
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
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Type extGetItemType(this IEnumerable ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return null;
            }

            foreach (var mCurrent in ioSource)
            {
                if (mCurrent.extIsNotNull())
                {
                    return mCurrent.GetType();
                }
            }

            iExceptionHandler.extInvoke(new ArgumentNullException("foreach (var mCurrent in ioSource)"));

            return null;
        }

        private static IEnumerable<T> SkipThanTake<T>(IEnumerable ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS)
        {
            if (iCount == CConst.EMPTY)
            {
                yield break;
            }

            if (iBeginIndex < CConst.BEGIN_INDEX)
            {
                iBeginIndex = CConst.BEGIN_INDEX;
            }

            if ((iBeginIndex == CConst.BEGIN_INDEX) && (iCount <= CConst.ALL_ITEMS))
            {
                foreach (T mItem in ioSource)
                {
                    yield return mItem;
                }

                yield break;
            }

            int mIndex = CConst.BEGIN_INDEX;

            if (iCount <= CConst.ALL_ITEMS)
            {
                foreach (T mItem in ioSource)
                {
                    if ((mIndex++) >= iBeginIndex)
                    {
                        yield return mItem;
                    }
                }

                yield break;
            }

            int mCount = CConst.EMPTY;

            foreach (T mItem in ioSource)
            {
                if ((mIndex++) >= iBeginIndex)
                {
                    yield return mItem;

                    if ((++mCount) == iCount)
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static IEnumerable<T> enumerableSkipThanTake<T>(this IEnumerable ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(() => SkipThanTake<T>(ioSource, iBeginIndex, iCount), iExceptionHandler).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static IEnumerable<T> enumerableSkipThanTake<T>(this IEnumerable ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.enumerableSkipThanTake<T>(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
        }
    }
}
