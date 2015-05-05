// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGitRepositoryActivityContent.cs">
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
    /// <summary> content for type 12, 13 </summary>
    public interface IGitRepositoryActivityContent : IActivityContent
    {
        /// <summary> リポジトリ情報を取得します． </summary>
        IRepository Repository { get; }

        /// <summary> 変更内容を取得します． </summary>
        string ChangeType { get; }

        /// <summary> リビジョンの種別を取得します． </summary>
        string RevisionType { get; }

        /// <summary> 参照先を取得します． </summary>
        string Ref { get; }

        /// <summary> リビジョン数を取得します． </summary>
        long RevisionCount { get; }

        /// <summary> リビジョンの一覧を取得します． </summary>
        IList<IRevision> Revisions { get; }
    }

    [DataContract]
    internal class GitRepositoryActivityContent : ActivityContent, IGitRepositoryActivityContent
    {
        [DataMember(Name = "repository")]
        private Repository _repository;

        [IgnoreDataMember]
        public IRepository Repository
        {
            get { return _repository; }
        }

        [DataMember(Name = "change_type")]
        public string ChangeType { get; private set; }

        [DataMember(Name = "revision_type")]
        public string RevisionType { get; private set; }

        [DataMember(Name = "ref")]
        public string Ref { get; private set; }

        [DataMember(Name = "revision_count")]
        public long RevisionCount { get; private set; }

        [DataMember(Name = "revisions")]
        private List<Revision> _revisions;

        public IList<IRevision> Revisions
        {
            get { return _revisions.ToList<IRevision>(); }
        }
    }
}