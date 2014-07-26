using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.IO;
using System.Net;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_ExceptionExtensions
{
    /// <summary>
    /// ExceptionExtensions
    /// </summary>
    public static class CExceptionExtensions
    {
        /// <summary>
        /// Get the exceptions that can be ToString().
        /// </summary>
        /// <param name="ioSource"></param>
        /// <returns>The exceptions that can be ToString().</returns>
        public static IEnumerable<Exception> extGetAllExceptions(this Exception ioSource)
        {
            Exception mSource = ioSource;

            if (mSource.extIsNull()) //Exception may be overridden.
            {
                yield break;
            }

            do
            {
                string mSourceString = string.Empty;

                try //Exception may be overridden.
                {
                    mSourceString = mSource.ToString();
                }
                catch //(Exception mException)
                { }
                finally
                { }

                if (!string.IsNullOrWhiteSpace(mSourceString))
                {
                    yield return mSource;
                }

                if (mSource is AggregateException)
                {
                    AggregateException mAggregateException = (mSource as AggregateException);

                    if (mAggregateException.InnerExceptions.extIsNull())
                    {
                        continue;
                    }

                    foreach (Exception mInnerException in mAggregateException.InnerExceptions)
                    {
                        foreach (Exception mException in extGetAllExceptions(mInnerException))
                        {
                            yield return mException;
                        }
                    }
                }
            } while ((mSource = mSource.InnerException).extIsNotNull());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iWhileError"></param>
        /// <returns></returns>
        public static string extGetResponseString(this WebException ioSource, string iWhileError)
        {
            try //Exception may be overridden.
            {
                using (StreamReader mStreamReader = new StreamReader(ioSource.Response.GetResponseStream()))
                {
                    return mStreamReader.ReadToEnd();
                }
            }
            catch //(Exception mException)
            {
                return iWhileError;
            }
            finally
            { }
        }

        /// <summary>
        /// Get the exceptions that can be ToString().
        /// Then merging all the exceptions to be one string.
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iDebug"></param>
        /// <param name="iSeparator"></param>
        /// <returns></returns>
        public static string extToString(this Exception ioSource, bool iDebug = true, string iSeparator = CConst.NEW_LINE)
        {
            IEnumerable<Exception> mExceptions = extGetAllExceptions(ioSource);

            if (string.IsNullOrEmpty(iSeparator))
            {
                iSeparator = CConst.NEW_LINE;
            }

            StringBuilder mExceptionString = new StringBuilder();

            return string.Join(iSeparator, mExceptions.Select(ioException =>
            {
                mExceptionString.Clear();

                //[[ioException.GetType().Name][ioException.Message][ioException.HelpLink][ioException.StackTrace]]
                mExceptionString.AppendFormat("[[{0}]", ioException.GetType().Name);

                try //Exception may be overridden.
                {
                    if (ioException is WebException)
                    {
                        WebException mWebException = (ioException as WebException);

                        if (!string.IsNullOrWhiteSpace(mWebException.Message))
                        {
                            mExceptionString.AppendFormat("[{0}]", mWebException.Message);
                        }

                        mExceptionString.AppendFormat("[{0}]", extGetResponseString(mWebException, CConst.STRING_EMPTY));

                        if (!string.IsNullOrWhiteSpace(mWebException.HelpLink))
                        {
                            mExceptionString.AppendFormat("[{0}]", mWebException.HelpLink);
                        }

                        if (iDebug)
                        {
                            if (mWebException.Response.ResponseUri.extIsNotNull())
                            {
                                mExceptionString.AppendFormat("[{0}]", mWebException.Response.ResponseUri);
                            }

                            if (!string.IsNullOrWhiteSpace(mWebException.StackTrace))
                            {
                                mExceptionString.AppendFormat("[{0}]", mWebException.StackTrace);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ioException.Message))
                        {
                            mExceptionString.AppendFormat("[{0}]", ioException.Message);
                        }

                        if (!string.IsNullOrWhiteSpace(ioException.HelpLink))
                        {
                            mExceptionString.AppendFormat("[{0}]", ioException.HelpLink);
                        }

                        if (iDebug)
                        {
                            if (!string.IsNullOrWhiteSpace(ioException.StackTrace))
                            {
                                mExceptionString.AppendFormat("[{0}]", ioException.StackTrace);
                            }
                        }
                    }
                }
                catch //(Exception mException)
                { }
                finally
                { }

                return mExceptionString.Append("]").ToString();
            }));
        }
    }
}
