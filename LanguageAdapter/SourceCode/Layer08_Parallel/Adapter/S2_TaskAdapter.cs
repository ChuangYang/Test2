using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Threading;
using System.Threading.Tasks;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_3_DateTimeHelper;
using LanguageAdapter.CSharp.L6_ObjectExtensions;
using LanguageAdapter.CSharp.L7_CreatedStacksObject;
using LanguageAdapter.CSharp.L8_0_ThreadObserver;
#endregion

#region Set the aliases.
using CL6StaticToolbox = LanguageAdapter.CSharp.L6_StaticToolbox.CStaticToolbox;
using CThreadAdapter = LanguageAdapter.CSharp.L8_0_ThreadObserver.CThreadObserver.CThreadAdapter;
#endregion

namespace LanguageAdapter.CSharp.L8_2_TaskObserver
{
    /// <summary>
    /// TaskObserver
    /// </summary>
    public static partial class CTaskObserver
    {
        /// <summary>
        /// TaskAdapter
        /// </summary>
        public sealed class CTaskAdapter : CCreatedStacksObject
        {
            #region Fields and properties.
            private volatile Task fTarget; //Adapter pattern.

            private readonly int fID;

            private DateTime fStartTime;
            private DateTime fEndTime;

            private volatile int fWorkingThreadID;
            #endregion

            #region Singleton, factory or constructor.
            private CTaskAdapter(Task ioTarget, SynchronizedReadOnlyCollection<string> ioCreatedStacks)
                : base(ioCreatedStacks)
            {
                if (ioTarget.extIsNull())
                {
                    throw new ArgumentNullException("if (ioThread.extIsNull())");
                }

                fTarget = ioTarget;
                fID = ioTarget.Id;

                fStartTime = CDateTimeHelper.getDefault();
                fEndTime = CDateTimeHelper.getDefault();

                fWorkingThreadID = CConst.NOT_FOUND;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ioTarget"></param>
            /// <param name="ioCreatedStacks"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            internal static CTaskAdapter Factory(Task ioTarget, SynchronizedReadOnlyCollection<string> ioCreatedStacks, Action<Exception> iExceptionHandler = null)
            {
                if (!CL6StaticToolbox.CheckAuthority(CStackFrameHelper.getModifiedStackFrameIndex(), typeof(CTaskObserver), iExceptionHandler))
                {
                    return null;
                }

                return CTryCatchObserver.Register(() => new CTaskAdapter(ioTarget, ioCreatedStacks), iExceptionHandler).Item2;
            }
            #endregion

            #region Destructor.
            /// <summary>
            /// 
            /// </summary>
            /// <param name="iDisposeManagedResources"></param>
            protected override void Dispose(bool iDisposeManagedResources)
            {
                if (isDisposed())
                {
                    return;
                }

                CL6StaticToolbox.CheckAuthority(2, typeof(CTaskObserver), CStaticToolbox.throwException);

                base.Dispose(iDisposeManagedResources);

                if (iDisposeManagedResources)
                {
                    fTarget.Dispose();
                    fTarget = null;
                }

                //Free native resources.
            }
            #endregion

            #region Methods.
            /// <summary>
            /// 
            /// </summary>
            public TaskCreationOptions? getCreationOptions()
            {
                return (fTarget.extIsNull() ? null : (TaskCreationOptions?)fTarget.CreationOptions);
            }

            /// <summary>
            /// 
            /// </summary>
            public AggregateException getException()
            {
                return (fTarget.extIsNull() ? null : fTarget.Exception);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public int getID()
            {
                return fID;
            }

            /// <summary>
            /// 
            /// </summary>
            public bool? isCanceled()
            {
                return (fTarget.extIsNull() ? null : (bool?)fTarget.IsCanceled);
            }

            /// <summary>
            /// 
            /// </summary>
            public bool? isCompleted()
            {
                return (fTarget.extIsNull() ? null : (bool?)fTarget.IsCompleted);
            }

            /// <summary>
            /// 
            /// </summary>
            public bool? isFaulted()
            {
                return (fTarget.extIsNull() ? null : (bool?)fTarget.IsFaulted);
            }

            /// <summary>
            /// 
            /// </summary>
            public TaskStatus? getStatus()
            {
                return (fTarget.extIsNull() ? null : (TaskStatus?)fTarget.Status);
            }

            /// <summary>
            /// 
            /// </summary>
            public DateTime getStartTime()
            {
                return fStartTime;
            }

            /// <summary>
            /// 
            /// </summary>
            public DateTime getEndTime()
            {
                return fEndTime;
            }

            /// <summary>
            /// 
            /// </summary>
            public CThreadAdapter getWorkingThread()
            {
                return (CThreadObserver.Contains(fWorkingThreadID) ? CThreadObserver.getThread(fWorkingThreadID) : null);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iStartTime"></param>
            /// <param name="iExceptionHandler"></param>
            internal void Start(DateTime iStartTime, Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return;
                }
                else if (!CL6StaticToolbox.CheckAuthority(CStackFrameHelper.getModifiedStackFrameIndex(), typeof(CTaskObserver), iExceptionHandler))
                {
                    return;
                }
                else if (!fStartTime.Equals(CDateTimeHelper.getDefault()))
                {
                    iExceptionHandler.extInvoke(new MethodAccessException(string.Format("if (!{0}.Equals({1}))", fStartTime, CDateTimeHelper.getDefault())));

                    return;
                }
                else if (iStartTime.Equals(CDateTimeHelper.getDefault()))
                {
                    iExceptionHandler.extInvoke(new ArgumentException(string.Format("if ({0}.Equals({1}))", iStartTime, CDateTimeHelper.getDefault())));

                    return;
                }

                fStartTime = iStartTime;
                fWorkingThreadID = CThreadObserver.Register().getID();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iExceptionHandler"></param>
            internal void End(Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return;
                }
                else if (!CL6StaticToolbox.CheckAuthority(CStackFrameHelper.getModifiedStackFrameIndex(), typeof(CTaskObserver), iExceptionHandler))
                {
                    return;
                }
                else if (!fEndTime.Equals(CDateTimeHelper.getDefault()))
                {
                    iExceptionHandler.extInvoke(new MethodAccessException(string.Format("if (!{0}.Equals({1}))", fEndTime, CDateTimeHelper.getDefault())));

                    return;
                }

                fEndTime = DateTime.UtcNow;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iTimeout"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Join(TimeSpan iTimeout, Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return false;
                }

                CThreadAdapter mWorkingThread = getWorkingThread();

                return (mWorkingThread.extIsNull() ? false : mWorkingThread.Join(iTimeout, iExceptionHandler));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iTimeout">Millisecond(s).</param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Join(int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return false;
                }

                CThreadAdapter mWorkingThread = getWorkingThread();

                return (mWorkingThread.extIsNull() ? false : mWorkingThread.Join(iTimeout, iExceptionHandler));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iJoinTimeout"></param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Abort(TimeSpan iJoinTimeout, Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return false;
                }

