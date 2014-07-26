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
using LanguageAdapter.CSharp.L0_ObjectExtensions;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_2_ActionObserver;
using LanguageAdapter.CSharp.L2_3_FuncObserver;
using LanguageAdapter.CSharp.L3_CollectionTExtensions;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L4_EnumerableTExtensions
{
    /// <summary>
    /// IEnumerableTExtensions
    /// </summary>
    public static class CIEnumerableTExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extForeach<T>(this IEnumerable<T> ioSource, Action<T> iAction, Func<T, bool> iBreak, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return;
            }
            else if (iAction == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (iAction == null)"));

                return;
            }

            if (iBreak == null)
            {
                foreach (T mItem in ioSource)
                {
                    iAction.extInvoke(mItem, iExceptionHandler);
                }
            }
            else
            {
                foreach (T mItem in ioSource)
                {
                    iAction.extInvoke(mItem, iExceptionHandler);

                    if (iBreak.extInvoke(mItem, iExceptionHandler).Item2)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extForeach<T>(this IEnumerable<T> ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            ioSource.extForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extForeach<T>(this IEnumerable<T> ioSource, Action<T, int> iAction, Func<T, int, bool> iBreak, Action<Exception, int> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"), CConst.NOT_FOUND);

                return;
            }
            else if (iAction == null)
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("else if (iAction == null)"), CConst.NOT_FOUND);

                return;
            }

            int mIndex = CConst.BEGIN_INDEX;

            if (iBreak == null)
            {
                foreach (T mItem in ioSource)
                {
                    iAction.extInvoke(mItem, mIndex, iExceptionHandler, mIndex++);
                }
            }
            else
            {
                foreach (T mItem in ioSource)
                {
                    iAction.extInvoke(mItem, mIndex, iExceptionHandler, mIndex);

                    if (iBreak.extInvoke(mItem, mIndex, iExceptionHandler, mIndex++).Item2)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extForeach<T>(this IEnumerable<T> ioSource, Action<T, int> iAction, Action<Exception, int> iExceptionHandler = null)
        {
            ioSource.extForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extReverseForeach<T>(this IEnumerable<T> ioSource, Action<T> iAction, Func<T, bool> iBreak, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return;
            }
            else if (!(ioSource is IList<T>))
            {
                ioSource.Reverse().extForeach(iAction, iBreak, iExceptionHandler);

                return;
            }

            IList<T> mSource = (ioSource as IList<T>);

            if (iBreak == null)
            {
                for (int i = mSource.collectionTLastIndex(iExceptionHandler); i >= CConst.BEGIN_INDEX; i--)
                {
                    iAction.extInvoke(mSource[i], iExceptionHandler);
                }
            }
            else
            {
                for (int i = mSource.collectionTLastIndex(iExceptionHandler); i >= CConst.BEGIN_INDEX; i--)
                {
                    iAction.extInvoke(mSource[i], iExceptionHandler);

                    if (iBreak.extInvoke(mSource[i], iExceptionHandler).Item2)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extReverseForeach<T>(this IEnumerable<T> ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            ioSource.extReverseForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extReverseForeach<T>(this IEnumerable<T> ioSource, Action<T, int> iAction, Func<T, int, bool> iBreak, Action<Exception, int> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"), CConst.NOT_FOUND);

                return;
            }
            else if (!(ioSource is IList<T>))
            {
                ioSource.Reverse().extForeach(iAction, iBreak, iExceptionHandler);

                return;
            }

            IList<T> mSource = (ioSource as IList<T>);
            int mIndex = CConst.BEGIN_INDEX;

            if (iBreak == null)
            {
                for (mIndex = mSource.collectionTLastIndex(ioException => iExceptionHandler.extInvoke(ioException, mIndex)); mIndex >= CConst.BEGIN_INDEX; mIndex--)
                {
                    iAction.extInvoke(mSource[mIndex], mIndex, iExceptionHandler, mIndex);
                }
            }
            else
            {
                for (mIndex = mSource.collectionTLastIndex(ioException => iExceptionHandler.extInvoke(ioException, mIndex)); mIndex >= CConst.BEGIN_INDEX; mIndex--)
                {
                    iAction.extInvoke(mSource[mIndex], mIndex, iExceptionHandler, mIndex);

                    if (iBreak.extInvoke(mSource[mIndex], mIndex, iExceptionHandler, mIndex).Item2)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void extReverseForeach<T>(this IEnumerable<T> ioSource, Action<T, int> iAction, Action<Exception, int> iExceptionHandler = null)
        {
            ioSource.extReverseForeach(iAction, null, iExceptionHandler);
        }
    }
}
