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
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_ConfigurationExtensions
{
    /// <summary>
    /// ConfigurationExtensions
    /// </summary>
    public static class CConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioConfiguration"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static KeyValueConfigurationCollection extSettings(this Configuration ioConfiguration, Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(() => ioConfiguration.AppSettings.Settings, iExceptionHandler).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioConfiguration"></param>
        /// <param name="iKey"></param>
        /// <returns></returns>
        public static bool extContains(this Configuration ioConfiguration, string iKey)
        {
            return CTryCatchObserver.Register(() => ioConfiguration.AppSettings.Settings.AllKeys.Contains(iKey), ioException => { }).Item2;
        }
    }
}
