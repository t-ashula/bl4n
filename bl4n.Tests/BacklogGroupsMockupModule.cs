// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogGroupsMockupModule.cs">
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
    /// /api/v2/groups routing module
    /// </summary>
    public class BacklogGroupsMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/groups routing
        /// </summary>
        public BacklogGroupsMockupModule()
            : base("/api/v2/groups")
        {
            #region /groups

            Get[""] = _ => Response.AsJson(new[]
            {
                new
                {
                    id = 1,
                    name = "test",
                    members = new[]
                    {
                        new { id = 2, userId = "developer", name = "developer", roleType = 2, /*lang = null,*/ mailAddress = "developer@nulab.example" }
                    },
                    displayOrder = -1,
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z"
                }
            });

            #endregion

            #region post /groups

            Post[""] = p =>
            {
                var req = Request.Form;
                var group = new
                {
                    id = new Random().Next(),
                    name = req.name,
                    members = new[]
                    {
                        new { id = 2, userId = "developer", name = "developer", roleType = 2, mailAddress = "developer@nulab.example" }
                    },
                    displayOrder = -1,
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z",
                };

                return Response.AsJson(group);
            };

            #endregion

            #region /groups/:groupId

            Get["/{groupId}"] = p => Response.AsJson(new
            {
                id = p.groupId,
                name = "test",
                members = new[]
                {
                    new { id = 2, userId = "developer", name = "developer", roleType = 2, mailAddress = "developer@nulab.example" }
                },
                displayOrder = -1,
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2013-05-30T09:11:36Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2013-05-30T09:11:36Z",
            });

            #endregion

            #region patch /groups/:groupId

            Patch["/{groupId}"] = p =>
            {
                var req = Request.Form;

                var group = new
                {
                    id = p.groupId,
                    name = string.IsNullOrEmpty(req.name) ? "oldName" : req.name,
                    members = new[]
                    {
                        new { id = 2, userId = "developer", name = "developer", roleType = 2, mailAddress = "developer@nulab.example" }
                    },
                    displayOrder = -1,
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z",
                };

                return Response.AsJson(group);
            };

            #endregion

            #region delete /groups/:groupId

            Delete["/{groupId}"] = p => Response.AsJson(new
            {
                id = p.groupId,
                name = "test",
                members = new[]
                {
                    new { id = 2, userId = "developer", name = "developer", roleType = 2, mailAddress = "developer@nulab.example" }
                },
                displayOrder = -1,
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2013-05-30T09:11:36Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2013-05-30T09:11:36Z",
            });

            #endregion
        }
    }
}