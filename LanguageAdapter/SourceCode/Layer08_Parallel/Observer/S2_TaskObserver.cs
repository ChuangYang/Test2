using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections.Concurrent;
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
using LanguageAdapter.CSharp.L2_2_ActionObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
using LanguageAdapter.CSharp.L8_0_ThreadObserver;
using LanguageAdapter.CSharp.L8_1_WhileObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L8_2_TaskObserver
{
    /// <summary>
    /// TaskObserver
    /// </summary>
    public static partial class CTaskObserver
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        private static readonly ConcurrentDictionary<int, CTaskAdapter> fCache;
        #endregion

        #region Singleton, factory or constructor.
        static CTaskObserver() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            fCache = new ConcurrentDictionary<int, CTaskAdapter>();

            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            CEnumerationHelper.Cache<TaskCreationOptions>(CStaticToolbox.throwException);
            CEnumerationHelper.Cache<TaskStatus>(CStaticToolbox.throwException);
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
        /// <param name="ioSender"></param>
        /// <param name="ioEventArgs"></param>
        public static void UnobservedTaskException(object ioSender, UnobservedTaskExceptionEventArgs ioEventArgs)
        {
            foreach (Exception mException in ioEventArgs.Exception.InnerExceptions)
            {
                CStaticToolbox.getDefaultExceptionHandler().extInvoke(mException);
            }

            ioEventArgs.SetObserved();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return fCache.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <returns></returns>
        public static bool Contains(int iThreadID)
        {
            return fCache.ContainsKey(iThreadID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CTaskAdapter> Watch()
        {
            foreach (CTaskAdapter mTaskAdapter in fCache.Values)
            {
                yield return mTaskAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CTaskAdapter getTask(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            CTaskAdapter mTaskAdapter = null;

            if (fCache.TryGetValue(iThreadID, out mTaskAdapter))
            {
                return mTaskAdapter;
            }

            iExceptionHandler.extInvoke(new KeyNotFoundException(string.Format("if (!fCache.TryGetValue({0}, out mTaskAdapter))", iThreadID)));

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iAction"></param>
        /// <param name="iCancellationToken"></param>
        /// <param name="iCreationOptions"></param>
        /// <param name="iMethodException"></param>
        /// <param name="iTaskException"></param>
        /// <returns></returns>
        public static CTaskAdapter Register(Action iAction, CancellationToken? iCancellationToken = null, TaskCreationOptions iCreationOptions = TaskCreationOptions.None, Action<Exception> iMethodException = null, Action<Exception> iTaskException = null)
        {
            if (iAction == null)
            {
                iMethodException.extInvoke(new ArgumentNullException("if (iAction == null)"));

                return null;
            }

            CTaskAdapter mTaskAdapter = null;
            Task mTask = null;
            SynchronizedReadOnlyCollection<string> mCreatedStacks = CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex());

            Action mWorker = new Action(() =>
            {
                DateTime mStartTime = DateTime.UtcNow;

                CWhileObserver.Register(
                    () => mTaskAdapter.extIsNull(),
                    () =>
                    {
                        if (mTask.extIsNull())
                        {
                            throw new NullReferenceException("if (mTask.extIsNull())");
                        }

                        return true;
                    }
                    );

                int mThreadID = CThreadObserver.Register(CStaticToolbox.throwException).getID();
                CTaskAdapter mOldTaskAdapter = null;

                if (fCache.TryRemove(mThreadID, out mOldTaskAdapter))
                {
                    mOldTaskAdapter.Dispose();
                }

                mTaskAdapter.Start(mStartTime, CStaticToolbox.throwException);
                fCache[mThreadID] = mTaskAdapter;

                iAction();
            });

            Action mObserver = new Action(() =>
            {
                mWorker.extInvoke(
                    ioException =>
                    {
                        if (ioException is ThreadAbortException)
                        {
                            CThreadObserver.ResetAbort(iTaskException);
                        }

                        iTaskException(ioException);
                    },
                    () =>
                    {
                        if (mTaskAdapter.extIsNotNull())
                        {
                            mTaskAdapter.End(iTaskException);
                        }
                    }
                );
            });

            CTryCatchObserver.Register(
                () =>
                {
                    if ((iCancellationToken == null) && (iCreationOptions == TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver);
                    }
                    else if ((iCancellationToken != null) && (iCreationOptions == TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver, iCancellationToken.Value);
                    }
                    else if ((iCancellationToken == null) && (iCreationOptions != TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver, iCreationOptions);
                    }
                    else
                    {
                        mTask = new Task(mObserver, iCancellationToken.Value, iCreationOptions);
                    }

                    mTask.Start();
                },
                ioException =>
                {
                    if (mTask.extIsNotNull())
                    {
                        mTask.Dispose();
                        mTask = null;
                    }

                    iMethodException(ioException);
                }
                );

            if (mTask.extIsNull())
            {
                return null;
            }
            else if ((mTaskAdapter = CTaskAdapter.Factory(mTask, mCreatedStacks, iMethodException)).extIsNull())
            {
                mTask.Dispose();
                mTask = null;
            }

            return mTaskAdapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iAction"></param>
        /// <param name="ioInput"></param>
        /// <param name="iCancellationToken"></param>
        /// <param name="iCreationOptions"></param>
        /// <param name="iMethodException"></param>
        /// <param name="iTaskException"></param>
        /// <returns></returns>
        public static CTaskAdapter Register<T>(Action<T> iAction, T ioInput, CancellationToken? iCancellationToken = null, TaskCreationOptions iCreationOptions = TaskCreationOptions.None, Action<Exception> iMethodException = null, Action<Exception> iTaskException = null)
        {
            if (iAction == null)
            {
                iMethodException.extInvoke(new ArgumentNullException("if (iAction == null)"));

                return null;
            }

            CTaskAdapter mTaskAdapter = null;
            Task mTask = null;
            SynchronizedReadOnlyCollection<string> mCreatedStacks = CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex());

            Action mWorker = new Action(() =>
            {
                DateTime mStartTime = DateTime.UtcNow;

                CWhileObserver.Register(
                    () => (mTaskAdapter.extIsNull()),
                    () =>
                    {
                        if (mTask.extIsNull())
                        {
                            throw new NullReferenceException("if (mTask.extIsNull())");
                        }

                        return true;
                    }
                    );

                int mThreadID = CThreadObserver.Register(CStaticToolbox.throwException).getID();
                CTaskAdapter mOldTaskAdapter = null;

                if (fCache.TryRemove(mThreadID, out mOldTaskAdapter))
                {
                    mOldTaskAdapter.Dispose();
                }

                mTaskAdapter.Start(mStartTime, CStaticToolbox.throwException);
                fCache[mThreadID] = mTaskAdapter;

                iAction(ioInput);
            });

            Action mObserver = new Action(() =>
            {
                mWorker.extInvoke(
                    ioException =>
                    {
                        if (ioException is ThreadAbortException)
                        {
                            CThreadObserver.ResetAbort(iTaskException);
                        }

                        iTaskException(ioException);
                    },
                    () =>
                    {
                        if (mTaskAdapter.extIsNotNull())
                        {
                            mTaskAdapter.End(iTaskException);
                        }
                    }
                );
            });

            CTryCatchObserver.Register(
                () =>
                {
                    if ((iCancellationToken == null) && (iCreationOptions == TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver);
                    }
                    else if ((iCancellationToken != null) && (iCreationOptions == TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver, iCancellationToken.Value);
                    }
                    else if ((iCancellationToken == null) && (iCreationOptions != TaskCreationOptions.None))
                    {
                        mTask = new Task(mObserver, iCreationOptions);
                    }
                    else
                    {
                        mTask = new Task(mObserver, iCancellationToken.Value, iCreationOptions);
                    }

                    mTask.Start();
                },
                ioException =>
                {
                    if (mTask.extIsNotNull())
                    {
                        mTask.Dispose();
                        mTask = null;
                    }

                    iMethodException(ioException);
                }
                );

            if (mTask.extIsNull())
            {
                return null;
            }
            else if ((mTaskAdapter = CTaskAdapter.Factory(mTask, mCreatedStacks, iMethodException)).extIsNull())
            {
                mTask.Dispose();
                mTask = null;
            }

            return mTaskAdapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParallelLoopResult ParallelFor(int iBeginIndex, int iCount, Action<int> iAction, Action<Exception, int> iExceptionHandler = null)
        {
            ParallelLoopResult mParallelLoopResult = default(ParallelLoopResult);

            if (iAction == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (iAction == null)"), CConst.NOT_FOUND);

                return mParallelLoopResult;
            }

            Action<int> mAction = new Action<int>(i =>
            {
                CThreadObserver.Register(ioException => iExceptionHandler.extInvoke(ioException, i, false));

                iAction.extInvoke(i, (ioException, iIndex) =>
                {
                    if (ioException is ThreadAbortException)
                    {
                        CThreadObserver.ResetAbort(ioInnerException => iExceptionHandler.extInvoke(ioInnerException, iIndex, false));
                    }

                    iExceptionHandler.extInvoke(ioException, iIndex, false);
                }, i);
            });

            CTryCatchObserver.Register(
                () =>
                {
                    mParallelLoopResult = Parallel.For(iBeginIndex, iCount, mAction);

                    if (!mParallelLoopResult.IsCompleted)
                    {
                        throw new TaskCanceledException(string.Format("[if (!mParallelLoopResult.IsCompleted)][{0}][{1}]", iBeginIndex, iCount));
                    }
                },
                ioException => iExceptionHandler.extInvoke(ioException, CConst.NOT_FOUND)
                );

            return mParallelLoopResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParallelLoopResult ParallelForeach<T>(IEnumerable<T> ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            ParallelLoopResult mParallelLoopResult = default(ParallelLoopResult);

            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return mParallelLoopResult;
            }
            else if (iAction == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (iAction == null)"));

                return mParallelLoopResult;
            }

            Action<T> mAction = new Action<T>(ioInput =>
            {
                CThreadObserver.Register(ioException => iExceptionHandler.extInvoke(ioException, false));

                iAction.extInvoke(ioInput, ioException =>
                {
                    if (ioException is ThreadAbortException)
                    {
                        CThreadObserver.ResetAbort(iExceptionHandler);
                    }

                    iExceptionHandler.extInvoke(ioException, false);
                });
            });

            CTryCatchObserver.Register(
                () =>
                {
                    mParallelLoopResult = Parallel.ForEach(ioSource, mAction);

                    if (!mParallelLoopResult.IsCompleted)
                    {
                        throw new TaskCanceledException("if (!mParallelLoopResult.IsCompleted)");
                    }
                },
                iExceptionHandler
                );

            return mParallelLoopResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParallelLoopResult ParallelForeach<T>(IEnumerable<T> ioSource, Action<T, long> iAction, Action<Exception, long> iExceptionHandler = null)
        {
            ParallelLoopResult mParallelLoopResult = default(ParallelLoopResult);

            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"), CConst.NOT_FOUND);

                return mParallelLoopResult;
            }
            else if (iAction == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (iAction == null)"), CConst.NOT_FOUND);

                return mParallelLoopResult;
            }

            Action<T, ParallelLoopState, long> mAction = new Action<T, ParallelLoopState, long>((ioInput, ioLoopState, i) =>
            {
                CThreadObserver.Register(ioException => iExceptionHandler.extInvoke(ioException, i, false));

                iAction.extInvoke(ioInput, i, (ioException, iIndex) =>
                {
                    if (ioException is ThreadAbortException)
                    {
                        CThreadObserver.ResetAbort(ioInnerException => iExceptionHandler.extInvoke(ioInnerException, iIndex, false));
                    }

                    iExceptionHandler.extInvoke(ioException, iIndex, false);
                }, i);
            });

            CTryCatchObserver.Register(
                () =>
                {
                    mParallelLoopResult = Parallel.ForEach(ioSource, mAction);

                    if (!mParallelLoopResult.IsCompleted)
                    {
                        throw new TaskCanceledException("if (!mParallelLoopResult.IsCompleted)");
                    }
                },
                ioException => iExceptionHandler.extInvoke(ioException, CConst.NOT_FOUND)
                );

            return mParallelLoopResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParallelLoopResult extParallelForeach<T>(this IEnumerable<T> ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            return ParallelForeach(ioSource, iAction, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParallelLoopResult extParallelForeach<T>(this IEnumerable<T> ioSource, Action<T, long> iAction, Action<Exception, long> iExceptionHandler = null)
        {
            return ParallelForeach(ioSource, iAction, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Join(int iThreadID, TimeSpan iTimeout, Action<Exception> iExceptionHandler = null)
        {
            CTaskAdapter mTaskAdapter = getTask(iThreadID, iExceptionHandler);

            return ((mTaskAdapter == null) ? false : mTaskAdapter.Join(iTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iTimeout">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Join(int iThreadID, int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            CTaskAdapter mTaskAdapter = getTask(iThreadID, iExceptionHandler);

            return ((mTaskAdapter == null) ? false : mTaskAdapter.Join(iTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iJoinTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Abort(int iThreadID, TimeSpan iJoinTimeout, Action<Exception> iExceptionHandler = null)
        {
            CTaskAdapter mTaskAdapter = getTask(iThreadID, iExceptionHandler);

            return ((mTaskAdapter == null) ? false : mTaskAdapter.Abort(iJoinTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iJoinTimeout">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Abort(int iThreadID, int iJoinTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            CTaskAdapter mTaskAdapter = getTask(iThreadID, iExceptionHandler);

            return ((mTaskAdapter == null) ? false : mTaskAdapter.Abort(iJoinTimeout, iExceptionHandler));
        }
        #endregion
    }
}
