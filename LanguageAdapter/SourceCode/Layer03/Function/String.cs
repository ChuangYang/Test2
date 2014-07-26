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
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_StringHelper
{
    /// <summary>
    /// StringHelper
    /// </summary>
    public static class CStringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool isTrue(ref string ioSource, Action<Exception> iExceptionHandler = null)
        {
            try
            {
                return (ioSource.Equals("true", StringComparison.OrdinalIgnoreCase) || ioSource.Equals("1", StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception mException)
            {
                iExceptionHandler.extInvoke(mException);

                return false;
            }
            finally
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsTrue(this string iSource, Action<Exception> iExceptionHandler = null)
        {
            return isTrue(ref iSource, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool isFalse(ref string ioSource, Action<Exception> iExceptionHandler = null)
        {
            return !isTrue(ref ioSource, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsFalse(this string iSource, Action<Exception> iExceptionHandler = null)
        {
            return isFalse(ref iSource, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Guid toGUID(ref string ioSource, Action<Exception> iExceptionHandler = null)
        {
            try
            {
                return new Guid(ioSource);
            }
            catch (Exception mException)
            {
                iExceptionHandler.extInvoke(mException);

                return Guid.Empty;
            }
            finally
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Guid extToGUID(this string iSource, Action<Exception> iExceptionHandler = null)
        {
            return toGUID(ref iSource, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSeparator"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioValues"></param>
        /// <returns></returns>
        public static string extJoin(this string iSeparator, Action<Exception> iExceptionHandler, params string[] ioValues)
        {
            if (string.IsNullOrEmpty(iSeparator))
            {
                iSeparator = CConst.DEFAULT_SEPARATOR;
            }

            try
            {
                return (iSeparator.Equals(CConst.DEFAULT_SEPARATOR) ? string.Format(CConst.DEFAULT_STRING_BUCKET, string.Join(iSeparator, ioValues)) : string.Join(iSeparator, ioValues));
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
        /// 
        /// </summary>
        /// <param name="iSeparator"></param>
        /// <param name="ioValues"></param>
        /// <returns></returns>
        public static string extJoin(this string iSeparator, params string[] ioValues)
        {
            return extJoin(iSeparator, null, ioValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int IndexOfTail(ref string ioSource, string iKeyword, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(ioSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(ioSource))"), false);

                return CConst.NOT_FOUND;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iTarget))"), false);

                return CConst.NOT_FOUND;
            }

            int mIndex = ((iComparisonType == null) ? ioSource.IndexOf(iKeyword) : ioSource.IndexOf(iKeyword, iComparisonType.Value));

            return ((mIndex == CConst.NOT_FOUND) ? mIndex : (mIndex + iKeyword.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extIndexOfTail(this string iSource, string iKeyword, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(iSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(iSource))"), false);

                return CConst.NOT_FOUND;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iTarget))"), false);

                return CConst.NOT_FOUND;
            }

            int mIndex = ((iComparisonType == null) ? iSource.IndexOf(iKeyword) : iSource.IndexOf(iKeyword, iComparisonType.Value));

            return ((mIndex == CConst.NOT_FOUND) ? mIndex : (mIndex + iKeyword.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int LastIndexOfTail(ref string ioSource, string iKeyword, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(ioSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(ioSource))"), false);

                return CConst.NOT_FOUND;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iTarget))"), false);

                return CConst.NOT_FOUND;
            }

            int mIndex = ((iComparisonType == null) ? ioSource.LastIndexOf(iKeyword) : ioSource.LastIndexOf(iKeyword, iComparisonType.Value));

            return ((mIndex == CConst.NOT_FOUND) ? mIndex : (mIndex + iKeyword.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extLastIndexOfTail(this string iSource, string iKeyword, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(iSource))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(iSource))"), false);

                return CConst.NOT_FOUND;
            }
            else if (string.IsNullOrEmpty(iKeyword))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrEmpty(iTarget))"), false);

                return CConst.NOT_FOUND;
            }

            int mIndex = ((iComparisonType == null) ? iSource.LastIndexOf(iKeyword) : iSource.LastIndexOf(iKeyword, iComparisonType.Value));

            return ((mIndex == CConst.NOT_FOUND) ? mIndex : (mIndex + iKeyword.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iSearchBeginFromHead"></param>
        /// <param name="iEnd"></param>
        /// <param name="iSearchEndFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string Substring(ref string ioSource, string iBegin, bool iSearchBeginFromHead, string iEnd, bool iSearchEndFromHead, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            int mBeginIndex = CConst.NOT_FOUND;
            int mEndIndex = CConst.NOT_FOUND;

            if ((mBeginIndex = ((iSearchBeginFromHead) ? IndexOfTail(ref ioSource, iBegin, iComparisonType, iExceptionHandler) : LastIndexOfTail(ref ioSource, iBegin, iComparisonType, iExceptionHandler))) == CConst.NOT_FOUND)
            {
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(iEnd))
            {
                return ioSource.Substring(mBeginIndex);
            }

            string mBegin = ioSource.Substring(mBeginIndex);

            if (iComparisonType == null)
            {
                mEndIndex = ((iSearchEndFromHead) ? mBegin.IndexOf(iEnd) : mBegin.LastIndexOf(iEnd));
            }
            else
            {
                mEndIndex = ((iSearchEndFromHead) ? mBegin.IndexOf(iEnd, iComparisonType.Value) : mBegin.LastIndexOf(iEnd, iComparisonType.Value));
            }

            return ((mEndIndex == CConst.NOT_FOUND) ? mBegin : mBegin.Substring(CConst.BEGIN_INDEX, mEndIndex));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iSearchBeginFromHead"></param>
        /// <param name="iEnd"></param>
        /// <param name="iSearchEndFromHead"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extSubstring(this string iSource, string iBegin, bool iSearchBeginFromHead, string iEnd, bool iSearchEndFromHead, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            int mBeginIndex = CConst.NOT_FOUND;
            int mEndIndex = CConst.NOT_FOUND;

            if ((mBeginIndex = ((iSearchBeginFromHead) ? IndexOfTail(ref iSource, iBegin, iComparisonType, iExceptionHandler) : LastIndexOfTail(ref iSource, iBegin, iComparisonType, iExceptionHandler))) == CConst.NOT_FOUND)
            {
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(iEnd))
            {
                return iSource.Substring(mBeginIndex);
            }

            string mBegin = iSource.Substring(mBeginIndex);

            if (iComparisonType == null)
            {
                mEndIndex = ((iSearchEndFromHead) ? mBegin.IndexOf(iEnd) : mBegin.LastIndexOf(iEnd));
            }
            else
            {
                mEndIndex = ((iSearchEndFromHead) ? mBegin.IndexOf(iEnd, iComparisonType.Value) : mBegin.LastIndexOf(iEnd, iComparisonType.Value));
            }

            return ((mEndIndex == CConst.NOT_FOUND) ? mBegin : mBegin.Substring(CConst.BEGIN_INDEX, mEndIndex));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iEnd"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string FirstSubstring(ref string ioSource, string iBegin, string iEnd, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref ioSource, iBegin, true, iEnd, true, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iEnd"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extFirstSubstring(this string iSource, string iBegin, string iEnd, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref iSource, iBegin, true, iEnd, true, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iEnd"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string LastSubstring(ref string ioSource, string iBegin, string iEnd, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref ioSource, iBegin, false, iEnd, false, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iBegin"></param>
        /// <param name="iEnd"></param>
        /// <param name="iComparisonType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extLastSubstring(this string iSource, string iBegin, string iEnd, StringComparison? iComparisonType = null, Action<Exception> iExceptionHandler = null)
        {
            return Substring(ref iSource, iBegin, false, iEnd, false, iComparisonType, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="ioEscapes"></param>
        /// <param name="ioReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string toValidString(ref string ioSource, Regex ioEscapes, ref string ioReplacement, Action<Exception> iExceptionHandler = null)
        {
            try
            {
                return ioEscapes.Replace(ioSource, ioReplacement);
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
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="ioInvalidChars"></param>
        /// <param name="ioReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string toValidString(ref string ioSource, ref string ioInvalidChars, ref string ioReplacement, Action<Exception> iExceptionHandler = null)
        {
            if (string.IsNullOrEmpty(ioInvalidChars))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (string.IsNullOrEmpty(ioInvalidChars))"));

                return string.Empty;
            }

            return toValidString(ref ioSource, new Regex(string.Format("[{0}]", Regex.Escape(ioInvalidChars))), ref ioReplacement, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="ioInvalidChars"></param>
        /// <param name="ioReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string toValidString(ref string ioSource, IEnumerable<char> ioInvalidChars, ref string ioReplacement, Action<Exception> iExceptionHandler = null)
        {
            if (ioInvalidChars.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioInvalidChars.extIsNull())"));

                return string.Empty;
            }

            string mInvalidChars = new string((ioInvalidChars is char[]) ? (ioInvalidChars as char[]) : ioInvalidChars.ToArray());

            return toValidString(ref ioSource, ref mInvalidChars, ref ioReplacement, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="ioEscapes"></param>
        /// <param name="iReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToValidString(this string iSource, Regex ioEscapes, string iReplacement, Action<Exception> iExceptionHandler = null)
        {
            return toValidString(ref iSource, ioEscapes, ref iReplacement, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iInvalidChars"></param>
        /// <param name="iReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToValidString(this string iSource, string iInvalidChars, string iReplacement, Action<Exception> iExceptionHandler = null)
        {
            return toValidString(ref iSource, ref iInvalidChars, ref iReplacement, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="ioInvalidChars"></param>
        /// <param name="iReplacement"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToValidString(this string iSource, IEnumerable<char> ioInvalidChars, string iReplacement, Action<Exception> iExceptionHandler = null)
        {
            return toValidString(ref iSource, ioInvalidChars, ref iReplacement, iExceptionHandler);
        }
    }
}
