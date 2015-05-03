// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogIssueMockupModule.cs" company="">
//   bl4n - Backlog.jp API Client library
//   //   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/issues routeing module
    /// </summary>
    public class BacklogIssueMockupModule : NancyModule
    {
        private List<long> ToIds(string key)
        {
            var val = Request.Query[key];
            return val.HasValue ? RequestUtils.ToIds((string)val).ToList() : new List<long>();
        }

        /// <summary>
        /// /api/v2/issues routing
        /// </summary>
        public BacklogIssueMockupModule()
            : base("/api/v2/issues")
        {
            #region /api/v2/issues

            Get[string.Empty] = p =>
            {
                var pids = ToIds("projectId[]");

                long projectId;
                if (pids.Count == 0)
                {
                    // TODO: error response
                    projectId = -1;
                }
                else
                {
                    projectId = pids[0];
                }

                var issueTypes = ToIds("issueTypeId[]");
                var issueTypeId = issueTypes.Count == 0 ? 2 : issueTypes[0];
                var statusIds = ToIds("statusId[]");
                var statusId = statusIds.Count == 0 ? 1 : statusIds[0];
                var assignerIds = ToIds("assignerId[]");
                var assignerId = assignerIds.Count == 0 ? 2 : assignerIds[0];
                var createdUsers = ToIds("createdUserId[]");
                var createdUser = createdUsers.Count == 0 ? 1 : createdUsers[0];
                return Response.AsText("["
                                       + "{"
                                       + @"""id"": 1,"
                                       + @"""projectId"": " + projectId + @","
                                       + @"""issueKey"": ""BLG-1"","
                                       + @"""keyId"": 1,"
                                       + @"""issueType"": { ""id"": " + issueTypeId + @", ""projectId"" :1,""name"": ""Task"",""color"": ""#7ea800"",""displayOrder"": 0 },"
                                       + @"""summary"": ""first issue"","
                                       + @"""description"":"""","
                                       + @"""resolutions"": null,"
                                       + @"""priority"": { ""id"": 3, ""name"": ""Normal"" },"
                                       + @"""status"": { ""id"": " + statusId + @", ""name"": ""Open"" },"
                                       + @"""assignee"": { ""id"": " + assignerId + @", ""name"": ""eguchi"", ""roleType"" :2, ""lang"": null, ""mailAddress"": ""eguchi@nulab.example"" },"
                                       + @"""category"": [],"
                                       + @"""versions"": [],"
                                       + @"""milestone"": [ { ""id"": 30, ""projectId"": " + projectId + @", ""name"": ""wait for release"", ""description"": """", ""startDate"": null, ""releaseDueDate"": null, ""archived"": false, ""displayOrder"": 0 } ],"
                                       + @"""startDate"": null, ""dueDate"": null, ""estimatedHours"": null, ""actualHours"": null, ""parentIssueId"": null,"
                                       + @"""createdUser"": { ""id"": " + createdUser + @", ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },"
                                       + @"""created"": ""2012-07-23T06:10:15Z"","
                                       + @"""updatedUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },"
                                       + @"""updated"": ""2013-02-07T08:09:49Z"","
                                       + @"""customFields"": [],"
                                       + @"""attachments"": [ { ""id"": 1, ""name"": ""IMGP0088.JPG"", ""size"": 85079 } ],"
                                       + @"""sharedFiles"": [ { ""id"": 454403, ""type"": ""file"", ""dir"": ""/userIcon/"", ""name"": ""01_male clerk.png"", ""size"": 2735,"
                                       + @"  ""createdUser"": { ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"":2, ""lang"":""ja"", ""mailAddress"":""takada@nulab.example"" },"
                                       + @"  ""created"": ""2009-02-27T03:26:15Z"","
                                       + @"  ""updatedUser"": { ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"":2, ""lang"":""ja"", ""mailAddress"":""takada@nulab.example"" },"
                                       + @"  ""updated"":""2009-03-03T16:57:47Z"" } ],"
                                       + @"""stars"": [ {"
                                       + @"  ""id"": 10,"
                                       + @"  ""comment"": null,"
                                       + @"  ""url"": ""https://xx.backlogtool.com/view/BLG-1"","
                                       + @"  ""title"": ""[BLG-1] first issue | Show issue - Backlog"","
                                       + @"  ""presenter"": { ""id"": 2, ""userId"": ""eguchi"", ""name"": ""eguchi"", ""roleType"": 2, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },"
                                       + @"  ""created"":""2013-07-08T10:24:28Z"" } ] "
                                       + "}" + "]");
            };

            #endregion

            #region /api/v2/issues/count

            Get["/count"] = p =>
            {
                var pids = ToIds("projectId[]");
                if (pids.Count == 0)
                {
                    // TODO: error handling
                }

                return Response.AsJson(new { count = 42 });
            };

            #endregion

            #region POST /api/v2/issues

            Post[string.Empty] = p =>
            {
                dynamic req = Request.Form;

                string summary = req.Summary;
                long pryid = req.PriorityId;
                long pid = req.ProjectId;
                long itid = req.IssueTypeId;
                return Response.AsJson(new
                {
                    id = 1,
                    projectId = pid,
                    issueKey = "BLG-1",
                    keyId = 1,
                    issueType = new
                    {
                        id = itid,
                        projectId = pid,
                        name = itid == 0 ? "Task" : "Bug",
                        color = "#7ea800",
                        displayOrder = 0
                    },
                    summary = summary,
                    description = req["description"],

                    //// resolutions= null,
                    priority = new { id = pryid, name = "Normal" },
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = req["assigneeId"],
                        userId = "eguchi",
                        name = "eguchi",
                        roleType = 2,

                        //// lang= null,
                        mailAddress = "eguchi@nulab.example"
                    },
                    category = new[]
                    {
                        new
                        {
                            id = RequestUtils.ToIds((string)req["categoryId[]"]).FirstOrDefault(),
                            name = "API",
                            displayOrder = 2147483646
                        }
                    },

                    //// versions = new[],
                    milestone = new[]
                    {
                        new
                        {
                            id = RequestUtils.ToIds((string)req["milestoneId[]"]).FirstOrDefault(),
                            projectId = pid,
                            name = "wait for release",
                            description = string.Empty,

                            //// startDate= null,
                            //// releaseDueDate= null,
                            archived = false,
                            displayOrder = 0
                        }
                    },
                    startDate = req["startDate"],
                    dueDate = req["dueDate"],
                    estimatedHours = req["estimatedHours"],
                    actualHours = req["actualHours"],
                    parentIssueId = req["parentIssueId"],
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2012-07-23T06:10:15Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2012-07-23T06:10:15Z",

                    //// customFields = new[],
                    attachments = new[]
                    {
                        new
                        {
                            id = RequestUtils.ToIds((string)req["attachmentId[]"]).FirstOrDefault(),
                            name = "IMGP0088.JPG",
                            size = 85079
                        }
                    }

                    //// sharedFiles= new [],
                    //// stars= []
                });
            };

            #endregion

            #region /api/v2/issues/:idOrKey

            Get["/{id}"] = p =>
            {
                long iid = p.id;
                return Response.AsJson(new
                {
                    id = iid,
                    projectId = 1,
                    issueKey = "BLG-1",
                    keyId = 1,
                    issueType = new { id = 2, projectId = 1, name = "Task", color = "#7ea800", displayOrder = 0 },
                    summary = "first issue",
                    description = "",
                    //// resolutions= null,
                    priority = new { id = 3, name = "Normal" },
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = 2,
                        name = "eguchi",
                        roleType = 2,
                        //// lang= null,
                        mailAddress = "eguchi@nulab.example"
                    },
                    //// category= [],
                    //// versions= [],
                    milestone = new[]
                    {
                        new
                        {
                            id = 30,
                            projectId = 1,
                            name = "wait for release",
                            description = "",
                            //// startDate= null,
                            //// releaseDueDate= null,
                            archived = false,
                            displayOrder = 0
                        }
                    },
                    //// startDate= null,
                    //// dueDate= null,
                    //// estimatedHours= null,
                    //// actualHours= null,
                    //// parentIssueId= null,
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2012-07-23T06:10:15Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2013-02-07T08:09:49Z",
                    //// customFields= [],
                    attachments = new[] { new { id = 1, name = "IMGP0088.JPG", size = 85079 } },
                    //// sharedFiles= [],
                    stars = new[]
                    {
                        new
                        {
                            id = 10,
                            //// comment= null,
                            url = "https://xx.backlogtool.com/view/BLG-1",
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
                });
            };

            #endregion

            #region PATCH /api/v2/issues/:idOrKey

            Patch["/{id}"] = p =>
            {
                dynamic form = Request.Form;

                string issueType = form["issueTypeId"].HasValue ? form["issueTypeId"] : "2";

                long rid = form["resolutionId"] ?? 1;
                var resolution = new { Id = rid, Name = string.Format("{0}", rid) };
                var requestCategory = RequestUtils.ToIds((string)form["categoryId[]"]).ToList();
                var categories = requestCategory.Any() ? requestCategory.Select(c => new { id = c, name = string.Format("{0}", c), displayOrder = 0 }) : null;
                var requestVersions = RequestUtils.ToIds((string)form["versionId[]"]).ToList();
                var versions = requestVersions.Any() ? requestVersions.Select(i => new { Id = i, Name = string.Format("{0}", i) }) : null;
                var requestMilestones = RequestUtils.ToIds((string)form["milestoneId[]"]).ToList();
                var milestones = requestMilestones.Any()
                    ? requestMilestones.Select(i => new { id = i, projectId = 1, name = string.Format("{0}", i), description = string.Empty, archived = false, displayOrder = 0 }).ToArray()
                    : new[] { new { id = (long)30, projectId = 1, name = "wait for release", description = string.Empty, archived = false, displayOrder = 0 } };
                var requestAttachment = RequestUtils.ToIds((string)form["attachmentId[]"]).FirstOrDefault();
                var attachmentId = requestAttachment == 0 ? 1 : requestAttachment;
                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = 1,
                    issueKey = "BLG-1",
                    keyId = 1,
                    issueType = new
                    {
                        id = issueType,
                        projectId = 1,
                        name = "Task",
                        color = "#7ea800",
                        displayOrder = 0
                    },
                    summary = form["summary"] ?? "first issue",
                    description = form["description"] ?? string.Empty,
                    resolutions = resolution,
                    priority = new { id = form["priorityId"], name = "Normal" },
                    status = new { id = form["statusId"] ?? "1", name = "Open" },
                    assignee = new { id = form["assigneeId"] ?? "2", name = "eguchi", roleType = 2, mailAddress = "private eguchi@nulab.example" },
                    category = categories,
                    versions = versions,
                    milestone = milestones,
                    startDate = form["startDate"],
                    dueDate = form["dueDate"],
                    estimatedHours = form["estimatedHours"],
                    actualHours = form["actualHours"],
                    parentIssueId = form["parentIssueId"],
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2012-07-23T06:10:15Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-02-07T08:09:49Z",
                    //// customFields= [],
                    attachments = new[] { new { id = attachmentId, name = "IMGP0088.JPG", size = 85079 } },
                    //// sharedFiles= [],
                    stars = new[]
                    {
                        new
                        {
                            id = 10,
                            //// comment= null,
                            url = "https://xx.backlogtool.com/view/BLG-1",
                            title = "[BLG-1] first issue | Show issue - Backlog",
                            presenter = new { id = 2, userId = "eguchi", name = "eguchi", roleType = 2, lang = "ja", mailAddress = "eguchi@nulab.example" },
                            created = "2013-07-08T10:24:28Z"
                        }
                    }
                });
            };

            #endregion

            #region DELETE /api/v2/issues/:idOrKey

            Delete["/{id}"] = p =>
            {
                long iid = p.id;
                return Response.AsJson(new
                {
                    id = iid,
                    projectId = 1,
                    issueKey = "BLG-1",
                    keyId = 1,
                    issueType = new { id = 2, projectId = 1, name = "Task", color = "#7ea800", displayOrder = 0 },
                    summary = "first issue",
                    description = "",
                    //// resolutions= null,
                    priority = new { id = 3, name = "Normal" },
                    status = new { id = 1, name = "Open" },
                    assignee = new
                    {
                        id = 2,
                        name = "eguchi",
                        roleType = 2,
                        //// lang= null,
                        mailAddress = "eguchi@nulab.example"
                    },
                    //// category= [],
                    //// versions= [],
                    milestone = new[]
                    {
                        new
                        {
                            id = 30,
                            projectId = 1,
                            name = "wait for release",
                            description = "",
                            //// startDate= null,
                            //// releaseDueDate= null,
                            archived = false,
                            displayOrder = 0
                        }
                    },
                    //// startDate= null,
                    //// dueDate= null,
                    //// estimatedHours= null,
                    //// actualHours= null,
                    //// parentIssueId= null,
                    createdUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2012-07-23T06:10:15Z",
                    updatedUser = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    updated = "2013-02-07T08:09:49Z",
                    //// customFields= [],
                    attachments = new[] { new { id = 1, name = "IMGP0088.JPG", size = 85079 } },
                    //// sharedFiles= [],
                    stars = new[]
                    {
                        new
                        {
                            id = 10,
                            //// comment= null,
                            url = "https://xx.backlogtool.com/view/BLG-1",
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
                });
            };

            #endregion
        }
    }
}