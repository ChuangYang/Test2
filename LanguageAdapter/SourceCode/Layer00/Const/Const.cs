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

namespace LanguageAdapter.CSharp.L0_Const
{
    /// <summary>
    /// Const
    /// </summary>
    public static class CConst
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int BEGIN_INDEX = 0;

        /// <summary>
        /// 0
        /// </summary>
        public const int EMPTY = 0;

        /// <summary>
        /// 0
        /// </summary>
        public const int ZERO = 0;

        /// <summary>
        /// -1
        /// </summary>
        public const int ALL_ITEMS = -1;

        /// <summary>
        /// -1
        /// </summary>
        public const int NOT_FOUND = (BEGIN_INDEX - 1);

        /// <summary>
        /// 1 == 100%
        /// </summary>
        public const int COMPLETED = 1;

        /// <summary>
        /// ""
        /// </summary>
        public const string STRING_EMPTY = "";

        /// <summary>
        /// "\r\n"
        /// </summary>
        public const string NEW_LINE = "\r\n";

        /// <summary>
        /// "]["
        /// </summary>
        public const string DEFAULT_SEPARATOR = "][";

        /// <summary>
        /// "[{0}]"
        /// </summary>
        public const string DEFAULT_STRING_BUCKET = "[{0}]";
    }
}
