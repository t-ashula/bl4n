// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiPage.cs">
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
    /// <summary> Wiki のページを表します． </summary>
    public interface IWikiPage
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクト ID を取得します． </summary>
        long ProjectId { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> ページ内容を取得します． </summary>
        string Content { get; }

        /// <summary> タグの一覧を取得します． </summary>
        IList<ITag> Tags { get; }

        /// <summary> 作成者を取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }

        /// <summary> 更新ユーザを取得します． </summary>
        IUser UpdatedUser { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class WikiPage : ExtraJsonPropertyReadableObject, IWikiPage
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "content")]
        public string Content { get; private set; }

        [DataMember(Name = "tags")]
        private List<Tag> _tags;

        [IgnoreDataMember]
        public IList<ITag> Tags
        {
            get { return _tags.ToList<ITag>(); }
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