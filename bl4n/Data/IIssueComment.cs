// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueComment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary>
    /// comment in issue
    /// </summary>
    public interface IIssueComment
    {
        /// <summary> ID を取得します </summary>
        long Id { get; }

        /// <summary> コメント本文を取得します </summary>
        string Content { get; }

        /// <summary> チェンジログを取得します </summary>
        IList<IChangeLogDetail> ChangeLog { get; }

        /// <summary> 作成ユーザを取得します </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します </summary>
        DateTime Created { get; }

        /// <summary> 更新日時を取得します </summary>
        DateTime Updated { get; }

        /// <summary> スターの一覧を取得します </summary>
        IList<IStar> Stars { get; }

        /// <summary> 通知の一覧を取得します </summary>
        IList<ICommentNotification> Notifications { get; }
    }

    [DataContract]
    internal class IssueComment : IIssueComment
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "content")]
        public string Content { get; private set; }

        [DataMember(Name = "changeLog")]
        private List<ChangeLogDetail> _changeLog;

        [IgnoreDataMember]
        public IList<IChangeLogDetail> ChangeLog
        {
            get { return _changeLog.ToList<IChangeLogDetail>(); }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }

        [DataMember(Name = "stars")]
        private List<Star> _stars;

        [IgnoreDataMember]
        public IList<IStar> Stars
        {
            get { return _stars.ToList<IStar>(); }
        }

        [DataMember(Name = "notifications")]
        private List<CommentNotification> _Notifications;

        [IgnoreDataMember]
        public IList<ICommentNotification> Notifications
        {
            get { return _Notifications.ToList<ICommentNotification>(); }
        }
    }
}