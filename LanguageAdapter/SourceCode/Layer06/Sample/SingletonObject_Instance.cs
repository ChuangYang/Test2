using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_SingletonObject
{
    /// <summary>
    /// SingletonObject
    /// </summary>
    internal partial class CSingletonObject
    {
        #region Singleton, factory or constructor.
        #region Create the thread-safe singleton class.
        //private static volatile CSingletonObject fInstance = null;
        //private static readonly object fSyncRoot = new Object();
        private static readonly Lazy<CSingletonObject> fInstance = new Lazy<CSingletonObject>(() => new CSingletonObject());
        #endregion

        #region Get the thread-safe singleton class.
        /// <summary>
        /// Is the thread-safe singleton class created?
        /// </summary>
        public static bool isCreated()
        {
            //return (fInstance != null);
            return fInstance.IsValueCreated;
        }

        /// <summary>
        /// Get the thread-safe singleton class.
        /// </summary>
        public static CSingletonObject getInstance() //Lazy initialization.
        {
            //if (fInstance == null)
            //{
            //    lock (fSyncRoot) //Double-Check Locking
            //    {
            //        if (fInstance == null)
            //        {
            //            fInstance = new CSingletonObject();
            //        }
            //    }
            //}

            //return fInstance;
            return fInstance.Value;
        }
        #endregion
        #endregion
    }
}
