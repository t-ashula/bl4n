// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogWikiMockupModule.cs">
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
    /// /api/v2/wikis routing module
    /// </summary>
    public class BacklogWikiMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/wikis routing
        /// </summary>
        public BacklogWikiMockupModule() :
            base("/api/v2/wikis")
        {
            #region /api/v2/wikis

            Get[string.Empty] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 112,
                    projectId = Request.Query["projectIdOrKey"], // TODO
                    name = "Home",
                    tags = new[] { new { id = 12, name = "proceedings" } },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z"
                }
            });

            #endregion

            #region /api/v2/wikis/count

            Get["/count"] = p => Response.AsJson(new { count = 5 });

            #endregion
        }
    }
}