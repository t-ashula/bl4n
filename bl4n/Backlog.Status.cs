// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Status.cs">
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
    /// <summary> The backlog. for Statuses API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Status List
        /// Returns list of statuses.
        /// </summary>
        /// <returns>list of <see cref="IStatus"/> </returns>
        public IList<IStatus> GetStatuses()
        {
            var api = GetApiUri(new[] { "statuses" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Status>>(api, jss);
            return res.Result.ToList<IStatus>();
        }
    }
}