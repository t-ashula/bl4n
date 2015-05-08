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

            #region /api/v2/wikis/tags

            Get["/tags"] = p => Response.AsJson(new[] { new { id = 1, name = "test" } });

            #endregion

            #region POST /api/v2/wikis

            Post[string.Empty] = p =>
            {
                string projectId = Request.Form["projectId"];
                string name = Request.Form["name"];
                string content = Request.Form["content"];
                //// bool notify = Request.Form["mailNotify"] != null;
                return Response.AsJson(new
                {
                    id = 1,
                    projectId = projectId,
                    name = name,
                    content = content,
                    tags = new[] { new { id = 12, name = "prpceedings" } },
                    //// attachments = [],
                    //// sharedFiles = [],
                    //// stars = [],
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2012-07-23T06:09:48Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2012-07-23T06:09:48Z"
                });
            };

            #endregion

            #region GET /api/v2/wikis/:id

            Get["/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = "Home",
                content = "Content",
                tags = new[] { new { id = 12, name = "prpceedings" } },
                //// attachments = [],
                //// sharedFiles = [],
                //// stars = [],
                createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2012-07-23T06:09:48Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2012-07-23T06:09:48Z"
            });

            #endregion

            #region PATCH /api/v2/wikis/:id

            Patch["/{id}"] = p =>
            {
                string name = Request.Form["name"];
                string content = Request.Form["content"];
                //// bool notify = Request.Form["mailNotify"] != null;
                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = 1,
                    name = name,
                    content = content,
                    tags = new[] { new { id = 12, name = "prpceedings" } },
                    //// attachments = [],
                    //// sharedFiles = [],
                    //// stars = [],
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2012-07-23T06:09:48Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2012-07-23T06:09:48Z"
                });
            };

            #endregion

            #region DELETE /api/v2/wikis/:id

            Delete["/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = "Home",
                content = "Content",
                tags = new[] { new { id = 12, name = "prpceedings" } },
                //// attachments = [],
                //// sharedFiles = [],
                //// stars = [],
                createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2012-07-23T06:09:48Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2012-07-23T06:09:48Z"
            });

            #endregion
        }
    }
}