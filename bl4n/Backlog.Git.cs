// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Git.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for Git API (deprecated) </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        [Obsolete("use GetProjectsGitRepositories")]
        public IList<IRepositoryDetail> GetGitRepositories(long projectId)
        {
            return GetGitRepositories(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        [Obsolete("use GetProjectsGitRepositories")]
        public IList<IRepositoryDetail> GetGitRepositories(string projectKey)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectKey)
            };
            var api = GetApiUri(new[] { "git", "repositories" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<RepositoryDetail>>(api, jss);
            return res.Result.ToList<IRepositoryDetail>();
        }
    }
}