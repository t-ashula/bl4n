// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogIssueMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BL4N.Data;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/issues routeing module
    /// </summary>
    public class BacklogIssueMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/issues routing
        /// </summary>
        public BacklogIssueMockupModule()
            : base("/api/v2/issues")
        {
            Get[""] = p =>
            {
                var req = Request.Query;
                var reqProjectIds = req["projectIds[]"];
                var pids = reqProjectIds.HasValue ? ((string)reqProjectIds).Split(',').Select(int.Parse).ToArray() : new[] { 0 };
                if (pids[0] == 0)
                {
                    // TODO: error response
                }

                return Response.AsJson(new[]
                {
                    new Issue{ Id = 1, ProjectId = pids[0] }
                });
            };
        }
    }
}