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
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L3_EnumerableExtensions;
using LanguageAdapter.CSharp.L3_StaticToolbox;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L4_ArrayExtensions
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
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Array extClone(this Array ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return Array.CreateInstance(typeof(object), CConst.EMPTY);
            }

            int mLength = ioSource.Length;

            if (mLength == CConst.EMPTY)
            {
                return Array.CreateInstance(typeof(object), CConst.EMPTY);
            }

            Type mItemType = ioSource.extGetItemType(iExceptionHandler);

            if (mItemType.extIsNull())
            {
                iExceptionHandler.extInvoke(new InvalidCastException("if (mItemType.extIsNull())"));

                return Array.CreateInstance(typeof(object), mLength);
            }

            Tuple<int, int> mPair = CStaticToolbox.getModifiedBeginIndexAndCount(mLength, iBeginIndex, iCount);

            Array mArray = Array.CreateInstance(mItemType, mPair.Item2);

            Array.Copy(ioSource, mPair.Item1, mArray, CConst.BEGIN_INDEX, mPair.Item2);

            return mArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Array extClone(this Array ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.extClone(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
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
        public static T[] extClone<T>(this Array ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            T[] mResult = ioSource.extClone(iBeginIndex, iCount, iExceptionHandler) as T[];

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
        public static T[] extClone<T>(this Array ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.extClone<T>(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
        }
    }
}
