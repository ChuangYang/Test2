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
using LanguageAdapter.CSharp.L3_EnumerableExtensions;
using LanguageAdapter.CSharp.L4_ArrayExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_CollectionExtensions
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
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Array extToArray(this ICollection ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return Array.CreateInstance(typeof(object), CConst.EMPTY);
            }

            int mCount = ioSource.Count;

            if (mCount == CConst.EMPTY)
            {
                return Array.CreateInstance(typeof(object), CConst.EMPTY);
            }

            Type mItemType = ioSource.extGetItemType(iExceptionHandler);

            if (mItemType.extIsNull())
            {
                iExceptionHandler.extInvoke(new InvalidCastException("if (mItemType.extIsNull())"));

                return Array.CreateInstance(typeof(object), mCount);
            }

            Array mArray = Array.CreateInstance(mItemType, mCount);

            ioSource.CopyTo(mArray, CConst.BEGIN_INDEX);

            return (((iBeginIndex == CConst.BEGIN_INDEX) && (iCount == CConst.ALL_ITEMS)) ? mArray : mArray.extClone(iBeginIndex, iCount, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Array extToArray(this ICollection ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.extToArray(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
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
        public static T[] extToArray<T>(this ICollection ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            T[] mResult = ioSource.extToArray(iBeginIndex, iCount, iExceptionHandler) as T[];

            if (mResult.extIsNull())
            {
                iExceptionHandler.extInvoke(new InvalidCastException("if (mResult.extIsNull())"));

                return new T[CConst.EMPTY];
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] extToArray<T>(this ICollection ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.extToArray<T>(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
        }
    }
}
