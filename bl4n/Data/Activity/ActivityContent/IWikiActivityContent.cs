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
        long Id { get; }

        string Name { get; }

        string Content { get; }

        string Diff { get; }

        long Version { get; }

        IList<IAttachment> Attachments { get; }

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