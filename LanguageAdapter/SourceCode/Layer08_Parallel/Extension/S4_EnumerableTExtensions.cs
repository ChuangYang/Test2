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
using LanguageAdapter.CSharp.L8_3_ThreadSafeRandom;
#endregion

#region Set the aliases.
using CL3IEnumerableTExtensions = LanguageAdapter.CSharp.L3_EnumerableTExtensions.CIEnumerableTExtensions;
#endregion

namespace LanguageAdapter.CSharp.L8_4_EnumerableTExtensions
{
    /// <summary>
    /// IEnumerableTExtensions
    /// </summary>
    public static class CIEnumerableTExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="iShufflingTimes"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] extShuffleItems<T>(this IEnumerable<T> ioBucket, int iShufflingTimes = CL3IEnumerableTExtensions.DEFAULT_SHUFFLING_TIMES, Action<Exception> iExceptionHandler = null)
        {
            if (ioBucket.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioBucket.extIsNull())"));

                return new T[CConst.EMPTY];
            }

            T[] mBucket = ((ioBucket is T[]) ? (ioBucket as T[]) : ioBucket.ToArray());
            int mLength = mBucket.Length;

            if (mLength <= 1)
            {
                return mBucket;
            }
            else if (iShufflingTimes <= CConst.EMPTY)
            {
                return mBucket;
            }

            int mHelfRight = ((int)Math.Ceiling(mLength / 2.0f) + 1);
            int mHelfLeft = ((int)Math.Floor(mLength / 2.0f) - 1);

            for (int i = CConst.BEGIN_INDEX; i < mHelfRight; i++)
            {
                int mRandomNumber = (CThreadSafeRandom.Next(mHelfRight) + mHelfLeft);

                T mItem = mBucket[i];
                mBucket[i] = mBucket[mRandomNumber];
                mBucket[mRandomNumber] = mItem;
            }

            if ((--iShufflingTimes) > CConst.EMPTY)
            {
                mBucket = extShuffleItems(mBucket, iShufflingTimes, iExceptionHandler);
            }

            return mBucket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iShufflingTimes"></param>
        /// <returns></returns>
        public static T[] extShuffleItems<T>(this IEnumerable<T> ioBucket, Action<Exception> iExceptionHandler, int iShufflingTimes = CL3IEnumerableTExtensions.DEFAULT_SHUFFLING_TIMES)
        {
            return extShuffleItems<T>(ioBucket, iShufflingTimes, iExceptionHandler);
        }
    }
}
