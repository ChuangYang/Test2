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
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L2_3_FuncObserver;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
using LanguageAdapter.CSharp.L5_3_DateTimeHelper;
using LanguageAdapter.CSharp.L7_Box;
using LanguageAdapter.CSharp.L8_0_ThreadObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L8_1_WhileObserver
{
    /// <summary>
    /// WhileObserver
    /// </summary>
    public static partial class CWhileObserver
    {
        /// <summary>
        /// 1 millisecond(s).
        /// </summary>
        public const int DEFAULT_INVOKE_INTERVAL = 1;

        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        private static readonly ThreadLocal<int> fThread;

        /// <summary>
        /// &lt;ThreadID, Tuple&lt;StartTime, EndTime, InvokeInterval, InvokeCounter, InvokeCountMax, StackFrames&gt;&gt;
        /// </summary>
        private static readonly ConcurrentDictionary<int, Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>>> fRecord;
        #endregion

        #region Singleton, factory or constructor.
        static CWhileObserver() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            fThread = new ThreadLocal<int>(() => CThreadObserver.Register().getID());

            fRecord = new ConcurrentDictionary<int, Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>>>();
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

        private static int getThreadID()
        {
            return fThread.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return fRecord.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <returns></returns>
        public static bool Contains(int iThreadID)
        {
            return fRecord.ContainsKey(iThreadID);
        }

        /// <summary>
        /// Tuple&lt;ThreadID, StartTime, EndTime, InvokeInterval, InvokeCounter, InvokeCountMax, StackFrames&gt;
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, DateTime, DateTime, TimeSpan, int, int, SynchronizedReadOnlyCollection<string>>> Watch()
        {
            foreach (KeyValuePair<int, Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>>> mRecord in fRecord)
            {
                yield return new Tuple<int, DateTime, DateTime, TimeSpan, int, int, SynchronizedReadOnlyCollection<string>>
                (
                    mRecord.Key,
                    mRecord.Value.Item1,
                    mRecord.Value.Item2.getItem(),
                    mRecord.Value.Item3,
                    mRecord.Value.Item4.getItem(),
                    mRecord.Value.Item5,
                    mRecord.Value.Item6
                );
            }
        }

        private static Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> Get(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = null;

            if (fRecord.TryGetValue(iThreadID, out mRecord))
            {
                return mRecord;
            }

            iExceptionHandler.extInvoke(new KeyNotFoundException(string.Format("if (!fRecord.TryGetValue({0}, out mRecord))", iThreadID)));

            return null;
        }

        /// <summary>
        /// Tuple&lt;ThreadID, StartTime, EndTime, InvokeInterval, InvokeCounter, InvokeCountMax, StackFrames&gt;
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<int, DateTime, DateTime, TimeSpan, int, int, SynchronizedReadOnlyCollection<string>> getRecord(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            return ((mRecord == null) ? null : new Tuple<int, DateTime, DateTime, TimeSpan, int, int, SynchronizedReadOnlyCollection<string>>
                (
                    iThreadID,
                    mRecord.Item1,
                    mRecord.Item2.getItem(),
                    mRecord.Item3,
                    mRecord.Item4.getItem(),
                    mRecord.Item5,
                    mRecord.Item6
                ));
        }

        private static bool isRunning(Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> ioRecord, Action<Exception> iExceptionHandler = null)
        {
            if (ioRecord == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioRecord == null)"));

                return false;
            }

            return (ioRecord.Item1 > ioRecord.Item2.getItem());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool isRunning(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            return ((mRecord == null) ? false : isRunning(mRecord, iExceptionHandler));
        }

        private static bool isEnd(Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> ioRecord, Action<Exception> iExceptionHandler = null)
        {
            if (ioRecord == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioRecord == null)"));

                return false;
            }

            return (ioRecord.Item1 <= ioRecord.Item2.getItem());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool isEnd(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            return ((mRecord == null) ? false : isEnd(mRecord, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioRecord"></param>
        /// <param name="iExceptionHandler"></param>
        private static void Cancel(Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> ioRecord, Action<Exception> iExceptionHandler = null)
        {
            if (ioRecord == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioRecord == null)"));

                return;
            }

            ioRecord.Item2.setItem(ioRecord.Item1.AddMilliseconds(-1)); //It is cancellation requested.
        }

        private static bool isCancellationRequested(Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> ioRecord, Action<Exception> iExceptionHandler = null)
        {
            if (ioRecord == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioRecord == null)"));

                return false;
            }

            return ((ioRecord.Item2.getItem() > CDateTimeHelper.getDefault()) && (ioRecord.Item2.getItem() < ioRecord.Item1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool isCancellationRequested(int iThreadID, Action<Exception> iExceptionHandler = null)
        {
            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            return ((mRecord == null) ? false : isCancellationRequested(mRecord, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPredicate"></param>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeInterval"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<bool> iPredicate, Func<bool> iInvoke, TimeSpan iInvokeInterval, int iInvokeCountMax = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            if (iInvoke == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (iInvoke == null)"));

                return CConst.NOT_FOUND;
            }
            else if ((iInvokeInterval == CDateTimeHelper.getInfinite()) || (iInvokeInterval.Ticks < CConst.EMPTY))
            {
                iExceptionHandler.extInvoke(new ArgumentException("else if ((iInvokeInterval == CDateTimeHelper.getInfinite()) || (iInvokeInterval.Ticks < CConst.EMPTY))"));

                return CConst.NOT_FOUND;
            }
            else if (iInvokeCountMax <= CConst.EMPTY)
            {
                iInvokeCountMax = Timeout.Infinite;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = new Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>>
                (
                    DateTime.UtcNow,
                    new CBox<DateTime>(CDateTimeHelper.getDefault(), iExceptionHandler),
                    iInvokeInterval,
                    new CBox<int>(CConst.EMPTY, iExceptionHandler),
                    iInvokeCountMax,
                    CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex())
                );

            fRecord[getThreadID()] = mRecord;

            while ((iPredicate == null) ? true : (iPredicate.extInvoke(iExceptionHandler).Item2))
            {
                if (isCancellationRequested(mRecord, iExceptionHandler))
                {
                    break;
                }
                else if (iInvokeInterval.Ticks > CConst.EMPTY)
                {
                    //Thread.Sleep(iInvokeInterval);
                    SpinWait.SpinUntil(() => false, iInvokeInterval);
                }

                mRecord.Item4.setItem(mRecord.Item4.getItem() + 1);

                if (!iInvoke.extInvoke(iExceptionHandler).Item2)
                {
                    break;
                }
                else if (iInvokeCountMax != Timeout.Infinite)
                {
                    if (mRecord.Item4.getItem() >= iInvokeCountMax)
                    {
                        iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("if ({0} >= {1})", mRecord.Item4.getItem(), iInvokeCountMax)));

                        break;
                    }
                }
            }

            mRecord.Item2.setItem(DateTime.UtcNow);

            return getThreadID();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPredicate"></param>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeInterval">Millisecond(s).</param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<bool> iPredicate, Func<bool> iInvoke, int iInvokeInterval = DEFAULT_INVOKE_INTERVAL, int iInvokeCountMax = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            return Register(iPredicate, iInvoke, new TimeSpan(CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, iInvokeInterval), iInvokeCountMax, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeInterval"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<bool> iInvoke, TimeSpan iInvokeInterval, int iInvokeCountMax = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            return Register(null, iInvoke, iInvokeInterval, iInvokeCountMax, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeInterval">Millisecond(s).</param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<bool> iInvoke, int iInvokeInterval = DEFAULT_INVOKE_INTERVAL, int iInvokeCountMax = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            return Register(null, iInvoke, new TimeSpan(CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, iInvokeInterval), iInvokeCountMax, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPredicate"></param>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iInvokeInterval"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<int, bool> iPredicate, Func<int, bool> iInvoke, int iInvokeCountMax, TimeSpan iInvokeInterval, Action<Exception> iExceptionHandler = null)
        {
            if (iInvoke == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (iInvoke == null)"));

                return CConst.NOT_FOUND;
            }
            else if ((iInvokeCountMax == Timeout.Infinite) || (iInvokeCountMax <= CConst.EMPTY))
            {
                iExceptionHandler.extInvoke(new ArgumentException("else if ((iInvokeCountMax == Timeout.Infinite) || (iInvokeCountMax <= CConst.EMPTY))"));

                return CConst.NOT_FOUND;
            }
            else if ((iInvokeInterval == CDateTimeHelper.getInfinite()) || (iInvokeInterval.Ticks < CConst.EMPTY))
            {
                iExceptionHandler.extInvoke(new ArgumentException("else if ((iInvokeInterval == CDateTimeHelper.getInfinite()) || (iInvokeInterval.Ticks < CConst.EMPTY))"));

                return CConst.NOT_FOUND;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = new Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>>
                (
                    DateTime.UtcNow,
                    new CBox<DateTime>(CDateTimeHelper.getDefault(), iExceptionHandler),
                    iInvokeInterval,
                    new CBox<int>(CConst.EMPTY, iExceptionHandler),
                    iInvokeCountMax,
                    CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex())
                );

            fRecord[getThreadID()] = mRecord;

            while ((iPredicate == null) ? true : (iPredicate.extInvoke(mRecord.Item4.setItem(mRecord.Item4.getItem() + 1), iExceptionHandler).Item2))
            {
                if (isCancellationRequested(mRecord, iExceptionHandler))
                {
                    break;
                }
                else if (iInvokeInterval.Ticks > CConst.EMPTY)
                {
                    //Thread.Sleep(iInvokeInterval);
                    SpinWait.SpinUntil(() => false, iInvokeInterval);
                }

                if (!iInvoke.extInvoke(mRecord.Item4.getItem(), iExceptionHandler).Item2)
                {
                    break;
                }
                else if (mRecord.Item4.getItem() >= iInvokeCountMax)
                {
                    iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} >= {1})", mRecord.Item4.getItem(), iInvokeCountMax)));

                    break;
                }
            }

            mRecord.Item2.setItem(DateTime.UtcNow);

            return getThreadID();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPredicate"></param>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iInvokeInterval">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<int, bool> iPredicate, Func<int, bool> iInvoke, int iInvokeCountMax, int iInvokeInterval = DEFAULT_INVOKE_INTERVAL, Action<Exception> iExceptionHandler = null)
        {
            return Register(iPredicate, iInvoke, iInvokeCountMax, new TimeSpan(CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, iInvokeInterval), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iInvokeInterval"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<int, bool> iInvoke, int iInvokeCountMax, TimeSpan iInvokeInterval, Action<Exception> iExceptionHandler = null)
        {
            return Register(null, iInvoke, iInvokeCountMax, iInvokeInterval, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInvoke"></param>
        /// <param name="iInvokeCountMax"></param>
        /// <param name="iInvokeInterval">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int Register(Func<int, bool> iInvoke, int iInvokeCountMax, int iInvokeInterval = DEFAULT_INVOKE_INTERVAL, Action<Exception> iExceptionHandler = null)
        {
            return Register(null, iInvoke, iInvokeCountMax, new TimeSpan(CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, CConst.EMPTY, iInvokeInterval), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iTimeout"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Wait(int iThreadID, TimeSpan iTimeout, Action<Exception> iExceptionHandler = null)
        {
            if (iThreadID == getThreadID())
            {
                iExceptionHandler.extInvoke(new ArgumentException(string.Format("if ({0} == ThreadID)", iThreadID)));

                return false;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            if (mRecord == null)
            {
                return false;
            }
            else if (isEnd(mRecord, iExceptionHandler))
            {
                return true;
            }
            else if (iTimeout.Ticks == CConst.EMPTY)
            {
                return false;
            }
            else if (iTimeout.Ticks < CConst.EMPTY)
            {
                iTimeout = CDateTimeHelper.getInfinite();
            }

            bool mResult = false;
            DateTime mStartTime = CDateTimeHelper.getDefault();

            Register(() =>
            {
                if (isEnd(mRecord, CStaticToolbox.throwException))
                {
                    mResult = true;

                    return false;
                }
                else if (mStartTime.Equals(CDateTimeHelper.getDefault()))
                {
                    mStartTime = fRecord[getThreadID()].Item1;
                }

                if (iTimeout != CDateTimeHelper.getInfinite())
                {
                    if ((DateTime.UtcNow - mStartTime) >= iTimeout)
                    {
                        return false;
                    }
                }

                return true;
            },
            DEFAULT_INVOKE_INTERVAL, Timeout.Infinite, iExceptionHandler);

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iTimeout">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Wait(int iThreadID, int iTimeout = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            if (iThreadID == getThreadID())
            {
                iExceptionHandler.extInvoke(new ArgumentException(string.Format("if ({0} == ThreadID)", iThreadID)));

                return false;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            if (mRecord == null)
            {
                return false;
            }
            else if (isEnd(mRecord, iExceptionHandler))
            {
                return true;
            }
            else if (iTimeout == CConst.EMPTY)
            {
                return false;
            }
            else if (iTimeout < CConst.EMPTY)
            {
                iTimeout = Timeout.Infinite;
            }

            bool mResult = false;
            DateTime mStartTime = CDateTimeHelper.getDefault();

            Register(() =>
            {
                if (isEnd(mRecord, CStaticToolbox.throwException))
                {
                    mResult = true;

                    return false;
                }
                else if (mStartTime.Equals(CDateTimeHelper.getDefault()))
                {
                    mStartTime = fRecord[getThreadID()].Item1;
                }

                if (iTimeout != Timeout.Infinite)
                {
                    if ((DateTime.UtcNow - mStartTime).TotalMilliseconds >= iTimeout)
                    {
                        return false;
                    }
                }

                return true;
            },
            DEFAULT_INVOKE_INTERVAL, Timeout.Infinite, iExceptionHandler);

            return mResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iWaitingTime"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Stop(int iThreadID, TimeSpan iWaitingTime, Action<Exception> iExceptionHandler = null)
        {
            if (iThreadID == getThreadID())
            {
                iExceptionHandler.extInvoke(new ArgumentException(string.Format("if ({0} == ThreadID)", iThreadID)));

                return false;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            if (mRecord == null)
            {
                return false;
            }
            else if (isEnd(mRecord, iExceptionHandler))
            {
                return true;
            }

            Cancel(mRecord, iExceptionHandler);

            return ((iWaitingTime.Ticks == CConst.EMPTY) ? true : Wait(iThreadID, iWaitingTime, iExceptionHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iThreadID"></param>
        /// <param name="iWaitingTime">Millisecond(s).</param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Stop(int iThreadID, int iWaitingTime = Timeout.Infinite, Action<Exception> iExceptionHandler = null)
        {
            if (iThreadID == getThreadID())
            {
                iExceptionHandler.extInvoke(new ArgumentException(string.Format("if ({0} == ThreadID)", iThreadID)));

                return false;
            }

            Tuple<DateTime, CBox<DateTime>, TimeSpan, CBox<int>, int, SynchronizedReadOnlyCollection<string>> mRecord = Get(iThreadID, iExceptionHandler);

            if (mRecord == null)
            {
                return false;
            }
            else if (isEnd(mRecord, iExceptionHandler))
            {
                return true;
            }

            Cancel(mRecord, iExceptionHandler);

            return ((iWaitingTime == CConst.EMPTY) ? true : Wait(iThreadID, iWaitingTime, iExceptionHandler));
        }
        #endregion
    }
}
