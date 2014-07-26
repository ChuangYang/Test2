using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
using System.Data;
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

namespace LanguageAdapter.CSharp.L3_CollectionTExtensions
{
    /// <summary>
    /// ICollectionTExtensions
    /// </summary>
    public static class CICollectionTExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static int collectionTLastIndex<T>(this ICollection<T> ioSource, Action<Exception> iExceptionHandler = null)
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
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extIsReadOnly<T>(this ICollection<T> ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return true;
            }

            return ioSource.IsReadOnly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioItem"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extAdd<T>(this ICollection<T> ioSource, T ioItem, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return false;
            }
            else if (ioSource.IsReadOnly)
            {
                iExceptionHandler.extInvoke(new ReadOnlyException("else if (ioSource.IsReadOnly)"), false);

                return false;
            }

            ioSource.Add(ioItem);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioItems"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extAdd<T>(this ICollection<T> ioSource, IEnumerable<T> ioItems, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return false;
            }
            else if (ioSource.IsReadOnly)
            {
                iExceptionHandler.extInvoke(new ReadOnlyException("else if (ioSource.IsReadOnly)"), false);

                return false;
            }
            else if (ioItems.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (ioItems.extIsNull())"), false);

                return false;
            }

            foreach (T mItem in ioItems)
            {
                ioSource.Add(mItem);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extClear<T>(this ICollection<T> ioSource, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return false;
            }
            else if (ioSource.IsReadOnly)
            {
                iExceptionHandler.extInvoke(new ReadOnlyException("else if (ioSource.IsReadOnly)"), false);

                return false;
            }

            ioSource.Clear();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioItems"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static IEnumerable<bool> extContains<T>(this ICollection<T> ioSource, IEnumerable<T> ioItems, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                yield break;
            }
            else if (ioItems.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (ioItems.extIsNull())"), false);

                yield break;
            }

            foreach (T mItem in ioItems)
            {
                yield return ioSource.Contains(mItem);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioItem"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static bool extRemove<T>(this ICollection<T> ioSource, T ioItem, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return false;
            }
            else if (ioSource.IsReadOnly)
            {
                iExceptionHandler.extInvoke(new ReadOnlyException("else if (ioSource.IsReadOnly)"), false);

                return false;
            }

            return ioSource.Remove(ioItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="ioItems"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static IEnumerable<bool> extRemove<T>(this ICollection<T> ioSource, IEnumerable<T> ioItems, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                yield break;
            }
            else if (ioSource.IsReadOnly)
            {
                iExceptionHandler.extInvoke(new ReadOnlyException("else if (ioSource.IsReadOnly)"), false);

                yield break;
            }
            else if (ioItems.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (ioItems.extIsNull())"), false);

                yield break;
            }

            foreach (T mItem in ioItems)
            {
                yield return ioSource.Remove(mItem);
            }
        }
    }
}
