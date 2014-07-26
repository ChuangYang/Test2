using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.ComponentModel;
using System.Reflection;
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L1_ExceptionExtensions;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_0_TypeExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L6_ObjectExtensions
{
    /// <summary>
    /// ObjectExtensions
    /// </summary>
    public static class CObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iWhenSourceIsNull"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToStringT<T>(this T ioSource, string iWhenSourceIsNull, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                if (iWhenSourceIsNull.extIsNotNull())
                {
                    return iWhenSourceIsNull;
                }
            }
            
            if (ioSource is string)
            {
                return (ioSource as string);
            }
            else if (ioSource is Enum)
            {
                return (ioSource as Enum).extToString(iExceptionHandler);
            }
            else if (ioSource is Exception)
            {
                return (ioSource as Exception).extToString();
            }

            return CTryCatchObserver.Register(
                () =>
                {
                    if (ioSource is DescriptionAttribute)
                    {
                        return (ioSource as DescriptionAttribute).Description;
                    }

                    return ioSource.ToString();
                },
                iExceptionHandler
                ).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToStringT<T>(this T ioSource, Action<Exception> iExceptionHandler)
        {
            return extToStringT(ioSource, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetDescriptions<T>(this T ioSource, Action<Exception> iExceptionHandler = null)
        {
            return CSharp.L3_TypeExtensions.CTypeExtensions.extGetDescriptions((ioSource.extIsNull() ? typeof(T) : ioSource.GetType()), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iName"></param>
        /// <param name="iMemberTypes"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetMemberDescriptions<T>(this T ioSource, string iName, MemberTypes iMemberTypes = MemberTypes.All, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return CTypeExtensions.extGetMemberDescriptions((ioSource.extIsNull() ? typeof(T) : ioSource.GetType()), iName, iMemberTypes, iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iFieldName"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo extGetField<T>(this T ioSource, string iFieldName, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return CTypeExtensions.extGetField((ioSource.extIsNull() ? typeof(T) : ioSource.GetType()), iFieldName, iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields<T>(this T ioSource, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return CTypeExtensions.extGetFields((ioSource.extIsNull() ? typeof(T) : ioSource.GetType()), iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioDesignatedFieldNames"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields<T>(this T ioSource, IEnumerable<string> ioDesignatedFieldNames, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return CTypeExtensions.extGetFields((ioSource.extIsNull() ? typeof(T) : ioSource.GetType()), ioDesignatedFieldNames, iBindingFlags, iExceptionHandler);
        }
    }
}
