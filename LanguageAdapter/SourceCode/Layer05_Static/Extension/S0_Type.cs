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
using LanguageAdapter.CSharp.L1_EnumerableExtensions;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L5_0_TypeExtensions
{
    /// <summary>
    /// TypeExtensions
    /// </summary>
    public static class CTypeExtensions
    {
        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        private static readonly BindingFlags fDefaultBindingFlags;
        #endregion

        #region Singleton, factory or constructor.
        static CTypeExtensions() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            fCreationTime = DateTime.UtcNow;

            fDefaultBindingFlags = (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.CreateInstance);

            CEnumerationHelper.Cache<MemberTypes>(CStaticToolbox.throwException);
            CEnumerationHelper.Cache<BindingFlags>(CStaticToolbox.throwException);
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
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsStaticClass(this _Type ioType, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return false;
            }
            else if (ioType.BaseType.extIsNull())
            {
                return false;
            }
            else if (!ioType.BaseType.Equals(typeof(object)))
            {
                return false;
            }
            else if (!(ioType.IsClass && ioType.IsAbstract && ioType.IsSealed))
            {
                return false;
            }

            ConstructorInfo[] mConstructors = ioType.GetConstructors(BindingFlags.Static | BindingFlags.NonPublic);

            if (mConstructors.Length == CConst.EMPTY) //If these is no definition in the code.
            {
                return true;
            }
            else if (mConstructors.Length > 1) //Static class is only have one constructor.
            {
                return false;
            }

            ConstructorInfo mConstructor = mConstructors[CConst.BEGIN_INDEX];

            return (mConstructor.IsPrivate && mConstructor.IsSpecialName && mConstructor.IsStatic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsStaticClass(this Type ioType, Action<Exception> iExceptionHandler = null)
        {
            return extIsStaticClass((ioType as _Type), iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iName"></param>
        /// <param name="iMemberTypes"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetMemberDescriptions(this _Type ioType, string iName, MemberTypes iMemberTypes = MemberTypes.All, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return new string[CConst.EMPTY];
            }
            else if (string.IsNullOrWhiteSpace(iName))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrWhiteSpace(iName))"));

                return new string[CConst.EMPTY];
            }

            MemberInfo[] mMemberInfos = ioType.GetMember(iName, iMemberTypes, (iBindingFlags ?? fDefaultBindingFlags));

            IEnumerable<DescriptionAttribute> mDescriptions = mMemberInfos.SelectMany(ioMemberInfo => (ioMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]));

            return mDescriptions.Select(ioAttribute => ioAttribute.Description).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iName"></param>
        /// <param name="iMemberTypes"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string[] extGetMemberDescriptions(this Type ioType, string iName, MemberTypes iMemberTypes = MemberTypes.All, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return extGetMemberDescriptions((ioType as _Type), iName, iMemberTypes, iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iName"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo extGetField(this _Type ioType, string iName, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return null;
            }
            else if (string.IsNullOrWhiteSpace(iName))
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (string.IsNullOrWhiteSpace(iName))"));

                return null;
            }

            return ioType.GetField(iName, (iBindingFlags ?? fDefaultBindingFlags));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iName"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo extGetField(this Type ioType, string iName, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return extGetField((ioType as _Type), iName, iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields(this _Type ioType, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return new FieldInfo[CConst.EMPTY];
            }

            return ioType.GetFields(iBindingFlags ?? fDefaultBindingFlags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields(this Type ioType, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return extGetFields((ioType as _Type), iBindingFlags, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="ioNames"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields(this _Type ioType, IEnumerable<string> ioNames, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            if (ioType.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioType.extIsNull())"));

                return new FieldInfo[CConst.EMPTY];
            }
            else if (ioNames.enumerableIsNullOrEmpty())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (ioNames.enumerableIsNullOrEmpty())"));

                return new FieldInfo[CConst.EMPTY];
            }

            return ioNames.Select(iName => ioType.extGetField(iName, iBindingFlags, iExceptionHandler)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioType"></param>
        /// <param name="ioDesignatedFieldNames"></param>
        /// <param name="iBindingFlags"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static FieldInfo[] extGetFields(this Type ioType, IEnumerable<string> ioDesignatedFieldNames, BindingFlags? iBindingFlags = null, Action<Exception> iExceptionHandler = null)
        {
            return extGetFields((ioType as _Type), ioDesignatedFieldNames, iBindingFlags, iExceptionHandler);
        }
        #endregion
    }
}
