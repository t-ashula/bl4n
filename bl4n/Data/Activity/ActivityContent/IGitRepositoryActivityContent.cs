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
        IRepository Repository { get; }

        string ChangeType { get; }

        string RevisionType { get; }

        string Ref { get; }

        long RevisionCount { get; }

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