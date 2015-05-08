// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogStarMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/stars routeing module
    /// </summary>
    public class BacklogStarMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/stars routing
        /// </summary>
        public BacklogStarMockupModule()
            : base("/api/v2/stars")
        {
            //// string issueId = Request.Form["issueId"];
            //// string commentId = Request.Form["commentId"];
            //// string wikiId = Request.Form["wikiId"];
            Post[string.Empty] = p => HttpStatusCode.NoContent;
        }
    }
}