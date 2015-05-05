// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogIssueMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace BL4N.Tests
{
    internal static class RequestUtils
    {
        public static IEnumerable<long> ToIds(string req)
        {
            return string.IsNullOrEmpty(req) ? new long[] { 0 } : req.Split(',').Select(long.Parse);
        }
    }

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
            Get[""] = p =>
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

            #region /api/v2/issues/:issueIdOrKey/comments

            Get["/{id}/comments"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 6586,
                    content = "test",
                    changeLog = new[]
                    {
                        new
                        {
                            field = "attachment",
                            newValue = "2013-07-20-kyotosuizokukan2.jpeg",
                            originalValue = string.Empty,
                            attachmentInfo = new { id = 437736, name = "2013-07-20-kyotosuizokukan2.jpeg" },
                            //// attributeInfo = null,
                            notificationInfo = new { type = "issue.create" }
                        }
                    },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-08-05T06:15:06Z",
                    updated = "2013-08-05T06:15:06Z",
                    //// stars= [],
                    //// notifications= []
                }
            });

            #endregion
        }
    }
}