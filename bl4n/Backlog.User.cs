// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.User.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for User API </summary>
    public partial class Backlog
    {
        /// <summary> Get User List Returns list of users in your space. </summary>
        /// <returns> List of <see cref="IUser"/>. </returns>
        public IList<IUser> GetUsers()
        {
            var api = GetApiUri(new[] { "users" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            var users = res.Result;
            return users.ToList<IUser>();
        }

        /// <summary> Get User returns information about user. </summary>
        /// <param name="userId">user id ( not nickname )</param>
        /// <returns><see cref="IUser"/></returns>
        public IUser GetUser(int userId)
        {
            var api = GetApiUri(new[] { "users", string.Format("{0}", userId) });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Adds new user to the space.
        /// "Project Administrator" cannot add "Admin" user.
        /// </summary>
        /// <param name="options"> new user options </param>
        /// <returns></returns>
        public IUser AddUser(AddUserOptions options)
        {
            var api = GetApiUri(new[] { "users" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update User
        /// Updates information about user.
        /// </summary>
        /// <param name="userId">user id (number)</param>
        /// <param name="options">update options</param>
        /// <returns>updated <see cref="IUser"/></returns>
        public IUser UpdateUser(long userId, UpdateUserOptions options)
        {
            var api = GetApiUri(new[] { "users", string.Format("{0}", userId) });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete User. Deletes user from the space.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>deleted user</returns>
        public IUser DeleteUser(long uid)
        {
            var api = GetApiUri(new[] { "users", string.Format("{0}", uid) });
            var jss = new JsonSerializerSettings();
            var res = DeleteApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Own User. Returns own information about user.
        /// /users/myself
        /// </summary>
        /// <returns> <see cref="IUser"/> of myself </returns>
        public IUser GetOwnUser()
        {
            var api = GetApiUri(new[] { "users", "myself" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get User Icon. Downloads user icon.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>user icon</returns>
        public ILogo GetUserIcon(long uid)
        {
            var api = GetApiUri(new[] { "users", string.Format("{0}", uid), "icon" });
            var res = GetApiResultAsFile(api);

            return new Logo(res.Result.Item1, res.Result.Item2);
        }

        /// <summary>
        /// Get User Recent Updates. Returns user's recent updates
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="filter">activity filtering option</param>
        /// <returns> List of <see cref="IActivity"/>. </returns>
        public IList<IActivity> GetUserRecentUpdates(long uid, RecentUpdateFilterOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", string.Format("{0}", uid), "activities" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
        }

        /// <summary>
        /// Get Received Star List.
        /// Returns the list of stars that user received.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="filter">result paging option</param>
        /// <returns>list of <see cref="IStar"/></returns>
        public IList<IStar> GetReceivedStarList(long uid, ResultPagingOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", string.Format("{0}", uid), "stars" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
            };
            var res = GetApiResult<List<Star>>(api, jss);
            return res.Result.ToList<IStar>();
        }

        /// <summary>
        /// Count User Received Stars
        /// Returns number of stars that user received.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="term">term (since and/or until)</param>
        /// <returns><see cref="ICounter"/></returns>
        public ICounter CountUserReceivedStars(long uid, TermOptions term = null)
        {
            var query = term == null ? null : term.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", string.Format("{0}", uid), "stars", "count" }, query);
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Recently Viewed Issues
        /// Returns list of issues which the user viewed recently.
        /// </summary>
        /// <param name="offset">offse/count/ordre option</param>
        /// <returns> list of <see cref="IIssue"/>. </returns>
        public IList<IIssueUpdate> GetListOfRecentlyViewedIssues(OffsetOptions offset = null)
        {
            var query = offset == null ? null : offset.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", "myself", "recentlyViewedIssues" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<IssueUpdate>>(api, jss);
            return res.Result.ToList<IIssueUpdate>();
        }

        /// <summary>
        /// Get List of Recently Viewed Projects
        /// Returns list of projects which the user viewed recently.
        /// </summary>
        /// <param name="offset">offse/count/ordre option</param>
        /// <returns>return list of <see cref="IProjectUpdate"/></returns>
        public IList<IProjectUpdate> GetListOfRecentlyViewedProjects(OffsetOptions offset = null)
        {
            var query = offset == null ? null : offset.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", "myself", "recentlyViewedProjects" }, query);
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<ProjectUpdate>>(api, jss);
            return res.Result.ToList<IProjectUpdate>();
        }

        /// <summary>
        /// Get List of Recently Viewed Wikis
        /// Returns list of Wikis which the user viewed recently.
        /// </summary>
        /// <param name="offset">offse/count/ordre option</param>
        /// <returns> return list of <see cref="IWikiPageUpdate"/> </returns>
        public IList<IWikiPageUpdate> GetListOfRecentlyViewedWikis(OffsetOptions offset = null)
        {
            var query = offset == null ? null : offset.ToKeyValuePairs();
            var api = GetApiUri(new[] { "users", "myself", "recentlyViewedWikis" }, query);
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<WikiPageUpdate>>(api, jss);
            return res.Result.ToList<IWikiPageUpdate>();
        }
    }
}