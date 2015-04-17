// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForMockServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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

        #region /api/v2/space

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

        #endregion

        #region /api/v2/users

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
        public override void GetUserRecentUpdatesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUserRecentUpdates(1);
            Assert.Equal(1, actual.Count);
            Assert.Equal(2, actual[0].Type);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetReceivedStarListTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetReceivedStarList(1);
            /*{
                new
                {
                    id=75,
                    // comment = null,
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
            });*/
            Assert.Equal(1, actual.Count);
            Assert.Equal(null, actual[0].Comment);
            Assert.Equal("https://xx.backlogtool.com/view/BLG-1", actual[0].Url);
            Assert.Equal("[BLG-1] first issue | Show issue - Backlog", actual[0].Title);

            Assert.Equal(1, actual[0].Presenter.Id);
            Assert.Equal(new DateTime(2014, 1, 23, 10, 55, 19, DateTimeKind.Utc), actual[0].Created);
        }

        /// <inheritdoc/>
        [Fact]
        public override void CountUserReceivedStarsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.CountUserReceivedStars(1);
            Assert.Equal(42, actual.Count);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedIssuesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetListOfRecentlyViewedIssues();
            Assert.Equal(1, actual.Count);
            var issue = actual[0].Issue;
            Assert.Equal(1, issue.Id);
            Assert.Equal(1, issue.ProjectId);
            Assert.Equal("BLG-1", issue.IssueKey);
            Assert.Equal(1, issue.KeyId);
            Assert.Equal(2, issue.IssueType.Id);
            Assert.Equal(1, issue.IssueType.ProjectId);
            Assert.Equal("Task", issue.IssueType.Name);
            Assert.Equal("#7ea800", issue.IssueType.Color);
            Assert.Equal(0, issue.IssueType.DisplayOrder);
            Assert.Equal("first issue", issue.Summary);
            Assert.Equal(string.Empty, issue.Description);
            Assert.Equal(0, issue.Resolutions.Id); // ""resolutions"": { ""id"": 0, ""name"": ""対応済み"" },
            Assert.Equal("対応済み", issue.Resolutions.Name);
            Assert.Equal(3, issue.Priority.Id);
            Assert.Equal("Normal", issue.Priority.Name);
            Assert.Equal(1, issue.Status.Id);
            Assert.Equal("Open", issue.Status.Name);
            Assert.Equal(2, issue.Assignee.Id);
            Assert.Equal("eguchi", issue.Assignee.Name);
            Assert.Equal(2, issue.Assignee.RoleType);
            Assert.Null(issue.Assignee.Lang);
            Assert.Equal("eguchi@nulab.example", issue.Assignee.MailAddress);
            Assert.Equal(1, issue.Categories.Count);
            Assert.Equal(54712, issue.Categories[0].Id);
            Assert.Equal("API", issue.Categories[0].Name);
            Assert.Equal(2147483646, issue.Categories[0].DisplayOrder);
            Assert.Equal(1, issue.Versions.Count);
            Assert.Equal(33856, issue.Versions[0].Id);
            Assert.Equal(26476, issue.Versions[0].ProjectId);
            Assert.Equal("1.0.0", issue.Versions[0].Name);
            Assert.Equal("initial release version", issue.Versions[0].Description);
            Assert.Equal(new DateTime(2015, 04, 01, 00, 00, 00, DateTimeKind.Utc), issue.Versions[0].StartDate);
            Assert.Equal(new DateTime(2015, 04, 30, 00, 00, 00, DateTimeKind.Utc), issue.Versions[0].ReleaseDueDate);
            Assert.Equal(false, issue.Versions[0].Archived);
            Assert.Equal(2147483646, issue.Versions[0].DisplayOrder);
            Assert.Equal(1, issue.Milestones.Count);
            Assert.Equal(33856, issue.Milestones[0].Id);
            Assert.Equal(26476, issue.Milestones[0].ProjectId);
            Assert.Equal("1.0.0", issue.Milestones[0].Name);
            Assert.Equal("initial release version", issue.Milestones[0].Description);
            Assert.Equal(new DateTime(2015, 04, 01, 00, 00, 00, DateTimeKind.Utc), issue.Milestones[0].StartDate);
            Assert.Equal(new DateTime(2015, 04, 30, 00, 00, 00, DateTimeKind.Utc), issue.Milestones[0].ReleaseDueDate);
            Assert.Equal(false, issue.Milestones[0].Archived);
            Assert.Equal(2147483646, issue.Milestones[0].DisplayOrder);
            Assert.Null(issue.StartDate); // DateTime
            Assert.Equal(new DateTime(2015, 4, 10, 0, 0, 0, DateTimeKind.Utc), issue.DueDate); // "dueDate": "2015-04-10T00:00:00Z"
            Assert.Equal(2.0, issue.EstimatedHours);
            Assert.Equal(3.0, issue.ActualHours);
            Assert.Null(issue.ParentIssueId);
            Assert.Equal(1, issue.CreatedUser.Id);
            Assert.Equal(new DateTime(2012, 7, 23, 6, 10, 15, DateTimeKind.Utc), issue.Created);
            Assert.Equal(1, issue.UpdatedUser.Id);
            Assert.Equal(new DateTime(2013, 2, 7, 8, 9, 49, DateTimeKind.Utc), issue.Updated);
            Assert.Equal(0, issue.CustomFields.Count); // TODO: custome field type
            Assert.Equal(1, issue.Attachments.Count);
            Assert.Equal(1, issue.Attachments[0].Id);
            Assert.Equal(1, issue.SharedFiles.Count);
            Assert.Equal(454403, issue.SharedFiles[0].Id);
            Assert.Equal(1, issue.Stars.Count);
            Assert.Equal(10, issue.Stars[0].Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedProjectsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetListOfRecentlyViewedProjects();
            Assert.Equal(1, actual.Count);
            Assert.IsAssignableFrom<IProject>(actual[0].Project);
            Assert.Equal(1, actual[0].Project.Id);
            Assert.Equal("TEST", actual[0].Project.ProjectKey);
            Assert.Equal(new DateTime(2014, 7, 11, 1, 59, 7), actual[0].Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedWikisTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetListOfRecentlyViewedWikis();
            Assert.Equal(1, actual.Count);
            Assert.IsAssignableFrom<IWikiPage>(actual[0].WikiPage);
            Assert.Equal(112, actual[0].WikiPage.Id);
            Assert.Equal(103, actual[0].WikiPage.ProjectId);
            Assert.Equal("Home", actual[0].WikiPage.Name);
            Assert.Equal(1, actual[0].WikiPage.Tags.Count);
            Assert.Equal(12, actual[0].WikiPage.Tags[0].Id);
            Assert.Equal("proceedings", actual[0].WikiPage.Tags[0].Name);
            Assert.Equal(1, actual[0].WikiPage.CreatedUser.Id);
            Assert.Equal(1, actual[0].WikiPage.UpdatedUser.Id);
            Assert.Equal(new DateTime(2013, 5, 30, 9, 11, 36), actual[0].WikiPage.Created);
            Assert.Equal(new DateTime(2013, 5, 30, 9, 11, 36), actual[0].WikiPage.Updated);
            Assert.Equal(new DateTime(2014, 7, 16, 7, 18, 16), actual[0].Updated);
        }

        #endregion

        #region /api/v2/groups

        /// <inheritdoc/>
        [Fact]
        public override void GetGroupsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetGroups();
            Assert.Equal(1, actual.Count);
            Assert.IsAssignableFrom<IGroup>(actual[0]);
            Assert.Equal(1, actual[0].Id);
            Assert.Equal("test", actual[0].Name);
            Assert.Equal(1, actual[0].Members.Count);
            Assert.Equal(2, actual[0].Members[0].Id);
            Assert.Equal("developer", actual[0].Members[0].UserId);
            Assert.Equal(-1, actual[0].DisplayOrder);
            Assert.Equal(1, actual[0].CreatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual[0].Created);
            Assert.Equal(1, actual[0].UpdatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual[0].Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddGroupTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var name = string.Format("g.{0}", DateTime.Now.Ticks);
            var members = new long[] { 2 };
            var actual = backlog.AddGroup(name, members);
            Assert.Equal(name, actual.Name);
            Assert.Equal(1, actual.Members.Count);
            Assert.Equal(2, actual.Members[0].Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetGroupTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetGroup(20);
            Assert.Equal(20, actual.Id);
            Assert.Equal("test", actual.Name);
            Assert.Equal(1, actual.Members.Count);
            Assert.Equal(2, actual.Members[0].Id);
            Assert.Equal("developer", actual.Members[0].UserId);
            Assert.Equal(-1, actual.DisplayOrder);
            Assert.Equal(1, actual.CreatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Created);
            Assert.Equal(1, actual.UpdatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateGroupTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var member = new long[] { 1, 2, 3 };
            var actual = backlog.UpdateGroup(20, "newName", member);
            Assert.Equal(20, actual.Id);
            Assert.Equal("newName", actual.Name);
            Assert.Equal(2, actual.Members[0].Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteGroupTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.DeleteGroup(50);
            Assert.Equal(50, actual.Id);
            Assert.Equal("test", actual.Name);
            Assert.Equal(1, actual.Members.Count);
            Assert.Equal(2, actual.Members[0].Id);
            Assert.Equal("developer", actual.Members[0].UserId);
            Assert.Equal(-1, actual.DisplayOrder);
            Assert.Equal(1, actual.CreatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Created);
            Assert.Equal(1, actual.UpdatedUser.Id);
            Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Updated);
        }

        #endregion

        #region /api/v2/statuses

        /// <inheritdoc/>
        [Fact]
        public override void GetStatusesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetStatuses();

            Assert.Equal(4, actual.Count);
            /*
                new { id = 1, name = "Open" },
                new { id = 2, name = "In Progress" },
                new { id = 3, name = "Resolved" },
                new { id = 4, name = "Closed" }
            */
            Assert.Equal(1, actual[0].Id);
            Assert.Equal("Open", actual[0].Name);
            Assert.Equal(2, actual[1].Id);
            Assert.Equal("In Progress", actual[1].Name);
            Assert.Equal(3, actual[2].Id);
            Assert.Equal("Resolved", actual[2].Name);
            Assert.Equal(4, actual[3].Id);
            Assert.Equal("Closed", actual[3].Name);
        }

        #endregion

        #region /api/v2/resolutions

        /// <inheritdoc/>
        [Fact]
        public override void GetResolutionsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetResolutions();
            /*[
             * { "id": 0, "name": "Fixed" }, { "id": 1, "name": "Won't Fix" }, { "id": 2, "name": "Invalid" },
             * { "id": 3, "name": "Duplication" }, { "id": 4, "name": "Cannot Reproduce" }
             *]
             */
            Assert.Equal(5, actual.Count);
            Assert.Equal(0, actual[0].Id);
            Assert.Equal("Fixed", actual[0].Name);
            Assert.Equal(1, actual[1].Id);
            Assert.Equal("Won't Fix", actual[1].Name);
            Assert.Equal(2, actual[2].Id);
            Assert.Equal("Invalid", actual[2].Name);
            Assert.Equal(3, actual[3].Id);
            Assert.Equal("Duplication", actual[3].Name);
            Assert.Equal(4, actual[4].Id);
            Assert.Equal("Cannot Reproduce", actual[4].Name);
        }

        #endregion

        #region /api/v2/priorities

        /// <inheritdoc/>
        [Fact]
        public override void GetPrioritiesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            IList<IPriority> actual = backlog.GetPriorities();
            // [ { "id": 2, "name": "High" }, { "id": 3, "name": "Normal" }, { "id": 4, "name": "Low" } ]
            Assert.Equal(3, actual.Count);
            Assert.Equal(2, actual[0].Id);
            Assert.Equal("High", actual[0].Name);
            Assert.Equal(3, actual[1].Id);
            Assert.Equal("Normal", actual[1].Name);
            Assert.Equal(4, actual[2].Id);
            Assert.Equal("Low", actual[2].Name);
        }

        #endregion

        #region /api/v2/projects

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetProjects();

            Assert.Equal(1, actual.Count);
            Assert.Equal(1, actual[0].Id);
            Assert.Equal("TEST", actual[0].ProjectKey);
            Assert.Equal("test", actual[0].Name);
            Assert.False(actual[0].ChartEnabled);
            Assert.False(actual[0].SubtaskingEnabled);
            Assert.Equal("markdown", actual[0].TextFormattingRule);
            Assert.False(actual[0].Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var newProject = new Project
            {
                ProjectKey = "TEST",
                Name = "test",
                ChartEnabled = false,
                SubtaskingEnabled = false,
                TextFormattingRule = "markdown",
                Archived = false
            };
            var actual = backlog.AddProject(newProject);
            /*{
             * "id": 1, "projectKey": "TEST", "name": "test",
             * "chartEnabled": false, "subtaskingEnabled": false,
             * "textFormattingRule": "markdown", "archived":false
             * }*/
            Assert.Equal(1, actual.Id);
            Assert.Equal("TEST", actual.ProjectKey);
            Assert.Equal("test", actual.Name);
            Assert.Equal(false, actual.ChartEnabled);
            Assert.Equal(false, actual.SubtaskingEnabled);
            Assert.Equal("markdown", actual.TextFormattingRule);
            Assert.Equal(false, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);

            var actual = backlog.GetProject("TEST");
            /*{
             * "id": 1, "projectKey": "TEST", "name": "test",
             * "chartEnabled": false, "subtaskingEnabled": false,
             * "textFormattingRule": "markdown", "archived":false
             * }*/
            Assert.Equal(1, actual.Id);
            Assert.Equal("TEST", actual.ProjectKey);
            Assert.Equal("test", actual.Name);
            Assert.Equal(false, actual.ChartEnabled);
            Assert.Equal(false, actual.SubtaskingEnabled);
            Assert.Equal("markdown", actual.TextFormattingRule);
            Assert.Equal(false, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var newProject = new Project
            {
                Id = new Random().Next(),
                Name = "hoge",
                ProjectKey = "hogehoge",
                ChartEnabled = false,
                SubtaskingEnabled = false,
                TextFormattingRule = "markdown",
                Archived = false
            };
            var actual = backlog.UpdateProject(newProject);
            Assert.Equal(newProject.Id, actual.Id);
            Assert.Equal(newProject.ProjectKey, actual.ProjectKey);
            Assert.Equal(newProject.Name, actual.Name);
            Assert.Equal(newProject.ChartEnabled, actual.ChartEnabled);
            Assert.Equal(newProject.SubtaskingEnabled, actual.SubtaskingEnabled);
            Assert.Equal(newProject.TextFormattingRule, actual.TextFormattingRule);
            Assert.Equal(newProject.Archived, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);

            var actual = backlog.DeleteProject("TEST");
            /*{
             * "id": 1, "projectKey": "TEST", "name": "test",
             * "chartEnabled": false, "subtaskingEnabled": false,
             * "textFormattingRule": "markdown", "archived":false
             * }*/
            Assert.Equal(1, actual.Id);
            Assert.Equal("TEST", actual.ProjectKey);
            Assert.Equal("test", actual.Name);
            Assert.Equal(false, actual.ChartEnabled);
            Assert.Equal(false, actual.SubtaskingEnabled);
            Assert.Equal("markdown", actual.TextFormattingRule);
            Assert.Equal(false, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIconTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetProjectImage("1");
            Assert.Equal("logo_mark.png", actual.FileName);
            var logo = Resources.projectIcon;
            Assert.Equal(logo.Size.Width, actual.Content.Size.Width);
            Assert.Equal(logo.Size.Height, actual.Content.Size.Height);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectRecentUpdatesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var activities = backlog.GetProjectRecentUpdates("1");
            Assert.Equal(1, activities.Count);
            Assert.Equal(2, activities[0].Type);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var uid = new Random().Next();
            var actual = backlog.AddProjectUser("test", uid);
            Assert.Equal(uid, actual.Id);
            Assert.Equal("admin", actual.UserId);
            Assert.Equal("admin", actual.Name);
            Assert.Equal(1, actual.RoleType);
            Assert.Equal("ja", actual.Lang);
            Assert.Equal("eguchi@nulab.example", actual.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectUsersTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetProjectUsers("test");
            Assert.Equal(1, actual.Count);
            Assert.Equal("admin", actual[0].UserId);
            Assert.Equal("admin", actual[0].Name);
            Assert.Equal(1, actual[0].RoleType);
            Assert.Equal("ja", actual[0].Lang);
            Assert.Equal("eguchi@nulab.example", actual[0].MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectUserTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var uid = new Random().Next();
            var actual = backlog.DeleteProjectUser("test", uid);
            Assert.Equal(uid, actual.Id);
            Assert.Equal("admin", actual.UserId);
            Assert.Equal("admin", actual.Name);
            Assert.Equal(1, actual.RoleType);
            Assert.Equal("ja", actual.Lang);
            Assert.Equal("eguchi@nulab.example", actual.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectAdministorTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var uid = new Random().Next();
            var actual = backlog.AddProjectAdministrator("test", uid);
            Assert.Equal(uid, actual.Id);
            Assert.Equal("takada", actual.UserId);
            Assert.Equal("takada", actual.Name);
            Assert.Equal(2, actual.RoleType);
            Assert.Equal("ja", actual.Lang);
            Assert.Equal("takada@nulab.example", actual.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectAdministratorsTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetProjectAdministrators("test");
            Assert.Equal(1, actual.Count);
            Assert.Equal(5686, actual[0].Id);
            Assert.Equal("takada", actual[0].UserId);
            Assert.Equal("takada", actual[0].Name);
            Assert.Equal(2, actual[0].RoleType);
            Assert.Equal("ja", actual[0].Lang);
            Assert.Equal("takada@nulab.example", actual[0].MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectAdministratorTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            var uid = new Random().Next();
            var actual = backlog.DeleteProjectAdministrator("test", uid);
            Assert.Equal(uid, actual.Id);
            Assert.Equal("takada", actual.UserId);
            Assert.Equal("takada", actual.Name);
            Assert.Equal(2, actual.RoleType);
            Assert.Equal("ja", actual.Lang);
            Assert.Equal("takada@nulab.example", actual.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIssueTypesTest()
        {
            SkipIfSettingIsBroken();
            SkipIfMockServerIsDown();

            var backlog = new Backlog(Settings);
            IList<IIssueType> actual = backlog.GetProjectIssueTypes("projectKey");
            Assert.Equal(1, actual.Count);

            Assert.Equal(1, actual[0].Id);
            Assert.Equal(1, actual[0].ProjectId);
            Assert.Equal("Bug", actual[0].Name);
            Assert.Equal("#990000", actual[0].Color);
            Assert.Equal(0, actual[0].DisplayOrder);
        }

        #endregion
    }
}