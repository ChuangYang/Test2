using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.

#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_StaticToolbox
{
    /// <summary>
    /// StaticToolbox
    /// </summary>
    public static class CStaticToolbox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStackFrameIndex"></param>
        /// <param name="ioTargetType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool CheckAuthority(int iStackFrameIndex, Type ioTargetType, Action<Exception> iExceptionHandler = null)
        {
            if (ioTargetType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioTargetType.extIsNull())"));

                return false;
            }

            Type mType = CStackFrameHelper.getDeclaringType(CStackFrameHelper.getModifiedStackFrameIndex(iStackFrameIndex));

            if (mType.Equals(ioTargetType))
            {
                return true;
            }
            else if (mType.DeclaringType.extIsNotNull() && (mType.DeclaringType.Equals(ioTargetType)))
            {
                return true;
            }

            iExceptionHandler.extInvoke(new MethodAccessException(string.Format("if (!{0}.Equals({1}))", mType, ioTargetType)));

            return false;
        }
    }
}
