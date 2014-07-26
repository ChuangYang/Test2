using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L2_1_TryCatchObserver
{
    /// <summary>
    /// TryCatchObserver
    /// </summary>
    public static class CTryCatchObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTry"></param>
        /// <param name="iException"></param>
        /// <param name="iFinally"></param>
        public static void Register(Action iTry, Action<Exception> iException = null, Action iFinally = null)
        {
            try
            {
                iTry();
            }
            catch (Exception mException)
            {
                iException.extInvoke(mException);
            }
            finally
            {
                if (iFinally != null)
                {
                    Register(iFinally, iException);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTry"></param>
        /// <param name="iFinally"></param>
        /// <param name="iException"></param>
        public static void Register(Action iTry, Action iFinally, Action<Exception> iException = null)
        {
            Register(iTry, iException, iFinally);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTryLock"></typeparam>
        /// <param name="ioSyncRoot"></param>
        /// <param name="iTry"></param>
        /// <param name="iException"></param>
        /// <param name="iFinally"></param>
        /// <returns></returns>
        public static bool Register<TTryLock>(TTryLock ioSyncRoot, Action iTry, Action<Exception> iException = null, Action iFinally = null)
        {
            if (ioSyncRoot.extIsNull())
            {
                iException.extInvoke(new ArgumentNullException("if (ioTryLock.extIsNull())"));

                return false;
            }
            else if (!Monitor.TryEnter(ioSyncRoot))
            {
                return false;
            }

            try
            {
                iTry();

                return true;
            }
            catch (Exception mException)
            {
                iException.extInvoke(mException);

                return false;
            }
            finally
            {
                if (iFinally != null)
                {
                    Register(iFinally, iException);
                }

                Register(() => Monitor.Exit(ioSyncRoot), iException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTryLock"></typeparam>
        /// <param name="ioSyncRoot"></param>
        /// <param name="iTry"></param>
        /// <param name="iFinally"></param>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static bool Register<TTryLock>(TTryLock ioSyncRoot, Action iTry, Action iFinally, Action<Exception> iException = null)
        {
            return Register(ioSyncRoot, iTry, iException, iFinally);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iTry"></param>
        /// <param name="iException"></param>
        /// <param name="iFinally"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> Register<TResult>(Func<TResult> iTry, Action<Exception> iException = null, Action iFinally = null)
        {
            try
            {
                return new Tuple<bool, TResult>(true, iTry());
            }
            catch (Exception mException)
            {
                iException.extInvoke(mException);

                return new Tuple<bool, TResult>(false, default(TResult));
            }
            finally
            {
                if (iFinally != null)
                {
                    Register(iFinally, iException);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iTry"></param>
        /// <param name="iFinally"></param>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> Register<TResult>(Func<TResult> iTry, Action iFinally, Action<Exception> iException = null)
        {
            return Register(iTry, iException, iFinally);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTryLock"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ioSyncRoot"></param>
        /// <param name="iTry"></param>
        /// <param name="iException"></param>
        /// <param name="iFinally"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> Register<TTryLock, TResult>(TTryLock ioSyncRoot, Func<TResult> iTry, Action<Exception> iException = null, Action iFinally = null)
        {
            if (ioSyncRoot.extIsNull())
            {
                iException.extInvoke(new ArgumentNullException("if (ioTryLock.extIsNull())"));

                return new Tuple<bool, TResult>(false, default(TResult));
            }
            else if (!Monitor.TryEnter(ioSyncRoot))
            {
                return new Tuple<bool, TResult>(false, default(TResult));
            }

            try
            {
                return new Tuple<bool, TResult>(true, iTry());
            }
            catch (Exception mException)
            {
                iException.extInvoke(mException);

                return new Tuple<bool, TResult>(false, default(TResult));
            }
            finally
            {
                if (iFinally != null)
                {
                    Register(iFinally, iException);
                }

                Register(() => Monitor.Exit(ioSyncRoot), iException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTryLock"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ioSyncRoot"></param>
        /// <param name="iTry"></param>
        /// <param name="iFinally"></param>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> Register<TTryLock, TResult>(TTryLock ioSyncRoot, Func<TResult> iTry, Action iFinally, Action<Exception> iException = null)
        {
            return Register(ioSyncRoot, iTry, iException, iFinally);
        }
    }
}
