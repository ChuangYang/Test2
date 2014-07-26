using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L3_StackFrameExtensions;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L5_0_TypeExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L5_1_StaticWatcher
{
    /// <summary>
    /// StaticWatcher
    /// </summary>
    public static class CStaticWatcher
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        /// <summary>
        /// KeyValuePair&lt;Type, Tuple&lt;fCreationTime, ThreadID, CreatedStacks&gt;&gt;
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>> fRecords;
        #endregion

        #region Singleton, factory or constructor.
        static CStaticWatcher() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            fCreationTime = DateTime.UtcNow;
            fRecords = new ConcurrentDictionary<Type, Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>>();

            fRecords[typeof(CStaticWatcher)] = new Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>(
                fCreationTime,
                Thread.CurrentThread.ManagedThreadId,
                CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex())
                );
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
            return fRecords.Count;
        }

        /// <summary>
        /// KeyValuePair&lt;Type, Tuple&lt;fCreationTime, ThreadID, CreatedStacks&gt;&gt;
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Type, Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>>> Watch()
        {
            foreach (KeyValuePair<Type, Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>> mRecord in fRecords)
            {
                yield return mRecord;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCreationTime"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool Register(DateTime iCreationTime, Action<Exception> iExceptionHandler = null)
        {
            if (iCreationTime.Ticks <= CConst.EMPTY)
            {
                iExceptionHandler.extInvoke(new ArgumentException("if (iCreationTime.Ticks <= CConst.EMPTY)"));

                return false;
            }

            SynchronizedReadOnlyCollection<string> mCreatedStacks = CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex());

            StackFrame mStackFrame = CStackFrameHelper.getStackFrame(CStackFrameHelper.getModifiedStackFrameIndex());
            Type mType = mStackFrame.extGetDeclaringType(iExceptionHandler);

            if (mType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (mType.extIsNull())"));

                return false;
            }
            else if (!mType.extIsStaticClass(iExceptionHandler))
            {
                iExceptionHandler.extInvoke(new SystemException(string.Format("[else if (!mType.extIsStaticClass(iExceptionHandler))][{0}]", mType)));

                return false;
            }
            else if (fRecords.ContainsKey(mType))
            {
                iExceptionHandler.extInvoke(new ArgumentException(string.Format("[else if (fRecords.ContainsKey(mType))][{0}]", mType)), false);

                return true;
            }

            MethodBase mMethodBase = mStackFrame.GetMethod();

            if (!(mMethodBase.IsPrivate && mMethodBase.IsSpecialName && mMethodBase.IsStatic))
            {
                iExceptionHandler.extInvoke(new SystemException(string.Format("[if (!(mMethodBase.IsPrivate && mMethodBase.IsSpecialName && mMethodBase.IsStatic))][{0}]", mMethodBase)));

                return false;
            }
            else if ((mMethodBase.MemberType & MemberTypes.Constructor) != MemberTypes.Constructor)
            {
                iExceptionHandler.extInvoke(new SystemException(string.Format("[else if ((mMethodBase.MemberType & MemberTypes.Constructor) != MemberTypes.Constructor)][{0}][{1}]", mMethodBase, mMethodBase.MemberType)));

                return false;
            }

            fRecords[mType] = new Tuple<DateTime, int, SynchronizedReadOnlyCollection<string>>(iCreationTime, Thread.CurrentThread.ManagedThreadId, mCreatedStacks);

            return true;
        }
        #endregion
    }
}
