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
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_3_DateTimeHelper;
using LanguageAdapter.CSharp.L6_ObjectExtensions;
using LanguageAdapter.CSharp.L7_CreatedStacksObject;
#endregion

#region Set the aliases.
using CL6StaticToolbox = LanguageAdapter.CSharp.L6_StaticToolbox.CStaticToolbox;
#endregion

namespace LanguageAdapter.CSharp.L8_0_ThreadObserver
{
    /// <summary>
    /// ThreadObserver
    /// </summary>
    public static partial class CThreadObserver
    {
        /// <summary>
        /// ThreadAdapter
        /// </summary>
        public sealed class CThreadAdapter : CCreatedStacksObject
        {
            #region Fields and properties.
            private readonly Thread fTarget; //Adapter pattern.
            #endregion

            #region Singleton, factory or constructor.
            private CThreadAdapter(Thread ioTarget, SynchronizedReadOnlyCollection<string> ioCreatedStacks)
                : base(ioCreatedStacks)
            {
                if (ioTarget.extIsNull())
                {
                    throw new ArgumentNullException("if (ioThread.extIsNull())");
                }

                fTarget = ioTarget;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ioTarget"></param>
            /// <param name="ioCreatedStacks"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            internal static CThreadAdapter Factory(Thread ioTarget, SynchronizedReadOnlyCollection<string> ioCreatedStacks, Action<Exception> iExceptionHandler = null)
            {
                if (!CL6StaticToolbox.CheckAuthority(CStackFrameHelper.getModifiedStackFrameIndex(), typeof(CThreadObserver), iExceptionHandler))
                {
                    return null;
                }

                return CTryCatchObserver.Register(() => new CThreadAdapter(ioTarget, ioCreatedStacks), iExceptionHandler).Item2;
            }
            #endregion

            #region Destructor.
            /// <summary>
            /// The lifetime of this class should equal the lifetime of application.
            /// </summary>
            /// <param name="iDisposeManagedResources"></param>
            protected override void Dispose(bool iDisposeManagedResources)
            {
                //throw new NotImplementedException();
                CL6StaticToolbox.CheckAuthority(CStackFrameHelper.getModifiedStackFrameIndex(), this.GetType(), CStaticToolbox.throwException);
            }
            #endregion

            #region Methods.
            /// <summary>
            /// 
            /// </summary>
            public bool isAlive()
            {
                return fTarget.IsAlive;
            }

            /// <summary>
            /// 
            /// </summary>
            public bool? isBackground()
            {
                return (isAlive() ? (bool?)fTarget.IsBackground : null);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iIsBackground"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool? setBackground(bool iIsBackground, Action<Exception> iExceptionHandler = null)
            {
                CTryCatchObserver.Register(() => (fTarget.IsBackground = iIsBackground), iExceptionHandler);

                return isBackground();
            }

            /// <summary>
            /// 
            /// </summary>
            public bool isThreadPoolThread()
            {
                return fTarget.IsThreadPoolThread;
            }

            /// <summary>
            /// 
            /// </summary>
            public int getID()
            {
                return fTarget.ManagedThreadId;
            }

            /// <summary>
            /// 
            /// </summary>
            public string getName()
            {
                return (isAlive() ? fTarget.Name : string.Empty);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iName"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public string setName(string iName, Action<Exception> iExceptionHandler = null)
            {
                CTryCatchObserver.Register(() => (fTarget.Name = iName), iExceptionHandler);

                return getName();
            }

            /// <summary>
            /// 
            /// </summary>
            public ThreadPriority? getPriority()
            {
                return (isAlive() ? (ThreadPriority?)fTarget.Priority : null);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iPriority"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public ThreadPriority? setPriority(ThreadPriority iPriority, Action<Exception> iExceptionHandler = null)
            {
                CTryCatchObserver.Register(() => (fTarget.Priority = iPriority), iExceptionHandler);

                return getPriority();
            }

            /// <summary>
            /// 
            /// </summary>
            public ThreadState getState()
            {
                return fTarget.ThreadState;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iTimeout"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Join(TimeSpan iTimeout, Action<Exception> iExceptionHandler = null)
            {
                if (iTimeout.Ticks == CConst.EMPTY)
                {
                    iExceptionHandler.extInvoke(new ArgumentException("if (iTimeout.Ticks == CConst.EMPTY)"));

                    return false;
                }

                return CTryCatchObserver.Register(
                    () =>
                    {
                        if (iTimeout.Equals(CDateTimeHelper.getInfinite()) || (iTimeout.Ticks < CConst.EMPTY))
                        {
                            fTarget.Join();

                            return true;
                        }

                        return fTarget.Join(iTimeout);
                    },
                    iExceptionHandler
                    ).Item2;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iTimeout">Millisecond(s).</param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Join(int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
            {
                if (iTimeout == CConst.EMPTY)
                {
                    iExceptionHandler.extInvoke(new ArgumentException("if (iTimeout == CConst.EMPTY)"));

                    return false;
                }
                else if (iTimeout < CConst.EMPTY)
                {
                    iTimeout = Timeout.Infinite;
                }

                return CTryCatchObserver.Register(
                    () =>
                    {
                        if (iTimeout == Timeout.Infinite)
                        {
                            fTarget.Join();

                            return true;
                        }

                        return fTarget.Join(iTimeout);
                    },
                    iExceptionHandler
                    ).Item2;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iJoinTimeout"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Abort(TimeSpan iJoinTimeout, Action<Exception> iExceptionHandler = null)
            {
                return CTryCatchObserver.Register(
                    () =>
                    {
                        fTarget.Abort();

                        if (iJoinTimeout.Ticks == CConst.EMPTY)
                        {
                            return true;
                        }

                        return Join(iJoinTimeout, iExceptionHandler);
                    },
                    iExceptionHandler
                    ).Item2;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iJoinTimeout">Millisecond(s).</param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Abort(int iJoinTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
            {
                return CTryCatchObserver.Register(
                    () =>
                    {
                        fTarget.Abort();

                        if (iJoinTimeout == CConst.EMPTY)
                        {
                            return true;
                        }

                        return Join(iJoinTimeout, iExceptionHandler);
                    },
                    iExceptionHandler
                    ).Item2;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iWithFieldNames"></param>
            /// <param name="iDateTimeKind"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public override string[] toStrings(bool iWithFieldNames, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null)
            {
                string[] mResult = new string[]
                {
                    getCreationTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
                    getElapsedTime().ToString(),
                    isAlive().ToString(),
                    isBackground().ToString(),
                    isThreadPoolThread().ToString(),
                    getID().ToString(),
                    getName(),
                    getPriority().extToStringT(iExceptionHandler),
                    getState().extToString(iExceptionHandler),
                    string.Join(string.Empty, getCreatedStacks()),
                };

                if (iWithFieldNames)
                {
                    mResult[0] = string.Format("CreationTime: {0}", mResult[0]);
                    mResult[1] = string.Format("ElapsedTime: {0}", mResult[1]);
                    mResult[2] = string.Format("isAlive: {0}", mResult[2]);
                    mResult[3] = string.Format("isBackground: {0}", mResult[3]);
                    mResult[4] = string.Format("isThreadPoolThread: {0}", mResult[4]);
                    mResult[5] = string.Format("ID: {0}", mResult[5]);
                    mResult[6] = string.Format("Name: {0}", mResult[6]);
                    mResult[7] = string.Format("Priority: {0}", mResult[7]);
                    mResult[8] = string.Format("ThreadState: {0}", mResult[8]);
                    mResult[10] = string.Format("CreatedStacks: {0}", mResult[9]);
                }

                return mResult;
            }
            #endregion
        }
    }
}
