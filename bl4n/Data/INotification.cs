// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> notification </summary>
    public interface INotification
    {
        long Id { get; }

        bool AlreadyRead { get; }

        int Reason { get; }

        IUser User { get; }

        bool ResouceAlreadyRead { get; }
    }

    /// <summary> </summary>
    [DataContract]
    internal sealed class Notification : INotification
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "alreadyRead")]
        public bool AlreadyRead { get; set; }

        [DataMember(Name = "reason")]
        public int Reason { get; set; }

        [DataMember(Name = "user")]
        private User _user;

        public IUser User
        {
            get { return _user; }
        }

        [DataMember(Name = "resouceAlreadyRead")]
        public bool ResouceAlreadyRead { get; set; }
    }

    [CollectionDataContract]
    internal sealed class Notifications : List<Notification>
    {
    }
}