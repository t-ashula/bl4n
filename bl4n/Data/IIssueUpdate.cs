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
    /// <summary> 課題の更新情報を表します </summary>
    public interface IIssueUpdate
    {
        /// <summary> 課題を取得します． </summary>
        IIssue Issue { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class IssueUpdate : ExtraJsonPropertyReadableObject, IIssueUpdate
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