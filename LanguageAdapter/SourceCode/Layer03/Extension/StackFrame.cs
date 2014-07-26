using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Diagnostics;
using System.Reflection;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_StackFrameExtensions
{
    /// <summary>
    /// StackFrameExtensions
    /// </summary>
    public static class CStackFrameExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extGetFileName(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return string.Empty;
            }

            return ioStackFrame.GetFileName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Type extGetDeclaringType(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return null;
            }

            return ioStackFrame.GetMethod().DeclaringType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extGetNamespace(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            Type mType = extGetDeclaringType(ioStackFrame, iExceptionHandler);

            return (mType.extIsNull() ? string.Empty : mType.Namespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extGetClassName(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            Type mType = extGetDeclaringType(ioStackFrame, iExceptionHandler);

            return (mType.extIsNull() ? string.Empty : mType.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extGetMethodName(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return string.Empty;
            }

            return ioStackFrame.GetMethod().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static ParameterInfo[] extGetParameters(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return new ParameterInfo[CConst.EMPTY];
            }

            return ioStackFrame.GetMethod().GetParameters();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetParametersNames(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            ParameterInfo[] mParameterInfos = extGetParameters(ioStackFrame, iExceptionHandler);

            return Array.ConvertAll(mParameterInfos, mParameterInfo => mParameterInfo.Name);
        }

        /// <summary>
        /// MethodName(ParametersName1,ParametersName2,ParametersName3,etc.)
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns>MethodName(ParametersName1,ParametersName2,ParametersName3,etc.)</returns>
        public static string extGetFullMethodName(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return string.Empty;
            }

            return string.Format("{0}({1})", extGetMethodName(ioStackFrame, iExceptionHandler), string.Join(",", extGetParametersNames(ioStackFrame, iExceptionHandler)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioStackFrame"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extGetLineNumber(this StackFrame ioStackFrame, Action<Exception> iExceptionHandler = null)
        {
            if (ioStackFrame.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioStackFrame.extIsNull())"));

                return CConst.BEGIN_INDEX;
            }

            return ioStackFrame.GetFileLineNumber();
        }
    }
}
