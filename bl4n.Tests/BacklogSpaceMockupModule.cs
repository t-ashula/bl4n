// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogSpaceMockupModule.cs">
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
    /// /api/v2/issues routeing module
    /// </summary>
    public class BacklogIssueMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/issues routing
        /// </summary>
        public BacklogIssueMockupModule()
            : base("/api/v2/issues")
        {
        }
    }

    /// <summary>
    /// /api/v2/space routing module
    /// </summary>
    public class BacklogSpaceMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/space routing
        /// </summary>
        public BacklogSpaceMockupModule()
            : base("/api/v2/space")
        {
            #region /space

            Get[""] = _ => Response.AsJson(new
            {
                spaceKey = "nulab",
                name = "Nubal Inc.",
                ownerId = 1,
                lang = "ja",
                timezone = "Asia/Tokyo",
                reportSendTime = "08:00:00",
                textFormattingRule = "markdown",
                created = "2008-07-06T15:00:00Z",
                updated = "2013-06-18T07:55:37Z"
            });

            #endregion

            #region /space/activities

            // TODO: test data file
            Get["/activities"] = _ => Response.AsText(@"["
                                                      + /* 1, 2, 3, 4 */
                                                      @"{
                    ""id"": 3153,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 2,
                    ""content"": {
                        ""id"": 4809,
                        ""key_id"": 121,
                        ""summary"": ""コメント"",
                        ""description"": """",
                        ""comment"": { ""id"": 7237,""content"": """" },
                        ""changes"": [
                            { ""field"": ""milestone"", ""new_value"": ""R2014-07-23"", ""old_value"": """", ""type"": ""standard"" },
                            { ""field"": ""status"", ""new_value"": ""4"", ""old_value"": ""1"", ""type"": ""standard"" }
                        ]
                    },
                    ""notifications"": [{
                        ""id"": 25,
                        ""alreadyRead"": false,
                        ""reason"": 2,
                        ""user"": { ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"": 2, ""lang"": ""ja"", ""mailAddress"": ""takada@nulab.example"" },
                        ""resourceAlreadyRead"": false
                    }],
                    ""createdUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },
                    ""created"": ""2013-12-27T07:50:44Z""
                }"
                                                      + /* 5, 6, 7 */
                                                      @",{
                    ""id"": 3153,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 6,
                    ""content"": {
                        ""id"": 67261,
                        ""name"": ""Home"",
                        ""content"": ""1. a\n2. b\n3. c\n"",
                        ""diff"": ""1a1,3\n>1. a\n>2. b\n>3. c\n"",
                        ""version"": 1,
                        ""attachments"": [ ],
                        ""shared_files"": [ ]
                    },
                    ""notifications"": [{
                        ""id"": 25,
                        ""alreadyRead"": false,
                        ""reason"": 2,
                        ""user"": { ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"": 2, ""lang"": ""ja"", ""mailAddress"": ""takada@nulab.example"" },
                        ""resourceAlreadyRead"": false
                    }],
                    ""createdUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },
                    ""created"": ""2013-12-27T07:50:44Z""
                }"
                                                      + /* 8, 9, 10 */
                                                      @",{
                    ""id"": 3153,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 8,
                    ""content"": { ""id"": 1, ""dir"": ""/"", ""name"": ""tempfile.pdf"", ""size"": 12345 },
                    ""notifications"": [{
                        ""id"": 25,
                        ""alreadyRead"": false,
                        ""reason"": 2,
                        ""user"": { ""id"": 5686, ""userId"": ""takada"", ""name"": ""takada"", ""roleType"": 2, ""lang"": ""ja"", ""mailAddress"": ""takada@nulab.example"" },
                        ""resourceAlreadyRead"": false
                    }],
                    ""createdUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },
                    ""created"": ""2013-12-27T07:50:44Z""
                }"
                                                      + /* 11 */
                                                      @",{
                    ""id"": 57269265,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 11,
                    ""content"": { ""rev"": 2, ""comment"": ""add Readme"" },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },
                    ""created"": ""2015-03-26T00:15:35Z""
                }"
                                                      + /* 12, 13 */
                                                      @",{
                    ""id"": 57269265,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 12,
                    ""content"": {
                        ""repository"": { ""id"": 9251, ""name"": ""bl4n"", ""description"": null },
                        ""change_type"": ""create"",
                        ""revision_type"": ""commit"",
                        ""ref"": ""refs/heads/master"",
                        ""revision_count"": 1,
                        ""revisions"": [ { ""rev"": ""56de3ef67126295552f9bfeb957816f955e36393"", ""comment"": ""add README.md"" }]
                    },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 1, ""userId"": ""admin"", ""name"": ""admin"", ""roleType"": 1, ""lang"": ""ja"", ""mailAddress"": ""eguchi@nulab.example"" },
                    ""created"": ""2015-03-26T00:15:35Z""
                }"
                                                      + /* 14 */
                                                      @",{
                    ""id"": 3254,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 14,
                    ""content"": {
                        ""tx_id"": 1,
                        ""comment"": { ""content"": ""お世話になっております．"" },
                        ""link"": [
                            { ""id"": 2, ""key_id"": 10, ""title"": ""[質問] 可能でしょうか？"" },
                            { ""id"": 3, ""key_id"": 20, ""title"": ""[質問] 色について"" },
                            { ""id"": 4, ""key_id"": 30, ""title"": ""[質問] 質問"" }
                        ],
                        ""changes"": [
                            { ""field"": ""status"",""new_value"": ""2"", ""type"": ""standard"" }
                        ]
                    },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 1, ""userId"": null, ""name"": ""user2"", ""roleType"": 2, ""lang"": null, ""mailAddress"": null },
                    ""created"": ""2015-03-17T09:56:16Z""
                }"
                                                      + /* 15, 16 */
                                                      @",{
                    ""id"": 8159779,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 15,
                    ""content"": {
                        ""users"": [{
                            ""id"": 60966,
                            ""userId"": ""t.ashula"",
                            ""name"": ""t.ashula"",
                            ""roleType"": 2,
                            ""lang"": null,
                            ""mailAddress"": ""t.ashula@gmail.com""
                        }],
                        ""group_project_activities"": [ { ""id"": 52355403, ""type"": 15 }, { ""id"": 52355404, ""type"": 15 } ],
                        ""comment"": """"
                    },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 60965, ""userId"": ""bl4n.admin"", ""name"": ""bl4n.admin"", ""roleType"": 1, ""lang"": null, ""mailAddress"": ""t.ashula+private nulab@gmail.com"" },
                    ""created"": ""2015-03-26T09:28:33Z""
                }"
                                                      + /* 17 */
                                                      @",{
                    ""id"": 8159779,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 17,
                    ""content"": {
                        ""id"": 1,
                        ""key_id"": 2,
                        ""summary"": ""サマリー"",
                        ""description"": ""追記をお願いします．"",
                        ""comment"": {
                            ""id"": 1115392520,
                            ""content"": ""よろしくお願い致します．""
                        },
                        ""changes"": [ ],
                        ""attachments"": [ ],
                        ""shared_files"": [ ]
                    },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 60965, ""userId"": ""bl4n.admin"", ""name"": ""bl4n.admin"", ""roleType"": 1, ""lang"": null, ""mailAddress"": ""t.ashula+private nulab@gmail.com"" },
                    ""created"": ""2015-03-26T09:28:33Z""
                }"
                                                      +
                                                      @"]", "application/json;charset=utf-8");

            #endregion

            #region /space/image

            Get["/image"] = _ =>
            {
                var response = new Response
                {
                    ContentType = "image/png",
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

                response.Headers.Add("Content-Disposition", "attachment; filename*=UTF-8''logo_mark.png");
                return response;
            };

            #endregion

            #region /space/notification

            // {"content":"お知らせの追加\":-)\"","updated":"2015-03-26T06:37:37Z"}"
            Get["/notification"] = _ => Response.AsJson(new
            {
                content = "お知らせの追加\":-)\"",
                updated = new DateTime(2015, 3, 26, 6, 37, 37, DateTimeKind.Unspecified)
            });

            #endregion

            #region put /space/notification

            Put["/notification"] = p =>
            {
                // var req = this.Bind<NotificationContent>();
                var req = Request.Form;
                return Response.AsJson(new
                {
                    content = req.Content,
                    updated = new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Unspecified)
                });
            };

            #endregion

            #region /space/diskUsage

            Get["/diskUsage"] = _ => Response.AsJson(new
            {
                capacity = 1073741824,
                issue = 119511,
                wiki = 48575,
                file = 0,
                subversion = 0,
                git = 0,
                details = new[]
                {
                    new
                    {
                        projectId = 1,
                        issue = 11931,
                        wiki = 0,
                        file = 0,
                        subversion = 0,
                        git = 0
                    }
                }
            });

            #endregion

            #region post /space/attachment

            Post["/attachment"] = _ =>
            {
                var file = Request.Files.FirstOrDefault();
                // TODO: error response, Key should be "file"
                // var key = file == null ? "" : file.Key;

                var name = file == null ? "" : file.Name;
                var size = file == null ? 0 : file.Value.Length;
                return Response.AsJson(new
                {
                    id = 1,
                    name = name,
                    size = size
                });
            };

            #endregion
        }
    }
}