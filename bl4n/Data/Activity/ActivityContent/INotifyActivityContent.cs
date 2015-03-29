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
        long Id { get; }

        long KeyId { get; }

        string Summary { get; }

        string Description { get; }

        IComment Comment { get; }

        IList<IChange> Changes { get; }

        IList<IAttachment> Attachments { get; }

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