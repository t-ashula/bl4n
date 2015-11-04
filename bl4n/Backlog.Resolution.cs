// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Resolution.cs">
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
    /// <summary> The backlog. for Resolution API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Resolution List
        /// Returns list of resolutions.
        /// </summary>
        /// <returns>list of <see cref="IResolution"/> </returns>
        public IList<IResolution> GetResolutions()
        {
            var api = GetApiUri(new[] { "resolutions" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Resolution>>(api, jss);
            return res.Result.ToList<IResolution>();
        }
    }
}