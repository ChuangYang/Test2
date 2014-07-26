using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L3_EnumerableTExtensions;
using LanguageAdapter.CSharp.L8_0_ThreadObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L8_3_ThreadSafeRandom
{
    /// <summary>
    /// ThreadSafeRandom
    /// </summary>
    public static partial class CThreadSafeRandom
    {
        #region Fields and properties.
        private static readonly ThreadLocal<int> fLocalSeed;
        private static readonly ThreadLocal<Random> fLocalRandom; //Adapter pattern.
        #endregion

        #region Singleton, factory or constructor.
        static CThreadSafeRandom() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            fLocalSeed = new ThreadLocal<int>(() =>
            {
                //101 - 1 - 1 = 99, 101 - 2 - 1 = 98.
                //102 - 2 - 2 = 98, 102 - 1 - 2 = 99.

                int mThreadID = CThreadObserver.Register().getID();

                return (Environment.TickCount ^ ((mThreadID << 16) - mThreadID));
            });

            fLocalRandom = new ThreadLocal<Random>(() => new Random(getLocalSeed()));
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 
        /// </summary>
        public static int getLocalSeed()
        {
            return fLocalSeed.Value;
        }

        private static Random getRandom()
        {
            return fLocalRandom.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return getRandom().Next();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int[] DistinctInts(int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iCount <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.EMPTY)", iCount)));

                return new int[CConst.EMPTY];
            }
            else if (iCount == int.MaxValue)
            {
                int[] mBucket = new int[iCount];

                for (int i = (iCount - 1); i >= CConst.BEGIN_INDEX; i--)
                {
                    mBucket[i] = i;
                }

                return mBucket.extShuffleItems(getRandom(), iExceptionHandler);
            }

            int[] mResult = new int[iCount];

            for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
            {
                int mNext = Next();

                if (mResult.Contains(mNext))
                {
                    i--;

                    continue;
                }

                mResult[i] = mNext;
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMaxValue"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Next(int iMaxValue, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(() => getRandom().Next(iMaxValue), iExceptionHandler).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMaxValue"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int[] DistinctInts(int iMaxValue, int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iMaxValue <= CConst.BEGIN_INDEX)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.BEGIN_INDEX)", iMaxValue)));

                return new int[(iCount >= CConst.EMPTY) ? iCount : CConst.EMPTY];
            }
            else if (iCount > iMaxValue)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} > {1})", iCount, iMaxValue)));

                return new int[iCount];
            }
            else if (iCount == iMaxValue)
            {
                int[] mBucket = new int[iCount];

                for (int i = (iCount - 1); i >= CConst.BEGIN_INDEX; i--)
                {
                    mBucket[i] = i;
                }

                return mBucket.extShuffleItems(getRandom(), iExceptionHandler);
            }

            int[] mResult = new int[iCount];

            for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
            {
                int mNext = getRandom().Next(iMaxValue);

                if (mResult.Contains(mNext))
                {
                    i--;

                    continue;
                }

                mResult[i] = mNext;
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMinValue"></param>
        /// <param name="iMaxValue"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Next(int iMinValue, int iMaxValue, Action<Exception> iExceptionHandler = null)
        {
            Tuple<bool, int> mNext = CTryCatchObserver.Register(() => getRandom().Next(iMinValue, iMaxValue), iExceptionHandler);

            return ((mNext.Item1) ? mNext.Item2 : iMinValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMinValue"></param>
        /// <param name="iMaxValue"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int[] DistinctInts(int iMinValue, int iMaxValue, int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iCount <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.EMPTY)", iCount)));

                return new int[CConst.EMPTY];
            }
            else if (iCount > (iMaxValue - iMinValue))
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} > ({1} - {2}))", iCount, iMaxValue, iMinValue)));

                return new int[iCount];
            }
            else if (iCount == (iMaxValue - iMinValue))
            {
                int[] mBucket = new int[iCount];

                for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
                {
                    mBucket[i] = (iMinValue + i);
                }

                return mBucket.extShuffleItems(getRandom(), iExceptionHandler);
            }

            int[] mResult = new int[iCount];

            for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
            {
                int mNext = getRandom().Next(iMinValue, iMaxValue);

                if (mResult.Contains(mNext))
                {
                    i--;

                    continue;
                }

                mResult[i] = mNext;
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioBuffer"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static byte[] NextBytes(byte[] ioBuffer, Action<Exception> iExceptionHandler = null)
        {
            Tuple<bool, byte[]> mBytes = CTryCatchObserver.Register(
                () =>
                {
                    getRandom().NextBytes(ioBuffer);
                    
                    return ioBuffer;
                },
                iExceptionHandler
                );

            return ((mBytes.Item1) ? mBytes.Item2 : ioBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static byte[] NextBytes(int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iCount <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.EMPTY)", iCount)));

                return new byte[CConst.EMPTY];
            }

            return NextBytes(new byte[iCount], iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static byte[] DistinctBytes(int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iCount <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.EMPTY)", iCount)));

                return new byte[CConst.EMPTY];
            }
            else if (iCount > (byte.MaxValue + 1))
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} > (byte.MaxValue + 1))", iCount)));

                return new byte[iCount];
            }
            else if (iCount == (byte.MaxValue + 1))
            {
                byte[] mBucket = new byte[iCount];

                for (int i = CConst.BEGIN_INDEX; i <= byte.MaxValue; i++)
                {
                    mBucket[i] = (byte)i;
                }

                return mBucket.extShuffleItems(getRandom(), iExceptionHandler);
            }

            byte[] mResult = new byte[iCount];

            for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
            {
                byte mNextByte = (byte)getRandom().Next(byte.MaxValue + 1);

                if (mResult.Contains(mNextByte))
                {
                    i--;

                    continue;
                }

                mResult[i] = mNextByte;
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
        {
            return getRandom().NextDouble();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static double[] DistinctDoubles(int iCount, Action<Exception> iExceptionHandler = null)
        {
            if (iCount <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} <= CConst.EMPTY)", iCount)));

                return new double[CConst.EMPTY];
            }

            double[] mResult = new double[iCount];

            for (int i = CConst.BEGIN_INDEX; i < iCount; i++)
            {
                double mNextDouble = NextDouble();

                if (mResult.Contains(mNextDouble))
                {
                    i--;

                    continue;
                }

                mResult[i] = mNextDouble;
            }

            return mResult;
        }
        #endregion
    }
}
