// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueActivityContent.cs">
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
    /// <summary> 課題に関するアクティビティを表します． </summary>
    public interface IIssueActivityContent : IActivityContent
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> KeyId を取得します． </summary>
        long KeyId { get; }

        /// <summary> サマリー を取得します． </summary>
        string Sumary { get; }

        /// <summary> 説明文字列 を取得します． </summary>
        string Description { get; }

        /// <summary> コメント を取得します． </summary>
        IComment Comment { get; }

        /// <summary> 差分情報のリスト を取得します． </summary>
        IList<IChange> Changes { get; }

        /// <summary> 添付ファイルのリスト を取得します． </summary>
        IList<IAttachment> Attachments { get; }

        /// <summary> リンクされた共有ファイルのリストを取得します． </summary>
        IList<ISharedFile> SharedFiles { get; }
    }

    [DataContract]
    internal sealed class IssueActivityContent : ActivityContent, IIssueActivityContent
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "key_id")]
        public long KeyId { get; private set; }

        [DataMember(Name = "summary")]
        public string Sumary { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "comment")]
        private Comment _comment;

        [IgnoreDataMember]
        public IComment Comment
        {
            get { return _comment; }
        }

        [DataMember(Name = "changes")]
        private List<Change> _changes;

        [IgnoreDataMember]
        public IList<IChange> Changes
        {
            get { return _changes.ToList<IChange>(); }
        }

        [DataMember(Name = "attachments")]
        private List<Attachment> _attachments;

        [IgnoreDataMember]
        public IList<IAttachment> Attachments
        {
            get { return _attachments.ToList<IAttachment>(); }
        }

        [DataMember(Name = "shared_files")]
        private List<SharedFile> _sharedfiles;

        [IgnoreDataMember]
        public IList<ISharedFile> SharedFiles
        {
            get { return _sharedfiles.ToList<ISharedFile>(); }
        }
    }
}