// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueUpdate.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IIssueUpdate
    {
        IIssue Issue { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class IssueUpdate : IIssueUpdate
    {
        [DataMember(Name = "issue")]
        private Issue _issue;

        [IgnoreDataMember]
        public IIssue Issue
        {
            get { return _issue; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}