using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L2_2_ActionObserver
{
    /// <summary>
    /// ActionObserver (Event Observer) (Delegate Extensions)
    /// </summary>
    public static class CActionObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEventHandler"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke(this Action iEventHandler, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<U>(this Action iEventHandler, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T>(this Action<T> iEventHandler, T ioTarget, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T, U>(this Action<T> iEventHandler, T ioTarget, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2>(this Action<T1, T2> iEventHandler, T1 ioTarget1, T2 ioTarget2, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, U>(this Action<T1, T2> iEventHandler, T1 ioTarget1, T2 ioTarget2, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3>(this Action<T1, T2, T3> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, U>(this Action<T1, T2, T3> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, U>(this Action<T1, T2, T3, T4> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, U>(this Action<T1, T2, T3, T4, T5> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6, U>(this Action<T1, T2, T3, T4, T5, T6> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="ioTarget7"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="ioTarget7"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6, T7, U>(this Action<T1, T2, T3, T4, T5, T6, T7> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="ioTarget7"></param>
        /// <param name="ioTarget8"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, T8 ioTarget8, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(() => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7, ioTarget8), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iEventHandler"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="ioTarget7"></param>
        /// <param name="ioTarget8"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        public static void extInvoke<T1, T2, T3, T4, T5, T6, T7, T8, U>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> iEventHandler, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, T8 ioTarget8, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            CTryCatchObserver.Register(
                () => iEventHandler(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7, ioTarget8),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        extInvoke(iFinalHandler, iExceptionHandler, ioArgument);
                    }
                }
                );
        }
    }
}
