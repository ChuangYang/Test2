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

namespace LanguageAdapter.CSharp.L0_Create
{
    /// <summary>
    /// ICreate
    /// </summary>
    public interface ICreate
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime getCreationTime();

        /// <summary>
        /// 
        /// </summary>
        TimeSpan getElapsedTime();
    }
}
