using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Collections;
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

namespace LanguageAdapter.CSharp.L3_CollectionExtensions
{
    /// <summary>
    /// ICollectionExtensions
    /// </summary>
    public static class CICollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int extLastIndex(this ICollection ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return CConst.NOT_FOUND;
            }

            return (ioSource.Count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static object extGetSyncRoot(this ICollection ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return null;
            }

            return CTryCatchObserver.Register(() => ioSource.SyncRoot, iExceptionHandler).Item2;
        }
    }
}
