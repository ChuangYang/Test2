using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections.Concurrent;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L3_TypeExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L4_EnumerationHelper
{
    /// <summary>
    /// EnumerationHelper
    /// </summary>
    public static class CEnumerationHelper
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        /// <summary>
        /// Tuple&lt;ToString, IsDefined, IsFlagEnum, Count, Index&gt;
        /// </summary>
        private static readonly ConcurrentDictionary<Enum, Tuple<string, bool, bool, int, int>> fCache;
        #endregion

        #region Singleton, factory or constructor.
        static CEnumerationHelper() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            fCreationTime = DateTime.UtcNow;
            fCache = new ConcurrentDictionary<Enum, Tuple<string, bool, bool, int, int>>();
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime getCreationTime()
        {
            return fCreationTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TimeSpan getElapsedTime()
        {
            return (DateTime.UtcNow - getCreationTime());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return fCache.Count;
        }

        /// <summary>
        /// Tuple&lt;ToString, IsDefined, IsFlagEnum, Count, Index&gt;
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Enum, Tuple<string, bool, bool, int, int>>> Watch()
        {
            foreach (KeyValuePair<Enum, Tuple<string, bool, bool, int, int>> mTarget in fCache)
            {
                yield return mTarget;
            }
        }

        private static Enum getEnumByIndex(Type ioType, int iIndex, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return null;
            }
            else if (!ioType.IsEnum)
            {
                iExceptionHandler.extInvoke(new NotSupportedException(string.Format("else if (!{0}.IsEnum)", ioType)), false);

                return null;
            }
            else if (iIndex < CConst.BEGIN_INDEX)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} < CConst.BEGIN_INDEX)", iIndex)), false);

                return null;
            }

            int mIndex = CConst.BEGIN_INDEX;

            foreach (Enum mEnum in ioType.GetEnumValues())
            {
                if ((mIndex++) == iIndex)
                {
                    return mEnum;
                }
            }

            iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(iIndex.ToString()), false);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T? getEnumByIndex<T>(int iIndex, Action<Exception> iExceptionHandler = null) where T : struct, IComparable, IFormattable, IConvertible
        {
            Enum mEnum = getEnumByIndex(typeof(T), iIndex, iExceptionHandler);

            return (mEnum.extIsNull() ? null : (T?)(object)mEnum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] getArray<T>(Action<Exception> iExceptionHandler = null) where T : struct, IComparable, IFormattable, IConvertible
        {
            Type mType = typeof(T);

            if (!mType.IsEnum)
            {
                iExceptionHandler.extInvoke(new NotSupportedException(string.Format("if (!{0}.IsEnum)", mType)), false);

                return new T[CConst.EMPTY];
            }

            return (mType.GetEnumValues() as T[]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] extGetArray<T>(this T iEnum, Action<Exception> iExceptionHandler = null) where T : struct, IComparable, IFormattable, IConvertible
        {
            return getArray<T>(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extCache(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            if (iEnum.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (iEnum.extIsNull())"));

                return false;
            }
            else if (fCache.ContainsKey(iEnum))
            {
                return true;
            }

            Type mType = iEnum.GetType();

            Array mArray = mType.GetEnumValues();

            bool mIsFlag = mType.extIsFlagEnum(iExceptionHandler);
            int mCount = mArray.Length;
            int mIndex = CConst.BEGIN_INDEX;

            foreach (Enum mEnum in mArray)
            {
                fCache[mEnum] = new Tuple<string, bool, bool, int, int>(mEnum.ToString(), true, mIsFlag, mCount, mIndex++);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Cache<T>(Action<Exception> iExceptionHandler = null) where T : struct, IComparable, IFormattable, IConvertible
        {
            Enum mEnum = getEnumByIndex(typeof(T), CConst.BEGIN_INDEX, iExceptionHandler);

            return (mEnum.extIsNull() ? false : extCache(mEnum, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns>Tuple&lt;ToString, IsDefined, IsFlagEnum, Count, Index&gt;</returns>
        private static Tuple<string, bool, bool, int, int> getCache(Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            if (iEnum.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (iEnum.extIsNull())"));

                return null;
            }

            Tuple<string, bool, bool, int, int> mResult = null;

            if (fCache.TryGetValue(iEnum, out mResult))
            {
                return mResult;
            }

            extCache(iEnum, iExceptionHandler);

            if (fCache.TryGetValue(iEnum, out mResult))
            {
                return mResult;
            }

            //!Enum.IsDefined. (out of range or has FlagsAttribute)
            Type mType = iEnum.GetType();
            Enum mFirstEnum = getEnumByIndex(mType, CConst.BEGIN_INDEX, iExceptionHandler);

            if (fCache.TryGetValue(mFirstEnum, out mResult))
            {
                return (fCache[iEnum] = new Tuple<string, bool, bool, int, int>(iEnum.ToString(), Enum.IsDefined(mType, iEnum), mResult.Item3, mResult.Item4, CConst.NOT_FOUND));
            }

            iExceptionHandler.extInvoke(new KeyNotFoundException(string.Format("if (!fCache.TryGetValue({0}, out mResult))", mFirstEnum)));

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToString(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            Tuple<string, bool, bool, int, int> mResult = getCache(iEnum, iExceptionHandler);

            return ((mResult == null) ? string.Empty : mResult.Item1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsDefined(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            Tuple<string, bool, bool, int, int> mResult = getCache(iEnum, iExceptionHandler);

            return ((mResult == null) ? false : mResult.Item2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsFlagEnum(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            Tuple<string, bool, bool, int, int> mResult = getCache(iEnum, iExceptionHandler);

            return ((mResult == null) ? false : mResult.Item3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extGetCount(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            Tuple<string, bool, bool, int, int> mResult = getCache(iEnum, iExceptionHandler);

            return ((mResult == null) ? CConst.EMPTY : mResult.Item4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnum"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extGetIndex(this Enum iEnum, Action<Exception> iExceptionHandler = null)
        {
            Tuple<string, bool, bool, int, int> mResult = getCache(iEnum, iExceptionHandler);

            return ((mResult == null) ? CConst.NOT_FOUND : mResult.Item5);
        }

        //The approved types for an enum are byte, sbyte, short, ushort, int, uint, long, or ulong.
        #region HasFlag
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="iSource"></param>
        ///// <param name="iTarget"></param>
        ///// <returns></returns>
        //public static bool extHasFlag<T>(this T iSource, T iTarget) where T : struct, IComparable, IFormattable, IConvertible
        //{
        //    Type mType = typeof(T);

        //    if (mType.IsEnum)
        //    {
        //        Tuple<string, bool, bool, int, int> mTarget = getCache(iTarget as Enum);

        //        if (mTarget == null)
        //        {
        //            return false;
        //        }

        //        mType = mType.GetEnumUnderlyingType();
        //    }

        //    //The approved types for an enum are byte, sbyte, short, ushort, int, uint, long, or ulong.
        //    if (mType.Equals(typeof(int)))
        //    {
        //        int mTarget = (int)(object)iTarget;

        //        return (((int)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(uint)))
        //    {
        //        uint mTarget = (uint)(object)iTarget;

        //        return (((uint)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(long)))
        //    {
        //        long mTarget = (long)(object)iTarget;

        //        return (((long)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(ulong)))
        //    {
        //        ulong mTarget = (ulong)(object)iTarget;

        //        return (((ulong)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(byte)))
        //    {
        //        byte mTarget = (byte)(object)iTarget;

        //        return (((byte)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(sbyte)))
        //    {
        //        sbyte mTarget = (sbyte)(object)iTarget;

        //        return (((sbyte)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(short)))
        //    {
        //        short mTarget = (short)(object)iTarget;

        //        return (((short)(object)iSource & mTarget) == mTarget);
        //    }
        //    else if (mType.Equals(typeof(ushort)))
        //    {
        //        ushort mTarget = (ushort)(object)iTarget;

        //        return (((ushort)(object)iSource & mTarget) == mTarget);
        //    }

        //    return false;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this byte iSource, byte iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this sbyte iSource, sbyte iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this short iSource, short iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this ushort iSource, ushort iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this int iSource, int iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this uint iSource, uint iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this long iSource, long iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iTarget"></param>
        /// <returns></returns>
        public static bool extHasFlag(this ulong iSource, ulong iTarget)
        {
            return ((iSource & iTarget) == iTarget);
        }
        #endregion
        #endregion
    }
}
