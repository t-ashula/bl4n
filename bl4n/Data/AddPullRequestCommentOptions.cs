// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPullRequestCommentOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// add pull request comment options
    /// </summary>
    public class AddPullRequestCommentOptions : OptionalParams
    {
        private const string ContentProperty = "content";
        private const string NotifiedUserIdProperty = "notifiedUserId[]";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPullRequestCommentOptions"/> class.
        /// </summary>
        /// <param name="content"> content </param>
        public AddPullRequestCommentOptions(string content)
            : base(NotifiedUserIdProperty)
        {
            Content = content;
            _notifiedUserIds = new List<long>();
        }

        /// <summary> Gets or sets the content. </summary>
        public string Content { get; set; }

        private readonly List<long> _notifiedUserIds;

        /// <summary> The add notified user id. </summary>
        /// <param name="id"> id </param>
        public void AddNotifiedUserId(long id)
        {
            _notifiedUserIds.Add(id);
            PropertyChanged(NotifiedUserIdProperty);
        }

        /// <summary> The clear notified user id. </summary>
        public void ClearNotifiedUserId()
        {
            _notifiedUserIds.Clear();
        }

        /// <summary> The remove notified user id. </summary>
        /// <param name="id"> id </param>
        public void RemoveNotifiedUserId(long id)
        {
            _notifiedUserIds.RemoveAll(i => i == id);
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(ContentProperty, Content)
            };

            if (IsPropertyChanged(NotifiedUserIdProperty))
            {
                pairs.AddRange(_notifiedUserIds.ToKeyValuePairs(NotifiedUserIdProperty));
            }

            return pairs;
        }
    }
}