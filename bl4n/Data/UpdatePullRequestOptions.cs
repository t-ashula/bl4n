// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePullRequestOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> </summary>
    public class UpdatePullRequestOptions : OptionalParams
    {
        private const string SummaryProperty = "summary";
        private const string DescriptionProperty = "description";
        private const string IssueIdProperty = "issueId";
        private const string AssigneeIdProperty = "assigneeId";
        private const string NotifiedUserIdsProperty = "notifiedUserId[]";
        private const string CommentProperty = "comment";

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePullRequestOptions"/> class.
        /// </summary>
        public UpdatePullRequestOptions()
            : base(SummaryProperty, DescriptionProperty, IssueIdProperty, AssigneeIdProperty, NotifiedUserIdsProperty, CommentProperty)
        {
            _notifiedUserIds = new List<long>();
        }

        private string _summary;

        /// <summary> Gets or sets the sumary. </summary>
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                PropertyChanged(SummaryProperty);
            }
        }

        private string _description;

        /// <summary> Gets or sets the description. </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(DescriptionProperty);
            }
        }

        private long _issueId;

        /// <summary> Gets or sets the issue id. </summary>
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

        /// <summary> Gets or sets the assignee id. </summary>
        public long AssigneeId
        {
            get { return _assigneeId; }
            set
            {
                _assigneeId = value;
                PropertyChanged(AssigneeIdProperty);
            }
        }

        private readonly List<long> _notifiedUserIds;

        /// <summary> The add notified user id. </summary>
        /// <param name="id"> id </param>
        public void AddNotifiedUserId(long id)
        {
            _notifiedUserIds.Add(id);
            PropertyChanged(NotifiedUserIdsProperty);
        }

        /// <summary> The clear notified user ids. </summary>
        public void ClearNotifiedUserIds()
        {
            _notifiedUserIds.Clear();
        }

        /// <summary> The remove notified user id. </summary>
        /// <param name="id"> id </param>
        public void RemoveNotifiedUserId(long id)
        {
            _notifiedUserIds.RemoveAll(i => i == id);
        }

        private string _comment;

        /// <summary> Gets or sets the comment. </summary>
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                PropertyChanged(CommentProperty);
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(SummaryProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(SummaryProperty, Summary));
            }

            if (IsPropertyChanged(DescriptionProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(DescriptionProperty, Description));
            }

            if (IsPropertyChanged(IssueIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(IssueIdProperty, $"{IssueId}"));
            }

            if (IsPropertyChanged(AssigneeIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(AssigneeIdProperty, $"{AssigneeId}"));
            }

            if (IsPropertyChanged(NotifiedUserIdsProperty))
            {
                pairs.AddRange(_notifiedUserIds.ToKeyValuePairs(NotifiedUserIdsProperty));
            }

            if (IsPropertyChanged(CommentProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(CommentProperty, Comment));
            }

            return pairs;
        }
    }
}