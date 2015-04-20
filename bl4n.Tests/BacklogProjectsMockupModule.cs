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
                // Content-Type:application/octet-stream
                // Content-Disposition:attachment;filename="logo_mark.png"

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
                            id = 92,
                            projectKey = "SUB",
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
                projectKey = 1,
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

            Get["/{projectKey}/versions"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 3,
                    projectId = 1,
                    name = "wait for release",
                    description = "",
                    // startDate = null,
                    // releaseDueDate = null,
                    archived = false,
                    displayOrder = 0
                }
            });

            #endregion

            #region POST /projects/:projectKey/versions

            Post["/{projectKey}/versions"] = p => Response.AsJson(new
            {
                id = 3,
                projectId = 1,
                name = Request.Form.name,
                description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                startDate = string.IsNullOrEmpty(Request.Form.startDate) ? null : Request.Form.startDate,
                releaseDueDate = string.IsNullOrEmpty(Request.Form.releaseDueDate) ? null : Request.Form.releaseDueDate,
                archived = false,
                displayOrder = 0
            });

            #endregion

            #region PATCH /projects/:projectKey/versions/:id

            Patch["/{projectKey}/versions/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = Request.Form.name,
                description = string.IsNullOrEmpty(Request.Form.description) ? string.Empty : Request.Form.description,
                startDate = string.IsNullOrEmpty(Request.Form.startDate) ? null : DateTime.Parse(Request.Form.startDate),
                releaseDueDate = string.IsNullOrEmpty(Request.Form.releaseDueDate) ? null : DateTime.Parse(Request.Form.releaseDueDate),
                archived = string.IsNullOrEmpty(Request.Form.archived) ? "false" : Request.Form.archived,
                displayOrder = 0
            });

            #endregion

            #region DELETE /projects/:projectKey/versions/:id

            Delete["/{projectKey}/versions/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = "wait for release",
                description = "",
                // startDate = null,
                // releaseDueDate = null,
                archived = false,
                displayOrder = 0
            });

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
        }
    }
}