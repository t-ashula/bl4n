// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebHook.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> webhook を表します </summary>
    public interface IWebHook
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 名前を取得します． </summary>
        string Name { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> フック URL を取得します． </summary>
        string HookUrl { get; }

        /// <summary> すべてのイベントで反応するかどうかを取得します． </summary>
        bool AllEvent { get; }

        /// <summary> 対象のイベントID の一覧を取得します． </summary>
        IList<int> ActivityTypeIds { get; }

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
    internal class WebHook : IWebHook
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "hookUrl")]
        public string HookUrl { get; set; }

        [DataMember(Name = "allEvent")]
        public bool AllEvent { get; set; }

        [DataMember(Name = "activityTypeIds")]
        private List<int> _activityTypeIds;

        [IgnoreDataMember]
        public IList<int> ActivityTypeIds
        {
            get { return _activityTypeIds; }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}