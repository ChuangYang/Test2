using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections.Concurrent;
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L8_0_ThreadObserver
{
    /// <summary>
    /// ThreadObserver
    /// </summary>
    public static partial class CThreadObserver
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        private static readonly ConcurrentDictionary<int, CThreadAdapter> fCache;

        private static volatile int fMainThreadID;
        #endregion

        #region Singleton, factory or constructor.
        static CThreadObserver() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            fCache = new ConcurrentDictionary<int, CThreadAdapter>();

            fMainThreadID = CConst.NOT_FOUND;

            CEnumerationHelper.Cache<ThreadPriority>(CStaticToolbox.throwException);
            CEnumerationHelper.Cache<ThreadState>(CStaticToolbox.throwException);
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
        public static IEnumerable<CThreadAdapter> Watch()
        {
            foreach (CThreadAdapter mThreadAdapter in fCache.Values)
            {
                yield return mThreadAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter getThread(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            CThreadAdapter mThread = null;

            if (fCache.TryGetValue(iThreadID, out mThread))
            {
                return mThread;
            }

            iExceptionHandler.extInvoke(new KeyNotFoundException(string.Format("if (!fCache.TryGetValue({0}, out mThread))", iThreadID)), false);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter getMainThread(Action<Exception> iExceptionHandler = null)
        {
            return getThread(fMainThreadID, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter Register(Thread ioThread = null, Action<Exception> iExceptionHandler = null)
        {
            Thread mThread = ioThread ?? Thread.CurrentThread;
            int mThreadID = mThread.ManagedThreadId;

            CThreadAdapter mResult = null;

            if (fCache.TryGetValue(mThreadID, out mResult))
            {
                return mResult;
            }
            else if ((mResult = CThreadAdapter.Factory(
                mThread,
                CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex()),
                iExceptionHandler)).extIsNull())
            {
                return null;
            }

            return (fCache[mThreadID] = mResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter Register(Action<Exception> iExceptionHandler)
        {
            return Register(null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter RegisterMainThread(Thread ioThread = null, Action<Exception> iExceptionHandler = null)
        {
            CThreadAdapter mThreadAdapter = ((fMainThreadID > CConst.EMPTY) ? getMainThread() : null);

            if (mThreadAdapter.extIsNotNull())
            {
                return mThreadAdapter;
            }

            fMainThreadID = (mThreadAdapter = Register(ioThread, iExceptionHandler)).getID();

            return mThreadAdapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter RegisterMainThread(Action<Exception> iExceptionHandler)
        {
            return RegisterMainThread(null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static CThreadAdapter extToAdapter(this Thread ioThread, Action<Exception> iExceptionHandler = null)
        {
            return Register(ioThread, iExceptionHandler);
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
            CThreadAdapter mThread = getThread(iThreadID, iExceptionHandler);

            return (mThread.extIsNull() ? false : mThread.Join(iTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID">Millisecond(s).</param>
        /// <param name="iTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Join(int iThreadID, int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            CThreadAdapter mThread = getThread(iThreadID, iExceptionHandler);

            return (mThread.extIsNull() ? false : mThread.Join(iTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extJoin(this Thread ioThread, TimeSpan iTimeout, Action<Exception> iExceptionHandler = null)
        {
            if (ioThread.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioThread.extIsNull())"));

                return false;
            }

            return Join(ioThread.ManagedThreadId, iTimeout, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread">Millisecond(s).</param>
        /// <param name="iTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extJoin(this Thread ioThread, int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            if (ioThread.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioThread.extIsNull())"));

                return false;
            }

            return Join(ioThread.ManagedThreadId, iTimeout, iExceptionHandler);
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
            CThreadAdapter mThread = getThread(iThreadID, iExceptionHandler);

            return (mThread.extIsNull() ? false : mThread.Abort(iJoinTimeout, iExceptionHandler));
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
            CThreadAdapter mThread = getThread(iThreadID, iExceptionHandler);

            return (mThread.extIsNull() ? false : mThread.Abort(iJoinTimeout, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iJoinTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extAbort(this Thread ioThread, TimeSpan iJoinTimeout, Action<Exception> iExceptionHandler = null)
        {
            if (ioThread.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioThread.extIsNull())"));

                return false;
            }

            return Abort(ioThread.ManagedThreadId, iJoinTimeout, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioThread"></param>
        /// <param name="iJoinTimeout">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extAbort(this Thread ioThread, int iJoinTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            if (ioThread.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioThread.extIsNull())"));

                return false;
            }

            return Abort(ioThread.ManagedThreadId, iJoinTimeout, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool ResetAbort(Action<Exception> iExceptionHandler = null)
        {
            CThreadAdapter mThread = Register(iExceptionHandler);

            if (mThread.extIsNull())
            {
                return false;
            }
            else if (((int)mThread.getState()).extHasFlag((int)ThreadState.Aborted) || ((int)mThread.getState()).extHasFlag((int)ThreadState.AbortRequested))
            {
                Thread.ResetAbort();

                return true;
            }

            return true;
        }
        #endregion
    }
}
