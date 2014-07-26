using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L5_2_StaticObject
{
    /// <summary>
    /// StaticObject
    /// </summary>
    internal static class CStaticObject //C: Class.
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;
        #endregion

        #region Singleton, factory or constructor.
        static CStaticObject() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            #region Check the input(s) and create the local parameter(s).
            #endregion

            #region Implement.
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);
            #endregion

            #region Handle the exception(s).
            #endregion

            #region Recheck the output(s).
            #endregion

            #region Return the output(s).
            #endregion
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
        #endregion
    }
}
