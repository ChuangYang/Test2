using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Configuration;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L3_ConfigurationExtensions;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
using LanguageAdapter.CSharp.L6_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L7_ConfigurationExtensions
{
    /// <summary>
    /// ConfigurationExtensions
    /// </summary>
    public static class CConfigurationExtensions
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;
        #endregion

        #region Singleton, factory or constructor.
        static CConfigurationExtensions() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            CEnumerationHelper.Cache<ConfigurationSaveMode>(CStaticToolbox.throwException);
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
        /// <param name="ioConfiguration"></param>
        /// <param name="iKey"></param>
        /// <param name="iValue"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extSaveValue(this Configuration ioConfiguration, string iKey, string iValue, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(
                () =>
                {
                    if (ioConfiguration.AppSettings.Settings.AllKeys.Contains(iKey))
                    {
                        ioConfiguration.AppSettings.Settings[iKey].Value = (iValue.extIsNull() ? string.Empty : iValue);
                    }
                    else
                    {
                        ioConfiguration.AppSettings.Settings.Add(iKey, (iValue.extIsNull() ? string.Empty : iValue));
                    }

                    ioConfiguration.Save(ConfigurationSaveMode.Modified);

                    return true;
                },
                iExceptionHandler
            ).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioConfiguration"></param>
        /// <param name="iKey"></param>
        /// <param name="ioValue"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extSaveValue<T>(this Configuration ioConfiguration, string iKey, T ioValue, Action<Exception> iExceptionHandler = null)
        {
            Exception mException = null;
            string mValue = ioValue.extToStringT(ioException => iExceptionHandler.extInvoke(mException = ioException));

            return (mException.extIsNull() ? extSaveValue(ioConfiguration, iKey, mValue, iExceptionHandler) : false);
        }
        #endregion
    }
}
