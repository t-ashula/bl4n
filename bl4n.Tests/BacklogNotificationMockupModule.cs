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
                        priority = new{id = 3,name = "Normal"},
                        status = new{id = 1,name = "Open"},
                        assignee = new
                        {
                            id = 2,
                            name = "eguchi",
                            roleType = 2,
                            //// lang = null,
                            mailAddress = "eguchi@nulab.example"
                        },
                        //// category = [],
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
                    created = "2013-10-31T06:58:59Z"
                }
            });

            #endregion
        }
    }
}