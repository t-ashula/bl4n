// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IActivity.cs">
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
    /// <summary> activity </summary>
    public interface IActivity
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクトを取得します． </summary>
        IProject Project { get; }

        /// <summary> 種別を取得します． </summary>
        int Type { get; }

        /// <summary> 更新内容を取得します． </summary>
        IActivityContent Content { get; }

        /// <summary> 通知の一覧を取得します． </summary>
        IList<INotification> Notifications { get; }

        /// <summary> 作成ユーザを取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal abstract class Activity : IActivity
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "project")]
        private Project _project;

        [IgnoreDataMember]
        public IProject Project
        {
            get { return _project; }
        }

        [DataMember(Name = "type")]
        public int Type { get; private set; }

        [IgnoreDataMember]
        public abstract IActivityContent Content { get; }

        [DataMember(Name = "notifications")]
        private List<Notification> _notifications;

        [IgnoreDataMember]
        public IList<INotification> Notifications
        {
            get { return _notifications.ToList<INotification>(); }
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
    }
}