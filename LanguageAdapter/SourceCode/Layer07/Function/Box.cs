using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Data;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L5_3_DateTimeHelper;
using LanguageAdapter.CSharp.L6_BaseObject;
using LanguageAdapter.CSharp.L6_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L7_Box
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CBox<T> : CBaseObject
    {
        /// <summary>
        /// false
        /// </summary>
        public const bool READ_ONLY_FALSE = false;

        #region Fields and properties.
        private T fItem;

        private volatile bool fIsSet;
        private readonly bool fReadOnly;

        private volatile Action<Exception> fExceptionHandler;
        #endregion

        #region Singleton, factory or constructor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioItem"></param>
        /// <param name="iReadOnly"></param>
        /// <param name="iExceptionHandler"></param>
        public CBox(T ioItem, bool iReadOnly = READ_ONLY_FALSE, Action<Exception> iExceptionHandler = null) //If the constructor is protected, the singleton class can be inherited.
        {
            fItem = ioItem;
            fReadOnly = iReadOnly;
            fExceptionHandler = iExceptionHandler;

            fIsSet = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioItem"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iReadOnly"></param>
        public CBox(T ioItem, Action<Exception> iExceptionHandler, bool iReadOnly = READ_ONLY_FALSE)
            : this(ioItem, iReadOnly, iExceptionHandler) //If the constructor is protected, the singleton class can be inherited.
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iReadOnly"></param>
        /// <param name="iExceptionHandler"></param>
        public CBox(bool iReadOnly = READ_ONLY_FALSE, Action<Exception> iExceptionHandler = null) //If the constructor is protected, the singleton class can be inherited.
        {
            fReadOnly = iReadOnly;
            fExceptionHandler = iExceptionHandler;

            fIsSet = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iReadOnly"></param>
        public CBox(Action<Exception> iExceptionHandler, bool iReadOnly = READ_ONLY_FALSE)
            : this(iReadOnly, iExceptionHandler) //If the constructor is protected, the singleton class can be inherited.
        { }
        #endregion

        #region Destructor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDisposeManagedResources"></param>
        protected override void Dispose(bool iDisposeManagedResources) //i: The input parameter of the method.
        {
            if (isDisposed())
            {
                return;
            }

            base.Dispose(iDisposeManagedResources);

            if (iDisposeManagedResources)
            {
                fItem = default(T);
                fExceptionHandler = null;
            }

            //Free native resources.
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool isSet()
        {
            return fIsSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool isReadOnly()
        {
            return fReadOnly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T getItem()
        {
            return fItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioItem"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public T setItem(T ioItem, Action<Exception> iExceptionHandler = null)
        {
            if (isDisposed())
            {
                iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                return fItem;
            }
            else if (isReadOnly())
            {
                if (fIsSet)
                {
                    (iExceptionHandler ?? fExceptionHandler).extInvoke(new ReadOnlyException("else if (isReadOnly())"));

                    return fItem;
                }
            }

            fIsSet = true;

            return (fItem = ioItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Action<Exception> getExceptionHandler()
        {
            return fExceptionHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public Action<Exception> setExceptionHandler(Action<Exception> iExceptionHandler)
        {
            if (isDisposed())
            {
                iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                return iExceptionHandler;
            }

            return (fExceptionHandler = iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public override string[] toStrings(bool iWithFieldNames, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null)
        {
            Action<Exception> mExceptionHandler = (iExceptionHandler ?? fExceptionHandler);

            string[] mResult = new string[]
            {
                getCreationTime().extToSQLDateTime(iDateTimeKind, mExceptionHandler),
                getElapsedTime().ToString(),
                getDisposedTime().extToSQLDateTime(iDateTimeKind, mExceptionHandler),
                isSet().ToString(),
                isReadOnly().ToString(),
                getItem().extToStringT(mExceptionHandler),
            };

            if (iWithFieldNames)
            {
                mResult[0] = string.Format("CreationTime: {0}", mResult[0]);
                mResult[1] = string.Format("ElapsedTime: {0}", mResult[1]);
                mResult[2] = string.Format("DisposedTime: {0}", mResult[2]);
                mResult[3] = string.Format("isSet: {0}", mResult[3]);
                mResult[4] = string.Format("isReadOnly: {0}", mResult[4]);
                mResult[5] = string.Format("Item: {0}", mResult[5]);
            }

            return mResult;
        }
        #endregion
    }
}
