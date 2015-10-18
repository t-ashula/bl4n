// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGroup.cs" company="">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> ユーザグループを表します． </summary>
    public interface IGroup
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> グループ名を取得します． </summary>
        string Name { get; }

        /// <summary> グループのメンバーリストを取得します． </summary>
        IList<IUser> Members { get; }

        /// <summary> 表示順を取得します． </summary>
        int DisplayOrder { get; }

        /// <summary> 作成ユーザを取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }

        /// <summary> 更新ユーザを取得します． </summary>
        IUser UpdatedUser { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class Group : ExtraJsonPropertyReadableObject, IGroup
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "members")]
        private List<User> _members;

        [IgnoreDataMember]
        public IList<IUser> Members
        {
            get { return _members.ToList<IUser>(); }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}