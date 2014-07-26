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
using LanguageAdapter.CSharp.L2_2_ActionObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L2_3_FuncObserver
{
    /// <summary>
    /// FuncObserver (Event Observer) (Delegate Extensions)
    /// </summary>
    public static class CFuncObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<TResult>(this Func<TResult> iFunc, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<TResult, U>(this Func<TResult> iFunc, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T, TResult>(this Func<T, TResult> iFunc, T ioTarget, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T, TResult, U>(this Func<T, TResult> iFunc, T ioTarget, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
                    }
                }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, TResult>(this Func<T1, T2, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, TResult, U>(this Func<T1, T2, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, TResult, U>(this Func<T1, T2, T3, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, TResult, U>(this Func<T1, T2, T3, T4, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5), iExceptionHandler, iFinalHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, TResult, U>(this Func<T1, T2, T3, T4, T5, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6), iExceptionHandler, iFinalHandler);
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
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, TResult, U>(this Func<T1, T2, T3, T4, T5, T6, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
        /// <param name="ioTarget1"></param>
        /// <param name="ioTarget2"></param>
        /// <param name="ioTarget3"></param>
        /// <param name="ioTarget4"></param>
        /// <param name="ioTarget5"></param>
        /// <param name="ioTarget6"></param>
        /// <param name="ioTarget7"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iFinalHandler"></param>
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7), iExceptionHandler, iFinalHandler);
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
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
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
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, T7, TResult, U>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iFunc"></param>
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
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, T8 ioTarget8, Action<Exception> iExceptionHandler, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(() => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7, ioTarget8), iExceptionHandler, iFinalHandler);
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
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="iFunc"></param>
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
        /// <returns></returns>
        public static Tuple<bool, TResult> extInvoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult, U>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> iFunc, T1 ioTarget1, T2 ioTarget2, T3 ioTarget3, T4 ioTarget4, T5 ioTarget5, T6 ioTarget6, T7 ioTarget7, T8 ioTarget8, Action<Exception, U> iExceptionHandler, U ioArgument, Action iFinalHandler = null)
        {
            return CTryCatchObserver.Register(
                () => iFunc(ioTarget1, ioTarget2, ioTarget3, ioTarget4, ioTarget5, ioTarget6, ioTarget7, ioTarget8),
                ioException => iExceptionHandler.extInvoke(ioException, ioArgument),
                () =>
                {
                    if (iFinalHandler != null)
                    {
                        iFinalHandler.extInvoke(iExceptionHandler, ioArgument);
                    }
                }
                );
        }
    }
}
