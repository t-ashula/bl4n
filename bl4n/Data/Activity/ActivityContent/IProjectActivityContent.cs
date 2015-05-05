// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectActivityContent.cs">
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
    /// <summary> content for type 15, 16 </summary>
    public interface IProjectActivityContent : IActivityContent
    {
        /// <summary> 関係するユーザの一覧を取得します． </summary>
        IList<IUser> Users { get; }

        /// <summary> グループの更新情報の一覧を取得します． </summary>
        IList<IGroupProjectActivity> GroupProjectActivites { get; }

        /// <summary> コメントを取得します． </summary>
        string Comment { get; }
    }

    [DataContract]
    internal sealed class ProjectActivityContent : ActivityContent, IProjectActivityContent
    {
        [DataMember(Name = "users")]
        private List<User> _users;

        [IgnoreDataMember]
        public IList<IUser> Users
        {
            get { return _users.ToList<IUser>(); }
        }

        [DataMember(Name = "group_project_activities")]
        private List<GroupProjectActivity> _groupProjectActivities;

        [IgnoreDataMember]
        public IList<IGroupProjectActivity> GroupProjectActivites
        {
            get { return _groupProjectActivities.ToList<IGroupProjectActivity>(); }
        }

        [DataMember(Name = "comment")]
        public string Comment { get; private set; }
    }
}