using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Diagnostics;
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L1_StaticToolbox
{
    /// <summary>
    /// StaticToolbox
    /// </summary>
    public static class CStaticToolbox
    {
        #region Fields and properties.
        private static readonly Stopwatch fStopwatch;
        private static readonly DateTime fCreationTime;

        private static readonly Process fCurrentProcess;

        private static readonly object fUniqueInt_SyncRoot;
        private static volatile int fUniqueInt;

        private static readonly object fUniqueLong_SyncRoot;
        private static long fUniqueLong;

        private static readonly object fUniqueTime_SyncRoot;
        private static readonly TimeSpan fMinTimeUnit;

        private static volatile Action<Exception> fDefaultExceptionHandler;
        #endregion

        #region Singleton, factory or constructor.
        static CStaticToolbox() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            fStopwatch = new Stopwatch();
            fStopwatch.Start();

            fCreationTime = (DateTime.UtcNow - getElapsedTime());

            fCurrentProcess = Process.GetCurrentProcess();

            fUniqueInt_SyncRoot = new object();
            fUniqueInt = 1;

            fUniqueLong_SyncRoot = new object();
            fUniqueLong = 1;

            fUniqueTime_SyncRoot = new object();
            fMinTimeUnit = new TimeSpan(1); //0.0001 millisecond.

            fDefaultExceptionHandler = null;
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
            return fStopwatch.Elapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioException"></param>
        public static void throwException(Exception ioException)
        {
            throw ioException;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioDefaultExceptionHandler"></param>
        public static void Start(params Action<Exception>[] ioDefaultExceptionHandler)
        {
            if (fDefaultExceptionHandler != null)
            {
                throw new ArgumentNullException("if (DefaultExceptionHandler != null)");
            }
            else if (ioDefaultExceptionHandler == null)
            {
                throw new ArgumentNullException("else if (ioDefaultExceptionHandler == null)");
            }
            else if (ioDefaultExceptionHandler.Length == CConst.EMPTY)
            {
                throw new ArgumentException("else if (ioDefaultExceptionHandler.Length == CConst.EMPTY)");
            }

            foreach (Action<Exception> mExceptionHandler in ioDefaultExceptionHandler)
            {
                if (mExceptionHandler == null)
                {
                    throw new ArgumentNullException("if (mExceptionHandler == null)");
                }

                fDefaultExceptionHandler += mExceptionHandler;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process getCurrentProcess()
        {
            return fCurrentProcess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int getUniqueInt()
        {
            lock (fUniqueInt_SyncRoot)
            {
                return fUniqueInt++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long getUniqueLong()
        {
            lock (fUniqueLong_SyncRoot)
            {
                return fUniqueLong++;
            }
        }

        /// <summary>
        /// 0.0001 millisecond.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan getMinTimeUnit()
        {
            return fMinTimeUnit;
        }

        /// <summary>
        /// 0.0001 millisecond.
        /// </summary>
        public static void MinSleeping()
        {
            //Thread.Sleep(MinTimeUnit);
            SpinWait.SpinUntil(() => false, fMinTimeUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime getUniqueTime()
        {
            lock (fUniqueTime_SyncRoot)
            {
                MinSleeping();

                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long getUniqueTicks()
        {
            return getUniqueTime().Ticks;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Action<Exception> getDefaultExceptionHandler()
        {
            return fDefaultExceptionHandler;
        }
        #endregion
    }
}
