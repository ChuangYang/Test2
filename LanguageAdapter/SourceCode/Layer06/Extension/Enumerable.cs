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
using LanguageAdapter.CSharp.L1_StaticToolbox;
using LanguageAdapter.CSharp.L2_0_ExceptionObserver;
using LanguageAdapter.CSharp.L2_1_TryCatchObserver;
using LanguageAdapter.CSharp.L2_2_ActionObserver;
using LanguageAdapter.CSharp.L2_3_FuncObserver;
using LanguageAdapter.CSharp.L3_CollectionExtensions;
using LanguageAdapter.CSharp.L3_EnumerableExtensions;
using LanguageAdapter.CSharp.L3_EnumerableTExtensions;
using LanguageAdapter.CSharp.L4_EnumerableTExtensions;
#endregion

#region Set the aliases.
using CL3IEnumerableTExtensions = LanguageAdapter.CSharp.L3_EnumerableTExtensions.CIEnumerableTExtensions;
#endregion

namespace LanguageAdapter.CSharp.L6_EnumerableExtensions
{
    /// <summary>
    /// IEnumerableExtensions
    /// </summary>
    public static class CIEnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableForeach<T>(this IEnumerable ioSource, Action<T> iAction, Func<T, bool> iBreak, Action<Exception> iExceptionHandler = null)
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

            CTryCatchObserver.Register(
                () =>
                {
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
                },
                iExceptionHandler
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableForeach<T>(this IEnumerable ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            ioSource.enumerableForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableForeach<T>(this IEnumerable ioSource, Action<T, int> iAction, Func<T, int, bool> iBreak, Action<Exception, int> iExceptionHandler = null)
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

            CTryCatchObserver.Register(
                () =>
                {
                    if (iBreak == null)
                    {
                        foreach (T mItem in ioSource)
                        {
                            iAction.extInvoke(mItem, mIndex, iExceptionHandler, mIndex);

                            mIndex++;
                        }
                    }
                    else
                    {
                        foreach (T mItem in ioSource)
                        {
                            iAction.extInvoke(mItem, mIndex, iExceptionHandler, mIndex);

                            if (iBreak.extInvoke(mItem, mIndex, iExceptionHandler, mIndex).Item2)
                            {
                                break;
                            }

                            mIndex++;
                        }
                    }
                },
                (ioException => iExceptionHandler.extInvoke(ioException, mIndex))
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableForeach<T>(this IEnumerable ioSource, Action<T, int> iAction, Action<Exception, int> iExceptionHandler = null)
        {
            ioSource.enumerableForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableReverseForeach<T>(this IEnumerable ioSource, Action<T> iAction, Func<T, bool> iBreak, Action<Exception> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"));

                return;
            }
            else if (!(ioSource is IList))
            {
                ioSource.enumerableSkipThanTake<T>().Reverse().extForeach(iAction, iBreak, iExceptionHandler);

                return;
            }

            IList mSource = (ioSource as IList);

            CTryCatchObserver.Register(
                () =>
                {
                    if (iBreak == null)
                    {
                        for (int i = mSource.extLastIndex(CStaticToolbox.throwException); i >= CConst.BEGIN_INDEX; i--)
                        {
                            iAction.extInvoke((T)mSource[i], iExceptionHandler);
                        }
                    }
                    else
                    {
                        for (int i = mSource.extLastIndex(CStaticToolbox.throwException); i >= CConst.BEGIN_INDEX; i--)
                        {
                            iAction.extInvoke((T)mSource[i], iExceptionHandler);

                            if (iBreak.extInvoke((T)mSource[i], iExceptionHandler).Item2)
                            {
                                break;
                            }
                        }
                    }
                },
                iExceptionHandler
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableReverseForeach<T>(this IEnumerable ioSource, Action<T> iAction, Action<Exception> iExceptionHandler = null)
        {
            ioSource.enumerableReverseForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iBreak"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableReverseForeach<T>(this IEnumerable ioSource, Action<T, int> iAction, Func<T, int, bool> iBreak, Action<Exception, int> iExceptionHandler = null)
        {
            if (ioSource.extIsNull())
            {
                iExceptionHandler.extInvoke(new ArgumentNullException("if (ioSource.extIsNull())"), CConst.NOT_FOUND);

                return;
            }
            else if (!(ioSource is IList))
            {
                ioSource.enumerableSkipThanTake<T>().Reverse().extForeach(iAction, iBreak, iExceptionHandler);

                return;
            }

            IList mSource = (ioSource as IList);
            int mIndex = CConst.BEGIN_INDEX;

            CTryCatchObserver.Register(
                () =>
                {
                    if (iBreak == null)
                    {
                        for (mIndex = mSource.extLastIndex(CStaticToolbox.throwException); mIndex >= CConst.BEGIN_INDEX; mIndex--)
                        {
                            iAction.extInvoke((T)mSource[mIndex], mIndex, iExceptionHandler, mIndex);
                        }
                    }
                    else
                    {
                        for (mIndex = mSource.extLastIndex(CStaticToolbox.throwException); mIndex >= CConst.BEGIN_INDEX; mIndex--)
                        {
                            iAction.extInvoke((T)mSource[mIndex], mIndex, iExceptionHandler, mIndex);

                            if (iBreak.extInvoke((T)mSource[mIndex], mIndex, iExceptionHandler, mIndex).Item2)
                            {
                                break;
                            }
                        }
                    }
                },
                (ioException => iExceptionHandler.extInvoke(ioException, mIndex))
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioSource"></param>
        /// <param name="iAction"></param>
        /// <param name="iExceptionHandler"></param>
        public static void enumerableReverseForeach<T>(this IEnumerable ioSource, Action<T, int> iAction, Action<Exception, int> iExceptionHandler = null)
        {
            ioSource.enumerableReverseForeach(iAction, null, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="ioRandom"></param>
        /// <param name="iShufflingTimes"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public static T[] enumerableShuffleItems<T>(this IEnumerable ioBucket, Random ioRandom, int iShufflingTimes, Action<Exception> iExceptionHandler = null)
        {
            return ioBucket.enumerableSkipThanTake<T>(iExceptionHandler).extShuffleItems(ioRandom, iShufflingTimes, iExceptionHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ioBucket"></param>
        /// <param name="ioRandom"></param>
        /// <param name="iExceptionHandler"></param>
        /// <param name="iShufflingTimes"></param>
        /// <returns></returns>
        public static T[] enumerableShuffleItems<T>(this IEnumerable ioBucket, Random ioRandom, Action<Exception> iExceptionHandler = null, int iShufflingTimes = CL3IEnumerableTExtensions.DEFAULT_SHUFFLING_TIMES)
        {
            return enumerableShuffleItems<T>(ioBucket, ioRandom, iShufflingTimes, iExceptionHandler);
        }
    }
}
