using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L1_StaticToolbox;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L2_0_ExceptionObserver
{
    /// <summary>
    /// ExceptionObserver (Event Observer) (Delegate Extensions)
    /// </summary>
    public static class CExceptionObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioException"></param>
        /// <param name="iThrowException"></param>
        public static void extInvoke(this Action<Exception> iExceptionHandler, Exception ioException, bool iThrowException = true)
        {
            if (iExceptionHandler == CStaticToolbox.throwException)
            {
                if (iThrowException)
                {
                    iExceptionHandler(ioException);

                    return;
                }

                iExceptionHandler = CStaticToolbox.getDefaultExceptionHandler();
            }
            else if (iExceptionHandler == null)
            {
                iExceptionHandler = CStaticToolbox.getDefaultExceptionHandler();
            }

            if (iExceptionHandler == null)
            {
                if (iThrowException)
                {
                    throw ioException;
                }

                return;
            }

            iExceptionHandler(ioException);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioException"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iThrowException"></param>
        public static void extInvoke<T>(this Action<Exception, T> iExceptionHandler, Exception ioException, T ioArgument, bool iThrowException = true)
        {
            if (iExceptionHandler == null)
            {
                if (iThrowException)
                {
                    throw ioException;
                }
                else if (CStaticToolbox.getDefaultExceptionHandler() != null)
                {
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), new ArgumentNullException("else if (CStaticToolbox.getDefaultExceptionHandler() != null)"), iThrowException);
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), ioException, iThrowException);
                }

                return;
            }

            iExceptionHandler(ioException, ioArgument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioException"></param>
        /// <param name="iThrowException"></param>
        /// <returns></returns>
        public static TResult extInvoke<TResult>(this Func<Exception, TResult> iExceptionHandler, Exception ioException, bool iThrowException = true)
        {
            if (iExceptionHandler == null)
            {
                if (iThrowException)
                {
                    throw ioException;
                }
                else if (CStaticToolbox.getDefaultExceptionHandler() != null)
                {
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), new ArgumentNullException("else if (CStaticToolbox.getDefaultExceptionHandler() != null)"), iThrowException);
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), ioException, iThrowException);
                }
            }

            return iExceptionHandler(ioException);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="iExceptionHandler"></param>
        /// <param name="ioException"></param>
        /// <param name="ioArgument"></param>
        /// <param name="iThrowException"></param>
        /// <returns></returns>
        public static TResult extInvoke<T, TResult>(this Func<Exception, T, TResult> iExceptionHandler, Exception ioException, T ioArgument, bool iThrowException = true)
        {
            if (iExceptionHandler == null)
            {
                if (iThrowException)
                {
                    throw ioException;
                }
                else if (CStaticToolbox.getDefaultExceptionHandler() != null)
                {
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), new ArgumentNullException("else if (CStaticToolbox.getDefaultExceptionHandler() != null)"), iThrowException);
                    extInvoke(CStaticToolbox.getDefaultExceptionHandler(), ioException, iThrowException);
                }
            }

            return iExceptionHandler(ioException, ioArgument);
        }
    }
}
