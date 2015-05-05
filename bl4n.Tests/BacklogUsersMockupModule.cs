// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogUsersMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BL4N.Data;
using BL4N.Tests.Properties;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/users routing module
    /// </summary>
    public class BacklogUsersMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/users routing
        /// </summary>
        public BacklogUsersMockupModule()
            : base("/api/v2/users")
        {
            #region /users

            Get[""] = _ => Response.AsJson(new[]
            {
                /*[ { "id": 1, "userId": "admin", "name": "admin", "roleType": 1, "lang": "ja", "mailAddress": "eguchi@nulab.example" } ] */
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

            #region /users/:userId

            Get["/{UserId}"] = p => Response.AsJson(new
            {
                id = (int)p.UserId,
                userId = "admin",
                name = "admin",
                roleType = 1,
                lang = "ja",
                mailAddress = "eguchi@nulab.example"
            });

            #endregion

            #region post /users/

            Post[""] = p =>
            {
                var req = Request.Form;
                var user = new User
                {
                    Id = new Random().Next(),
                    Lang = null,
                    UserId = req.userId,
                    MailAddress = req.mailAddress,
                    Name = req.name
                };
                int roleType;
                if (int.TryParse(req.roleType, out roleType))
                {
                    user.RoleType = roleType;
                }

                return Response.AsJson(user);
            };

            #endregion

            #region patch /users/:userId

            Patch["/{userId}"] = p =>
            {
                var req = Request.Form;
                var user = new User
                {
                    Id = p.userId,
                    Lang = null,
                    UserId = req.userId,
                    MailAddress = req.mailAddress,
                    Name = req.name
                };
                int roleType;
                if (int.TryParse(req.roleType, out roleType))
                {
                    user.RoleType = roleType;
                }

                return Response.AsJson(user);
            };

            #endregion

            #region delete /users/:userId

            Delete["/{userId}"] = p => Response.AsJson(new
            {
                id = p.userId,
                userId = "admin",
                name = "admin",
                roleType = 1,
                lang = "ja",
                mailAddress = "eguchi@nulab.example"
            });

            #endregion

            #region /users/myself

            Get["/myself"] = _ => Response.AsJson(new
            {
                id = 1,
                userId = "admin",
                name = "admin",
                roleType = 1,
                lang = "ja",
                mailAddress = "eguchi@nulab.example"
            });

            #endregion

            #region /users/:userId/icon

            Get["/{userId}/icon"] = p =>
            {
                //// Content-Type:application/octet-stream
                //// Content-Disposition:attachment;filename="person_168.gif"
                var response = new Response
                {
                    ContentType = "application/octet-stream",
                    Contents = stream =>
                    {
                        var logo = Resources.logo;
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

                response.Headers.Add("Content-Disposition", "attachment; filename*=UTF-8''person_168.gif");
                return response;
            };

            #endregion

            #region /users/:userId/activities

            Get["/{userId}/activities"] = p => Response.AsJson(new[]
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
                        //// textFormattingRule = null,
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
                        comment = new
                        {
                            id = 7237,
                            content = ""
                        },
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

            #endregion

            #region /users/:userId/stars

            Get["/{userId}/stars"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 75,
                    //// comment = null,
                    url = "https://xx.backlogtool.com/view/BLG-1",
                    title = "[BLG-1] first issue | Show issue - Backlog",
                    presenter = new
                    {
                        id = 1,
                        userId = "admin",
                        name = "admin",
                        roleType = 1,
                        lang = "ja",
                        mailAddress = "eguchi@nulab.example"
                    },
                    created = "2014-01-23T10:55:19Z"
                }
            });

            #endregion

            #region /users/:userId/start/count

            Get["/{userId}/stars/count"] = p => Response.AsJson(new { count = 42 });

            #endregion

            #region /users/myself/recentlyViewedIssues

            const string JsonStr = "["
                                   +
                                   @"{""issue"":{
      ""id"": 1, ""projectId"": 1, ""issueKey"": ""BLG-1"", ""keyId"": 1,
      ""issueType"": { ""id"": 2, ""projectId"" :1, ""name"": ""Task"", ""color"": ""#7ea800"", ""displayOrder"": 0 },
      ""summary"": ""first issue"",
      ""description"": """",
      ""resolutions"": { ""id"": 0, ""name"": ""対応済み"" },
      ""priority"": { ""id"": 3, ""name"": ""Normal"" },
      ""status"": { ""id"": 1, ""name"": ""Open"" },
      ""assignee"": {
        ""id"": 2, ""name"": ""eguchi"", ""roleType"" :2, ""lang"": null, ""mailAddress"": ""eguchi@nulab.example""
      },
      ""category"": [ { ""id"": 54712, ""name"": ""API"", ""displayOrder"": 2147483646 } ],
      ""versions"": [ {
        ""id"": 33856, ""projectId"": 26476, ""name"": ""1.0.0"", ""description"": ""initial release version"",
        ""startDate"": ""2015-04-01T00:00:00Z"", ""releaseDueDate"": ""2015-04-30T00:00:00Z"", ""archived"": false, ""displayOrder"": 2147483646 } ],
      ""milestone"": [ {
        ""id"": 33856, ""projectId"": 26476, ""name"": ""1.0.0"", ""description"": ""initial release version"",
        ""startDate"": ""2015-04-01T00:00:00Z"", ""releaseDueDate"": ""2015-04-30T00:00:00Z"", ""archived"": false, ""displayOrder"": 2147483646 } ],
      ""startDate"": null,
      ""dueDate"": ""2015-04-10T00:00:00Z"",
      ""estimatedHours"": 2.0,
      ""actualHours"": 3.0,
      ""parentIssueId"": null,
      ""createdUser"": {
        ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example""
      },
      ""created"": ""2012-07-23T06:10:15Z"",
      ""updatedUser"": {
        ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example""
      },
      ""updated"": ""2013-02-07T08:09:49Z"",
      ""customFields"": [],
      ""attachments"": [ { ""id"": 1, ""name"": ""IMGP0088.JPG"", ""size"": 85079 }],
      ""sharedFiles"": [ {
        ""id"": 454403, ""type"": ""file"", ""dir"": ""/usericon/"", ""name"": ""01.png"", ""size"": 2735,
        ""createdUser"": {
          ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"":2, ""lang"":""ja"", ""mailAddress"":""takada@nulab.example""
        },
        ""created"": ""2009-02-27T03:26:15Z"",
        ""updatedUser"": {
          ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"":2, ""lang"":""ja"", ""mailAddress"":""takada@nulab.example""
        },
        ""updated"":""2009-03-03T16:57:47Z""
      }],
      ""stars"": [ {
        ""id"": 10,
        ""comment"": null,
        ""url"": ""https://xx.backlogtool.com/view/BLG-1"",
        ""title"": ""[BLG-1] first issue | Show issue - Backlog"",
        ""presenter"": {
          ""id"": 2, ""userId"": ""eguchi"", ""name"": ""eguchi"", ""roleType"": 2, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example""
        },
        ""created"":""2013-07-08T10:24:28Z""
      }]
    },
    ""updated"": ""2014-07-11T02:00:00Z""
}"
                                   + "]";
            Get["/myself/recentlyViewedIssues"] = _ => Response.AsText(JsonStr);

            #endregion

            #region /users/myself/recentlyViewedProjects

            Get["/myself/recentlyViewedProjects"] = _ => Response.AsJson(new[]
            {
                new
                {
                    project = new
                    {
                        id = 1,
                        projectKey = "TEST",
                        name = "test",
                        chartEnabled = false,
                        subtaskingEnabled = false,
                        textFormattingRule = "markdown",
                        archived = false,
                        displayOrder = 0
                    },
                    updated = "2014-07-11T01:59:07Z"
                }
            });

            #endregion

            #region /users/myself/recentlyViewedWikis

            Get["/myself/recentlyViewedWikis"] = _ => Response.AsJson(new[]
            {
                new
                {
                    page = new
                    {
                        id = 112,
                        projectId = 103,
                        name = "Home",
                        tags = new[] { new { id = 12, name = "proceedings" } },
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2013-05-30T09:11:36Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-05-30T09:11:36Z"
                    },
                    updated = "2014-07-16T07:18:16Z"
                }
            });

            #endregion
        }
    }
}