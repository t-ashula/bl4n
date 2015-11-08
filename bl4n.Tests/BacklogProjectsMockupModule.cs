// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogProjectsMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BL4N.Tests.Properties;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/projects routing module
    /// </summary>
    public class BacklogProjectsMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/projects routing
        /// </summary>
        public BacklogProjectsMockupModule()
            : base("/api/v2/projects")
        {
            #region /projects

            Get[""] = _ => Response.AsJson(new[]
            {
                new
                {
                    id = 1,
                    projectKey = "TEST",
                    name = "test",
                    chartEnabled = false,
                    subtaskingEnabled = false,
                    textFormattingRule = "markdown",
                    archived = false
                }
            });

            #endregion

            #region post /projects

            Post[""] = p =>
            {
                var req = Request.Form;
                return Response.AsJson(new
                {
                    id = 1,
                    name = req.name,
                    projectKey = req.key,
                    chartEnabled = req.chartEnabled,
                    subtaskingEnabled = req.subtaskingEnabled,
                    textFormattingRule = req.textFormattingRule
                });
            };

            #endregion

            #region /projects/:projectKey

            Get["/{projectKey}"] = p =>
            {
                var key = p.projectKey;
                long pid;

                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = pid == -1 ? 1 : pid,
                    projectKey = pid == -1 ? key : "TEST",
                    name = "test",
                    chartEnabled = false,
                    subtaskingEnabled = false,
                    textFormattingRule = "markdown",
                    archived = false
                });
            };

            #endregion

            #region patch /projects/:projectKey

            Patch["/{projectKey}"] = p =>
            {
                var req = Request.Form;
                var key = p.projectKey;
                long pid;

                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = pid == -1 ? 1 : pid,
                    projectKey = string.IsNullOrEmpty(req.key) ? (pid == -1 ? key : "TEST") : req.key,
                    name = string.IsNullOrEmpty(req.name) ? "test" : req.name,
                    chartEnabled = false,
                    subtaskingEnabled = false,
                    textFormattingRule = string.IsNullOrEmpty(req.textFormattingRule) ? "markdown" : req.textFormattingRule,
                    archived = false
                });
            };

            #endregion

            #region delete /projects/:projectKey

            Delete["/{projectKey}"] = p =>
            {
                var key = p.projectKey;
                long pid;

                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = pid == -1 ? 1 : pid,
                    projectKey = pid == -1 ? key : "TEST",
                    name = "test",
                    chartEnabled = false,
                    subtaskingEnabled = false,
                    textFormattingRule = "markdown",
                    archived = false
                });
            };

            #endregion

            #region /projects/:projectKey/image

            Get["/{projectKey}/image"] = p =>
            {
                //// Content-Type:application/octet-stream
                //// Content-Disposition:attachment;filename="logo_mark.png"

                var response = new Response
                {
                    ContentType = "application/octet-stream",
                    Contents = stream =>
                    {
                        var logo = Resources.projectIcon;
                        using (var ms = new MemoryStream())
                        {
                            logo.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            using (var writer = new BinaryWriter(stream))
                            {
                                writer.Write(ms.GetBuffer());
                            }
                        }
                    }
                };

                response.Headers.Add("Content-Disposition", "attachment; filename*=UTF-8''logo_mark.png");
                return response;
            };

            #endregion

            #region /projects/:projectKey/activities

            Get["/{projectKey}/activities"] = p =>
            {
                var key = p.projectKey;
                long pid;

                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new[]
                {
                    new
                    {
                        id = 3153,
                        project = new
                        {
                            id = pid == -1 ? 1 : pid,
                            projectKey = pid == -1 ? key : "SUB",
                            name = "Subtasking",
                            chartEnabled = true,
                            subtaskingEnabled = true,
                            textFormattingRule = "markdown",
                            archived = false,
                            displayOrder = 0
                        },
                        type = 2,
                        content = new
                        {
                            id = 4809,
                            key_id = 121,
                            summary = "Comment",
                            description = "",
                            comment = new { id = 7237, content = "" },
                            changes = new[]
                            {
                                new { field = "milestone", new_value = " R2014-07-23", old_value = "", type = "standard" },
                                new { field = "status", new_value = "4", old_value = "1", type = "standard" }
                            }
                        },
                        notifications = new[]
                        {
                            new
                            {
                                id = 25,
                                alreadyRead = false,
                                reason = 2,
                                user = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                resourceAlreadyRead = false
                            },
                        },
                        createdUser = new
                        {
                            id = 1,
                            userId = "admin",
                            name = "admin",
                            roleType = 1,
                            lang = "ja",
                            mailAddress = "eguchi@nulab.example"
                        },
                        created = "2013-12-27T07:50:44Z"
                    }
                });
            };

            #endregion

            #region /projects/:projectKey/diskUsage

            Get["/{projectKey}/diskUsage"] = p =>
            {
                var key = p.projectKey;
                long pid;

                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    projectId = pid == -1 ? 1 : pid,
                    issue = 11931,
                    wiki = 0,
                    file = 0,
                    subversion = 0,
                    git = 0
                });
            };

            #endregion

            #region POST /projects/:projectKey/users

            Post["/{projectKey}/users"] = p =>
            {
                var req = Request.Form;
                var user = new
                {
                    id = req.userId,
                    userId = "admin",
                    name = "admin",
                    roleType = 1,
                    lang = "ja",
                    mailAddress = "eguchi@nulab.example"
                };

                return Response.AsJson(user);
            };

            #endregion

            #region /projects/:projectKey/users

            Get["/{projectKey}/users"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 1,
                    userId = "admin",
                    name = "admin",
                    roleType = 1,
                    lang = "ja",
                    mailAddress = "eguchi@nulab.example"
                }
            });

            #endregion

            #region delete /projects/:projectKey/users

            Delete["/{projectKey}/users"] = p =>
            {
                var req = Request.Form;
                var user = new
                {
                    id = req.userId,
                    userId = "admin",
                    name = "admin",
                    roleType = 1,
                    lang = "ja",
                    mailAddress = "eguchi@nulab.example"
                };

                return Response.AsJson(user);
            };

            #endregion

            #region POST /projects/:projectKey/administrators

            Post["/{projectKey}/administrators"] = p =>
            {
                var req = Request.Form;
                var user = new
                {
                    id = req.userId,
                    userId = "takada",
                    name = "takada",
                    roleType = 2,
                    lang = "ja",
                    mailAddress = "takada@nulab.example"
                };

                return Response.AsJson(user);
            };

            #endregion

            #region /projects/:projectKey/administrators

            Get["/{projectKey}/administrators"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 5686,
                    userId = "takada",
                    name = "takada",
                    roleType = 2,
                    lang = "ja",
                    mailAddress = "takada@nulab.example"
                }
            });

            #endregion

            #region delete /projects/:projectKey/administrators

            Delete["/{projectKey}/administrators"] = p => Response.AsJson(new
            {
                id = Request.Form.userId,
                userId = "takada",
                name = "takada",
                roleType = 2,
                lang = "ja",
                mailAddress = "takada@nulab.example"
            });

            #endregion

            #region /projects/:projectKey/issueTypes

            Get["/{projectKey}/issueTypes"] = p => Response.AsJson(new[]
            {
                new { id = 1, projectId = 1, name = "Bug", color = "#990000", displayOrder = 0 }
            });

            #endregion

            #region POST /projects/:projectKey/issueTypes

            Post["/{projectKey}/issueTypes"] = p => Response.AsJson(new
            {
                id = 1,
                projectId = 1,
                name = Request.Form.name,
                color = Request.Form.color,
                displayOrder = 0
            });

            #endregion

            #region PATCH /projects/:projectKey/issueTypes/:id

            Patch["/{projectKey}/issueTypes/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = string.IsNullOrEmpty(Request.Form.name) ? "Bug" : Request.Form.name,
                color = string.IsNullOrEmpty(Request.Form.color) ? "#990000" : Request.Form.color,
                displayOrder = 0
            });

            #endregion

            #region DELETE /api/v2/projects/:projectIdOrKey/issueTypes/:id

            Delete["/{projectIdOrKey}/issueTypes/{id}"] = p =>
            {
                var subPara = Request.Form.substituteIssueTypeId;
                long subid;
                if (subPara == null || !long.TryParse(subPara, out subid))
                {
                    // error
                }

                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = 1,
                    name = "Bug",
                    color = "#990000",
                    displayOrder = 0
                });
            };

            #endregion

            #region /projects/:projectKey/categories

            Get["/{projectKey}/categories"] = p => Response.AsJson(new[]
            {
                new { id = 12, name = "Development", displayOrder = 0 }
            });

            #endregion

            #region POST /projects/:projectKey/categories

            Post["/{projectKey}/categories"] = p => Response.AsJson(new
            {
                id = 1,
                name = Request.Form.name,
                displayOrder = 0
            });

            #endregion

            #region PATCH /projects/:projectKey/categories/:id

            Patch["/{projectKey}/categories/{id}"] = p => Response.AsJson(new
            {
                id = 1,
                name = Request.Form.name,
                displayOrder = 0
            });

            #endregion

            #region delete /projects/:projectKey/categories/:id

            Delete["/{projectIdOrKey}/categories/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                name = "Bug",
                displayOrder = 0
            });

            #endregion

            #region /projects/:projectKey/versions

            Get["/{projectKey}/versions"] = p =>
            {
                var key = p.projectKey;
                long pid;
                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new[]
                {
                    new
                    {
                        id = 3,
                        projectId = pid == -1 ? 1 : pid,
                        name = "wait for release",
                        description = "",
                        //// startDate = null,
                        //// releaseDueDate = null,
                        archived = false,
                        displayOrder = 0
                    }
                });
            };

            #endregion

            #region POST /projects/:projectKey/versions

            Post["/{projectKey}/versions"] = p =>
            {
                var key = p.projectKey;
                long pid;
                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = 3,
                    projectId = pid == -1 ? 1 : pid,
                    name = Request.Form.name,
                    description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                    startDate = string.IsNullOrEmpty(Request.Form.startDate) ? null : Request.Form.startDate,
                    releaseDueDate = string.IsNullOrEmpty(Request.Form.releaseDueDate) ? null : Request.Form.releaseDueDate,
                    archived = false,
                    displayOrder = 0
                });
            };

            #endregion

            #region PATCH /projects/:projectKey/versions/:id

            Patch["/{projectKey}/versions/{id}"] = p =>
            {
                var key = p.projectKey;
                long pid;
                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = pid == -1 ? 1 : pid,
                    name = Request.Form.name,
                    description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                    startDate = string.IsNullOrEmpty(Request.Form.startDate) ? null : DateTime.Parse(Request.Form.startDate),
                    releaseDueDate = string.IsNullOrEmpty(Request.Form.releaseDueDate) ? null : DateTime.Parse(Request.Form.releaseDueDate),
                    archived = string.IsNullOrEmpty(Request.Form.archived) ? "false" : Request.Form.archived,
                    displayOrder = 0
                });
            };

            #endregion

            #region DELETE /projects/:projectKey/versions/:id

            Delete["/{projectKey}/versions/{id}"] = p =>
            {
                var key = p.projectKey;
                long pid;
                if (!long.TryParse(key, out pid))
                {
                    pid = -1;
                }

                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = pid == -1 ? 1 : pid,
                    name = "wait for release",
                    description = "",
                    //// startDate = null,
                    //// releaseDueDate = null,
                    archived = false,
                    displayOrder = 0
                });
            };

            #endregion

            #region /projects/:projectKey/customFields

            Get["/{projectKey}/customFields"] = p => Response.AsText(
                "["
                + "{"
                + @"""id"": 1, ""typeId"": 6, ""name"": ""custom"", ""description"": """", ""required"": false, ""applicableIssueTypes"": [],""allowAddItem"": false,"
                + @"""items"": [ { ""id"": 1, ""name"": ""Windows 8"", ""displayOrder"": 0 }] "
                + "}, {"
                + @"""id"": 2, ""typeId"": 1, ""name"": ""Attribute for Bug"", ""description"": """", ""required"": false, ""applicableIssueTypes"": [1] "
                + "}" +
                "]");

            #endregion

            #region POST /projects/:projectKey/customFields

            Post["/{projectKey}/customFields"] = p => Response.AsJson(new
            {
                id = 2,
                typeId = 1,
                name = Request.Form.name,
                description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                required = string.IsNullOrEmpty(Request.Form.required) ? "false" : Request.Form.required,
                applicableIssueTypes = new long[] { 1 }
            });

            #endregion

            #region PATCH /projects/:projectKey/customFields

            Patch["/{projectKey}/customFields/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                typeId = 1,
                name = Request.Form.name,
                description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                required = string.IsNullOrEmpty(Request.Form.required) ? "false" : Request.Form.required,
                applicableIssueTypes = new long[] { 1 }
            });

            #endregion

            #region DELETE /projects/:projectKey/customFields

            Delete["/{projectKey}/customFields/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                typeId = 1,
                name = "Attribute for Bug",
                description = string.Empty,
                required = false,
                applicableIssueTypes = new long[] { 1 }
            });

            #endregion

            #region POST /projects/:projectKey/customFields/:id/items

            Post["/{projectKey}/customFields/{id}/items"] = p => Response.AsJson(new
            {
                id = p.id,
                typeId = 5,
                name = "language",
                description = "",
                required = false,
                applicableIssueTypes = new long[0],
                allowAddItem = true,
                items = new[]
                {
                    new { id = 2, name = Request.Form.name, displayOrder = 1 }
                }
            });

            #endregion

            #region PATCH /projects/:projectKey/customFields/:id/items/:itemId

            Patch["/{projectKey}/customFields/{id}/items/{itemId}"] = p => Response.AsJson(new
            {
                id = p.id,
                typeId = 5,
                name = "language",
                description = "",
                required = false,
                applicableIssueTypes = new long[0],
                allowAddItem = true,
                items = new[]
                {
                    new { id = p.itemId, name = Request.Form.name, displayOrder = 1 }
                }
            });

            #endregion

            #region DELETE /projects/:projectKey/customFields/:id/items/:itemId

            Delete["/{projectKey}/customFields/{id}/items/{itemId}"] = p => Response.AsJson(new
            {
                id = p.id,
                typeId = 5,
                name = "language",
                description = "",
                required = false,
                applicableIssueTypes = new long[0],
                allowAddItem = true,
                items = new[]
                {
                    new { id = p.itemId, name = "fsharp", displayOrder = 1 }
                }
            });

            #endregion

            #region /projects/:projectIdOrKey/files/metadata/:path

            // root に対して扱えないので
            Get["/{projectKey}/files/metadata/"] = p => GetFilesResponse(string.Empty);

            Get["/{projectKey}/files/metadata/{path}"] = p => GetFilesResponse(p.path);

            #endregion

            #region /projects/:projectKey/files/:id

            Get["/{projectKey}/files/{id}"] = p =>
            {
                //// Content-Type:application/octet-stream
                //// Content-Disposition:attachment;filename="logo_mark.png"

                var response = new Response
                {
                    ContentType = "application/octet-stream",
                    Contents = stream =>
                    {
                        var logo = Resources.projectIcon;
                        using (var ms = new MemoryStream())
                        {
                            logo.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            using (var writer = new BinaryWriter(stream))
                            {
                                writer.Write(ms.GetBuffer());
                            }
                        }
                    }
                };

                response.Headers.Add("Content-Disposition", "attachment; filename*=UTF-8''logo_mark.png");
                return response;
            };

            #endregion

            #region /projects/:projectKey/webhooks

            Get["/{projectKey}/webhooks"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 3,
                    name = "webhook",
                    description = "",
                    hookUrl = "http://nulab.test/",
                    allEvent = false,
                    activityTypeIds = new[] { 1, 2, 3, 4, 5 },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2014-11-30T01:22:21Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2014-11-30T01:22:21Z"
                }
            });

            #endregion

            #region POST /projects/:projectKey/webhooks

            Post["/{projectKey}/webhooks"] = p =>
            {
                var req = Request.Form;

                var idReq = req["activityTypeIds[]"];
                return Response.AsJson(new
                {
                    id = 3,
                    name = req.name,
                    description = !string.IsNullOrEmpty(req.description) ? req.description : string.Empty,
                    hookUrl = !string.IsNullOrEmpty(req.hookUrl) ? req.hookUrl : string.Empty,
                    allEvent = !string.IsNullOrEmpty(req.allEvent) ? req.allEvent : "false",
                    activityTypeIds = idReq.HasValue ? ((string)idReq).Split(',').Select(int.Parse).ToArray() : new[] { 0 },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2014-11-30T01:22:21Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2014-11-30T01:22:21Z"
                });
            };

            #endregion

            #region /projects/:projectKey/webhooks/:id

            Get["/{projectKey}/webhooks/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                name = "webhook",
                description = "",
                hookUrl = "http://nulab.test/",
                allEvent = false,
                activityTypeIds = new[] { 1, 2, 3, 4, 5 },
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2014-11-30T01:22:21Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2014-11-30T01:22:21Z"
            });

            #endregion

            #region PATCH /projects/:projectKey/webhooks/:id

            Patch["/{projectKey}/webhooks/{id}"] = p =>
            {
                var req = Request.Form;

                var idReq = req["activityTypeIds[]"];
                return Response.AsJson(new
                {
                    id = p.id,
                    name = req.name,
                    description = !string.IsNullOrEmpty(req.description) ? req.description : string.Empty,
                    hookUrl = !string.IsNullOrEmpty(req.hookUrl) ? req.hookUrl : string.Empty,
                    allEvent = !string.IsNullOrEmpty(req.allEvent) ? req.allEvent : "false",
                    activityTypeIds = idReq.HasValue ? ((string)idReq).Split(',').Select(int.Parse).ToArray() : new[] { 0 },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2014-11-30T01:22:21Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2014-11-30T01:22:21Z"
                });
            };

            #endregion

            #region DELETE /projects/:projectKey/webhooks/:id

            Delete["/{projectKey}/webhooks/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                name = "webhook",
                description = "",
                hookUrl = "http://nulab.test/",
                allEvent = false,
                activityTypeIds = new[] { 1, 2, 3, 4, 5 },
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2014-11-30T01:22:21Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2014-11-30T01:22:21Z"
            });

            #endregion

            #region /projects/:projectKey/git/repositories

            Get["/{projectKey}/git/repositories"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 151;
                }

                return Response.AsJson(new[]
                {
                    new
                    {
                        id = 1,
                        projectId = pid,
                        name = "app",
                        description = string.Empty,
                        hookUrl = string.Empty,
                        httpUrl = "https://xx.backlogtool.com/git/BLG/app.git",
                        sshUrl = "xx@xx.git.backlogtool.com:/BLG/app.git",
                        displayOrder = 0,
                        //// pushedAt = null,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2013-05-30T09:11:36Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-05-30T09:11:36Z"
                    }
                });
            };

            #endregion

            #region /projects/:projectKey/git/repositories/:repoName

            Get["/{projectKey}/git/repositories/{repoName}"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 151;
                }

                o = p.repoName;
                long rid;
                if (!long.TryParse(o, out rid))
                {
                    rid = 1;
                }

                return Response.AsJson(new
                {
                    id = rid,
                    projectId = pid,
                    name = rid != 1 ? "app" : p.repoName,
                    description = string.Empty,
                    hookUrl = string.Empty,
                    httpUrl = "https://xx.backlogtool.com/git/BLG/app.git",
                    sshUrl = "xx@xx.git.backlogtool.com:/BLG/app.git",
                    displayOrder = 0,
                    //// pushedAt = null,
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z"
                });
            };

            #endregion

            #region /projects/:projectKey/git/repositories/:repoName/pullRequests

            Get["/{projectKey}/git/repositories/{repoName}/pullRequests"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 151;
                }

                o = p.repoName;
                long rid;
                if (!long.TryParse(o, out rid))
                {
                    rid = 1;
                }

                return Response.AsJson(new[]
                {
                    new
                    {
                        id = 2,
                        projectId = 3,
                        repositoryId = 5,
                        number = 1,
                        summary = "test",
                        description = "test data",
                        @base = "master",
                        branch = "develop",
                        status = new
                        {
                            id = 1,
                            name = "Open"
                        },
                        assignee = new
                        {
                            id = 5,
                            userId = "testuser2",
                            name = "testuser2",
                            roleType = 1,
                            //// lang = null,
                            mailAddress = "testuser2@nulab.test"
                        },
                        issue = new
                        {
                            id = 31
                        },
                        //// baseCommit = null,
                        //// branchCommit = null,
                        //// closeAt= null,
                        //// mergeAt= null,
                        createdUser = new
                        {
                            id = 1,
                            userId = "admin",
                            name = "admin",
                            roleType = 1,
                            lang = "ja",
                            mailAddres = "eguchi@nulab.example"
                        },
                        created = "2015-04-23T03:04:14Z",
                        updatedUser = new
                        {
                            id = 1,
                            userId = "admin",
                            name = "admin",
                            roleType = 1,
                            lang = "ja",
                            mailAddress = "eguchi@nulab.example"
                        },
                        updated = "2015-04-23T03:04:14Z"
                    }
                });
            };

            #endregion

            #region /projects/:projectKey/git/repositories/:repoName/count

            Get["/{projectKey}/git/repositories/{repoName}/pullRequests/count"] = p => Response.AsJson(new { count = 10 });

            #endregion

            #region POST /projects/:projectKey/git/repositories/:repoName/pullRequests

            Post["/{projectKey}/git/repositories/{repoName}/pullRequests"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 3;
                }

                o = p.repoName;
                long rid = 5;
                if (!long.TryParse(o, out rid))
                {
                    rid = 5;
                }

                o = Request.Form["assigneeId"];
                long auid = 5;
                if (!string.IsNullOrEmpty(o))
                {
                    if (!long.TryParse(o, out auid))
                    {
                        auid = 5;
                    }
                }

                o = Request.Form["issueId"];
                long iid = 1;
                if (!string.IsNullOrEmpty(o))
                {
                    if (!long.TryParse(o, out iid))
                    {
                        iid = 1;
                    }
                }

                return Response.AsJson(new
                {
                    id = 2,
                    projectId = pid,
                    repositoryId = rid,
                    number = 1,
                    summary = Request.Form["summary"],
                    description = Request.Form["description"],
                    @base = Request.Form["base"],
                    branch = Request.Form["branch"],
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = auid,
                        userId = "testuser2",
                        name = "testuser2",
                        roleType = 1,
                        mailAddress = "testuser2@nulab.test"
                    },
                    issue = new
                    {
                        id = iid,
                        projectId = pid,
                        issueKey = "BLG-1",
                        keyId = 1,
                        issueType = new { id = 2, projectId = 1, name = "Task", color = "#7ea800", displayOrder = 0 },
                        summary = "first issue",
                        description = string.Empty,
                        //// resolution = null
                        priority = new { id = 3, name = "Normal" },
                        status = new { id = 1, name = "Open" },
                        assignee = new { id = 2, name = "eguchi", roleType = 2, mailAddress = "eguchi@nulab.example" },
                        //// category = new [] { new {} },
                        //// versions = new [] { new {} },
                        milestone = new[]
                        {
                            new
                            {
                                id = 30, projectId = 1, name = "wait for release", description = "", archived = false
                            }
                        },
                        //// startDate = null,
                        //// dueDate = null,
                        //// estimatedHours = null,
                        //// actualHours = null,
                        //// parentIssueId = null,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2012-07-23T06:10:15Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-02-07T08:09:49Z",
                        //// customFields = new [] { new {} },
                        attachments = new[] { new { id = 1, name = "IMGP0088.JPG", size = 85079 } },
                        sharedFiles = new[]
                        {
                            new
                            {
                                id = 454403,
                                type = "file",
                                dir = "/User icon/",
                                name = "male clerk 1.png",
                                size = 2735,
                                createdUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                created = "2009-02-27T03:26:15Z",
                                updatedUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                updated = "2009-03-03T16:57:47Z"
                            }
                        },
                        stars = new[]
                        {
                            new
                            {
                                id = 10,
                                //// comment = null,
                                url = "https://xx.backlog.jp/view/BLG-1",
                                title = "[BLG-1] first issue | Show issue - Backlog",
                                presenter = new
                                {
                                    id = 2,
                                    userId = "eguchi",
                                    name = "eguchi",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "eguchi@nulab.example"
                                },
                                created = "2013-07-08T10:24:28Z"
                            }
                        }
                    },
                    //// baseCommit = null,
                    //// branchCommit = null,
                    //// closeAt = null,
                    //// mergeAt = null,
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2015-04-23T03:04:14Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2015-04-23T03:04:14Z"
                });
            };

            #endregion

            #region /projects/:projectKey/git/repositories/:repoName/pullRequests

            Get["/{projectKey}/git/repositories/{repoName}/pullRequests/{number}"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 3;
                }

                o = p.repoName;
                long rid;
                if (!long.TryParse(o, out rid))
                {
                    rid = 5;
                }

                return Response.AsJson(new
                {
                    id = 2,
                    projectId = pid,
                    repositoryId = rid,
                    number = p.number,
                    summary = "test",
                    description = "test data",
                    @base = "master",
                    branch = "develop",
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = 5,
                        userId = "testuser2",
                        name = "testuser2",
                        roleType = 1,
                        mailAddress = "testuser2@nulab.test"
                    },
                    issue = new
                    {
                        id = 1,
                        projectId = pid,
                        issueKey = "BLG-1",
                        keyId = 1,
                        issueType = new { id = 2, projectId = 1, name = "Task", color = "#7ea800", displayOrder = 0 },
                        summary = "first issue",
                        description = string.Empty,
                        //// resolution = null
                        priority = new { id = 3, name = "Normal" },
                        status = new { id = 1, name = "Open" },
                        assignee = new { id = 2, name = "eguchi", roleType = 2, mailAddress = "eguchi@nulab.example" },
                        //// category = new [] { new {} },
                        //// versions = new [] { new {} },
                        milestone = new[]
                        {
                            new
                            {
                                id = 30, projectId = 1, name = "wait for release", description = "", archived = false
                            }
                        },
                        //// startDate = null,
                        //// dueDate = null,
                        //// estimatedHours = null,
                        //// actualHours = null,
                        //// parentIssueId = null,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2012-07-23T06:10:15Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-02-07T08:09:49Z",
                        //// customFields = new [] { new {} },
                        attachments = new[] { new { id = 1, name = "IMGP0088.JPG", size = 85079 } },
                        sharedFiles = new[]
                        {
                            new
                            {
                                id = 454403,
                                type = "file",
                                dir = "/User icon/",
                                name = "male clerk 1.png",
                                size = 2735,
                                createdUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                created = "2009-02-27T03:26:15Z",
                                updatedUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                updated = "2009-03-03T16:57:47Z"
                            }
                        },
                        stars = new[]
                        {
                            new
                            {
                                id = 10,
                                //// comment = null,
                                url = "https://xx.backlog.jp/view/BLG-1",
                                title = "[BLG-1] first issue | Show issue - Backlog",
                                presenter = new
                                {
                                    id = 2,
                                    userId = "eguchi",
                                    name = "eguchi",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "eguchi@nulab.example"
                                },
                                created = "2013-07-08T10:24:28Z"
                            }
                        }
                    },
                    //// baseCommit = null,
                    //// branchCommit = null,
                    //// closeAt = null,
                    //// mergeAt = null,
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2015-04-23T03:04:14Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2015-04-23T03:04:14Z"
                });
            };

            #endregion

            #region PATCH /projects/:projectKey/git/repositories/:repoName/pullRequests/:number

            Patch["/{projectKey}/git/repositories/{repoName}/pullRequests/{number}"] = p =>
            {
                string o = p.projectKey;
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 3;
                }

                o = p.repoName;
                long rid;
                if (!long.TryParse(o, out rid))
                {
                    rid = 5;
                }

                string summary = Request.Form["summary"];
                string desc = Request.Form["description"];

                long aid = 5;
                o = Request.Form["assigneeId"];
                if (!string.IsNullOrEmpty(o))
                {
                    if (!long.TryParse(o, out aid))
                    {
                        aid = 5;
                    }
                }

                long iid = 1;
                o = Request.Form["issueId"];
                if (!string.IsNullOrEmpty(o))
                {
                    if (!long.TryParse(o, out iid))
                    {
                        iid = 5;
                    }
                }

                return Response.AsJson(new
                {
                    id = 2,
                    projectId = pid,
                    repositoryId = rid,
                    number = p.number,
                    summary = string.IsNullOrEmpty(summary) ? "test" : summary,
                    description = string.IsNullOrEmpty(desc) ? "test data" : desc,
                    @base = "master",
                    branch = "develop",
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = aid,
                        userId = "testuser2",
                        name = "testuser2",
                        roleType = 1,
                        mailAddress = "testuser2@nulab.test"
                    },
                    issue = new
                    {
                        id = iid,
                        projectId = pid,
                        issueKey = "BLG-1",
                        keyId = 1,
                        issueType = new { id = 2, projectId = 1, name = "Task", color = "#7ea800", displayOrder = 0 },
                        summary = "first issue",
                        description = string.Empty,
                        //// resolution = null
                        priority = new { id = 3, name = "Normal" },
                        status = new { id = 1, name = "Open" },
                        assignee = new { id = 2, name = "eguchi", roleType = 2, mailAddress = "eguchi@nulab.example" },
                        //// category = new [] { new {} },
                        //// versions = new [] { new {} },
                        milestone = new[]
                        {
                            new
                            {
                                id = 30, projectId = 1, name = "wait for release", description = "", archived = false
                            }
                        },
                        //// startDate = null,
                        //// dueDate = null,
                        //// estimatedHours = null,
                        //// actualHours = null,
                        //// parentIssueId = null,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2012-07-23T06:10:15Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-02-07T08:09:49Z",
                        //// customFields = new [] { new {} },
                        attachments = new[] { new { id = 1, name = "IMGP0088.JPG", size = 85079 } },
                        sharedFiles = new[]
                        {
                            new
                            {
                                id = 454403,
                                type = "file",
                                dir = "/User icon/",
                                name = "male clerk 1.png",
                                size = 2735,
                                createdUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                created = "2009-02-27T03:26:15Z",
                                updatedUser = new
                                {
                                    id = 5686,
                                    userId = "takada",
                                    name = "takada",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "takada@nulab.example"
                                },
                                updated = "2009-03-03T16:57:47Z"
                            }
                        },
                        stars = new[]
                        {
                            new
                            {
                                id = 10,
                                //// comment = null,
                                url = "https://xx.backlog.jp/view/BLG-1",
                                title = "[BLG-1] first issue | Show issue - Backlog",
                                presenter = new
                                {
                                    id = 2,
                                    userId = "eguchi",
                                    name = "eguchi",
                                    roleType = 2,
                                    lang = "ja",
                                    mailAddress = "eguchi@nulab.example"
                                },
                                created = "2013-07-08T10:24:28Z"
                            }
                        }
                    },
                    //// baseCommit = null,
                    //// branchCommit = null,
                    //// closeAt = null,
                    //// mergeAt = null,
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2015-04-23T03:04:14Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2015-04-23T03:04:14Z"
                });
            };

            #endregion

            #region /projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments

            Get["/{projectKey}/git/repositories/{repoIdOrName}/pullRequests/{number}/comments"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 35,
                    content = "from api",
                    changeLog = new[]
                    {
                        new
                        {
                            field = "dependentIssue",
                            newValue = "GIT-3",
                            //// originalValue = null
                        }
                    },
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2015-05-14T01:53:38Z",
                    updated = "2015-05-14T01:53:38Z",
                    //// stars = new[] { new { } },
                    //// notifications = new[] { new { } }
                }
            });

            #endregion

            #region POST /projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments

            Post["/{projectKey}/git/repositories/{repoIdOrName}/pullRequests/{number}/comments"] = p =>
            {
                string content = Request.Form["content"];
                return Response.AsJson(new
                {
                    id = 35,
                    content = content,
                    changeLog = new[]
                    {
                        new
                        {
                            field = "dependentIssue",
                            newValue = "GIT-3",
                            //// originalValue = null
                        }
                    },
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2015-05-14T01:53:38Z",
                    updated = "2015-05-14T01:53:38Z",
                    //// stars = new[] { new { } },
                    //// notifications = new[] { new { } }
                });
            };

            #endregion
        }

        private Response GetFilesResponse(string path)
        {
            return Response.AsJson(new[]
            {
                new
                {
                    id = 825952, type = "file", dir = path.EndsWith("/") ? path : path + "/", name = "20091130.txt", size = 4836,
                    createdUser = new
                    {
                        id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example"
                    },
                    created = "2009-11-30T01:22:21Z",
                    //// "updatedUser": null,
                    updated = "2009-11-30T01:22:21Z"
                }
            });
        }
    }
}