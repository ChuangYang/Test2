using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Diagnostics;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L3_StackFrameExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L4_StackFrameHelper
{
    /// <summary>
    /// StackFrameHelper
    /// </summary>
    public static class CStackFrameHelper
    {
        /// <summary>
        /// (16 - 1)
        /// </summary>
        public const int DEFAULT_STACK_FRAMES = (16 - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBeginIndex"></param>
        /// <returns></returns>
        public static int getModifiedStackFrameIndex(int iBeginIndex = CConst.BEGIN_INDEX)
        {
            const int mShift = 1;

            if (iBeginIndex < int.MaxValue)
            {
                iBeginIndex += mShift;
            }

            return ((iBeginIndex < mShift) ? mShift : iBeginIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public static StackFrame getStackFrame(int iIndex = CConst.BEGIN_INDEX)
        {
            StackTrace mStackTrace = new StackTrace(true); // get call stack
            int mCount = mStackTrace.FrameCount;

            iIndex = getModifiedStackFrameIndex(iIndex);
            iIndex = ((iIndex >= mCount) ? (mCount - 1) : iIndex);

            return mStackTrace.GetFrame(iIndex); // get method calls (frames)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static StackFrame[] getStackFrames(int iBeginIndex = CConst.BEGIN_INDEX, int iCount = DEFAULT_STACK_FRAMES)
        {
            StackFrame[] mStackFrames = null;

            StackTrace mStackTrace = new StackTrace(true); // get call stack
            int mCount = mStackTrace.FrameCount;

            int mBeginIndex = getModifiedStackFrameIndex(iBeginIndex);
            int mLength = CConst.EMPTY;

            mBeginIndex = ((mBeginIndex >= mCount) ? (mCount - 1) : mBeginIndex);
            mLength = (((iCount < CConst.EMPTY) || (iCount >= (mCount - mBeginIndex))) ? (mCount - mBeginIndex) : iCount);

            mStackFrames = new StackFrame[mLength];

            for (int i = CConst.BEGIN_INDEX; i < mLength; i++)
            {
                mStackFrames[i] = mStackTrace.GetFrame(mBeginIndex + i);
            }

            return mStackFrames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static string[] getStackFrameStrings(int iBeginIndex = CConst.BEGIN_INDEX, int iCount = DEFAULT_STACK_FRAMES)
        {
            return Array.ConvertAll(getStackFrames(getModifiedStackFrameIndex(iBeginIndex), iCount), ioStackFrame => ioStackFrame.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static SynchronizedReadOnlyCollection<string> getReadOnlyStackFrames(int iBeginIndex = CConst.BEGIN_INDEX, int iCount = DEFAULT_STACK_FRAMES)
        {
            return new SynchronizedReadOnlyCollection<string>(new object(), getStackFrameStrings(getModifiedStackFrameIndex(iBeginIndex), iCount));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getFileName(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetFileName(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Type getDeclaringType(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetDeclaringType(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getNamespace(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetNamespace(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getClassName(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetClassName(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getMethodName(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetMethodName(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] getParametersNames(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetParametersNames(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getFullMethodName(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetFullMethodName(iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int getLineNumber(int iIndex = CConst.BEGIN_INDEX, Action<Exception> iExceptionHandler = null)
        {
            return getStackFrame(getModifiedStackFrameIndex(iIndex)).extGetLineNumber(iExceptionHandler);
        }
    }
}
