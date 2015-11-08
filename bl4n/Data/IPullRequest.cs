// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPullRequest.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary>
    /// git repository pull request
    /// </summary>
    public interface IPullRequest
    {
        /// <summary>gets pull request id </summary>
        long Id { get; }

        /// <summary>gets project id </summary>
        long ProjectId { get; }

        /// <summary>gets repository id </summary>
        long RepositoryId { get; }

        /// <summary>gets pull request number </summary>
        long Number { get; }

        /// <summary>gets pull request summary </summary>
        string Summary { get; }

        /// <summary>gets pull request description </summary>
        string Description { get; }

        /// <summary>gets branch name merge to </summary>
        string Base { get; }

        /// <summary>gets branch name request from</summary>
        string Branch { get; }

        /// <summary>gets status </summary>
        IStatus Status { get; }

        /// <summary>gets assignee user </summary>
        IUser Assignee { get; }

        /// <summary>get issue (id only) </summary>
        IIssue Issue { get; }

        /// <summary>gets base commit </summary>
        string BaseCommit { get; } // TODO: type

        /// <summary>gets branch commit </summary>
        string BranchCommit { get; } // TODO: type

        /// <summary>gets close datetime </summary>
        DateTime? CloseAt { get; }

        /// <summary>gets merge datetime </summary>
        DateTime? MergeAt { get; }

        /// <summary>gets create user </summary>
        IUser CreatedUser { get; }

        /// <summary>gets create datetime </summary>
        DateTime Created { get; }

        /// <summary>gets update user </summary>
        IUser UpdatedUser { get; }

        /// <summary>gets update datetime </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal class PullRequest : ExtraJsonPropertyReadableObject, IPullRequest
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; set; }

        [DataMember(Name = "repositoryId")]
        public long RepositoryId { get; set; }

        [DataMember(Name = "number")]
        public long Number { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "branch")]
        public string Branch { get; set; }

        [DataMember(Name = "status")]
        private Status _status;

        public IStatus Status
        {
            get { return _status; }
        }

        [DataMember(Name = "assignee")]
        private User _assignee;

        public IUser Assignee
        {
            get { return _assignee; }
        }

        [DataMember(Name = "issue")]
        private Issue _issue;

        public IIssue Issue
        {
            get { return _issue; }
        }

        [DataMember(Name = "baseCommit")]
        public string BaseCommit { get; set; }

        [DataMember(Name = "branchCommit")]
        public string BranchCommit { get; set; }

        [DataMember(Name = "closeAt")]
        public DateTime? CloseAt { get; set; }

        [DataMember(Name = "mergeAt")]
        public DateTime? MergeAt { get; set; }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}