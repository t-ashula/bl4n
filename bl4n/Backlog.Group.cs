// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Group.cs">
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
    /// <summary> The backlog. for Group API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get List of Groups
        /// Returns list of groups.
        /// </summary>
        /// <param name="offset">offse/count/ordre option</param>
        /// <returns>retur list of <see cref="IGroup"/></returns>
        public IList<IGroup> GetGroups(OffsetOptions offset = null)
        {
            var query = offset == null ? null : offset.ToKeyValuePairs();
            var api = GetApiUri(new[] { "groups" }, query);
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<Group>>(api, jss);
            return res.Result.ToList<IGroup>();
        }

        /// <summary>
        /// Add Group.
        /// Adds new group.
        /// </summary>
        /// <param name="options">group options</param>
        /// <returns>created <see cref="IGroup"/></returns>
        public IGroup AddGroup(AddGroupOptions options)
        {
            var api = GetApiUri(new[] { "groups" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Group>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Group
        /// Returns information about group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <returns><see cref="IGroup"/></returns>
        public IGroup GetGroup(long groupId)
        {
            var api = GetApiUri(new[] { "groups", string.Format("{0}", groupId) });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<Group>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Group
        /// Updates information about group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="options">update group options</param>
        /// <returns>changed <see cref="IGroup"/></returns>
        public IGroup UpdateGroup(long groupId, UpdateGroupOptions options)
        {
            var api = GetApiUri(new[] { "groups", string.Format("{0}", groupId) });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Group>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Group
        /// Deletes group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <returns>deleted <see cref="IGroup"/></returns>
        public IGroup DeleteGroup(long groupId)
        {
            var api = GetApiUri(new[] { "groups", string.Format("{0}", groupId) });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = DeleteApiResult<Group>(api, jss);
            return res.Result;
        }
    }
}