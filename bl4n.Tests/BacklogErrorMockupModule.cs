// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogErrorMockupModule.cs">
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
    /// for error handling test, not part of Backlog API
    /// </summary>
    public class BacklogErrorMockupModule : NancyModule
    {
        /// <summary>
        /// error test routing
        /// </summary>
        public BacklogErrorMockupModule() :
            base("/api/v2/_error")
        {
            Get["/{reason:int}/{status:int}"] = p =>
            {
                int status = p.status;
                int reason = p.reason;
                var errorResponse = new { errors = new[] { new { message = "error message", code = reason, moreInfo = string.Empty } } };
                return Response.AsJson(errorResponse, (HttpStatusCode)status);
            };
        }
    }
}