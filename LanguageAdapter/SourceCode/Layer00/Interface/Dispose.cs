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

namespace LanguageAdapter.CSharp.L0_Dispose
{
    /// <summary>
    /// IDispose
    /// </summary>
    public interface IDispose : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime getDisposedTime();

        /// <summary>
        /// 
        /// </summary>
        bool isDisposed();
    }
}
