// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiActivityContent.cs">
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
    /// <summary> content for type 5, 6, 7 </summary>
    public interface IWikiActivityContent : IActivityContent
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> ページ名を取得します． </summary>
        string Name { get; }

        /// <summary> 内容を取得します． </summary>
        string Content { get; }

        /// <summary> 差分を取得します． </summary>
        string Diff { get; }

        /// <summary> ヴァージョン文字列を取得します． </summary>
        long Version { get; }

        /// <summary> 添付ファイルの一覧を取得します． </summary>
        IList<IAttachment> Attachments { get; }

        /// <summary> 共有ファイルへの参照の一覧を取得します． </summary>
        IList<ISharedFile> SharedFiles { get; }
    }

    [DataContract]
    internal sealed class WikiActivityContent : IWikiActivityContent
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "content")]
        public string Content { get; private set; }

        [DataMember(Name = "diff")]
        public string Diff { get; private set; }

        [DataMember(Name = "version")]
        private long? _version;

        public long Version
        {
            get { return _version ?? 0; }
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