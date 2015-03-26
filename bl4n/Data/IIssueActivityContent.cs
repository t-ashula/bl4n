// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 1, 2, 3, 4 </summary>
    public interface IIssueActivityContent : IActivityContent
    {
        long Id { get; }

        long KeyId { get; }

        string Sumary { get; }

        string Description { get; }

        IComment Comment { get; }

        IList<IChange> Changes { get; }

        IList<IAttachment> Attachments { get; }

        IList<ISharedFile> SharedFiles { get; }
    }
}