// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogMockupModule.cs">
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
    /// Nancy の http hander module
    /// </summary>
    public class BacklogMockupModule : NancyModule
    {
        /// <summary> <see cref="BacklogMockupModule"/> を初期化します </summary>
        public BacklogMockupModule()
            : base("/api/v2")
        {
            Get["/space"] = _ => Response.AsJson(new
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

            // TODO: test data file
            /*        */
            Get["/space/activities"] = _ => Response.AsText(@"[
                {
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
                },{
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
                },{
                    ""id"": 3254,
                    ""project"": {
                        ""id"": 92, ""projectKey"": ""SUB"", ""name"": ""サブタスク"",
                        ""chartEnabled"": true, ""subtaskingEnabled"": true, ""textFormattingRule"": null, ""archived"": false, ""displayOrder"": 0
                    },
                    ""type"": 14,
                    ""content"": {
                        ""tx_id"": 230217,
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
                },{
                    ""id"": 8159779,
                    ""project"": {
                        ""id"": 26476,
                        ""projectKey"": ""BL4N"",
                        ""name"": ""bl4n"",
                        ""chartEnabled"": false,
                        ""subtaskingEnabled"": false,
                        ""textFormattingRule"": ""markdown"",
                        ""archived"": false,
                        ""displayOrder"": 2147483646
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
                        ""group_project_activities"": [
                            { ""id"": 52355403, ""type"": 15 },
                            { ""id"": 52355404, ""type"": 15 }
                        ],
                        ""comment"": """"
                    },
                    ""notifications"": [ ],
                    ""createdUser"": { ""id"": 60965, ""userId"": ""bl4n.admin"", ""name"": ""bl4n.admin"", ""roleType"": 1, ""lang"": null, ""mailAddress"": ""t.ashula+private nulab@gmail.com"" },
                    ""created"": ""2015-03-26T09:28:33Z""
                }
            ]", "application/json;charset=utf-8");
        }
    }
}