// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPullRequestOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加する PullRequest のオプションを表します </summary>
    public class AddPullRequestOptions : OptionalParams
    {
        private const string SummaryProperty = "summary";
        private const string DescriptionProperty = "description";
        private const string BaseProperty = "base";
        private const string BranchProperty = "branch";
        private const string IssueIdProperty = "issueId";
        private const string AssigneeIdProperty = "assigneeId";
        private const string AttachmentIdProperty = "attachmentId[]";
        private const string NotifiedUserIdProperty = "notifiedUserId[]";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPullRequestOptions"/> class.
        /// </summary>
        /// <param name="sumary"> The sumary. </param>
        /// <param name="description"> pull request description </param>
        /// <param name="base">base branch name</param>
        /// <param name="branch">requesting branch name</param>
        public AddPullRequestOptions(string sumary, string description, string @base, string branch)
            : base(IssueIdProperty, AssigneeIdProperty, AttachmentIdProperty, NotifiedUserIdProperty)
        {
            Summary = sumary;
            Description = description;
            Base = @base;
            Branch = branch;
            _notifiedUserId = new List<long>();
            _attachmentIds = new List<long>();
        }

        /// <summary> gets or sets pull request summary </summary>
        public string Summary { get; set; }

        /// <summary> gets or sets pull request description </summary>
        public string Description { get; set; }

        /// <summary> gets or sets pull request branch name merge from </summary>
        public string Base { get; set; }

        /// <summary> gets or sets pull request branch name merge to </summary>
        public string Branch { get; set; }

        private long _issueId;

        /// <summary> gets or sets related issue id </summary>
        public long IssueId
        {
            get { return _issueId; }
            set
            {
                _issueId = value;
                PropertyChanged(IssueIdProperty);
            }
        }

        private long _assigneeId;

        /// <summary> gets or sets assignee user id </summary>
        public long AssigneeId
        {
            get { return _assigneeId; }
            set
            {
                _assigneeId = value;
                PropertyChanged(AssigneeIdProperty);
            }
        }

        private readonly List<long> _notifiedUserId;

        /// <summary> add notified user id </summary>
        /// <param name="id">user id</param>
        public void AddNotifiedUserId(long id)
        {
            _notifiedUserId.Add(id);
            PropertyChanged(NotifiedUserIdProperty);
        }

        /// <summary> clear notified user ids </summary>
        public void ClearNotifiedUserId()
        {
            _notifiedUserId.Clear();
            PropertyChanged(NotifiedUserIdProperty);
        }

        /// <summary> remove notified user id </summary>
        public void RemoveNotifiedUserId(long id)
        {
            _notifiedUserId.RemoveAll(i => i == id);
            PropertyChanged(NotifiedUserIdProperty);
        }

        private readonly List<long> _attachmentIds;

        /// <summary> add attachment file id </summary>
        /// <param name="id">user id</param>
        public void AddAttachmentId(long id)
        {
            _attachmentIds.Add(id);
            PropertyChanged(AttachmentIdProperty);
        }

        /// <summary> clear attachment file ids </summary>
        public void ClearAttachmentId()
        {
            _attachmentIds.Clear();
            PropertyChanged(AttachmentIdProperty);
        }

        /// <summary> remove attachment file id </summary>
        public void RemoveAttachmentId(long id)
        {
            _attachmentIds.RemoveAll(i => i == id);
            PropertyChanged(AttachmentIdProperty);
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(SummaryProperty, Summary),
                new KeyValuePair<string, string>(DescriptionProperty, Description),
                new KeyValuePair<string, string>(BaseProperty, Base),
                new KeyValuePair<string, string>(BranchProperty, Branch)
            };

            if (IsPropertyChanged(IssueIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(IssueIdProperty, $"{IssueId}"));
            }

            if (IsPropertyChanged(AssigneeIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(AssigneeIdProperty, $"{AssigneeId}"));
            }

            if (IsPropertyChanged(NotifiedUserIdProperty))
            {
                pairs.AddRange(_notifiedUserId.ToKeyValuePairs(NotifiedUserIdProperty));
            }

            if (IsPropertyChanged(AttachmentIdProperty))
            {
                pairs.AddRange(_attachmentIds.ToKeyValuePairs(AttachmentIdProperty));
            }

            return pairs;
        }
    }
}