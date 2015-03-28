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
        long Id { get; }

        IProject Project { get; }

        int Type { get; }

        IActivityContent Content { get; }

        IList<INotification> Notifications { get; }

        IUser CreatedUser { get; }

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
        private Notifications _notifications;

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