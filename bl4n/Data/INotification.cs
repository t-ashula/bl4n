// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> notification </summary>
    public interface INotification
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 既読かどうかを取得します． </summary>
        bool AlreadyRead { get; }

        /// <summary> 理由を取得します． </summary>
        int Reason { get; }

        /// <summary> 対象を既読かどうかを取得します． </summary>
        bool ResouceAlreadyRead { get; }

        /// <summary> プロジェクトを取得します </summary>
        IProject Project { get; }

        /// <summary> 課題を取得します </summary>
        IIssue Issue { get; }

        /// <summary> コメントを取得します </summary>
        IComment Comment { get; }

        /// <summary> 送信ユーザーを取得します </summary>
        IUser Sender { get; }

        /// <summary> 作成日時を取得します </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal class Notification : ExtraJsonPropertyReadableObject, INotification
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "alreadyRead")]
        public bool AlreadyRead { get; private set; }

        [DataMember(Name = "reason")]
        public int Reason { get; private set; }

        [DataMember(Name = "resouceAlreadyRead")]
        public bool ResouceAlreadyRead { get; private set; }

        [DataMember(Name = "project")]
        private Project _project;

        [IgnoreDataMember]
        public IProject Project
        {
            get { return _project; }
        }

        [DataMember(Name = "issue")]
        private Issue _issue;

        [IgnoreDataMember]
        public IIssue Issue
        {
            get { return _issue; }
        }

        [DataMember(Name = "comment")]
        private Comment _comment;

        [IgnoreDataMember]
        public IComment Comment
        {
            get { return _comment; }
        }

        [DataMember(Name = "sender")]
        private User _sender;

        [IgnoreDataMember]
        public IUser Sender
        {
            get { return _sender; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }
    }
}