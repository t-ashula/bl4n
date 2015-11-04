// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Priority.cs">
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
    /// <summary> The backlog. for Priority API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Priority List
        /// Returns list of priorities.
        /// </summary>
        /// <returns>list of <see cref="IPriority"/></returns>
        public IList<IPriority> GetPriorities()
        {
            var api = GetApiUri(new[] { "priorities" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Priority>>(api, jss);
            return res.Result.ToList<IPriority>();
        }
    }
}