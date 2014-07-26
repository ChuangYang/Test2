using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region .NET Framework namespace.
#endregion

#region Third party libraries.
#endregion

#region Users' libraries.
using LanguageAdapter.CSharp.L1_CollectionExtensions;
using LanguageAdapter.CSharp.L4_StackFrameHelper;
using LanguageAdapter.CSharp.L6_BaseObject;
#endregion

#region Set the aliases.
#endregion

namespace LanguageAdapter.CSharp.L7_CreatedStacksObject
{
    /// <summary>
    /// CreatedStacksObject
    /// </summary>
    [Serializable]
    public abstract class CCreatedStacksObject : CBaseObject
    {
        #region Fields and properties.
        //[NonSerialized]
        //private readonly SynchronizedReadOnlyCollection<string> fCreatedStacks;

        private readonly string[] fCreatedStacks;
        #endregion

        #region Singleton, factory or constructor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioCreatedStacks"></param>
        protected CCreatedStacksObject(SynchronizedReadOnlyCollection<string> ioCreatedStacks)
            //: base() //If the constructor is protected, the singleton class can be inherited.
        {
            #region Check the input(s) and create the local parameter(s).
            #endregion

            #region Implement.
            SynchronizedReadOnlyCollection<string> mCreatedStacks = (ioCreatedStacks.extIsNullOrEmpty() ?
                CStackFrameHelper.getReadOnlyStackFrames(CStackFrameHelper.getModifiedStackFrameIndex()) :
                ioCreatedStacks);

            fCreatedStacks = Array.ConvertAll(mCreatedStacks.ToArray(), ioStackFrame => ioStackFrame.ToString());
            #endregion

            #region Handle the exception(s).
            #endregion

            #region Recheck the output(s).
            #endregion

            #region Return the output(s).
            #endregion
        }
        #endregion

        #region Destructor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDisposeManagedResources"></param>
        protected override void Dispose(bool iDisposeManagedResources) //i: The input parameter of the method.
        {
            if (isDisposed())
            {
                return;
            }

            base.Dispose(iDisposeManagedResources);

            if (iDisposeManagedResources)
            { }

            //Free native resources.
        }
        #endregion

        #region Methods.
        ///// <summary>
        ///// This is [NonSerialized].
        ///// </summary>
        ///// <returns></returns>
        //public SynchronizedReadOnlyCollection<string> getCreatedStacks()
        //{
        //    return fCreatedStacks;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] getCreatedStacks()
        {
            return fCreatedStacks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWithFieldNames"></param>
        /// <param name="iDateTimeKind"></param>
        /// <param name="iExceptionHandler"></param>
        /// <returns></returns>
        public abstract override string[] toStrings(bool iWithFieldNames, DateTimeKind iDateTimeKind = DateTimeKind.Utc, Action<Exception> iExceptionHandler = null);
        #endregion
    }
}
