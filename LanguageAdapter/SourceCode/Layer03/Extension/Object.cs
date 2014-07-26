using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace LanguageAdapter.CSharp.L3_ObjectExtensions
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
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsDefault<T>(this T ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                return true;
            }

            Type mType = ioSource.GetType();

            if (mType.IsClass)
            {
                return false;
            }

            //IsValueType.
            return CTryCatchObserver.Register(() => ioSource.Equals(default(T)), iExceptionHandler).Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static MemoryStream extToMemoryStream<T>(this T ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return null;
            }
            else if (!ioSource.GetType().IsSerializable)
            {
                iExceptionHandler.extInvoke(new NotSupportedException("else if (!ioSource.GetType().IsSerializable)"));

                return null;
            }

            MemoryStream mMemoryStream = new MemoryStream();

            (new BinaryFormatter()).Serialize(mMemoryStream, ioSource);

            mMemoryStream.Position = CConst.BEGIN_INDEX;

            return mMemoryStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static byte[] extToBytes<T>(this T ioSource, Action<Exception> iExceptionHandler = null)
        {
            using (MemoryStream mMemoryStream = extToMemoryStream(ioSource, iExceptionHandler))
            {
                return ((mMemoryStream == null) ? new byte[CConst.EMPTY] : mMemoryStream.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T extDeepCopy<T>(this T ioSource, Action<Exception> iExceptionHandler = null) where T : class
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return null;
            }
            else if (ioSource is string) //Call by Copy.
            {
                return ioSource;
            }
            else if (ioSource is decimal) //Call by Copy.
            {
                return ioSource;
            }
            else if (ioSource.GetType().IsPrimitive) //Call by Copy.
            {
                return ioSource;
            }
            //else if (ioSource.GetType().IsValueType) //The member(s) of the structure may not be serializable.
            //{
            //    return ioSource;
            //}

            using (MemoryStream mMemoryStream = extToMemoryStream(ioSource, iExceptionHandler))
            {
                return ((mMemoryStream == null) ? null : ((new BinaryFormatter()).Deserialize(mMemoryStream) as T));
            }
        }
    }
}
