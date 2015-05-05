// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
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

        /// <summary> ユーザーを取得します． </summary>
        IUser User { get; }

        /// <summary> 対象を既読かどうかを取得します． </summary>
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
}