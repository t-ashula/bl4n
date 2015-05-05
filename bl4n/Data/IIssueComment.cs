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
        /// <summary> ID ���擾���܂� </summary>
        long Id { get; }

        /// <summary> �R�����g�{�����擾���܂� </summary>
        string Content { get; }

        /// <summary> �`�F���W���O���擾���܂� </summary>
        IList<IChangeLogDetail> ChangeLog { get; }

        /// <summary> �쐬���[�U���擾���܂� </summary>
        IUser CreatedUser { get; }

        /// <summary> �쐬�������擾���܂� </summary>
        DateTime Created { get; }

        /// <summary> �X�V�������擾���܂� </summary>
        DateTime Updated { get; }

        /// <summary> �X�^�[�̈ꗗ���擾���܂� </summary>
        IList<IStar> Stars { get; }

        /// <summary> �ʒm�̈ꗗ���擾���܂� </summary>
        IList<INotification> Notifications { get; }
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
        private List<Notification> _Notifications;

        [IgnoreDataMember]
        public IList<INotification> Notifications
        {
            get { return _Notifications.ToList<INotification>(); }
        }
    }
}