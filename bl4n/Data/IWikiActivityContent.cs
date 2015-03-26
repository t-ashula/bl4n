// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 5, 6, 7 </summary>
    public interface IWikiActivityContent : IActivityContent
    {
        long Id { get; }

        string Name { get; }

        string Content { get; }

        string Diff { get; }

        long Version { get; }

        IList<IAttachment> Attachments { get; }

        IList<ISharedFile> SharedFile { get; }
    }
}