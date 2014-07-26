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
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_EnumerableTExtensions
{
    /// <summary>
    /// IEnumerableTExtensions
    /// </summary>
    public static class CIEnumerableTExtensions
    {
        /// <summary>
        /// 2
        /// </summary>
        public const int DEFAULT_SHUFFLING_TIMES = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, T> extFirstOrDefault<T>(this IEnumerable<T> ioSource, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(() => ioSource.First(), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iPredicate"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, T> extFirstOrDefault<T>(this IEnumerable<T> ioSource, Func<T, bool> iPredicate, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(() => ((iPredicate == null) ? ioSource.First() : ioSource.First(iPredicate)), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, T> extSingleOrDefault<T>(this IEnumerable<T> ioSource, Action<Exception> iExceptionHandler = null)
        {
            Tuple<bool, T> mSingleOrDefault = CTryCatchObserver.Register(() => ioSource.SingleOrDefault(), iExceptionHandler);

            return ((mSingleOrDefault.Item1) ? mSingleOrDefault : new Tuple<bool, T>(false, ioSource.First()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iPredicate"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, T> extSingleOrDefault<T>(this IEnumerable<T> ioSource, Func<T, bool> iPredicate, Action<Exception> iExceptionHandler = null)
        {
            Tuple<bool, T> mSingleOrDefault = CTryCatchObserver.Register(() => ((iPredicate == null) ? ioSource.SingleOrDefault() : ioSource.SingleOrDefault(iPredicate)), iExceptionHandler);

            return ((mSingleOrDefault.Item1) ? mSingleOrDefault : new Tuple<bool, T>(false, ((iPredicate == null) ? ioSource.First() : ioSource.First(iPredicate))));
        }

        private static IEnumerable<T> SkipThanTake<T>(this IEnumerable<T> ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS)
        {
            //if (iCount == CConst.EMPTY)
            //{
            //    yield break;
            //}

            //if (iBeginIndex < CConst.BEGIN_INDEX)
            //{
            //    iBeginIndex = CConst.BEGIN_INDEX;
            //}

            //if ((iBeginIndex == CConst.BEGIN_INDEX) && (iCount <= CConst.ALL_ITEMS))
            //{
            //    foreach (T mItem in ioSource)
            //    {
            //        yield return mItem;
            //    }

            //    yield break;
            //}

            //int mIndex = CConst.BEGIN_INDEX;

            //if (iCount <= CConst.ALL_ITEMS)
            //{
            //    foreach (T mItem in ioSource)
            //    {
            //        if ((mIndex++) >= iBeginIndex)
            //        {
            //            yield return mItem;
            //        }
            //    }

            //    yield break;
            //}

            //int mCount = CConst.EMPTY;

            //foreach (T mItem in ioSource)
            //{
            //    if ((mIndex++) >= iBeginIndex)
            //    {
            //        yield return mItem;

            //        if ((++mCount) == iCount)
            //        {
            //            yield break;
            //        }
            //    }
            //}

            if (iCount == CConst.EMPTY)
            {
                return new T[CConst.EMPTY];
            }
            else if (iBeginIndex < CConst.BEGIN_INDEX)
            {
                iBeginIndex = CConst.BEGIN_INDEX;
            }

            return ((iCount <= CConst.ALL_ITEMS) ? ioSource.Skip(iBeginIndex) : ioSource.Skip(iBeginIndex).Take(iCount));
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
        public static IEnumerable<T> extSkipThanTake<T>(this IEnumerable<T> ioSource, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
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
        public static IEnumerable<T> extSkipThanTake<T>(this IEnumerable<T> ioSource, Action<Exception> iExceptionHandler)
        {
            return ioSource.extSkipThanTake<T>(CConst.BEGIN_INDEX, CConst.ALL_ITEMS, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="ioRandom"></param>
        /// <param name="iShufflingTimes"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] extShuffleItems<T>(this IEnumerable<T> ioBucket, Random ioRandom, int iShufflingTimes = DEFAULT_SHUFFLING_TIMES, Action<Exception> iExceptionHandler = null)
        {
            if (ioBucket.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioBucket.extIsNull())"));

                return new T[CConst.EMPTY];
            }
            else if (ioRandom.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (ioRandom.extIsNull())"));

                return ioBucket.ToArray();
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
                int mRandomNumber = (ioRandom.Next(mHelfRight) + mHelfLeft);

                T mItem = mBucket[i];
                mBucket[i] = mBucket[mRandomNumber];
                mBucket[mRandomNumber] = mItem;
            }

            if ((--iShufflingTimes) > CConst.EMPTY)
            {
                mBucket = extShuffleItems(mBucket, ioRandom, iShufflingTimes, iExceptionHandler);
            }

            return mBucket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="ioRandom"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iShufflingTimes"></param>
        /// <returns></returns>
        public static T[] extShuffleItems<T>(this IEnumerable<T> ioBucket, Random ioRandom, Action<Exception> iExceptionHandler, int iShufflingTimes = DEFAULT_SHUFFLING_TIMES)
        {
            return extShuffleItems<T>(ioBucket, ioRandom, iShufflingTimes, iExceptionHandler);
        }
    }
}
