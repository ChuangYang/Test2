using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L3_EnumerableTExtensions;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
using LanguageAdapter.CSharp.L6_ObjectExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_NetworkHelper
{
    /// <summary>
    /// NetworkHelper
    /// </summary>
    public static class CNetworkHelper
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;
        #endregion

        #region Singleton, factory or constructor.
        static CNetworkHelper() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            CEnumerationHelper.Cache<AddressFamily>(CStaticToolbox.throwException);
            CEnumerationHelper.Cache<NetworkInterfaceType>(CStaticToolbox.throwException);
            CEnumerationHelper.Cache<OperationalStatus>(CStaticToolbox.throwException);
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
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static IPAddress getLocalIP(Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(
                () => Dns.GetHostEntry(Dns.GetHostName()).AddressList.extFirstOrDefault(
                    ioIPAddress => (ioIPAddress.AddressFamily == AddressFamily.InterNetwork),
                    iExceptionHandler
                ).Item2,
                iExceptionHandler
            ).Item2;
        }

        /// <summary>
        /// Get the MAC address.
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static PhysicalAddress getMacAddress(Action<Exception> iExceptionHandler = null)
        {
            return CTryCatchObserver.Register(
                () =>
                {
                    NetworkInterface mNetworkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                        ioNetworkInterface => ((ioNetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) && (ioNetworkInterface.OperationalStatus == OperationalStatus.Up))
                    );

                    if (mNetworkInterface.extIsNotNull())
                    {
                        return mNetworkInterface.GetPhysicalAddress();
                    }
                    else if ((mNetworkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                        ioNetworkInterface => ((ioNetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) && (ioNetworkInterface.OperationalStatus == OperationalStatus.Up))
                    )).extIsNotNull())
                    {
                        return mNetworkInterface.GetPhysicalAddress();
                    }
                    else if ((mNetworkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                        ioNetworkInterface => ((ioNetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Ppp) && (ioNetworkInterface.OperationalStatus == OperationalStatus.Up))
                    )).extIsNotNull())
                    {
                        return mNetworkInterface.GetPhysicalAddress();
                    }
                    else if ((mNetworkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                        ioNetworkInterface => ((ioNetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Tunnel) && (ioNetworkInterface.OperationalStatus == OperationalStatus.Up))
                    )).extIsNotNull())
                    {
                        return mNetworkInterface.GetPhysicalAddress();
                    }

                    iExceptionHandler.extInvoke(new ArgumentNullException("if (mNetworkInterface.extIsNull())"));

                    return null;
                },
                iExceptionHandler
            ).Item2;
        }
        #endregion
    }
}
