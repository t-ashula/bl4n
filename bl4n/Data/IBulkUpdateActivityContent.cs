// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBulkUpdateActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 14 </summary>
    public interface IBulkUpdateActivityContent : IActivityContent
    {
        long TxId { get; }

        // comment but no id
        IComment Comment { get; }

        IList<ILink> Link { get; }

        IList<IChange> Changes { get; }
    }
}