// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Star.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace BL4N
{
    /// <summary> The backlog. for Star API </summary>
    public partial class Backlog
    {
        private void AddStar(string key, long id)
        {
            var api = GetApiUri(new[] { "stars" });
            var kvs = new[] { new KeyValuePair<string, string>(key, id.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult(api, hc);
            System.Diagnostics.Trace.WriteLine(res);
        }

        /// <summary>
        /// Add Star
        /// Adds star to issue
        /// </summary>
        /// <param name="issueId">issue id</param>
        public void AddStarToIssue(long issueId)
        {
            AddStar("issueKey", issueId);
        }

        /// <summary>
        /// Add Star
        /// Adds star to comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        public void AddStarToComment(long commentId)
        {
            AddStar("commentId", commentId);
        }

        /// <summary>
        /// Add Star
        /// Adds star to wiki page
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        public void AddStarToWikiPage(long wikiId)
        {
            AddStar("wikiId", wikiId);
        }
    }
}