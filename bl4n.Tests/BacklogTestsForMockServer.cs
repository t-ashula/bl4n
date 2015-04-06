// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForMockServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using BL4N.Data;
using BL4N.Tests.Properties;
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
            Assert.Equal(8, activities.Count);

            // type 1, 2, 3, 4
            IssueActivityTest(activities[0]);

            // type 5, 6, 7
            WikiActivityTest(activities[1]);

            // type 8, 9, 10
            FileActivityTest(activities[2]);

            // type 11
            SVNRepositoryActivityTest(activities[3]);

            // type 12, 13
            GitRepositoryActivityTest(activities[4]);

            // type 14
            BulkUpdateActivityTest(activities[5]);

            // type 15, 16
            ProjectActivityTest(activities[6]);

            // type 17
            NotifyActivityTest(activities[7]);
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

        private static void WikiActivityTest(IActivity activity)
        {
            // type = 5, 6, 7
            // Assert.InRange(activity.Type, 5, 7);
            Assert.Equal(6, activity.Type);

            var content = activity.Content as IWikiActivityContent;
            Assert.NotNull(content);
            /*
             * ""content"": {
             *   ""id"": 67261,
             *   ""name"": ""Home"",
             *   ""content"": ""1. a\n2. b\n3. c\n"",
             *   ""diff"": ""1a1,3\n>1. a\n>2. b\n>3. c\n"",
             *   ""version"": 1,
             *   ""attachments"": [ ],
             *   ""shared_files"": [ ]
             * },
             */
            Assert.Equal(67261, content.Id);
            Assert.Equal("Home", content.Name);
            Assert.Equal("1. a\n2. b\n3. c\n", content.Content);
            Assert.Equal("1a1,3\n>1. a\n>2. b\n>3. c\n", content.Diff);
            Assert.Equal(1, content.Version);
            Assert.Equal(0, content.Attachments.Count); // TODO: more
            Assert.Equal(0, content.SharedFiles.Count); // TODO: more
        }

        private static void FileActivityTest(IActivity activity)
        {
            // type = 8, 9, 10
            // Assert.InRange(activity.Type, 8, 10);
            Assert.Equal(8, activity.Type);

            var content = activity.Content as IFileActivityContent;
            Assert.NotNull(content);

            // ""content"": { ""id"": 1, ""dir"": ""/"", ""name"": ""tempfile.pdf"", ""size"": 12345 },
            Assert.Equal(1, content.Id);
            Assert.Equal("/", content.Dir);
            Assert.Equal("tempfile.pdf", content.Name);
            Assert.Equal(12345, content.Size);
        }

        private static void SVNRepositoryActivityTest(IActivity activity)
        {
            // type 11
            Assert.Equal(11, activity.Type);

            var content = activity.Content as ISVNRepositoryActivityContent;
            Assert.NotNull(content);

            // ""content"": { ""rev"":"" 2, ""comment"": ""add Readme"" },
            Assert.Equal(2, content.Rev);
            Assert.Equal("add Readme", content.Comment);
        }

        private static void GitRepositoryActivityTest(IActivity activity)
        {
            // type 12, 13
            Assert.Equal(12, activity.Type);

            var content = activity.Content as IGitRepositoryActivityContent;
            Assert.NotNull(content);

            // ""content"": {
            //  ""repository"": { ""id"": 9251, ""name"": ""bl4n"", ""description"": null },
            //    ""change_type"": ""create"",
            //    ""revision_type"": ""commit"",
            //    ""ref"": ""refs/heads/master"",
            //    ""revision_count"": 1,
            //    ""revisions"": [ { ""rev"": ""56de3ef67126295552f9bfeb957816f955e36393"", ""comment"": ""add README.md"" }]
            // }
            Assert.Equal(9251, content.Repository.Id);
            Assert.Equal("bl4n", content.Repository.Name);
            Assert.Equal(null, content.Repository.Description);
            Assert.Equal("create", content.ChangeType);
            Assert.Equal("commit", content.RevisionType);
            Assert.Equal("refs/heads/master", content.Ref);
            Assert.Equal(1, content.RevisionCount);
            Assert.Equal(1, content.Revisions.Count);
            Assert.Equal("56de3ef67126295552f9bfeb957816f955e36393", content.Revisions[0].Rev);
            Assert.Equal("add README.md", content.Revisions[0].Comment);
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
             *   ""tx_id"": 1,
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
            Assert.Equal(1, content.TxId);
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
            // Assert.InRange(15, 16, activity.Type);
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

        private static void NotifyActivityTest(IActivity activity)
        {
            // type = 17
            // Assert.InRange(17, 17, activity.Type);
            Assert.Equal(17, activity.Type);

            var content = activity.Content as INotifyActivityContent;
            Assert.NotNull(content);
            /*
             * ""content"": {
             *   ""id"": 1,
             *   ""key_id"": 2,
             *   ""summary"": ""サマリー"",
             *   ""description"": ""追記をお願いします．"",
             *   ""comment"": {
             *     ""id"": 1115392520,
             *     ""content"": ""よろしくお願い致します．""
             *   },
             *   ""changes"": [ ],
             *   ""attachments"": [ ],
             *   ""shared_files"": [ ]
             * }
             */
            Assert.Equal(1, content.Id);
            Assert.Equal(2, content.KeyId);
            Assert.Equal("サマリー", content.Summary);
            Assert.Equal("追記をお願いします．", content.Description);
            Assert.Equal(1115392520, content.Comment.Id);
            Assert.Equal("よろしくお願い致します．", content.Comment.Content);
            Assert.Equal(0, content.Changes.Count);
            Assert.Equal(0, content.Attachments.Count);
            Assert.Equal(0, content.SharedFiles.Count);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceLogoTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpaceImage();
            Assert.Equal("logo_mark.png", actual.FileName);
            var logo = Resources.logo;
            Assert.Equal(logo.Size.Width, actual.Content.Size.Width);
            Assert.Equal(logo.Size.Height, actual.Content.Size.Height);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceNotificationTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpaceNotifiacation();
            // {"content":"お知らせの追加\":-)\"","updated":"2015-03-26T06:37:37Z"}
            Assert.Equal("お知らせの追加\":-)\"", actual.Content);
            Assert.Equal(new DateTime(2015, 3, 26, 6, 37, 37, DateTimeKind.Utc), actual.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateSpaceNotificationTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var content = string.Format("{0:F} に更新しました．", DateTime.Now);
            var actual = backlog.UpdateSpaceNotification(content);
            Assert.Equal(content, actual.Content);
            Assert.Equal(new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc), actual.Updated); // mockserver always returns 2015-04-01T00:00:00Z
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceDiskUsageTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();
            var backlog = new Backlog(Settings);
            IDiskUsage actual = backlog.GetSpaceDiskUsage();
            /*
             * "capacity": 1073741824,
             * "issue": 119511,
             * "wiki": 48575,
             * "file": 0,
             * "subversion": 0,
             * "git": 0,
             * "details":[ { "projectId": 1,"issue": 11931, "wiki": 0, "file": 0, "subversion": 0, "git": 0 }]
             */
            Assert.Equal(1073741824, actual.Capacity);
            Assert.Equal(119511, actual.Issue);
            Assert.Equal(48575, actual.Wiki);
            Assert.Equal(0, actual.File);
            Assert.Equal(0, actual.Subversion);
            Assert.Equal(0, actual.Git);
            Assert.Equal(1, actual.Details.Count);
            Assert.Equal(1, actual.Details[0].ProjectId);
            Assert.Equal(11931, actual.Details[0].Issue);
            Assert.Equal(0, actual.Details[0].Wiki);
            Assert.Equal(0, actual.Details[0].File);
            Assert.Equal(0, actual.Details[0].Subversion);
            Assert.Equal(0, actual.Details[0].Git);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddAttachmentTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            using (var ms = new MemoryStream())
            {
                var bmp = Resources.logo;
                bmp.Save(ms, bmp.RawFormat);
                var size = ms.Length;
                ms.Position = 0;
                var actual = backlog.AddAttachment("logo.png", ms);
                Assert.Equal(1, actual.Id);
                Assert.Equal("logo.png", actual.Name);
                Assert.Equal(size, actual.Size);
            }
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUsersTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUsers();
            Assert.Equal(1, actual.Count);
            /*[ { "id": 1, "userId": "admin", "name": "admin", "roleType": 1, "lang": "ja", "mailAddress": "eguchi@nulab.example" } ] */
            Assert.Equal(1, actual[0].Id);
            Assert.Equal("admin", actual[0].UserId);
            Assert.Equal("admin", actual[0].Name);
            Assert.Equal(1, actual[0].RoleType);
            Assert.Equal("ja", actual[0].Lang);
            Assert.Equal("eguchi@nulab.example", actual[0].MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUser(200);

            Assert.Equal(200, actual.Id);
            Assert.Equal("admin", actual.UserId);
            Assert.Equal("admin", actual.Name);
            Assert.Equal(1, actual.RoleType);
            Assert.Equal("ja", actual.Lang);
            Assert.Equal("eguchi@nulab.example", actual.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            // { "id": 1, "userId": "admin", "name": "admin", "roleType": 1, "lang": "ja", "mailAddress": "eguchi@nulab.example" }
            var user = new User
            {
                UserId = "admin",
                Name = string.Format("admin.{0}", DateTime.Now.Ticks),
                Lang = null,
                MailAddress = string.Format("{0}@{1}", "admin", "example.com"),
                RoleType = 6 // guest viewer
            };
            var pass = Path.GetTempFileName().PadLeft(21);
            pass = pass.Substring(pass.Length - 20, 20);

            var actual = backlog.AddUser(user, pass);
            Assert.True(actual.Id > 0);
            Assert.Equal(user.UserId, actual.UserId);
            Assert.Equal(user.Name, actual.Name);
            // Assert.Equal(user.Lang, actual.Lang);
            Assert.Equal(user.MailAddress, actual.MailAddress);
            Assert.Equal(user.RoleType, actual.RoleType);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();
            var backlog = new Backlog(Settings);
            var user = new User
            {
                Id = 1,
                UserId = "guest",
                Name = string.Format("guest.{0}", DateTime.Now.Ticks),
                Lang = null,
                MailAddress = string.Format("{0}@{1}", "guest", "example.com"),
                RoleType = 5 // guest reporter
            };
            var password = Path.GetTempFileName().PadLeft(21);
            password = password.Substring(password.Length - 20, 20);

            var actual = backlog.UpdateUser(user, password);
            Assert.True(actual.Id > 0);
            // Assert.Equal(user.UserId, actual.UserId); // userId does not provide?
            Assert.Equal(user.Name, actual.Name);
            // Assert.Equal(user.Lang, actual.Lang);
            Assert.Equal(user.MailAddress, actual.MailAddress);
            Assert.Equal(user.RoleType, actual.RoleType);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();
            var backlog = new Backlog(Settings);
            var delId = DateTime.Now.Ticks;
            var deleted = backlog.DeleteUser(delId);
            Assert.Equal(delId, deleted.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetOwnUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var myself = backlog.GetOwnUser();
            var dummySelf = new
            {
                id = 1,
                userId = "admin",
                name = "admin",
                roleType = 1,
                lang = "ja",
                mailAddress = "eguchi@nulab.example"
            };

            Assert.Equal(dummySelf.id, myself.Id);
            Assert.Equal(dummySelf.userId, myself.UserId);
            Assert.Equal(dummySelf.name, myself.Name);
            Assert.Equal(dummySelf.roleType, myself.RoleType);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserIconTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUserIcon(1);
            Assert.Equal("person_168.gif", actual.FileName);
            var logo = Resources.logo;
            Assert.Equal(logo.Size.Width, actual.Content.Size.Width);
            Assert.Equal(logo.Size.Height, actual.Content.Size.Height);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserActivitiesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUserActivities(1);
            Assert.Equal(1, actual.Count);
            Assert.Equal(2, actual[0].Type);
        }
    }
}