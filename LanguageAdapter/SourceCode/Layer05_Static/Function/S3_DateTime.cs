using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L0_Const;
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L4_EnumerationHelper;
using LanguageAdapter.CSharp.L5_1_StaticWatcher;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L5_3_DateTimeHelper
{
    /// <summary>
    /// DateTimeHelper
    /// </summary>
    public static class CDateTimeHelper
    {
        /// <summary>
        /// "yyyy-MM-dd HH:mm:ss.fffffff"
        /// </summary>
        public const string SQLFormat = "yyyy-MM-dd HH:mm:ss.fffffff";

        #region Fields and properties.
        private static readonly DateTime fCreationTime;

        private static readonly Type fType;
        private static readonly DateTime fDefault;

        private static readonly TimeSpan fInfinite;
        #endregion

        #region Singleton, factory or constructor.
        static CDateTimeHelper() //The CLR guarantees that the static constructor will be invoked only once for the entire lifetime of the application domain.
        {
            CStaticWatcher.Register(fCreationTime = DateTime.UtcNow);

            fType = typeof(DateTime);
            fDefault = new DateTime(CConst.EMPTY, DateTimeKind.Utc);

            fInfinite = new TimeSpan(CConst.NOT_FOUND);

            CEnumerationHelper.Cache<DateTimeKind>(CStaticToolbox.throwException);
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
        public static Type getType()
        {
            return fType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime getDefault()
        {
            return fDefault;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TimeSpan getInfinite()
        {
            return fInfinite;
        }

        /// <summary>
        /// Do not use DateTimeKind.Unspecified.
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static DateTime extToKind(this DateTime iSource, DateTimeKind iKind, Action<Exception> iExceptionHandler = null)
        {
            if (iKind == DateTimeKind.Unspecified)
            {
                iExceptionHandler.extInvoke(new ArgumentException("if (iKind == DateTimeKind.Unspecified)"));

                return iSource;
            }
            else if (iSource.Kind == iKind)
            {
                return iSource;
            }

            switch (iSource.Kind)
            {
                case DateTimeKind.Utc:
                    return iSource.ToLocalTime();

                case DateTimeKind.Local:
                    return iSource.ToUniversalTime();

                default:
                    return new DateTime(iSource.Ticks, iKind);
            }
        }

        /// <summary>
        /// <para>SQL DateTime2(7).</para>
        /// <para>Do not use DateTimeKind.Unspecified.</para>
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToSQLDateTime(this DateTime iSource, Action<Exception> iExceptionHandler = null)
        {
            if (iSource.Kind == DateTimeKind.Unspecified)
            {
                iExceptionHandler.extInvoke(new ArgumentException("if (iSource.Kind == DateTimeKind.Unspecified)"), false);
            }

            return iSource.ToString(SQLFormat);
        }

        /// <summary>
        /// <para>SQL DateTime2(7).</para>
        /// <para>Do not use DateTimeKind.Unspecified.</para>
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static string extToSQLDateTime(this DateTime iSource, DateTimeKind iKind, Action<Exception> iExceptionHandler = null)
        {
            return extToSQLDateTime(extToKind(iSource, iKind, iExceptionHandler), iExceptionHandler);
        }
        #endregion
    }
}
