using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L0_ObjectExtensions
{
    /// <summary>
    /// ObjectExtensions
    /// </summary>
    public static class CObjectExtensions
    {
        /// <summary>
        /// <para>Equals may be overridden.</para>
        /// <para>Structure may be nullable or be boxed like an object class.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNotNull<T>(this T ioSource)
        {
            //try
            //{
            //    ioSource.GetType();

            //    return false;
            //}
            //catch (Exception mException)
            //{
            //    return true;
            //}
            //finally
            //{ }

            return (ioSource is T);
        }

        /// <summary>
        /// <para>Equals may be overridden.</para>
        /// <para>Structure may be nullable or be boxed like an object class.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <returns></returns>
        public static bool extIsNull<T>(this T ioSource)
        {
            return !extIsNotNull(ioSource);
        }
    }
}
