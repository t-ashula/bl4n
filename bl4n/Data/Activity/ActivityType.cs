// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityType.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// Activity Type
    /// see http://developer.nulab-inc.com/docs/backlog/api/2/get-activities
    /// </summary>
    public enum ActivityType
    {
        /// <summary> unknown </summary>
        Unknown = -1,

        /// <summary> Issue Created </summary>
        IssueCreated = 1,

        /// <summary> Issue Updated </summary>
        IssueUpdated = 2,

        /// <summary> Issue Commented </summary>
        IssueCommented = 3,

        /// <summary> Issue Deleted </summary>
        IssueDeleted = 4,

        /// <summary> Wiki Page Created </summary>
        WikiCreated = 5,

        /// <summary> Wiki Page Updated </summary>
        WikiUpdated = 6,

        /// <summary> Wiki Page Deleted </summary>
        WikiDeleted = 7,

        /// <summary> Shared File Added </summary>
        FileAdded = 8,

        /// <summary> Shared File Updated </summary>
        FileUpdated = 9,

        /// <summary> Shared File Deleted </summary>
        FileDeleted = 10,

        /// <summary> Subersion Committed </summary>
        SVNCommitted = 11,

        /// <summary> Git Pushed </summary>
        GitPushed = 12,

        /// <summary> Git Repository Created </summary>
        GitRepositoryCreated = 13,

        /// <summary> Multiple Issue Updated </summary>
        IssueMultiUpdated = 14,

        /// <summary> User Added to Project </summary>
        ProjectUserAdded = 15,

        /// <summary> User Deleted from Project </summary>
        ProjectUserDeleted = 16,

        /// <summary> Comment Notification Added </summary>
        CommentNotificationAdded = 17
    }
}