                CThreadAdapter mWorkingThread = getWorkingThread();

                return (mWorkingThread.extIsNull() ? false : mWorkingThread.Abort(iJoinTimeout, iExceptionHandler));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="iJoinTimeout">Millisecond(s).</param>
            /// <param name="iExceptionHandler"></param>
            /// <returns></returns>
            public bool Abort(int iJoinTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
            {
                if (isDisposed())
                {
                    iExceptionHandler.extInvoke(new MethodAccessException("if (isDisposed())"));

                    return false;
                }

                CThreadAdapter mWorkingThread = getWorkingThread();

                return (mWorkingThread.extIsNull() ? false : mWorkingThread.Abort(iJoinTimeout, iExceptionHandler));
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
                    getDisposedTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
                    getCreationOptions().extToStringT(iExceptionHandler),
                    getException().extToStringT(iExceptionHandler),
                    getID().ToString(),
                    isCanceled().ToString(),
                    isCompleted().ToString(),
                    isFaulted().ToString(),
                    getStatus().extToStringT(iExceptionHandler),
                    getStartTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
                    getEndTime().extToSQLDateTime(iDateTimeKind, iExceptionHandler),
                    fWorkingThreadID.ToString(),
                    string.Join(string.Empty, getCreatedStacks()),
                };

                if (iWithFieldNames)
                {
                    mResult[0] = string.Format("CreationTime: {0}", mResult[0]);
                    mResult[1] = string.Format("ElapsedTime: {0}", mResult[1]);
                    mResult[2] = string.Format("DisposedTime: {0}", mResult[2]);
                    mResult[3] = string.Format("CreationOptions: {0}", mResult[3]);
                    mResult[4] = string.Format("Exception: {0}", mResult[4]);
                    mResult[5] = string.Format("ID: {0}", mResult[5]);
                    mResult[6] = string.Format("isCanceled: {0}", mResult[6]);
                    mResult[7] = string.Format("isCompleted: {0}", mResult[7]);
                    mResult[8] = string.Format("isFaulted: {0}", mResult[8]);
                    mResult[9] = string.Format("Status: {0}", mResult[9]);
                    mResult[10] = string.Format("StartTime: {0}", mResult[10]);
                    mResult[11] = string.Format("EndTime: {0}", mResult[11]);
                    mResult[12] = string.Format("ThreadID: {0}", mResult[12]);
                    mResult[13] = string.Format("StackFrames: {0}", mResult[13]);
                }

                return mResult;
            }
            #endregion
        }
    }
}
