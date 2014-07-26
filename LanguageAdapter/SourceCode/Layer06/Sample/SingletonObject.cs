using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.ComponentModel;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_Dispose;
using LanguageAdapter.CSharp.L1_toString;
using LanguageAdapter.CSharp.L3_StringHelper;
using LanguageAdapter.CSharp.L5_3_DateTimeHelper;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_SingletonObject
{
    /// <summary>
    /// SingletonObject
    /// </summary>
    //[Serializable] //The singleton class may not be serialized, or it can be copied deeply.
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Description("The code should be built in release mode with no warning and work in runtime mode well.")]
    internal partial class CSingletonObject : IToString, IDispose //C: Class.
    {
        #region Fields and properties.
        private readonly DateTime fCreationTime; //f: Private field or protected field of the class.

        private DateTime fDisposedTime;
        #endregion

        #region Singleton, factory or constructor.
        #region Constructor.
        private CSingletonObject() //If the constructor is protected, the singleton class can be inherited.
        {
            #region Check the input(s) and create the local parameter(s).
            #endregion

            #region Implement.
            fCreationTime = DateTime.UtcNow;
            fDisposedTime = CDateTimeHelper.getDefault();
            #endregion

            #region Handle the exception(s).
            #endregion

            #region Recheck the output(s).
            #endregion

            #region Return the output(s).
            #endregion
        }
        #endregion
        #endregion

        #region Destructor.
        /// <summary>
        /// 
        /// </summary>
        public DateTime getDisposedTime()
        {
            return fDisposedTime;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool isDisposed()
        {
            return !getDisposedTime().Equals(CDateTimeHelper.getDefault());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDisposeManagedResources"></param>
        protected virtual void Dispose(bool iDisposeManagedResources) //i: The input parameter of the method.
        {
            if (isDisposed())
            {
                return;
            }

            fDisposedTime = DateTime.UtcNow;

            if (iDisposeManagedResources)
            { }

            //Free native resources.
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        ~CSingletonObject()
        {
            Dispose(false);
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 
        /// </summary>
        public DateTime getCreationTime()
        {
            return fCreationTime;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan getElapsedTime()
        {
            return (isDisposed() ? (getDisposedTime() - getCreationTime()) : (DateTime.UtcNow - getCreationTime()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public virtual string[] toStrings(bool iWithFieldNames, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null)
        {
            string[] mResult = new string[]
            {
                getCreationTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
                getElapsedTime().ToString(),
                getDisposedTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
            };

            if (iWithFieldNames)
            {
                mResult[0] = string.Format("CreationTime: {0}", mResult[0]);
                mResult[1] = string.Format("ElapsedTime: {0}", mResult[1]);
                mResult[2] = string.Format("DisposedTime: {0}", mResult[2]);
            }

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iSeparator"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public virtual string toString(bool iWithFieldNames, string iSeparator = CConst.DEFAULT_SEPARATOR, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null)
        {
            return iSeparator.extJoin(iExceptionHandler, toStrings(iWithFieldNames, iDateTimeKind, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public string toString(Action<Exception> iExceptionHandler)
        {
            return toString(false, CConst.DEFAULT_SEPARATOR, DateTimeKind.Utc, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public sealed override string ToString()
        {
            return toString(false);
        }
        #endregion
    }
}
