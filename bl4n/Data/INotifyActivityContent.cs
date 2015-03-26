// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotifyActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 17 </summary>
    public interface INotifyActivityContent : IActivityContent
    {
        long Id { get; }

        long KeyId { get; }

        string Summary { get; }

        string Description { get; }

        IComment Comment { get; }

        IList<IChange> Changes { get; }

        IList<IAttachment> Attachments { get; }

        IList<ISharedFile> SharedFiles { get; }
    }
}