// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogNotificationMockupModule.cs">
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
    /// /api/v2/notifications roting module
    /// </summary>
    public class BacklogNotificationMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/notification routing
        /// </summary>
        public BacklogNotificationMockupModule() :
            base("/api/v2/notifications")
        {
            #region /apiv/v2/notifications

            Get[string.Empty] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 299,
                    alreadyRead = true,
                    reason = 2,
                    resourceAlreadyRead = true,
                    project = new
                    {
                        id = 2,
                        projectKey = "TEST2",
                        name = "test2",
                        chartEnabled = true,
                        subtaskingEnabled = true,
                        textFormattingRule = "backlog",
                        archived = false,
                        displayOrder = 0
                    },
                    issue = new
                    {
                        id = 4531,
                        projectId = 2,
                        issueKey = "TEST2-17",
                        keyId = 17,
                        issueType = new
                        {
                            id = 7,
                            projectId = 2,
                            name = "Bug",
                            color = "#990000",
                            displayOrder = 0
                        },
                        summary = "aaa",
                        description = "",
                        //// resolutions = null,
                        priority = new {id = 3, name = "Normal"},
                        status = new {id = 1, name = "Open"},
                        assignee = new
                        {
                            id = 2,
                            name = "eguchi",
                            roleType = 2,
                            //// lang = null,
                            mailAddress = "eguchi@nulab.example"
                        },
                        //// category = new[]{new{}},
                        //// versions = [],
                        //// milestone = [],
                        startDate = "2013-08-29T15:00:00Z",
                        dueDate = "2013-09-03T15:00:00Z",
                        //// estimatedHours = null,
                        //// actualHours = null,
                        //// parentIssueId = null,
                        createdUser = new
                        {
                            id = 1,
                            userId = "admin",
                            name = "admin",
                            roleType = 1,
                            lang = "ja",
                            mailAddress = "eguchi@nulab.example"
                        },
                        created = "2013-04-23T07:38:59Z",
                        updatedUser = new
                        {
                            id = 1,
                            userId = "admin",
                            name = "admin",
                            roleType = 1,
                            lang = "ja",
                            mailAddress = "eguchi@nulab.example"
                        },
                        updated = "2013-09-06T09:25:41Z",
                        //// customFields = [],
                        //// attachments = [],
                        //// sharedFiles = [],
                        //// stars = []
                    },
                    comment = new
                    {
                        id = 7007,
                        content = "hoge",
                        //// changeLog = null,
                        createdUser = new
                        {
                            id = 2,
                            userId = "eguchi",
                            name = "eguchi",
                            roleType = 2,
                            //// lang = null,
                            mailAddress = "eguchi@nulab.example"
                        },
                        created = "2013-10-31T06:58:58Z",
                        updated = "2013-10-31T06:58:58Z",
                        //// stars = [],
                        //// notifications = []
                    },
                    sender = new
                    {
                        id = 2,
                        userId = "eguchi",
                        name = "eguchi",
                        roleType = 2,
                        //// lang = null,
                        mailAddress = "eguchi@nulab.example"
                    },
                    pullRequest = new
                    {
                        stars = new[]
                        {
                            new
                            {
                                created = "2015-11-08T04:04:47Z",
                                presenter = new
                                {
                                    mailAddress = "t.ashula@gmail.com",
                                    //// lang": null,
                                    roleType = 2,
                                    name = "t.ashula",
                                    userId = "t.ashula",
                                    id = 60966
                                },
                                title = "[BL4N/bl4n#1] develop",
                                url = "https://bl4n.backlog.jp/git/BL4N/bl4n/pullRequests/1",
                                //// comment": null,
                                id = 226364
                            }
                        },
                        attachments = new[] {new {}},
                        updated = "2015-11-08T14:37:13Z",
                        updatedUser = new
                        {
                            mailAddress = "t.ashula@gmail.com",
                            //// lang": null,
                            roleType = 2,
                            name = "t.ashula",
                            userId = "t.ashula",
                            id = 60966
                        },
                        created = "2015-11-08T03:43:43Z",
                        createdUser = new
                        {
                            mailAddress = "t.ashula@gmail.com",
                            //// lang = null,
                            roleType = 2,
                            name = "t.ashula",
                            userId = "t.ashula",
                            id = 60966
                        },
                        //// "mergeAt= null,
                        //// "closeAt= null,
                        //// "branchCommit= null,
                        //// "baseCommit= null,
                        issue = new
                        {
                            stars = new[] {new {}},
                            sharedFiles = new[] {new {}},
                            attachments = new[] {new {}},
                            customFields = new[] {new {}},
                            updated = "2015-11-08T06:15:17Z",
                            updatedUser = new
                            {
                                mailAddress = "t.ashula+nulab@gmail.com",
                                //// lang = null,
                                roleType = 1,
                                name = "bl4n.admin",
                                userId = "bl4n.admin",
                                id = 60965
                            },
                            created = "2015-11-08T03:43:15Z",
                            createdUser = new
                            {
                                mailAddress = "t.ashula@gmail.com",
                                //// lang = null,
                                roleType = 2,
                                name = "t.ashula",
                                userId = "t.ashula",
                                id = 60966
                            },
                            //// parentIssueId = null,
                            //// actualHours = null,
                            //// estimatedHours = null,
                            //// dueDate = null,
                            //// startDate = null,
                            milestone = new[] {new {}},
                            versions = new[] {new {}},
                            category = new[] {new {displayOrder = 2147483646, name = "API", id = 61309}},
                            //// assignee= null,
                            status = new {name = "\u672a\u5bfe\u5fdc", id = 1},
                            priority = new {name = "\u4e2d", id = 3},
                            //// resolution = null,
                            description = "update readme.md",
                            summary = "update readme",
                            issueType = new
                            {
                                displayOrder = 1,
                                color = "#990000",
                                name = "\u30d0\u30b0",
                                projectId = 26476,
                                id = 116112
                            },
                            keyId = 113,
                            issueKey = "BL4N-113",
                            projectId = 26476,
                            id = 2329470
                        },
                        assignee = new
                        {
                            mailAddress = "t.ashula@gmail.com",
                            //// lang = null,
                            roleType = 2,
                            name = "t.ashula",
                            userId = "t.ashula",
                            id = 60966
                        },
                        status = new
                        {
                            name = "Open",
                            id = 1
                        },
                        branch = "develop",
                        @base = "master",
                        description = "update readme",
                        summary = "develop",
                        number = 1,
                        epositoryId = 9251,
                        projectId = 26476,
                        id = 2553
                    },
                    created = "2013-10-31T06:58:59Z"
                }
            });

            #endregion

            #region /api/v2/notifications/count

            Get["/count"] = p => Response.AsJson(new { count = 138 });

            #endregion

            #region POST /api/v2/notifications/markAsRead

            Post["/markAsRead"] = p => Response.AsJson(new { count = 42 });

            #endregion

            #region POST /api/v2/notifications/:id/markAsRead

            Post["/{id}/markAsRead"] = p =>
            {
                return HttpStatusCode.NoContent;
            };

            #endregion
        }
    }
}