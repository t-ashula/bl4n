// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryDetail.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> git repository Detail </summary>
    public interface IRepositoryDetail
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクト ID を取得します． </summary>
        long ProjectId { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> フック URL を取得します． </summary>
        string HookUrl { get; }

        /// <summary> リポジトリの HTTPS でのアクセス用 URL を取得します． </summary>
        string HttpUrl { get; }

        /// <summary> リポジトリの SSH/git でのアクセス用 URL を取得します． </summary>
        string SshUrl { get; }

        /// <summary> 表示順を取得します． </summary>
        long DisplayOrder { get; }

        /// <summary> 最終 push 日時を取得します． </summary>
        DateTime PushedAt { get; }

        /// <summary> 作成ユーザーを取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }

        /// <summary> 更新ユーザーを取得します． </summary>
        IUser UpdatedUser { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal class RepositoryDetail : ExtraJsonPropertyReadableObject, IRepositoryDetail
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "hookUrl")]
        public string HookUrl { get; private set; }

        [DataMember(Name = "httpUrl")]
        public string HttpUrl { get; private set; }

        [DataMember(Name = "sshUrl")]
        public string SshUrl { get; private set; }

        [DataMember(Name = "displayOrder")]
        public long DisplayOrder { get; private set; }

        [DataMember(Name = "pushedAd")]
        public DateTime PushedAt { get; private set; }

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