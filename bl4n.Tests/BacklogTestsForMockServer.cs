// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForMockServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BL4N.Data;
using Nancy.Hosting.Self;
using Xunit;

namespace BL4N.Tests
{
    /// <summary> mock server を使ったテスト </summary>
    public class BacklogTestsForMockServer : BacklogTests, IClassFixture<BacklogMockServerFixture>
    {
        private readonly BacklogMockServerFixture _fixture;

        private NancyHost MockServer
        {
            get { return _fixture.MockServer; }
        }

        public BacklogTestsForMockServer(BacklogMockServerFixture fixture)
            : base(new BacklogConnectionSettings("mock", APIType.APIKey, "dummyapikey", "localhost", 34567, false))
        {
            _fixture = fixture;
        }

        private void SkipIfMockServerIsDown()
        {
            if (MockServer == null)
            {
                Assert.False(true, "mockup server is down");
            }
        }

        /// <inheritdoc/>
        [Fact]
        public override void BacklogConstructorTest()
        {
            SkipIfSettingIsBroken();

            var realClient = new Backlog(Settings);
            Assert.Equal(Settings.SpaceName, realClient.SpaceName);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var spaceInfo = backlog.GetSpace();
            Assert.Equal("nulab", spaceInfo.SpaceKey);
            Assert.Equal("Nubal Inc.", spaceInfo.Name);
            Assert.Equal(1, spaceInfo.OwnerId);
            Assert.Equal("ja", spaceInfo.Lang);
            Assert.Equal("Asia/Tokyo", spaceInfo.Timezone);
            Assert.Equal("08:00:00", spaceInfo.ReportSendTime);
            Assert.Equal("markdown", spaceInfo.TextFormattingRule);
            Assert.Equal(new DateTime(2008, 7, 6, 15, 0, 0), spaceInfo.Created);
            Assert.Equal(new DateTime(2013, 6, 18, 7, 55, 37), spaceInfo.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceActivitiesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var activities = backlog.GetSpaceActivities();
            Assert.Equal(3, activities.Count);

            // type 1,2,3,4
            IssueActivityTest(activities[0]);

            // type 14
            BulkUpdateActivityTest(activities[1]);

            // type 15, 16
            ProjectActivityTest(activities[2]);
        }

        private static void IssueActivityTest(IActivity activity)
        {
            // type = 1,2,3,4
            // Assert.InRange(1, 4, activity.Type);
            Assert.Equal(2, activity.Type);
            // project = new { id = 92, projectKey = "SUB", name = "サブタスク", chartEnabled = true },
            Assert.Equal(92, activity.Project.Id);
            Assert.Equal("SUB", activity.Project.ProjectKey);
            Assert.Equal("サブタスク", activity.Project.Name);
            Assert.True(activity.Project.ChartEnabled);
            Assert.True(activity.Project.SubtaskingEnabled);
            Assert.IsAssignableFrom<IActivityContent>(activity.Content);

            var content = activity.Content as IIssueActivityContent;
            Assert.NotNull(content);
            /*
             * ""content"": {
             *   ""id"": 4809,
             *   ""key_id"": 121,
             *   ""summary"": ""コメント"",
             *   ""description"": """",
             *   ""comment"": { ""id"": 7237,""content"": """" },
             *   ""changes"": [
             *     { ""field"": ""milestone"", ""new_value"": ""R2014-07-23"", ""old_value"": """", ""type"": ""standard"" },
             *     { ""field"": ""status"", ""new_value"": ""4"", ""old_value"": ""1"", ""type"": ""standard"" }
             *   ]
             * }
             */
            Assert.Equal(4809, content.Id);
            Assert.Equal(121, content.KeyId);
            Assert.Equal("コメント", content.Sumary);
            Assert.Equal(string.Empty, content.Description);
            Assert.Equal(7237, content.Comment.Id);
            Assert.Equal(string.Empty, content.Comment.Content);
            Assert.Equal(2, content.Changes.Count);
            Assert.Equal("milestone", content.Changes[0].Field);
            Assert.Equal("R2014-07-23", content.Changes[0].NewValue);
            Assert.Equal("", content.Changes[0].OldValue);
            Assert.Equal("standard", content.Changes[0].Type);
        }

        private static void BulkUpdateActivityTest(IActivity activity)
        {
            // type = 14
            Assert.Equal(14, activity.Type);

            Assert.IsAssignableFrom<IActivityContent>(activity.Content);
            var content = activity.Content as IBulkUpdateActivityContent;
            Assert.NotNull(content);
            /*
             * ""content"": {
             *   ""tx_id"": 230217,
             *   ""comment"": { ""content"": ""お世話になっております．"" },
             *   ""link"": [
             *       { ""id"": 2, ""key_id"": 10, ""title"": ""[質問] 可能でしょうか？"" },
             *       { ""id"": 3, ""key_id"": 20, ""title"": ""[質問] 色について"" },
             *       { ""id"": 4, ""key_id"": 30, ""title"": ""[質問] 質問"" }
             *   ],
             *   ""changes"": [
             *       { ""field"": ""status"",""new_value"": ""2"", ""type"": ""standard"" }
             *   ]
             * }
             */
            Assert.Equal(230217, content.TxId);
            Assert.Equal("お世話になっております．", content.Comment.Content);
            Assert.Equal(3, content.Link.Count);
            Assert.Equal(2, content.Link[0].Id);
            Assert.Equal(10, content.Link[0].KeyId);
            Assert.Equal("[質問] 可能でしょうか？", content.Link[0].Title);
            Assert.Equal(1, content.Changes.Count);
            Assert.Equal("status", content.Changes[0].Field);
            Assert.Equal("2", content.Changes[0].NewValue);
            Assert.Equal("standard", content.Changes[0].Type);
        }

        private static void ProjectActivityTest(IActivity activity)
        {
            // type = 15, 16
            // Assert.InRange(1, 4, activity.Type);
            Assert.Equal(15, activity.Type);

            var content = activity.Content as IProjectActivityContent;
            Assert.NotNull(content);
            /*
             * "content": {
             *   "users": [{
             *     "id": 60966,
             *     "userId": "t.ashula",
             *     "name": "t.ashula",
             *     "roleType": 2,
             *     "lang": null,
             *     "mailAddress": "t.ashula@gmail.com"
             *   }],
             *   "group_project_activities": [ { "id": 52355403, "type": 15 }, { "id": 52355404, "type": 15 } ],
             *   "comment": ""
             * },
             */
            Assert.Equal(1, content.Users.Count);
            Assert.Equal(60966, content.Users[0].Id);
            Assert.Equal("t.ashula", content.Users[0].UserId);
            Assert.Equal("t.ashula", content.Users[0].Name);
            Assert.Equal(2, content.Users[0].RoleType);
            Assert.Null(content.Users[0].Lang);
            Assert.Equal(2, content.GroupProjectActivites.Count);
            Assert.Equal(52355403, content.GroupProjectActivites[0].Id);
            Assert.Equal(15, content.GroupProjectActivites[0].Type);
            Assert.Equal(string.Empty, content.Comment);
        }
    }
}