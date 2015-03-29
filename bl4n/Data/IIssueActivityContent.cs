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
    public interface IIssueActivityContent : IActivityContent
    {
        long Id { get; }

        long KeyId { get; }

        string Sumary { get; }

        string Description { get; }

        IComment Comment { get; }

        IList<IChange> Changes { get; }

        IList<IAttachment> Attachments { get; }

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
        private Changes _changes;

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