using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_CollectionExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L3_TypeExtensions
{
    /// <summary>
    /// TypeExtensions
    /// </summary>
    public static class CTypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsFlagEnum(this _Type ioType, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return false;
            }
            else if (!ioType.IsEnum)
            {
                return false;
            }

            return ioType.GetCustomAttributes(typeof(FlagsAttribute), false).extIsNotEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsFlagEnum(this Type ioType, Action<Exception> iExceptionHandler = null)
        {
            return extIsFlagEnum((ioType as _Type), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetDescriptions(this _Type ioType, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return new string[CConst.EMPTY];
            }

            return Array.ConvertAll((ioType.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]), (ioAttribute => ioAttribute.Description));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetDescriptions(this Type ioType, Action<Exception> iExceptionHandler = null)
        {
            return extGetDescriptions((ioType as _Type), iExceptionHandler);
        }
    }
}
