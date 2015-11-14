// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPullRequestActivityContent.cs">
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
    /// <summary> content for type 18,19,20 </summary>
    public interface IPullRequestActivityContent
    {
        /// <summary> Gets ID </summary>
        long Id { get; }

        /// <summary> Gets PR number </summary>
        long Number { get; }

        /// <summary> Get PR summary </summary>
        string Summary { get; }

        /// <summary> Gets PR description </summary>
        string Description { get; }

        /// <summary> Gets PR repository  </summary>
        IRepository Repository { get; }

        /// <summary> Gets PR change content </summary>
        IList<IChange> Changes { get; }

        /// <summary> Gets PR comment </summary>
        IComment Comment { get; }
    }

    [DataContract]
    internal sealed class PullRequestActivityContent : ActivityContent, IPullRequestActivityContent
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "summary")]
        public string Summary { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "number")]
        public long Number { get; private set; }

        [DataMember(Name = "repository")]
        private Repository _repository;

        [IgnoreDataMember]
        public IRepository Repository
        {
            get { return _repository; }
        }

        [DataMember(Name = "changes")]
        private List<Change> _changes;

        [IgnoreDataMember]
        public IList<IChange> Changes
        {
            get { return _changes.ToList<IChange>(); }
        }

        [DataMember(Name = "comment")]
        private Comment _comment;

        [IgnoreDataMember]
        public IComment Comment
        {
            get { return _comment; }
        }
    }
}