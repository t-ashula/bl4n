// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGitRepositoryActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 12, 13 </summary>
    public interface IGitRepositoryActivityContent : IActivityContent
    {
        IRepository Repository { get; }

        string ChangeType { get; }

        string RevisionType { get; }

        string Ref { get; }

        long RevisionCount { get; }

        IList<IRevision> Revisions { get; }
    }
}