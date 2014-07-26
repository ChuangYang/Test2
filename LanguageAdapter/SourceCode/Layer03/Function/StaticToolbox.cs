using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_StaticToolbox
{
    /// <summary>
    /// StaticToolbox
    /// </summary>
    public static class CStaticToolbox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSourceLength"></param>
        /// <param name="iBeginIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static Tuple<int, int> getModifiedBeginIndexAndCount(int iSourceLength, int iBeginIndex = CConst.BEGIN_INDEX, int iCount = CConst.ALL_ITEMS, Action<Exception> iExceptionHandler = null)
        {
            if (iSourceLength <= CConst.EMPTY)
            {
                return new Tuple<int, int>(CConst.BEGIN_INDEX, CConst.EMPTY);
            }
            else if ((iBeginIndex == CConst.BEGIN_INDEX) && (iCount == CConst.ALL_ITEMS))
            {
                return new Tuple<int, int>(iBeginIndex, iSourceLength);
            }
            else if (iBeginIndex >= iSourceLength)
            {
                iExceptionHandler.extInvoke(new ArgumentOutOfRangeException(string.Format("else if ({0} >= {1})", iBeginIndex, iSourceLength)), false);

                return new Tuple<int, int>(CConst.BEGIN_INDEX, CConst.EMPTY);
            }
            else if (iCount == CConst.EMPTY)
            {
                return new Tuple<int, int>(((iBeginIndex < CConst.BEGIN_INDEX) ? CConst.BEGIN_INDEX : iBeginIndex), CConst.EMPTY);
            }

            int mBeginIndex = ((iBeginIndex < CConst.BEGIN_INDEX) ? CConst.BEGIN_INDEX : iBeginIndex);
            int mCount = (((iCount < CConst.EMPTY) || (iCount >= (iSourceLength - mBeginIndex))) ? (iSourceLength - mBeginIndex) : iCount);

            return new Tuple<int, int>(mBeginIndex, mCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string getAppVersion(Action<Exception> iExceptionHandler = null)
        {
            Version mVersion = CTryCatchObserver.Register(() => ApplicationDeployment.CurrentDeployment.CurrentVersion, ioException => { }).Item2;

            if (mVersion.extIsNotNull())
            {
                return mVersion.ToString();
            }

            return CTryCatchObserver.Register(() => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion).Item2;
        }
    }
}
