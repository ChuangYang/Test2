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
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
using LanguageAdapter.CSharp.L7_Box;
#endregion

#region Set the aliases.
using CL6WebRequestHelper = LanguageAdapter.CSharp.L6_WebRequestHelper.CWebRequestHelper;
#endregion

namespace LanguageAdapter.CSharp.L9_WebRequestHelper
{
    /// <summary>
    /// WebRequestHelper
    /// </summary>
    public static class CWebRequestHelper
    {
        /// <summary>
        /// 8192
        /// </summary>
        public const int DEFAULT_BUFFER_SIZE = 8192;

        #region Fields and properties.
        private static readonly DateTime fCreationTime;
        #endregion

        #region Singleton, factory or constructor.
        static CWebRequestHelper() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            CEnumerationHelper.Cache<FileMode>();
            CEnumerationHelper.Cache<FileAccess>();
            CEnumerationHelper.Cache<FileShare>();
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
        /// <param name="ioWebRequest"></param>
        /// <param name="iFilePath"></param>
        /// <param name="ioPercentage"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iBufferSize"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extUploadFileThanGettingResponseString(this HttpWebRequest ioWebRequest, string iFilePath, CBox<float> ioPercentage = null, WebHeaderCollection ioWebHeaderCollection = null, int iBufferSize = DEFAULT_BUFFER_SIZE, Action<Exception> iExceptionHandler = null)
        {
            if (ioWebRequest.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioWebRequest.extIsNull())"));

                return string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(iFilePath))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrWhiteSpace(iFilePath))"));

                return string.Empty;
            }
            else if (!File.Exists(iFilePath))
            {
                iExceptionHandler.extInvoke(new FileNotFoundException(string.Format("else if (!File.Exists({0}))", iFilePath)));

                return string.Empty;
            }

            CBox<float> mPercentage = (ioPercentage.extIsNull() ? new CBox<float>(CConst.ZERO, iExceptionHandler) : ioPercentage);

            if (mPercentage.getItem() < CConst.ZERO) //Means cancel uploading.
            {
                return string.Empty;
            }
            else if (iBufferSize <= CConst.ZERO)
            {
                iBufferSize = DEFAULT_BUFFER_SIZE;
            }

            return CTryCatchObserver.Register(
                () =>
                {
                    using (Stream mStream = ioWebRequest.GetRequestStream())
                    {
                        using (FileStream mFileStream = File.Open(iFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            byte[] mBuffer = new byte[iBufferSize];
                            int mCount = CConst.BEGIN_INDEX;

                            for (long i = (mFileStream.Position = CConst.BEGIN_INDEX); i < mFileStream.Length; i += iBufferSize)
                            {
                                if (mPercentage.getItem() < CConst.ZERO) //Means cancel uploading.
                                {
                                    break;
                                }

                                mPercentage.setItem((float)i / mFileStream.Length);

                                if ((mCount = mFileStream.Read(mBuffer, CConst.BEGIN_INDEX, iBufferSize)) > CConst.ZERO)
                                {
                                    mStream.Write(mBuffer, CConst.BEGIN_INDEX, mCount);
                                }
                                
                                if (mPercentage.getItem() < CConst.ZERO) //Means cancel uploading.
                                {
                                    break;
                                }
                            }

                            if (mPercentage.getItem() >= CConst.ZERO)
                            {
                                mPercentage.setItem(CConst.COMPLETED);
                            }
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
        /// <param name="iRequestUri"></param>
        /// <param name="iFilePath"></param>
        /// <param name="iTimeout"></param>
        /// <param name="ioPercentage"></param>
        /// <param name="ioWebHeaderCollection"></param>
        /// <param name="iBufferSize"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string UploadFileThanGettingResponseString(string iRequestUri, string iFilePath, int iTimeout, CBox<float> ioPercentage = null, WebHeaderCollection ioWebHeaderCollection = null, int iBufferSize = DEFAULT_BUFFER_SIZE, Action<Exception> iExceptionHandler = null)
        {
            HttpWebRequest mWebRequest = CL6WebRequestHelper.Factory(iRequestUri, iTimeout, ioWebHeaderCollection, iExceptionHandler);

            if (mWebRequest.extIsNull())
            {
                return string.Empty;
            }

            return CTryCatchObserver.Register(
                () =>
                {
                    mWebRequest.Method = "POST";

                    mWebRequest.AllowWriteStreamBuffering = false;
                    mWebRequest.ContentType = "application/octet-stream";
                    mWebRequest.ContentLength = (new FileInfo(iFilePath)).Length;
                    mWebRequest.SendChunked = true;

                    return extUploadFileThanGettingResponseString(mWebRequest, iFilePath, ioPercentage, ioWebHeaderCollection, iBufferSize, CStaticToolbox.throwException);
                },
                iExceptionHandler
            ).Item2;
        }
        #endregion
    }
}
