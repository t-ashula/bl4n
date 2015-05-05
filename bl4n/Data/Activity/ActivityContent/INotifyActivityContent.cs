// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotifyActivityContent.cs">
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
    /// <summary> content for type 17 </summary>
    public interface INotifyActivityContent : IActivityContent
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> キーID を取得します． </summary>
        long KeyId { get; }

        /// <summary> サマリーを取得します． </summary>
        string Summary { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> コメントを取得します． </summary>
        IComment Comment { get; }

        /// <summary> 変更内容の一覧を取得します． </summary>
        IList<IChange> Changes { get; }

        /// <summary> 添付ファイルの一覧を取得します． </summary>
        IList<IAttachment> Attachments { get; }

        /// <summary> 共有ファイルへの参照の一覧を取得します． </summary>
        IList<ISharedFile> SharedFiles { get; }
    }

    [DataContract]
    internal sealed class NotifyActivityContent : ActivityContent, INotifyActivityContent
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "key_id")]
        public long KeyId { get; private set; }

        [DataMember(Name = "summary")]
        public string Summary { get; private set; }

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
        private List<SharedFile> _sharedFiles;

        [IgnoreDataMember]
        public IList<ISharedFile> SharedFiles
        {
            get { return _sharedFiles.ToList<ISharedFile>(); }
        }
    }
}