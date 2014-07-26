using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.IO;
using System.Net;
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_ArrayExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_WebRequestHelper
{
    /// <summary>
    /// WebRequestHelper
    /// </summary>
    public static class CWebRequestHelper
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;
        #endregion

        #region Singleton, factory or constructor.
        static CWebRequestHelper() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            CEnumerationHelper.Cache<StringComparison>(CStaticToolbox.throwException);
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
        /// <param name="iRequestUri"></param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static HttpWebRequest Factory(string iRequestUri, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(
                () =>
                {
                    Uri mUri = new Uri(iRequestUri); //For debug.

                    HttpWebRequest mWebRequest = (HttpWebRequest.Create(mUri) as HttpWebRequest);

                    if (iRequestUri.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                        mWebRequest.ProtocolVersion = HttpVersion.Version11;
                    }

                    mWebRequest.Timeout = ((iTimeout > CConst.ZERO) ? iTimeout : Timeout.Infinite);

                    if (ioWebHeaderCollection.extIsNotNull())
                    {
                        mWebRequest.Headers = ioWebHeaderCollection;
                    }

                    mWebRequest.AllowAutoRedirect = true;
                    mWebRequest.KeepAlive = true;
                    mWebRequest.UserAgent = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.70 Safari/533.4";
                    mWebRequest.ContentType = "application/x-www-form-urlencoded";

                    return mWebRequest;
                },
                iExceptionHandler
            ).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioWebRequest"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extGetResponseString(this HttpWebRequest ioWebRequest, Action<Exception> iExceptionHandler = null)
        {
            if (ioWebRequest.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioWebRequest.extIsNull())"));

                return string.Empty;
            }

            return CTryCatchObserver.Register(
                () =>
                {
                    using (WebResponse mWebResponse = ioWebRequest.GetResponse())
                    {
                        using (StreamReader mStreamReader = new StreamReader(mWebResponse.GetResponseStream()))
                        {
                            return mStreamReader.ReadToEnd();
                        }
                    }
                },
                iExceptionHandler
            ).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRequestUri"></param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string GetResponseString(string iRequestUri, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            HttpWebRequest mWebRequest = Factory(iRequestUri, iTimeout, ioWebHeaderCollection, iExceptionHandler);

            if (mWebRequest.extIsNull())
            {
                return string.Empty;
            }

            mWebRequest.Method = "GET";

            return extGetResponseString(mWebRequest, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioWebRequest"></param>
        /// <param name="ioParams"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extExecuteThanGettingResponseString(this HttpWebRequest ioWebRequest, byte[] ioParams, Action<Exception> iExceptionHandler = null)
        {
            if (ioWebRequest.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioWebRequest.extIsNull())"));

                return string.Empty;
            }

            return CTryCatchObserver.Register(
                () =>
                {
                    if (ioParams.extIsNullOrEmpty())
                    {
                        ioWebRequest.ContentLength = CConst.ZERO;
                    }
                    else
                    {
                        ioWebRequest.ContentLength = ioParams.Length;

                        using (Stream mStream = ioWebRequest.GetRequestStream())
                        {
                            mStream.Write(ioParams, CConst.BEGIN_INDEX, ioParams.Length);
                        }
                    }

                    using (WebResponse mWebResponse = ioWebRequest.GetResponse())
                    {
                        using (StreamReader mStreamReader = new StreamReader(mWebResponse.GetResponseStream()))
                        {
                            return mStreamReader.ReadToEnd();
                        }
                    }
                },
                iExceptionHandler
            ).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioWebRequest"></param>
        /// <param name="iParams">Encoding.UTF8.GetBytes(iParams))</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extExecuteThanGettingResponseString(this HttpWebRequest ioWebRequest, string iParams, Action<Exception> iExceptionHandler = null)
        {
            return extExecuteThanGettingResponseString(ioWebRequest, (string.IsNullOrWhiteSpace(iParams) ? null : Encoding.UTF8.GetBytes(iParams)), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRequestUri"></param>
        /// <param name="ioParams"></param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string PostThanGettingResponseString(string iRequestUri, byte[] ioParams, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            HttpWebRequest mWebRequest = Factory(iRequestUri, iTimeout, ioWebHeaderCollection, iExceptionHandler);

            if (mWebRequest.extIsNull())
            {
                return string.Empty;
            }

            mWebRequest.Method = "POST";

            return extExecuteThanGettingResponseString(mWebRequest, ioParams, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRequestUri"></param>
        /// <param name="iParams">Encoding.UTF8.GetBytes(iParams))</param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string PostThanGettingResponseString(string iRequestUri, string iParams, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            return PostThanGettingResponseString(iRequestUri, (string.IsNullOrWhiteSpace(iParams) ? null : Encoding.UTF8.GetBytes(iParams)), iTimeout, ioWebHeaderCollection, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRequestUri"></param>
        /// <param name="ioParams"></param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string PutThanGettingResponseString(string iRequestUri, byte[] ioParams, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            HttpWebRequest mWebRequest = Factory(iRequestUri, iTimeout, ioWebHeaderCollection, iExceptionHandler);

            if (mWebRequest.extIsNull())
            {
                return string.Empty;
            }

            mWebRequest.Method = "PUT";

            return extExecuteThanGettingResponseString(mWebRequest, ioParams, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRequestUri"></param>
        /// <param name="iParams">Encoding.UTF8.GetBytes(iParams))</param>
        /// <param name="iTimeout"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string PutThanGettingResponseString(string iRequestUri, string iParams, int iTimeout, WebHeaderCollection ioWebHeaderCollection = null, Action<Exception> iExceptionHandler = null)
        {
            return PutThanGettingResponseString(iRequestUri, (string.IsNullOrWhiteSpace(iParams) ? null : Encoding.UTF8.GetBytes(iParams)), iTimeout, ioWebHeaderCollection, iExceptionHandler);
        }
        #endregion
    }
}
