using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Text.RegularExpressions;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L3_StaticToolbox;
using LanguageAdapter.CSharp.L3_StringHelper;
#endregion

#region Set the aliases.
using CL3StringHelper = LanguageAdapter.CSharp.L3_StringHelper.CStringHelper;
#endregion

namespace LanguageAdapter.CSharp.L4_StringHelper
{
    /// <summary>
    /// StringHelper
    /// </summary>
    public static class CStringHelper
    {
        private const string f_SEQUENTIAL_FORMAT_BEGIN = "{S";
        private const string f_SEQUENTIAL_FORMAT_PATTERN = @"\{{1,}[S|s](\}|:[^\}]*\})";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iLength"></param>
        /// <param name="iIncludeKeyword"></param>
        /// <param name="iSearchFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string Substring(ref string ioSource, string iKeyword, int iLength, bool iIncludeKeyword = false, bool iSearchFromHead = true, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(ioSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(ioSource))"), false);

                return string.Empty;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iKeyword))"), false);

                return string.Empty;
            }
            else if (iLength == CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentException("else if (iLength == CConst.EMPTY)"), false);

                return string.Empty;
            }

            int mBeginIndex = CConst.NOT_FOUND;

            if (iSearchFromHead)
            {
                if (iIncludeKeyword)
                {
                    mBeginIndex = ((iComparisonType == null) ? ioSource.IndexOf(iKeyword) : ioSource.IndexOf(iKeyword, iComparisonType.Value));
                }
                else
                {
                    mBeginIndex = CL3StringHelper.IndexOfTail(ref ioSource, iKeyword, iComparisonType, iExceptionHandler);
                }
            }
            else
            {
                if (iIncludeKeyword)
                {
                    mBeginIndex = ((iComparisonType == null) ? ioSource.LastIndexOf(iKeyword) : ioSource.LastIndexOf(iKeyword, iComparisonType.Value));
                }
                else
                {
                    mBeginIndex = CL3StringHelper.LastIndexOfTail(ref ioSource, iKeyword, iComparisonType, iExceptionHandler);
                }
            }

            if (mBeginIndex == CConst.NOT_FOUND)
            {
                return string.Empty;
            }

            Tuple<int, int> mTuple = CStaticToolbox.getModifiedBeginIndexAndCount(ioSource.Length, mBeginIndex, iLength);

            return ((mTuple.Item2 <= CConst.EMPTY) ? string.Empty : ioSource.Substring(mTuple.Item1, mTuple.Item2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iSearchFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string Substring(ref string ioSource, string iKeyword, bool iSearchFromHead = true, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref ioSource, iKeyword, ((iKeyword == null) ? CConst.EMPTY : iKeyword.Length), true, iSearchFromHead, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iLength"></param>
        /// <param name="iIncludeKeyword"></param>
        /// <param name="iSearchFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extSubstring(this string iSource, string iKeyword, int iLength, bool iIncludeKeyword = false, bool iSearchFromHead = true, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(iSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(iSource))"), false);

                return string.Empty;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iKeyword))"), false);

                return string.Empty;
            }
            else if (iLength == CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentException("else if (iLength == CConst.EMPTY)"), false);

                return string.Empty;
            }

            int mBeginIndex = CConst.NOT_FOUND;

            if (iSearchFromHead)
            {
                if (iIncludeKeyword)
                {
                    mBeginIndex = ((iComparisonType == null) ? iSource.IndexOf(iKeyword) : iSource.IndexOf(iKeyword, iComparisonType.Value));
                }
                else
                {
                    mBeginIndex = CL3StringHelper.IndexOfTail(ref iSource, iKeyword, iComparisonType, iExceptionHandler);
                }
            }
            else
            {
                if (iIncludeKeyword)
                {
                    mBeginIndex = ((iComparisonType == null) ? iSource.LastIndexOf(iKeyword) : iSource.LastIndexOf(iKeyword, iComparisonType.Value));
                }
                else
                {
                    mBeginIndex = CL3StringHelper.LastIndexOfTail(ref iSource, iKeyword, iComparisonType, iExceptionHandler);
                }
            }

            if (mBeginIndex == CConst.NOT_FOUND)
            {
                return string.Empty;
            }

            Tuple<int, int> mTuple = CStaticToolbox.getModifiedBeginIndexAndCount(iSource.Length, mBeginIndex, iLength);

            return ((mTuple.Item2 <= CConst.EMPTY) ? string.Empty : iSource.Substring(mTuple.Item1, mTuple.Item2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iSearchFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extSubstring(this string iSource, string iKeyword, bool iSearchFromHead = true, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref iSource, iKeyword, ((iKeyword == null) ? CConst.EMPTY : iKeyword.Length), true, iSearchFromHead, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// <para>|</para>
        /// <para>| string mString = "S{S}S{S:111}S";</para>
        /// <para>| mString = CStringHelper.SequentialFormat(ref mString, "AAA", "BBB", "CCC");</para>
        /// <para>| mString = "S{S}S{S:111}S".extSequentialFormat("AAA", "BBB", "CCC");</para>
        /// <para>|</para>
        /// <para>| mString = "SAAASBBBS";</para>
        /// </summary>
        /// <param name="ioFormat"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArguments"></param>
        /// <returns></returns>
        public static string SequentialFormat(ref string ioFormat, Action<Exception> iExceptionHandler, params object[] ioArguments)
        {
            if (string.IsNullOrEmpty(ioFormat))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(ioFormat))"));

                return string.Empty;
            }

            int mIndex = CConst.BEGIN_INDEX;

            ioFormat = Regex.Replace(ioFormat, f_SEQUENTIAL_FORMAT_PATTERN, ioMatch =>
            {
                int mSequentialIndex = ioMatch.Value.IndexOf(f_SEQUENTIAL_FORMAT_BEGIN, StringComparison.OrdinalIgnoreCase);

                if ((mSequentialIndex % 2) != CConst.ZERO)
                {
                    return ioMatch.Value;
                }

                return ioMatch.Value.Replace(ioMatch.Value.extSubstring(f_SEQUENTIAL_FORMAT_BEGIN, true, StringComparison.OrdinalIgnoreCase, iExceptionHandler), string.Format("{{{0}", mIndex++));
            });

            List<object> mArguments = ((ioArguments == null) ? new List<object>() : new List<object>(ioArguments));

            if (mIndex > mArguments.Count)
            {
                iExceptionHandler.extInvoke(new FormatException(string.Format("[if (mIndex > mArguments.Count)][{0}][{1}]", mIndex, mArguments.Count)), false);

                int mCount = mIndex - mArguments.Count;

                for (int i = CConst.BEGIN_INDEX; i < mCount; i++)
                {
                    mArguments.Add(null);
                }
            }

            try
            {
                return string.Format(ioFormat, mArguments.ToArray());
            }
            catch (Exception mException)
            {
                iExceptionHandler.extInvoke(mException);

                return string.Empty;
            }
            finally
            { }
        }

        /// <summary>
        /// <para>|</para>
        /// <para>| string mString = "S{S}S{S:111}S";</para>
        /// <para>| mString = CStringHelper.SequentialFormat(ref mString, "AAA", "BBB", "CCC");</para>
        /// <para>| mString = "S{S}S{S:111}S".extSequentialFormat("AAA", "BBB", "CCC");</para>
        /// <para>|</para>
        /// <para>| mString = "SAAASBBBS";</para>
        /// </summary>
        /// <param name="ioFormat"></param>
        /// <param name="ioArguments"></param>
        /// <returns></returns>
        public static string SequentialFormat(ref string ioFormat, params object[] ioArguments)
        {
            return SequentialFormat(ref ioFormat, null, ioArguments);
        }

        /// <summary>
        /// <para>|</para>
        /// <para>| string mString = "S{S}S{S:111}S";</para>
        /// <para>| mString = CStringHelper.SequentialFormat(ref mString, "AAA", "BBB", "CCC");</para>
        /// <para>| mString = "S{S}S{S:111}S".extSequentialFormat("AAA", "BBB", "CCC");</para>
        /// <para>|</para>
        /// <para>| mString = "SAAASBBBS";</para>
        /// </summary>
        /// <param name="iFormat"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArguments"></param>
        /// <returns></returns>
        public static string extSequentialFormat(this string iFormat, Action<Exception> iExceptionHandler, params object[] ioArguments)
        {
            return SequentialFormat(ref iFormat, iExceptionHandler, ioArguments);
        }

        /// <summary>
        /// <para>|</para>
        /// <para>| string mString = "S{S}S{S:111}S";</para>
        /// <para>| mString = CStringHelper.SequentialFormat(ref mString, "AAA", "BBB", "CCC");</para>
        /// <para>| mString = "S{S}S{S:111}S".extSequentialFormat("AAA", "BBB", "CCC");</para>
        /// <para>|</para>
        /// <para>| mString = "SAAASBBBS";</para>
        /// </summary>
        /// <param name="iFormat"></param>
        /// <param name="ioArguments"></param>
        /// <returns></returns>
        public static string extSequentialFormat(this string iFormat, params object[] ioArguments)
        {
            return SequentialFormat(ref iFormat, null, ioArguments);
        }
    }
}
