// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForRealServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using BL4N.Data;
using BL4N.Tests.Properties;
using DynamicSkipExample;
using Xunit;

namespace BL4N.Tests
{
    /// <summary> 実サーバを使ったテスト </summary>
    public class BacklogTestsForRealServer : BacklogTests
    {
        /// <summary> </summary>
        public BacklogTestsForRealServer()
            : base(BacklogConnectionSettings.Load("bl4n.owner.json"))
        {
        }

        /// <inheritdoc/>
        [SkippableFact]
        public override void BacklogConstructorTest()
        {
            SkipIfSettingIsBroken();

            var realClient = new Backlog(Settings);
            Assert.Equal(Settings.SpaceName, realClient.SpaceName);
        }

        #region /api/v2/space

        /// <inheritdoc/>
        [SkippableFact]
        public override void GetSpaceTest()
        {
            SkipIfSettingIsBroken();

            // {"spaceKey":"bl4n","name":"bl4n","ownerId":60965,"lang":"ja","timezone":"Asia/Tokyo","reportSendTime":"18:00:00","textFormattingRule":"backlog","created":"2015-03-21T04:00:14Z","updated":"2015-03-21T04:00:14Z"}
            const string RealResult = @"{
""spaceKey"":""bl4n"",
""name"":""bl4n"",
""ownerId"":60965,
""lang"":""ja"",
""timezone"":""Asia/Tokyo"",
""reportSendTime"":""18:00:00"",
""textFormattingRule"":""backlog"",
""created"":""2015-03-21T04:00:14Z"",
""updated"":""2015-03-21T04:00:14Z""}";

            var expected = Backlog.DeserializeObj<Space>(RealResult);
            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpace();

            Assert.Equal(expected.SpaceKey, actual.SpaceKey);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.OwnerId, actual.OwnerId);
            Assert.Equal(expected.Lang, actual.Lang);
            Assert.Equal(expected.Timezone, actual.Timezone);
            Assert.Equal(expected.ReportSendTime, actual.ReportSendTime);
            Assert.Equal(expected.TextFormattingRule, actual.TextFormattingRule);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.Updated, actual.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceActivitiesTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpaceActivities();
            Assert.NotNull(actual);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceActivities_with_filter_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var filter = new RecentUpdateFilterOptions
            {
                Count = 5,
                Ascending = true
            };
            var actual = backlog.GetSpaceActivities(filter);
            Assert.NotNull(actual);
            Assert.True(filter.Count >= actual.Count);
            if (actual.Count > 2)
            {
                Assert.True(actual[0].Id < actual[1].Id);
            }
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceLogoTest()
        {
            SkipIfSettingIsBroken();

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

            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpaceNotifiacation();

            // UpdateSpaceNotificationTest update real data
            Assert.True(actual.Content.EndsWith(" に更新しました．"));
            //// Assert.Equal(new DateTime(2015, 3, 26, 6, 37, 37, DateTimeKind.Utc), actual.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateSpaceNotificationTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var content = string.Format("{0:F} に更新しました．", DateTime.Now);
            var actual = backlog.UpdateSpaceNotification(content);
            Assert.Equal(content, actual.Content);
            //// Assert.Equal(new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc), actual.Updated);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetSpaceDiskUsageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetSpaceDiskUsage();

            // Assert.Equal(104857600, actual.Capacity);
            Assert.True(actual.Capacity >= 0);

            // Assert.Equal(238554, actual.Issue);
            Assert.True(actual.Issue >= 0);

            // Assert.Equal(0, actual.Wiki);
            Assert.True(actual.Wiki >= 0);

            // Assert.Equal(14948, actual.File);
            Assert.True(actual.File >= 0);

            // Assert.Equal(0, actual.Subversion);
            Assert.True(actual.Subversion >= 0);

            // Assert.Equal(40960, actual.Git);
            Assert.True(actual.Git >= 0);

            Assert.Equal(1, actual.Details.Count);

            // Assert.Equal(26476, actual.Details[0].ProjectId);
            // Assert.Equal(238554, actual.Details[0].Issue);
            // Assert.Equal(0, actual.Details[0].Wiki);
            // Assert.Equal(14948, actual.Details[0].File);
            // Assert.Equal(0, actual.Details[0].Subversion);
            // Assert.Equal(40960, actual.Details[0].Git);
            Assert.True(actual.Details[0].Git >= 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddAttachmentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            using (var ms = new MemoryStream())
            {
                var bmp = Resources.logo;
                bmp.Save(ms, bmp.RawFormat);
                var size = ms.Length;
                ms.Position = 0;
                var actual = backlog.AddAttachment("logo.png", ms);
                Assert.True(actual.Id > 0);
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

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUsers();

            // [{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //  {"id":60966,"userId":"t.ashula","name":"t.ashula","roleType":2,"lang":null,"mailAddress":"t.ashula@gmail.com"}]
            Assert.True(actual.Count > 1);
            Assert.Equal(60965, actual[0].Id);
            Assert.Equal("bl4n.admin", actual[0].UserId);
            Assert.Equal("bl4n.admin", actual[0].Name);
            Assert.Equal(1, actual[0].RoleType);
            Assert.Equal(60966, actual[1].Id);
            Assert.Equal("t.ashula", actual[1].UserId);
            Assert.Equal("t.ashula", actual[1].Name);
            Assert.Equal(2, actual[1].RoleType);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetUser(60966);

            // [{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //  {"id":60966,"userId":"t.ashula","name":"t.ashula","roleType":2,"lang":null,"mailAddress":"t.ashula@gmail.com"}]
            Assert.Equal(60966, actual.Id);
            Assert.Equal("t.ashula", actual.UserId);
            Assert.Equal("t.ashula", actual.Name);
            Assert.Equal(2, actual.RoleType);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var name = string.Format("bl4n.{0}", DateTime.Now.Ticks);
            var userId = name;
            var mailAddress = string.Format("t.ashula+{0}@{1}", name, "gmail.com");

            var pass = Path.GetTempFileName().PadLeft(21);
            pass = pass.Substring(pass.Length - 20, 20);

            var options = new AddUserOptions(userId, pass, name, mailAddress, UserRoleType.GuestViewer);
            var actual = backlog.AddUser(options);
            Assert.True(actual.Id > 0);
            Assert.Equal(options.UserId, actual.UserId);
            Assert.Equal(options.Name, actual.Name);
            Assert.Equal(options.MailAddress, actual.MailAddress);
            Assert.Equal((int)options.RoleType, actual.RoleType);
            backlog.DeleteUser(actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateUserTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);

            var userId = string.Format("bl4n.{0}", DateTime.Now.Ticks % 1000);
            var addOptions = new AddUserOptions(userId, "hogehoge", "bl4n.old", "bl4n.old@example.com", UserRoleType.GuestViewer);
            var oldUser = backlog.AddUser(addOptions);
            Assert.True(oldUser.Id > 0);

            var newName = string.Format("bl4n.{0}", DateTime.Now.Ticks);
            var updateOptions = new UpdateUserOptions { Name = newName };
            var changed = backlog.UpdateUser(oldUser.Id, updateOptions);
            Assert.Equal(newName, changed.Name);
            backlog.DeleteUser(oldUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteUserTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var name = string.Format("bl4n.{0}", DateTime.Now.Ticks);
            var userId = name;
            var mailAddress = string.Format("t.ashula+{0}@{1}", name, "gmail.com");

            var pass = Path.GetTempFileName().PadLeft(21);
            pass = pass.Substring(pass.Length - 20, 20);

            var options = new AddUserOptions(userId, pass, name, mailAddress, UserRoleType.GuestViewer);
            var added = backlog.AddUser(options);

            var deleted = backlog.DeleteUser(added.Id);
            Assert.Equal(added.Id, deleted.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetOwnUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var myself = backlog.GetOwnUser();
            //// {"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"}
            var dummySelf = new
            {
                id = 60965,
                userId = "bl4n.admin",
                name = "bl4n.admin",
                roleType = 1,
                //// lang = null,
                mailAddress = "t.ashula+nulab@gmail.com"
            };

            Assert.Equal(dummySelf.id, myself.Id);
            Assert.Equal(dummySelf.userId, myself.UserId);
            Assert.Equal(dummySelf.name, myself.Name);
            Assert.Equal(dummySelf.roleType, myself.RoleType);
            Assert.Equal(dummySelf.mailAddress, myself.MailAddress);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserIconTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var self = backlog.GetOwnUser();
            var actual = backlog.GetUserIcon(self.Id);
            Assert.Equal("22_pierrot.gif", actual.FileName);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserRecentUpdatesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var activities = backlog.GetUserRecentUpdates(uid);
            Assert.True(activities.Count > 0);
            Assert.Equal(uid, activities[0].CreatedUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetUserRecentUpdates_with_filter_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var filter = new RecentUpdateFilterOptions { Count = 5 };
            var activities = backlog.GetUserRecentUpdates(uid, filter);
            Assert.True(activities.Count > 0);
            Assert.True(activities.Count <= filter.Count);
            Assert.Equal(uid, activities[0].CreatedUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetReceivedStarListTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var actual = backlog.GetReceivedStarList(uid);
            Assert.True(actual.Count > 0);
            Assert.Equal(null, actual[0].Comment);
            Assert.StartsWith("https://bl4n.backlog.jp/", actual[0].Url);
            //// Assert.StartsWith("[BL4N-", actual[0].Title);
            //// Assert.Equal(60965, actual[0].Presenter.Id);
            //// Assert.Equal(new DateTime(2015, 4, 6, 5, 52, 46, DateTimeKind.Utc), actual[0].Created);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetReceivedStarList_with_filter_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var filter = new ResultPagingOptions { Count = 5 };
            var actual = backlog.GetReceivedStarList(uid, filter);
            Assert.True(actual.Count > 0);
            Assert.True(actual.Count <= filter.Count);
            Assert.Equal(null, actual[0].Comment);
            Assert.StartsWith("https://bl4n.backlog.jp/", actual[0].Url);
        }

        /// <inheritdoc/>
        [Fact]
        public override void CountUserReceivedStarsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var actual = backlog.CountUserReceivedStars(uid);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void CountUserReceivedStars_with_term_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var term = new TermOptions(null, new DateTime(2015, 1, 1));
            var actual = backlog.CountUserReceivedStars(uid, term);
            Assert.True(actual.Count == 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedIssuesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var actual = backlog.GetListOfRecentlyViewedIssues();
            Assert.InRange(actual.Count, 1, 20);
            var issue = actual[0].Issue;
            Assert.IsAssignableFrom<IIssue>(issue);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedIssues_with_offset_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var offset = new OffsetOptions { Count = 5, Offset = 5 };
            var actual = backlog.GetListOfRecentlyViewedIssues(offset);
            Assert.InRange(actual.Count, 1, offset.Count);
            var issue = actual[0].Issue;
            Assert.IsAssignableFrom<IIssue>(issue);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedProjectsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var actual = backlog.GetListOfRecentlyViewedProjects();
            Assert.InRange(actual.Count, 1, 20);
            var project = actual[0].Project;
            Assert.IsAssignableFrom<IProject>(project);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedProjects_with_offset_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var offset = new OffsetOptions { Count = 5, Offset = 5 };
            var actual = backlog.GetListOfRecentlyViewedProjects(offset);
            Assert.Equal(actual.Count, 0); // Free plan has only one project
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedWikisTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetListOfRecentlyViewedWikis();

            Assert.InRange(actual.Count, 1, 20);
            Assert.IsAssignableFrom<IWikiPage>(actual[0].WikiPage);
            Assert.True(actual[0].WikiPage.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetListOfRecentlyViewedWikis_with_offset_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var offset = new OffsetOptions { Count = 5, Offset = 5 };
            var actual = backlog.GetListOfRecentlyViewedWikis(offset);
            Assert.InRange(actual.Count, 1, offset.Count);
            Assert.IsAssignableFrom<IWikiPage>(actual[0].WikiPage);
            Assert.True(actual[0].WikiPage.Id > 0);
        }

        #endregion

        #region /api/v2/groups

        /// <inheritdoc/>
        [Fact]
        public override void GetGroupsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetGroups();
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetGroups_with_offset_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var offset = new OffsetOptions { Count = 4 };
            var actual = backlog.GetGroups(offset);
            Assert.True(actual.Count > 0);
            Assert.True(actual.Count <= 4);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var users = backlog.GetUsers().Select(u => u.Id).OrderBy(_ => _).ToArray();
            var name = string.Format("g.{0}", DateTime.Now.Ticks);
            var options = new AddGroupOptions(name);
            options.AddMembers(users);
            var actual = backlog.AddGroup(options);
            Assert.Equal(name, actual.Name);
            Assert.Equal(users.Length, actual.Members.Count);
            var members = actual.Members.Select(u => u.Id).OrderBy(_ => _).ToArray();
            Assert.Equal(users, members);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var users = backlog.GetUsers().Select(u => u.Id).OrderBy(_ => _).ToArray();
            var name = string.Format("g1.{0}", DateTime.Now.Ticks % 100000);
            var options = new AddGroupOptions(name);
            options.AddMembers(users);

            var added = backlog.AddGroup(options);
            Assert.True(added.Id > 0);

            var actual = backlog.GetGroup(added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.StartsWith(name, actual.Name);
            Assert.Equal(users.Length, actual.Members.Count);

            backlog.DeleteGroup(added.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var g = DateTime.Now.Ticks % 100000;

            var addOptions = new AddGroupOptions(string.Format("g.{0:0000}", g));
            var added = backlog.AddGroup(addOptions);
            Assert.True(added.Id > 0);

            var newName = string.Format("g1.{0:00000}", g + 1); // group.name.length must be less than 20
            var options = new UpdateGroupOptions { Name = newName };
            var actual = backlog.UpdateGroup(added.Id, options);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(newName, actual.Name);
            Assert.Equal(added.Members.Count, actual.Members.Count);
            Assert.InRange(actual.Created, added.Created, added.Created.AddSeconds(2)); // why created_at updated?

            backlog.DeleteGroup(added.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var options = new AddGroupOptions("delete");
            var added = backlog.AddGroup(options);
            var actual = backlog.DeleteGroup(added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.Members.Count, actual.Members.Count);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.CreatedUser.Id, actual.CreatedUser.Id);
            //// Assert.Equal(added.Created, actual.Created); // ???
            //// Assert.Equal(1, actual.UpdatedUser.Id);
            //// Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Updated);
        }

        #endregion

        #region /api/v2/statuses

        /// <inheritdoc/>
        [Fact]
        public override void GetStatusesTest()
        {
            // [{"id":1,"name":"未対応"},{"id":2,"name":"処理中"},{"id":3,"name":"処理済み"},{"id":4,"name":"完了"}]
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetStatuses();

            Assert.Equal(4, actual.Count);

            Assert.Equal(1, actual[0].Id);
            Assert.Equal("未対応", actual[0].Name);
            Assert.Equal(2, actual[1].Id);
            Assert.Equal("処理中", actual[1].Name);
            Assert.Equal(3, actual[2].Id);
            Assert.Equal("処理済み", actual[2].Name);
            Assert.Equal(4, actual[3].Id);
            Assert.Equal("完了", actual[3].Name);
        }

        #endregion

        #region /api/v2/resolutions

        /// <inheritdoc/>
        [Fact]
        public override void GetResolutionsTest()
        {
            // [{"id":0,"name":"対応済み"},{"id":1,"name":"対応しない"},{"id":2,"name":"無効"},{"id":3,"name":"重複"},{"id":4,"name":"再現しない"}]
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetResolutions();

            Assert.Equal(5, actual.Count);

            Assert.Equal(0, actual[0].Id);
            Assert.Equal("対応済み", actual[0].Name);
            Assert.Equal(1, actual[1].Id);
            Assert.Equal("対応しない", actual[1].Name);
            Assert.Equal(2, actual[2].Id);
            Assert.Equal("無効", actual[2].Name);
            Assert.Equal(3, actual[3].Id);
            Assert.Equal("重複", actual[3].Name);
            Assert.Equal(4, actual[4].Id);
            Assert.Equal("再現しない", actual[4].Name);
        }

        #endregion

        #region /api/v2/priorities

        /// <inheritdoc/>
        [Fact]
        public override void GetPrioritiesTest()
        {
            // [{"id":2,"name":"高"},{"id":3,"name":"中"},{"id":4,"name":"低"}]
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetPriorities();

            Assert.Equal(3, actual.Count);

            Assert.Equal(2, actual[0].Id);
            Assert.Equal("高", actual[0].Name);
            Assert.Equal(3, actual[1].Id);
            Assert.Equal("中", actual[1].Name);
            Assert.Equal(4, actual[2].Id);
            Assert.Equal("低", actual[2].Name);
        }

        #endregion

        #region /api/v2/projects

        #region project

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetProjects();

            // [{"id":26476,"projectKey":"BL4N","name":"bl4n","chartEnabled":false,"subtaskingEnabled":false,"textFormattingRule":"markdown","archived":false,"displayOrder":2147483646}]
            Assert.Equal(1, actual.Count);

            Assert.Equal(26476, actual[0].Id);
            Assert.Equal("BL4N", actual[0].ProjectKey);
            Assert.StartsWith("bl4n", actual[0].Name);
            Assert.False(actual[0].ChartEnabled);
            Assert.False(actual[0].SubtaskingEnabled);
            Assert.Equal("markdown", actual[0].TextFormattingRule);
            Assert.False(actual[0].Archived);
            Assert.Equal(2147483646, actual[0].DisplayOrder);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var np = new AddProjectOptions("newproject", "NEWPRO", chartEnabled: false, subtaskingEnabled: false, textFormattingRule: "markdown");

            // プランの都合上プロジェクトの追加ができないので追加自体のテストは不可
            var result = Assert.Throws<AggregateException>(() =>
            {
                var actual = backlog.AddProject(np);
                Assert.True(actual.Id > 0);
            });
            var inner = result.InnerExceptions[0] as BacklogException; // TODO: direct
            Assert.NotNull(inner);
            Assert.Equal(BacklogException.ErrorReason.InvalidRequestError, inner.Reasons[0]); // XXX: license error ?
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectTest()
        {
            // id:26476, key:BL4N
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects().FirstOrDefault();
            if (project == null)
            {
                Assert.False(true, "no project!!");
                return;
            }

            var actual = backlog.GetProject(project.Id);
            Assert.Equal(project.Id, actual.Id);
            Assert.Equal(project.ProjectKey, actual.ProjectKey);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProject_with_key_Test()
        {
            // id:26476, key:BL4N
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects().FirstOrDefault();
            if (project == null)
            {
                Assert.False(true, "no project!!");
                return;
            }

            var actual = backlog.GetProject(project.ProjectKey);
            Assert.Equal(project.Id, actual.Id);
            Assert.Equal(project.ProjectKey, actual.ProjectKey);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var current = backlog.GetProjects().First();
            var newName = "bl4n." + new Random().Next(99);
            var newProject = new UpdateProjectOptions { Name = newName };
            var actual = backlog.UpdateProject(current.Id, newProject);
            Assert.Equal(current.Id, actual.Id);
            Assert.Equal(current.ProjectKey, actual.ProjectKey);
            Assert.Equal(newProject.Name, actual.Name); // only name update
            Assert.Equal(current.ChartEnabled, actual.ChartEnabled);
            Assert.Equal(current.SubtaskingEnabled, actual.SubtaskingEnabled);
            Assert.Equal(current.TextFormattingRule, actual.TextFormattingRule);
            Assert.Equal(current.Archived, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProject_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var current = backlog.GetProjects().First();
            var newName = "bl4n." + new Random().Next(99);
            var newProject = new UpdateProjectOptions { Name = newName };
            var actual = backlog.UpdateProject(current.ProjectKey, newProject);
            Assert.Equal(current.Id, actual.Id);
            Assert.Equal(current.ProjectKey, actual.ProjectKey);
            Assert.Equal(newProject.Name, actual.Name); // only name update
            Assert.Equal(current.ChartEnabled, actual.ChartEnabled);
            Assert.Equal(current.SubtaskingEnabled, actual.SubtaskingEnabled);
            Assert.Equal(current.TextFormattingRule, actual.TextFormattingRule);
            Assert.Equal(current.Archived, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectTest()
        {
            // プランの都合上プロジェクトの追加ができないのでテスト不可
            // Assert.Equal("newproject", actual.Name);
            Assert.True(true, "cant del project on free plan.");
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProject_with_key_Test()
        {
            // プランの都合上プロジェクトの追加ができないのでテスト不可
            // Assert.Equal("newproject", actual.Name);
            Assert.True(true, "cant del project on free plan.");
        }

        #endregion

        #region project/misc

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIconTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectImage(projectId);
            Assert.Equal("26476", actual.FileName);
            var logo = Resources.projectIcon;
            Assert.Equal(logo.Size.Width, actual.Content.Size.Width);
            Assert.Equal(logo.Size.Height, actual.Content.Size.Height);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIcon_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectImage(projectKey);
            Assert.Equal("26476", actual.FileName);
            var logo = Resources.projectIcon;
            Assert.Equal(logo.Size.Width, actual.Content.Size.Width);
            Assert.Equal(logo.Size.Height, actual.Content.Size.Height);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectRecentUpdatesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var actual = backlog.GetProjectRecentUpdates(projectId);

            Assert.InRange(actual.Count, 1, 20);
            Assert.Equal(projectId, actual[0].Project.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectRecentUpdates_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var actual = backlog.GetProjectRecentUpdates(projectKey);

            Assert.InRange(actual.Count, 1, 20);
            Assert.Equal(projectKey, actual[0].Project.ProjectKey);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectRecentUpdates_with_filter_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var filter = new RecentUpdateFilterOptions { Count = 5 };
            var actual = backlog.GetProjectRecentUpdates(projectId, filter);

            Assert.True(actual.Count > 0);
            Assert.True(actual.Count <= filter.Count);
            Assert.Equal(projectId, actual[0].Project.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectDiskUsageTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectDiskUsage(projectId);
            Assert.Equal(projectId, actual.ProjectId);
            Assert.True(actual.File > 0);
            Assert.True(actual.Issue > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectDiskUsage_with_key_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectDiskUsage(projectKey);
            Assert.True(actual.ProjectId > 0);
            Assert.True(actual.File > 0);
            Assert.True(actual.Issue > 0);
        }

        #endregion

        #region project/user

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.GuestReporter);
            var newUser = backlog.AddUser(options);
            var actual = backlog.AddProjectUser(projectId, newUser.Id);
            Assert.Equal(newUser.Id, actual.Id);
            Assert.Equal(newUser.UserId, actual.UserId);
            Assert.Equal(newUser.Name, actual.Name);
            Assert.Equal(newUser.RoleType, actual.RoleType);
            Assert.Equal(newUser.Lang, actual.Lang);
            Assert.Equal(newUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(newUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectUser_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.GuestReporter);
            var newUser = backlog.AddUser(options);
            var actual = backlog.AddProjectUser(projectKey, newUser.Id);
            Assert.Equal(newUser.Id, actual.Id);
            Assert.Equal(newUser.UserId, actual.UserId);
            Assert.Equal(newUser.Name, actual.Name);
            Assert.Equal(newUser.RoleType, actual.RoleType);
            Assert.Equal(newUser.Lang, actual.Lang);
            Assert.Equal(newUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(newUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectUsersTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectUsers(projectId);
            Assert.True(actual.Count >= 2); // bl4n.admin, t.ashula and more
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectUsers_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectUsers(projectKey);
            Assert.True(actual.Count >= 2); // bl4n.admin, t.ashula and more
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.GuestReporter);
            var newUser = backlog.AddUser(options);
            var addedUser = backlog.AddProjectUser(projectId, newUser.Id);
            var actual = backlog.DeleteProjectUser(projectId, addedUser.Id);
            Assert.Equal(newUser.Id, actual.Id);
            Assert.Equal(newUser.UserId, actual.UserId);
            Assert.Equal(newUser.Name, actual.Name);
            Assert.Equal(newUser.RoleType, actual.RoleType);
            Assert.Equal(newUser.Lang, actual.Lang);
            Assert.Equal(newUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(newUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectUser_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.GuestReporter);
            var newUser = backlog.AddUser(options);
            var addedUser = backlog.AddProjectUser(projectKey, newUser.Id);
            var actual = backlog.DeleteProjectUser(projectKey, addedUser.Id);
            Assert.Equal(newUser.Id, actual.Id);
            Assert.Equal(newUser.UserId, actual.UserId);
            Assert.Equal(newUser.Name, actual.Name);
            Assert.Equal(newUser.RoleType, actual.RoleType);
            Assert.Equal(newUser.Lang, actual.Lang);
            Assert.Equal(newUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(newUser.Id);
        }

        #endregion

        #region project/admin

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectAdministratorTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.NormalUser);
            var newUser = backlog.AddUser(options);
            Assert.NotEqual(0, newUser.Id);
            var addedUser = backlog.AddProjectUser(projectId, newUser.Id);
            Assert.Equal(newUser.Id, addedUser.Id);
            var actual = backlog.AddProjectAdministrator(projectId, addedUser.Id);
            Assert.Equal(addedUser.Id, actual.Id);
            Assert.Equal(addedUser.UserId, actual.UserId);
            Assert.Equal(addedUser.Name, actual.Name);
            Assert.Equal(addedUser.RoleType, actual.RoleType);
            Assert.Equal(addedUser.Lang, actual.Lang);
            Assert.Equal(addedUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(addedUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectAdministrator_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.NormalUser);
            var newUser = backlog.AddUser(options);
            Assert.NotEqual(0, newUser.Id);
            var addedUser = backlog.AddProjectUser(projectKey, newUser.Id);
            Assert.Equal(newUser.Id, addedUser.Id);
            var actual = backlog.AddProjectAdministrator(projectKey, addedUser.Id);
            Assert.Equal(addedUser.Id, actual.Id);
            Assert.Equal(addedUser.UserId, actual.UserId);
            Assert.Equal(addedUser.Name, actual.Name);
            Assert.Equal(addedUser.RoleType, actual.RoleType);
            Assert.Equal(addedUser.Lang, actual.Lang);
            Assert.Equal(addedUser.MailAddress, actual.MailAddress);

            backlog.DeleteUser(addedUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectAdministratorsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var actual = backlog.GetProjectAdministrators(projectId);
            Assert.Equal(1, actual.Count); // t.ashula as a project admin, space admin is not project admin
            // [{"id":60966,"userId":"t.ashula","name":"t.ashula","roleType":2,"lang":null,"mailAddress":"t.ashula@gmail.com"}]
            Assert.Equal(60966, actual[0].Id);
            Assert.Equal("t.ashula", actual[0].Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectAdministrators_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var actual = backlog.GetProjectAdministrators(projectKey);
            Assert.Equal(1, actual.Count); // t.ashula as a project admin, space admin is not project admin
            // [{"id":60966,"userId":"t.ashula","name":"t.ashula","roleType":2,"lang":null,"mailAddress":"t.ashula@gmail.com"}]
            Assert.Equal(60966, actual[0].Id);
            Assert.Equal("t.ashula", actual[0].Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectAdministratorTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.NormalUser);
            var newUser = backlog.AddUser(options);

            Assert.NotEqual(0, newUser.Id);

            var addedUser = backlog.AddProjectUser(projectId, newUser.Id);
            Assert.Equal(newUser.Id, addedUser.Id);

            var addedAdmin = backlog.AddProjectAdministrator(projectId, addedUser.Id);
            Assert.Equal(addedAdmin.Id, addedUser.Id);

            var actual = backlog.DeleteProjectAdministrator(projectId, addedAdmin.Id);
            Assert.Equal(addedAdmin.Id, actual.Id);
            Assert.Equal(addedAdmin.UserId, actual.UserId);
            Assert.Equal(addedAdmin.Name, actual.Name);
            Assert.Equal(addedAdmin.RoleType, actual.RoleType);
            Assert.Equal(addedAdmin.Lang, actual.Lang);
            Assert.Equal(addedAdmin.MailAddress, actual.MailAddress);

            backlog.DeleteUser(addedUser.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectAdministrator_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var options = new AddUserOptions(newUserName, "hogehoge", newUserName, newUserName + "@example.com", UserRoleType.NormalUser);
            var newUser = backlog.AddUser(options);

            Assert.NotEqual(0, newUser.Id);

            var addedUser = backlog.AddProjectUser(projectKey, newUser.Id);
            Assert.Equal(newUser.Id, addedUser.Id);

            var addedAdmin = backlog.AddProjectAdministrator(projectKey, addedUser.Id);
            Assert.Equal(addedAdmin.Id, addedUser.Id);

            var actual = backlog.DeleteProjectAdministrator(projectKey, addedAdmin.Id);
            Assert.Equal(addedAdmin.Id, actual.Id);
            Assert.Equal(addedAdmin.UserId, actual.UserId);
            Assert.Equal(addedAdmin.Name, actual.Name);
            Assert.Equal(addedAdmin.RoleType, actual.RoleType);
            Assert.Equal(addedAdmin.Lang, actual.Lang);
            Assert.Equal(addedAdmin.MailAddress, actual.MailAddress);

            backlog.DeleteUser(addedUser.Id);
        }

        #endregion

        #region project/issueType

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIssueTypesTest()
        {
            // [{"id":116113,"projectId":26476,"name":"タスク","color":"#7ea800","displayOrder":0},
            //  {"id":116112,"projectId":26476,"name":"バグ","color":"#990000","displayOrder":1},
            //  {"id":116114,"projectId":26476,"name":"要望","color":"#ff9200","displayOrder":2},
            //  {"id":116115,"projectId":26476,"name":"その他","color":"#2779ca","displayOrder":3}]
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectIssueTypes(projectKey);
            Assert.True(actual.Count >= 1);
            var taskIssue = actual.First(i => i.DisplayOrder == 0);
            Assert.Equal(26476, taskIssue.ProjectId);
            Assert.Equal("タスク", taskIssue.Name);
            Assert.Equal("#7ea800", taskIssue.Color);
            Assert.Equal(0, taskIssue.DisplayOrder);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIssueTypes_with_key_Test()
        {
            // [{"id":116113,"projectId":26476,"name":"タスク","color":"#7ea800","displayOrder":0},
            //  {"id":116112,"projectId":26476,"name":"バグ","color":"#990000","displayOrder":1},
            //  {"id":116114,"projectId":26476,"name":"要望","color":"#ff9200","displayOrder":2},
            //  {"id":116115,"projectId":26476,"name":"その他","color":"#2779ca","displayOrder":3}]
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectIssueTypes(projectKey);
            Assert.True(actual.Count >= 1);
            var taskIssue = actual.First(i => i.DisplayOrder == 0);
            Assert.Equal(26476, taskIssue.ProjectId);
            Assert.Equal("タスク", taskIssue.Name);
            Assert.Equal("#7ea800", taskIssue.Color);
            Assert.Equal(0, taskIssue.DisplayOrder);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectIssueTypeTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var colors = new[]
            {
                IssueTypeColor.Color1,
                IssueTypeColor.Color2,
                IssueTypeColor.Color3,
                IssueTypeColor.Color4,
                IssueTypeColor.Color5,
                IssueTypeColor.Color6,
                IssueTypeColor.Color7,
                IssueTypeColor.Color8,
                IssueTypeColor.Color9,
                IssueTypeColor.Color10
            };
            var name = string.Format("is.{0}", new Random().Next(2000));
            var color = colors[new Random().Next(colors.Length - 1)];
            var options = new AddProjectIssueTypeOptions(name, color);

            var actual = backlog.AddProjectIssueType(projectId, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(options.TypeColor.ColorCode, actual.Color);
            Assert.Equal(options.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectIssueType_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var colors = new[]
            {
                IssueTypeColor.Color1,
                IssueTypeColor.Color2,
                IssueTypeColor.Color3,
                IssueTypeColor.Color4,
                IssueTypeColor.Color5,
                IssueTypeColor.Color6,
                IssueTypeColor.Color7,
                IssueTypeColor.Color8,
                IssueTypeColor.Color9,
                IssueTypeColor.Color10
            };
            var name = string.Format("is.{0}", new Random().Next(2000));
            var color = colors[new Random().Next(colors.Length - 1)];
            var options = new AddProjectIssueTypeOptions(name, color);

            var actual = backlog.AddProjectIssueType(projectKey, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(options.TypeColor.ColorCode, actual.Color);
            Assert.Equal(options.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectIssueTypeTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectId = backlog.GetProjects()[0].Id;
            var colors = new[]
            {
                IssueTypeColor.Color1,
                IssueTypeColor.Color2,
                IssueTypeColor.Color3,
                IssueTypeColor.Color4,
                IssueTypeColor.Color5,
                IssueTypeColor.Color6,
                IssueTypeColor.Color7,
                IssueTypeColor.Color8,
                IssueTypeColor.Color9,
                IssueTypeColor.Color10
            };
            var name = string.Format("is.{0}", new Random().Next(2000));
            var color = colors[new Random().Next(colors.Length - 1)];
            var options = new AddProjectIssueTypeOptions(name, color);

            var added = backlog.AddProjectIssueType(projectId, options);
            Assert.True(added.Id > 0);
            var change = new UpdateProjectIssueTypeOptions
            {
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };

            var actual = backlog.UpdateProjectIssueType(projectId, added.Id, change);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);

            Assert.Equal(change.Color.ColorCode, actual.Color);
            Assert.Equal(change.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectIssueType_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var colors = new[]
            {
                IssueTypeColor.Color1,
                IssueTypeColor.Color2,
                IssueTypeColor.Color3,
                IssueTypeColor.Color4,
                IssueTypeColor.Color5,
                IssueTypeColor.Color6,
                IssueTypeColor.Color7,
                IssueTypeColor.Color8,
                IssueTypeColor.Color9,
                IssueTypeColor.Color10
            };
            var name = string.Format("is.{0}", new Random().Next(2000));
            var color = colors[new Random().Next(colors.Length - 1)];
            var options = new AddProjectIssueTypeOptions(name, color);

            var added = backlog.AddProjectIssueType(projectKey, options);
            Assert.True(added.Id > 0);
            var change = new UpdateProjectIssueTypeOptions
            {
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };

            var actual = backlog.UpdateProjectIssueType(projectKey, added.Id, change);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);

            Assert.Equal(change.Color.ColorCode, actual.Color);
            Assert.Equal(change.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectIssueTypeTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var name = string.Format("is.{0}", new Random().Next(2000));
            var options = new AddProjectIssueTypeOptions(name, IssueTypeColor.Color10);
            var substIssueType = backlog.GetProjectIssueTypes(projectId)[0];

            var added = backlog.AddProjectIssueType(projectId, options);
            Assert.True(added.Id > 0);

            Assert.NotEqual(0, added.Id);

            var actual = backlog.DeleteProjectIssueType(projectId, added.Id, substIssueType.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Color, actual.Color);
            Assert.Equal(added.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectIssueType_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var name = string.Format("is.{0}", new Random().Next(2000));
            var options = new AddProjectIssueTypeOptions(name, IssueTypeColor.Color10);
            var substIssueType = backlog.GetProjectIssueTypes(projectKey)[0];

            var added = backlog.AddProjectIssueType(projectKey, options);
            Assert.True(added.Id > 0);

            Assert.NotEqual(0, added.Id);

            var actual = backlog.DeleteProjectIssueType(projectKey, added.Id, substIssueType.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Color, actual.Color);
            Assert.Equal(added.Name, actual.Name);
        }

        #endregion

        #region project/category

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectCategoriesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectCategories(projectId);
            Assert.True(actual.Count >= 1);

            // [{"id":61309,"name":"API","displayOrder":2147483646}]
            var apiCat = actual.OrderBy(p => p.Id).First();
            Assert.Equal(61309, apiCat.Id);
            Assert.Equal(2147483646, apiCat.DisplayOrder);
            Assert.Equal("API", apiCat.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectCategories_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectCategories(projectKey);
            Assert.True(actual.Count >= 1);

            // [{"id":61309,"name":"API","displayOrder":2147483646}]
            var apiCat = actual.OrderBy(p => p.Id).First();
            Assert.Equal(61309, apiCat.Id);
            Assert.Equal(2147483646, apiCat.DisplayOrder);
            Assert.Equal("API", apiCat.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCategoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var options = new AddProjectCategoryOptions(name);
            var actual = backlog.AddProjectCategory(projectId, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCategory_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var options = new AddProjectCategoryOptions(name);
            var actual = backlog.AddProjectCategory(projectKey, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCategoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var cat = new AddProjectCategoryOptions(name);
            var added = backlog.AddProjectCategory(projectId, cat);
            Assert.True(added.Id > 0);
            Assert.Equal(cat.Name, added.Name);

            var newCat = new UpdateProjectCategoryOptions { Name = added.Name + "1" };
            var actual = backlog.UpdateProjectCategory(projectId, added.Id, newCat);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(newCat.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCategory_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var cat = new AddProjectCategoryOptions(name);
            var added = backlog.AddProjectCategory(projectKey, cat);
            Assert.True(added.Id > 0);
            Assert.Equal(cat.Name, added.Name);

            var newCat = new UpdateProjectCategoryOptions { Name = added.Name + "1" };
            var actual = backlog.UpdateProjectCategory(projectKey, added.Id, newCat);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(newCat.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCategoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var options = new AddProjectCategoryOptions(name);

            var added = backlog.AddProjectCategory(projectId, options);
            Assert.True(added.Id > 0);
            Assert.Equal(options.Name, added.Name);

            var actual = backlog.DeleteProjectCategory(projectId, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCategory_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var name = string.Format("cat.{0}", new Random().Next(2000));
            var options = new AddProjectCategoryOptions(name);

            var added = backlog.AddProjectCategory(projectKey, options);
            Assert.True(added.Id > 0);
            Assert.Equal(options.Name, added.Name);

            var actual = backlog.DeleteProjectCategory(projectKey, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Name, actual.Name);
        }

        #endregion

        #region project/version

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectVersionsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectVersions(projectId);
            Assert.True(actual.Count >= 1);

            // [{"id":33856,
            //   "projectId":26476,
            //   "name":"1.0.0",
            //   "description":"initial release version",
            //   "startDate":"2015-04-01T00:00:00Z",
            //   "releaseDueDate":"2015-04-30T00:00:00Z",
            //   "archived":false,"displayOrder":2147483646}]
            var first = actual.OrderBy(p => p.Id).First();
            Assert.Equal(33856, first.Id);
            Assert.Equal(26476, first.ProjectId);
            Assert.Equal(2147483646, first.DisplayOrder);
            Assert.Equal("1.0.0", first.Name);
            Assert.Equal("initial release version", first.Description);
            Assert.Equal(new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc), first.StartDate);
            Assert.Equal(new DateTime(2015, 4, 30, 0, 0, 0, DateTimeKind.Utc), first.ReleaseDueDate);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectVersions_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectVersions(projectKey);
            Assert.True(actual.Count >= 1);

            // [{"id":33856,
            //   "projectId":26476,
            //   "name":"1.0.0",
            //   "description":"initial release version",
            //   "startDate":"2015-04-01T00:00:00Z",
            //   "releaseDueDate":"2015-04-30T00:00:00Z",
            //   "archived":false,"displayOrder":2147483646}]
            var first = actual.OrderBy(p => p.Id).First();
            Assert.Equal(33856, first.Id);
            Assert.Equal(26476, first.ProjectId);
            Assert.Equal(2147483646, first.DisplayOrder);
            Assert.Equal("1.0.0", first.Name);
            Assert.Equal("initial release version", first.Description);
            Assert.Equal(new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc), first.StartDate);
            Assert.Equal(new DateTime(2015, 4, 30, 0, 0, 0, DateTimeKind.Utc), first.ReleaseDueDate);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectVersionTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var name = string.Format("v.{0}", new Random().Next(10000));
            var startDate = DateTime.UtcNow.Date;
            var newVersionOptions = new AddProjectVersionOptions(name) { StartDate = DateTime.UtcNow.Date };
            var actual = backlog.AddProjectVersion(projectId, newVersionOptions);
            Assert.True(actual.Id > 0);
            Assert.True(actual.ProjectId > 0);
            Assert.Equal(-1, actual.DisplayOrder);
            Assert.Null(actual.Description);
            Assert.Equal(name, actual.Name);
            Assert.Equal(startDate, actual.StartDate);
            Assert.Null(actual.ReleaseDueDate);
            Assert.False(actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectVersion_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var name = string.Format("v.{0}", new Random().Next(10000));
            var startDate = DateTime.UtcNow.Date;
            var newVersionOptions = new AddProjectVersionOptions(name) { StartDate = DateTime.UtcNow.Date };
            var actual = backlog.AddProjectVersion(projectKey, newVersionOptions);
            Assert.True(actual.Id > 0);
            Assert.True(actual.ProjectId > 0);
            Assert.Equal(-1, actual.DisplayOrder);
            Assert.Null(actual.Description);
            Assert.Equal(name, actual.Name);
            Assert.Equal(startDate, actual.StartDate);
            Assert.Null(actual.ReleaseDueDate);
            Assert.False(actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectVersionTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var name = string.Format("v.{0}", new Random().Next(10000));
            var startDate = DateTime.UtcNow.Date;
            var newVersionOptions = new AddProjectVersionOptions(name) { StartDate = startDate };
            var added = backlog.AddProjectVersion(projectId, newVersionOptions);
            Assert.True(added.Id > 0);
            var newDueDate = startDate.AddDays(50);
            var newDesc = string.Format("desc.{0}", DateTime.Now);
            var options = new UpdateProjectVersionOptions(added.Name)
            {
                Description = newDesc,
                ReleaseDueDate = newDueDate
            };
            var actual = backlog.UpdateProjectVersion(projectId, added.Id, options);
            Assert.Equal(added.Id, actual.Id); // unchange
            Assert.Equal(added.Name, actual.Name); // unchange
            Assert.Equal(added.StartDate, actual.StartDate); // unchange
            Assert.Equal(newDesc, actual.Description); // update
            Assert.True(actual.ReleaseDueDate.HasValue);
            Assert.Equal(newDueDate.Date, actual.ReleaseDueDate.Value.Date); // update
            Assert.False(actual.Archived); // unchage
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectVersion_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var name = string.Format("v.{0}", new Random().Next(10000));
            var startDate = DateTime.UtcNow.Date;
            var newVersionOptions = new AddProjectVersionOptions(name) { StartDate = startDate };
            var added = backlog.AddProjectVersion(projectKey, newVersionOptions);
            Assert.True(added.Id > 0);
            var newDueDate = startDate.AddDays(50);
            var newDesc = string.Format("desc.{0}", DateTime.Now);
            var options = new UpdateProjectVersionOptions(added.Name)
            {
                Description = newDesc,
                ReleaseDueDate = newDueDate
            };
            var actual = backlog.UpdateProjectVersion(projectKey, added.Id, options);
            Assert.Equal(added.Id, actual.Id); // unchange
            Assert.Equal(added.Name, actual.Name); // unchange
            Assert.Equal(added.StartDate, actual.StartDate); // unchange
            Assert.Equal(newDesc, actual.Description); // update
            Assert.True(actual.ReleaseDueDate.HasValue);
            Assert.Equal(newDueDate.Date, actual.ReleaseDueDate.Value.Date); // update
            Assert.False(actual.Archived); // unchage
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectVersionTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var options = new AddProjectVersionOptions(string.Format("v.{0}", new Random().Next(10000)))
            {
                StartDate = DateTime.UtcNow
            };
            var added = backlog.AddProjectVersion(projectId, options);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteProjectVersion(projectId, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.StartDate, actual.StartDate);
            Assert.Equal(added.Description, actual.Description);
            Assert.Equal(added.Archived, actual.Archived);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectVersion_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var options = new AddProjectVersionOptions(string.Format("v.{0}", new Random().Next(10000)))
            {
                StartDate = DateTime.UtcNow
            };
            var added = backlog.AddProjectVersion(projectKey, options);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteProjectVersion(projectKey, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.StartDate, actual.StartDate);
            Assert.Equal(added.Description, actual.Description);
            Assert.Equal(added.Archived, actual.Archived);
        }

        #endregion

        #region project/customField

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectCustomFieldsTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectCustomFields_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCustomFieldsTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCustomFields_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCustomFieldTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCustomField_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCustomFieldTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCustomField_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCustomFieldListItemTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectCustomFieldListItem_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCustomFieldListItemTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCustomFieldListItem_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCustomFieldListItemTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCustomFieldListItem_with_key_Test()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support custom field"); // TODO:
        }

        #endregion

        #region project/files

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectSharedFilesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectSharedFiles(projectId);
            Assert.True(actual.Count > 0);

            // [{"id":2585041,"type":"directory","dir":"/","name":"dir1","size":null,
            //   "createdUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "created":"2015-04-20T00:10:21Z",
            //   "updatedUser":null,
            //   "updated":"2015-04-20T00:10:21Z"},
            //  {"id":2314867,"type":"file","dir":"/","name":"0s.jpg","size":14948,
            //   "createdUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "created":"2015-03-26T03:41:39Z",
            //   "updatedUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "updated":"2015-03-26T08:59:26Z"}]
            Assert.True(actual.All(f => f.Dir == "/"));
            Assert.Equal("directory", actual[0].Type);
            Assert.Equal("dir1", actual[0].Name);
            Assert.Equal(0, actual[0].Size);
            Assert.Equal("file", actual[1].Type);
            Assert.Equal("0s.jpg", actual[1].Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectSharedFiles_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectSharedFiles(projectKey);
            Assert.True(actual.Count > 0);

            // [{"id":2585041,"type":"directory","dir":"/","name":"dir1","size":null,
            //   "createdUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "created":"2015-04-20T00:10:21Z",
            //   "updatedUser":null,
            //   "updated":"2015-04-20T00:10:21Z"},
            //  {"id":2314867,"type":"file","dir":"/","name":"0s.jpg","size":14948,
            //   "createdUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "created":"2015-03-26T03:41:39Z",
            //   "updatedUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //   "updated":"2015-03-26T08:59:26Z"}]
            Assert.True(actual.All(f => f.Dir == "/"));
            Assert.Equal("directory", actual[0].Type);
            Assert.Equal("dir1", actual[0].Name);
            Assert.Equal(0, actual[0].Size);
            Assert.Equal("file", actual[1].Type);
            Assert.Equal("0s.jpg", actual[1].Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectSharedFileTest()
        {
            // Resources.projectIcon => /dir1/26476.png, id=2585042
            // TODO: verify content
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectSharedFile(projectId, 2585042); // XXX
            Assert.NotNull(actual.Content);
            Assert.Equal("26476.png", actual.FileName);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectSharedFile_with_key_Test()
        {
            // Resources.projectIcon => /dir1/26476.png, id=2585042
            // TODO: verify content
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectSharedFile(projectKey, 2585042); // XXX
            Assert.NotNull(actual.Content);
            Assert.Equal("26476.png", actual.FileName);
        }

        #endregion

        #region project/webhook

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectWebHooksTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetProjectWebHooks(projectId);

            Assert.True(actual.Count > 0);
            var wh1 = actual.OrderBy(w => w.Id).FirstOrDefault();
            Assert.NotNull(wh1);
            Assert.True(wh1.Id > 0);
            Assert.Equal("wh1", wh1.Name);
            Assert.False(wh1.AllEvent);
            Assert.Equal(new[] { 13 }, wh1.ActivityTypeIds.ToArray()); // 13 : git repository created
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectWebHooks_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetProjectWebHooks(projectKey);

            Assert.True(actual.Count > 0);
            var wh1 = actual.OrderBy(w => w.Id).FirstOrDefault();
            Assert.NotNull(wh1);
            Assert.True(wh1.Id > 0);
            Assert.Equal("wh1", wh1.Name);
            Assert.False(wh1.AllEvent);
            Assert.Equal(new[] { 13 }, wh1.ActivityTypeIds.ToArray()); // 13 : git repository created
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectWebHookTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));

            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var actual = backlog.AddProjectWebHook(projectId, wh);

            Assert.True(actual.Id > 0);
            Assert.Equal(name, actual.Name);
            Assert.Equal(desc, actual.Description);
            Assert.False(actual.AllEvent);
            Assert.Equal(types.Select(i => (int)i).OrderBy(_ => _), actual.ActivityTypeIds.OrderBy(_ => _));
            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectWebHook_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));

            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var actual = backlog.AddProjectWebHook(projectKey, wh);

            Assert.True(actual.Id > 0);
            Assert.Equal(name, actual.Name);
            Assert.Equal(desc, actual.Description);
            Assert.False(actual.AllEvent);
            Assert.Equal(types.Select(i => (int)i).OrderBy(_ => _), actual.ActivityTypeIds.OrderBy(_ => _));
            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectWebHookTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var hooks = backlog.GetProjectWebHooks(projectId);

            Assert.True(hooks.Count > 0);
            var wh1 = hooks.OrderBy(w => w.Id).FirstOrDefault();

            Assert.NotNull(wh1);
            Assert.True(wh1.Id > 0);

            var actual = backlog.GetProjectWebHook(projectId, wh1.Id);
            Assert.Equal(wh1.Id, actual.Id);
            Assert.Equal(wh1.Name, actual.Name);
            Assert.False(actual.AllEvent);
            Assert.Equal(new[] { 13 }, actual.ActivityTypeIds.ToArray()); // 13 : git repository created
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectWebHook_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var hooks = backlog.GetProjectWebHooks(projectKey);

            Assert.True(hooks.Count > 0);
            var wh1 = hooks.OrderBy(w => w.Id).FirstOrDefault();

            Assert.NotNull(wh1);
            Assert.True(wh1.Id > 0);

            var actual = backlog.GetProjectWebHook(projectKey, wh1.Id);
            Assert.Equal(wh1.Id, actual.Id);
            Assert.Equal(wh1.Name, actual.Name);
            Assert.False(actual.AllEvent);
            Assert.Equal(new[] { 13 }, actual.ActivityTypeIds.ToArray()); // 13 : git repository created
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectWebHookTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            // add new hook
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));
            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var added = backlog.AddProjectWebHook(projectId, wh);
            Assert.True(added.Id > 0);

            var update = new UpdateWebHookOptions
            {
                AllEvent = true,
                Description = "<>" + added.Description,
                Name = "<>" + added.Name
            };

            var actual = backlog.UpdateProjectWebHook(projectId, added.Id, update);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(update.Name, actual.Name);
            Assert.Equal(update.Description, actual.Description);
            Assert.True(actual.AllEvent);

            // XXX: API Server does not clear ActivityTypeIds when AllEvent has changed to true
            //// Assert.Equal(2, added.ActivityTypeIds.Count);

            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectWebHook_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            // add new hook
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));
            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var added = backlog.AddProjectWebHook(projectKey, wh);
            Assert.True(added.Id > 0);

            var update = new UpdateWebHookOptions
            {
                AllEvent = true,
                Description = "<>" + added.Description,
                Name = "<>" + added.Name
            };

            var actual = backlog.UpdateProjectWebHook(projectKey, added.Id, update);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(update.Name, actual.Name);
            Assert.Equal(update.Description, actual.Description);
            Assert.True(actual.AllEvent);

            // XXX: API Server does not clear ActivityTypeIds when AllEvent has changed to true
            //// Assert.Equal(2, added.ActivityTypeIds.Count);

            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectWebHookTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));
            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var added = backlog.AddProjectWebHook(projectId, wh);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteProjectWebHook(projectId, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.Description, actual.Description);

            Assert.Equal(added.AllEvent, actual.AllEvent);

            Assert.Equal(added.ActivityTypeIds, added.ActivityTypeIds);

            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectWebHook_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var name = string.Format("wh.{0}", new Random().Next(1000));
            var desc = string.Format("test.{0}", DateTime.UtcNow);
            var hookUrl = string.Format("http://example.test/{0}/", new Random().Next(1000));
            var wh = new AddWebHookOptions(name)
            {
                Description = desc,
                HookUrl = hookUrl,
                AllEvent = false
            };

            var types = new[] { ActivityType.CommentNotificationAdded, ActivityType.FileAdded };
            wh.AddActivityTypes(types);
            var added = backlog.AddProjectWebHook(projectKey, wh);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteProjectWebHook(projectKey, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.Description, actual.Description);

            Assert.Equal(added.AllEvent, actual.AllEvent);

            Assert.Equal(added.ActivityTypeIds, added.ActivityTypeIds);

            Assert.True(actual.CreatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Created.ToUniversalTime().Date);
            Assert.True(actual.UpdatedUser.Id > 0);
            Assert.Equal(DateTime.UtcNow.Date, actual.Updated.ToUniversalTime().Date);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoriesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var actual = backlog.GetProjectGitRepositories(projectId);
            Assert.True(actual.Count > 0);
            Assert.Equal(projectId, actual[0].ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositories_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects()[0];
            Assert.True(project.Id > 0);
            var actual = backlog.GetProjectGitRepositories(project.ProjectKey);
            Assert.True(actual.Count > 0);
            Assert.Equal(project.Id, actual[0].ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var repos = backlog.GetProjectGitRepositories(projectId);
            Assert.True(repos.Count > 0);
            var actual = backlog.GetProjectGitRepository(projectId, repos[0].Id);
            Assert.Equal(repos[0].Id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepository_with_name_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var repos = backlog.GetProjectGitRepositories(projectId);
            Assert.True(repos.Count > 0);
            var actual = backlog.GetProjectGitRepository(projectId, repos[0].Name);
            Assert.Equal(repos[0].Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepository_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects()[0];
            Assert.True(project.Id > 0);
            var repos = backlog.GetProjectGitRepositories(project.ProjectKey);
            Assert.True(repos.Count > 0);
            var actual = backlog.GetProjectGitRepository(project.ProjectKey, repos[0].Id);
            Assert.Equal(repos[0].Id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepository_with_name_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects()[0];
            Assert.True(project.Id > 0);
            var repos = backlog.GetProjectGitRepositories(project.ProjectKey);
            Assert.True(repos.Count > 0);
            var actual = backlog.GetProjectGitRepository(project.ProjectKey, repos[0].Name);
            Assert.Equal(repos[0].Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryPullRequestsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var actual = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(actual.Count > 0);
            var pr = actual[0];
            //// Assert.Equal(1, pr.Id);
            Assert.Equal(projectId, pr.ProjectId);
            Assert.Equal(repoId, pr.RepositoryId);
            Assert.Equal(1, pr.Number);
            Assert.Equal("develop", pr.Summary);
            Assert.Equal("update readme", pr.Description);
            Assert.Equal("master", pr.Base);
            Assert.Equal("develop", pr.Branch);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryPullRequestsCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var actual = backlog.GetProjectGitRepositoryPullRequestsCount(projectId, repoId);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectGitRepositoryPullRequestTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            string sumary = $"sumary.{new Random().Next()}";
            string desc = $"desc.{new Random().Next()}";
            var opt = new AddPullRequestOptions(sumary, desc, "master", "develop");
            var actual = backlog.AddProjectGitRepositoryPullRequest(projectId, repoId, opt);
            Assert.Equal(sumary, actual.Summary);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryPullRequestTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var prs = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(prs.Count > 0);
            var number = prs[0].Number;
            var actual = backlog.GetProjectGitRepositoryPullRequest(projectId, repoId, number);
            Assert.Equal(projectId, actual.ProjectId);
            Assert.Equal(repoId, actual.RepositoryId);
            Assert.Equal(number, actual.Number);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectGitRepositoryPullRequestTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var prs = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(prs.Count > 1);
            var number = prs.First(pr => pr.Number != 1).Number;
            var r = new Random();
            var summary = $"summary.{r.Next()}";
            var desc = $"desc.{r.Next()}";
            var param = new UpdatePullRequestOptions { Summary = summary, Description = desc };
            var actual = backlog.UpdateProjectGitRepositoryPullRequest(projectId, repoId, number, param);
            Assert.Equal(projectId, actual.ProjectId);
            Assert.Equal(repoId, actual.RepositoryId);
            Assert.Equal(number, actual.Number);
            Assert.Equal(summary, actual.Summary);
            Assert.Equal(desc, actual.Description);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryPullRequestCommentsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var prs = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(prs.Count > 0);
            var number = prs.First().Number;
            var actual = backlog.GetProjectGitRepositoryPullRequestComments(projectId, repoId, number);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectGitRepositoryPullRequestCommentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var prs = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(prs.Count > 0);
            var number = prs.First().Number;
            var r = new Random();
            var comment = $"comment.{r.Next()}";
            var param = new AddPullRequestCommentOptions(comment);
            foreach (var uid in backlog.GetProjectUsers(projectId).Select(u => u.Id))
            {
                param.AddNotifiedUserId(uid);
            }

            var actual = backlog.AddProjectGitRepositoryPullRequestComment(projectId, repoId, number, param);
            Assert.Equal(comment, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectGitRepositoryPullRequestCommentsCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var number = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId)[0].Number;
            var actual = backlog.GetProjectGitRepositoryPullRequestCommentsCount(projectId, repoId, number);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectGitRepositoryPullRequestCommentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var repoId = backlog.GetProjectGitRepositories(projectId)[0].Id;
            var prs = backlog.GetProjectGitRepositoryPullRequests(projectId, repoId);
            Assert.True(prs.Count > 0);
            var number = prs.First().Number;
            var param = new AddPullRequestCommentOptions("First comment");
            var comment = backlog.AddProjectGitRepositoryPullRequestComment(projectId, repoId, number, param);
            Assert.True(comment.Id > 0);
            var secondComment = "Second comment";
            var actual = backlog.UpdateProjectGitRepositoryPullRequestComment(projectId, repoId, number, comment.Id, secondComment);
            Assert.Equal(secondComment, actual.Content);
        }

        #endregion

        #endregion

        #region /api/v2/issues

        #region issues

        /// <inheritdoc/>
        [Fact]
        public override void GetIssuesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var condition = new IssueSearchConditions();
            var actual = backlog.GetIssues(new[] { projectId }, condition);
            Assert.True(actual.Count > 0);
            Assert.Equal(projectId, actual[0].ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssuesCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            var condition = new IssueSearchConditions();
            var actual = backlog.GetIssuesCount(new[] { projectId }, condition);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var issueTypeId = backlog.GetProjectIssueTypes(projectId.ToString())[0].Id;
            Assert.True(issueTypeId > 0);
            var priorityId = backlog.GetPriorities()[0].Id;
            Assert.True(priorityId > 0);
            var sumary = string.Format("test issue.{0:R}", DateTime.Now);
            var settings = new NewIssueSettings(projectId, issueTypeId, priorityId, sumary);

            var actual = backlog.AddIssue(settings);
            Assert.NotNull(actual);
            Assert.True(actual.Id > 0);
            Assert.Equal(projectId, actual.ProjectId);
            Assert.Equal(issueTypeId, actual.IssueType.Id);
            Assert.Equal(priorityId, actual.Priority.Id);
            Assert.Equal(sumary, actual.Summary);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[new Random().Next(issues.Count - 1)].Id;
            var actual = backlog.GetIssue(issueId);
            Assert.NotNull(actual);
            Assert.Equal(issueId, actual.Id);
            Assert.Equal(projectId, actual.ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssue_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[new Random().Next(issues.Count - 1)].IssueKey;
            var actual = backlog.GetIssue(issueKey);
            Assert.NotNull(actual);
            Assert.Equal(issueKey, actual.IssueKey);
            Assert.Equal(projectId, actual.ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateIssueTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issue = issues[new Random().Next(issues.Count - 1)];

            var issueTypeId = issue.IssueType.Id;
            var priorityId = issue.Priority.Id;
            var startDate = DateTime.Now;
            var dueDate = DateTime.Now.AddDays(20);
            var description = issue.Description + "\nupdated " + DateTime.Now.ToString("F");
            var updateSettings = new IssueUpdateSettings(issueTypeId, priorityId)
            {
                StartDate = startDate,
                DueDate = dueDate,
                Description = description
            };

            var actual = backlog.UpdateIssue(issue.Id, updateSettings);
            Assert.Equal(issue.Id, actual.Id);
            Assert.Equal(issue.Summary, actual.Summary);
            Assert.Equal(issue.IssueType.Id, actual.IssueType.Id);
            Assert.Equal(issue.Priority.Id, actual.Priority.Id);
            Assert.Equal(issue.ParentIssueId, actual.ParentIssueId);

            // free plan can not set StartDate
            //// Assert.NotNull(actual.StartDate);
            //// Assert.Equal(startDate.ToString(Backlog.DateFormat), actual.StartDate.Value.ToString(Backlog.DateFormat));
            Assert.NotNull(actual.DueDate);
            Assert.Equal(dueDate.ToString(Backlog.DateFormat), actual.DueDate.Value.ToString(Backlog.DateFormat));
            Assert.NotNull(actual.Description);
            Assert.Equal(description, actual.Description);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateIssue_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issue = issues[new Random().Next(issues.Count - 1)];

            var issueTypeId = issue.IssueType.Id;
            var priorityId = issue.Priority.Id;
            var startDate = DateTime.Now;
            var dueDate = DateTime.Now.AddDays(20);
            var description = issue.Description + "\nupdated " + DateTime.Now.ToString("F");
            var updateSettings = new IssueUpdateSettings(issueTypeId, priorityId)
            {
                StartDate = startDate,
                DueDate = dueDate,
                Description = description
            };

            var actual = backlog.UpdateIssue(issue.IssueKey, updateSettings);
            Assert.Equal(issue.Id, actual.Id);
            Assert.Equal(issue.IssueKey, actual.IssueKey);
            Assert.Equal(issue.Summary, actual.Summary);
            Assert.Equal(issue.IssueType.Id, actual.IssueType.Id);
            Assert.Equal(issue.Priority.Id, actual.Priority.Id);
            Assert.Equal(issue.ParentIssueId, actual.ParentIssueId);

            // free plan can not set StartDate
            //// Assert.NotNull(actual.StartDate);
            //// Assert.Equal(startDate.ToString(Backlog.DateFormat), actual.StartDate.Value.ToString(Backlog.DateFormat));
            Assert.NotNull(actual.DueDate);
            Assert.Equal(dueDate.ToString(Backlog.DateFormat), actual.DueDate.Value.ToString(Backlog.DateFormat));
            Assert.NotNull(actual.Description);
            Assert.Equal(description, actual.Description);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteIssueTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var issueTypeId = backlog.GetProjectIssueTypes(projectId.ToString())[0].Id;
            Assert.True(issueTypeId > 0);
            var priorityId = backlog.GetPriorities()[0].Id;
            Assert.True(priorityId > 0);
            var sumary = string.Format("test issue.{0:R}", DateTime.Now);
            var settings = new NewIssueSettings(projectId, issueTypeId, priorityId, sumary);

            var added = backlog.AddIssue(settings);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteIssue(added.Id);
            Assert.Equal(added.Id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteIssue_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var issueTypeId = backlog.GetProjectIssueTypes(projectId.ToString())[0].Id;
            Assert.True(issueTypeId > 0);
            var priorityId = backlog.GetPriorities()[0].Id;
            Assert.True(priorityId > 0);
            var sumary = string.Format("test issue.{0:R}", DateTime.Now);
            var settings = new NewIssueSettings(projectId, issueTypeId, priorityId, sumary);

            var added = backlog.AddIssue(settings);
            Assert.True(added.Id > 0);

            var actual = backlog.DeleteIssue(added.IssueKey);
            Assert.Equal(added.IssueKey, actual.IssueKey);
        }

        #endregion

        #region issue/comment

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var actual = backlog.GetIssueComments(issueId);
            Assert.True(actual.Count > 0);
            var comment = actual[0];
            Assert.True(comment.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueComments_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var actual = backlog.GetIssueComments(issueKey);
            Assert.True(actual.Count > 0);
            var comment = actual[0];
            Assert.True(comment.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueComments_with_filter_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            backlog.AddIssueComment(issueId, new CommentAddContent(DateTime.Now.ToLongDateString()));
            backlog.AddIssueComment(issueId, new CommentAddContent(DateTime.Now.ToLongDateString()));
            backlog.AddIssueComment(issueId, new CommentAddContent(DateTime.Now.ToLongDateString()));
            var filter = new ResultPagingOptions { Count = 2 };
            var actual = backlog.GetIssueComments(issueId, filter);
            Assert.Equal(filter.Count, actual.Count);
            var comment = actual[0];
            Assert.True(comment.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueCommentTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var content = string.Format("content.{0}", DateTime.Now);
            var options = new CommentAddContent(content);
            var users = backlog.GetProjectUsers(projectId.ToString());
            options.NotifiedUserIds.AddRange(users.Select(u => u.Id));
            var actual = backlog.AddIssueComment(issueId, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueComment_with_key_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var content = string.Format("content.{0}", DateTime.Now);
            var options = new CommentAddContent(content);
            var users = backlog.GetProjectUsers(projectId.ToString());
            options.NotifiedUserIds.AddRange(users.Select(u => u.Id));
            var actual = backlog.AddIssueComment(issueKey, options);
            Assert.True(actual.Id > 0);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentCountTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var com = new CommentAddContent("GetIssueCommentCount");
            var added = backlog.AddIssueComment(issueId, com);
            Assert.True(added.Id > 0);
            var actual = backlog.GetIssueCommentCount(issueId);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentCount_with_key_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var com = new CommentAddContent("GetIssueCommentCount_with_key_Test");
            var added = backlog.AddIssueComment(issueKey, com);
            Assert.True(added.Id > 0);
            var actual = backlog.GetIssueCommentCount(issueKey);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var comments = backlog.GetIssueComments(issueId);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var actual = backlog.GetIssueComment(issueId, commentId);
            Assert.Equal(commentId, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentTest_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var comments = backlog.GetIssueComments(issueKey);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var actual = backlog.GetIssueComment(issueKey, commentId);
            Assert.Equal(commentId, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateIssueCommentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var comments = backlog.GetIssueComments(issueId);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var content = string.Format("new comment.{0}", DateTime.Now);
            var actual = backlog.UpdateIssueComment(issueId, commentId, content);
            Assert.Equal(commentId, actual.Id);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateIssueComment_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var comments = backlog.GetIssueComments(issueKey);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var content = string.Format("new comment.{0}", DateTime.Now);
            var actual = backlog.UpdateIssueComment(issueKey, commentId, content);
            Assert.Equal(commentId, actual.Id);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentNotificationsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var content = new CommentAddContent("GetIssueCommentNotificationsTest");
            backlog.AddIssueComment(issueId, content);
            var comments = backlog.GetIssueComments(issueId);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var userIds = backlog.GetProjectUsers(projectId.ToString()).Where(_ => _.Id != comments[0].CreatedUser.Id).Select(_ => _.Id).ToList();
            var added = backlog.AddIssueCommentNotification(issueId, commentId, userIds);
            Assert.True(added.Id > 0);
            var actual = backlog.GetIssueCommentNotifications(issueId, commentId);
            Assert.True(actual.Count > 0);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueCommentNotifications_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var content = new CommentAddContent("GetIssueCommentNotificationsTest");
            backlog.AddIssueComment(issueKey, content);
            var comments = backlog.GetIssueComments(issueKey);
            Assert.True(comments.Count > 0);
            var commentId = comments[0].Id;
            var userIds = backlog.GetProjectUsers(projectId.ToString()).Where(_ => _.Id != comments[0].CreatedUser.Id).Select(_ => _.Id).ToList();
            var added = backlog.AddIssueCommentNotification(issueKey, commentId, userIds);
            Assert.True(added.Id > 0);
            var actual = backlog.GetIssueCommentNotifications(issueKey, commentId);
            Assert.True(actual.Count > 0);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueCommentNotificationTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[0].Id;
            var added = backlog.AddIssueComment(issueId, new CommentAddContent(string.Format("new comment.{0}", DateTime.Now)));
            Assert.True(added.Id > 0);
            var commentId = added.Id;
            var userIds = backlog.GetProjectUsers(projectId.ToString()).Where(_ => _.Id != added.CreatedUser.Id).Select(_ => _.Id).ToList();
            var actual = backlog.AddIssueCommentNotification(issueId, commentId, userIds);
            Assert.Equal(commentId, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueCommentNotification_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueKey = issues[0].IssueKey;
            var added = backlog.AddIssueComment(issueKey, new CommentAddContent(string.Format("new comment.{0}", DateTime.Now)));
            Assert.True(added.Id > 0);
            var commentId = added.Id;
            var userIds = backlog.GetProjectUsers(projectId.ToString()).Where(_ => _.Id != added.CreatedUser.Id).Select(_ => _.Id).ToList();
            var actual = backlog.AddIssueCommentNotification(issueKey, commentId, userIds);
            Assert.Equal(commentId, actual.Id);
        }

        #endregion

        #region issue/attachment

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueAttachmentsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueId = 992829; // BL4N-2
            var actual = backlog.GetIssueAttachments(issueId);
            Assert.True(actual.Count > 0);
            var attachment = actual[0];
            Assert.True(attachment.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueAttachments_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueKey = "BL4N-2"; // var issueId = 992829;
            var actual = backlog.GetIssueAttachments(issueKey);
            Assert.True(actual.Count > 0);
            var attachment = actual[0];
            Assert.True(attachment.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueAttachmentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueId = 992829; // BL4N-2
            var attachments = backlog.GetIssueAttachments(issueId);
            Assert.True(attachments.Count > 0);
            var attachment = attachments[0];
            Assert.True(attachment.Id > 0);
            var actual = backlog.GetIssueAttachment(issueId, attachment.Id);
            Assert.Equal("2013-07-20-kyotosuizokukan2.jpeg", actual.FileName);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueAttachment_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueKey = "BL4N-2"; // 992829;
            var attachments = backlog.GetIssueAttachments(issueKey);
            Assert.True(attachments.Count > 0);
            var attachment = attachments[0];
            Assert.True(attachment.Id > 0);
            var actual = backlog.GetIssueAttachment(issueKey, attachment.Id);
            Assert.Equal("2013-07-20-kyotosuizokukan2.jpeg", actual.FileName);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteIssueAttachmentTest()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            long attachmentId = 0;
            using (var ms = new MemoryStream())
            {
                var bmp = Resources.logo;
                bmp.Save(ms, bmp.RawFormat);
                ms.Position = 0;
                var added = backlog.AddAttachment(string.Format("logo.{0}.png", new Random().Next(1000)), ms);
                attachmentId = added.Id;
                Assert.True(attachmentId > 0);
            }

            var projectId = backlog.GetProjects()[0].Id;
            var issueIds = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issueIds.Any());
            var issueId = issueIds[0].Id;
            var content = new CommentAddContent("attachment test");
            content.AttachmentIds.Add(attachmentId);
            var comment = backlog.AddIssueComment(issueId, content);
            Assert.True(comment.Id > 0);
            var changlog = comment.ChangeLog.FirstOrDefault(c => c.AttachmentInfo != null && c.AttachmentInfo.Id > 0);
            Assert.NotNull(changlog);
            var id = changlog.AttachmentInfo.Id;
            var actual = backlog.DeleteIssueAttachment(issueId, id);
            Assert.Equal(id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteIssueAttachment_with_key_Test()
        {
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            long attachmentId;
            using (var ms = new MemoryStream())
            {
                var bmp = Resources.logo;
                bmp.Save(ms, bmp.RawFormat);
                ms.Position = 0;
                var added = backlog.AddAttachment(string.Format("logo.{0}.png", new Random().Next(1000)), ms);
                attachmentId = added.Id;
                Assert.True(attachmentId > 0);
            }

            var projectId = backlog.GetProjects()[0].Id;
            var issueIds = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issueIds.Any());
            var issueKey = issueIds[0].IssueKey;
            var content = new CommentAddContent("attachment test");
            content.AttachmentIds.Add(attachmentId);
            var comment = backlog.AddIssueComment(issueKey, content);
            Assert.True(comment.Id > 0);
            var changlog = comment.ChangeLog.FirstOrDefault(c => c.AttachmentInfo != null && c.AttachmentInfo.Id > 0);
            Assert.NotNull(changlog);
            var id = changlog.AttachmentInfo.Id;
            var actual = backlog.DeleteIssueAttachment(issueKey, id);
            Assert.Equal(id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueLinkedSharedFilesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueId = 1157540; // BL4N-6
            var actual = backlog.GetIssueLinkedSharedFiles(issueId);
            Assert.True(actual.Count > 0);
            var sharedFile = actual[0];
            Assert.True(sharedFile.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetIssueLinkedSharedFiles_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var issueKey = "BL4N-6"; // 1157540;
            var actual = backlog.GetIssueLinkedSharedFiles(issueKey);
            Assert.True(actual.Count > 0);
            var sharedFile = actual[0];
            Assert.True(sharedFile.Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueLinkedSharedFilesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var fileId = backlog.GetProjectSharedFiles(projectId.ToString()).Where(s => s.Type == "file").Select(s => s.Id).First();
            var types = backlog.GetProjectIssueTypes(projectId.ToString());
            var priorityes = backlog.GetPriorities();
            var newIssue = new NewIssueSettings(projectId, types[0].Id, priorityes[0].Id, "shared file test");
            var issue = backlog.AddIssue(newIssue);
            Assert.True(issue.Id > 0);
            var actual = backlog.AddIssueLinkedSharedFiles(issue.Id, new[] { fileId });
            Assert.Equal(1, actual.Count);
            var sharedFile = actual[0];
            Assert.Equal(sharedFile.Id, fileId);
            backlog.DeleteIssue(issue.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddIssueLinkedSharedFiles_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var fileId = backlog.GetProjectSharedFiles(projectId.ToString()).Where(s => s.Type == "file").Select(s => s.Id).First();
            var types = backlog.GetProjectIssueTypes(projectId.ToString());
            var priorityes = backlog.GetPriorities();
            var newIssue = new NewIssueSettings(projectId, types[0].Id, priorityes[0].Id, "add shared file test with key");
            var issue = backlog.AddIssue(newIssue);
            Assert.True(issue.Id > 0);
            var actual = backlog.AddIssueLinkedSharedFiles(issue.IssueKey, new[] { fileId });
            Assert.Equal(1, actual.Count);
            var sharedFile = actual[0];
            Assert.Equal(sharedFile.Id, fileId);
            backlog.DeleteIssue(issue.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void RemoveIssueLinkedSharedFileTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            // Link Shared Files to Issue API (POST /api/v2/issues/:issueIdOrKey/sharedFiles) is broken?
            // linkSharedFile(dir) return success, but no file link
            var projectId = backlog.GetProjects()[0].Id;
            long fileId = backlog.GetProjectSharedFiles(projectId.ToString()).Where(s => s.Type == "file").Select(s => s.Id).First();
            var types = backlog.GetProjectIssueTypes(projectId.ToString());
            var priorityes = backlog.GetPriorities();
            var newIssue = new NewIssueSettings(projectId, types[0].Id, priorityes[0].Id, "remove shared file test");
            var issue = backlog.AddIssue(newIssue);
            Assert.True(issue.Id > 0);
            var issueId = issue.Id;
            var added = backlog.AddIssueLinkedSharedFiles(issueId, new[] { fileId });
            Assert.Equal(1, added.Count);
            var sharedFile = added[0];
            Assert.Equal(sharedFile.Id, fileId);
            var sharedFileId = sharedFile.Id; // long sharedFileId = 2585042; // /dir1/26476.png:2585042
            var actual = backlog.RemoveIssueLinkedSharedFile(issueId, sharedFileId);
            Assert.Equal(sharedFileId, actual.Id);

            backlog.DeleteIssue(issue.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void RemoveIssueLinkedSharedFile_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;

            // Link Shared Files to Issue API (POST /api/v2/issues/:issueIdOrKey/sharedFiles) is broken?
            // linkSharedFile(dir) return success, but no file link
            var file = backlog.GetProjectSharedFiles(projectId.ToString()).Where(s => s.Type == "file").Select(s => s.Id).First();
            var types = backlog.GetProjectIssueTypes(projectId.ToString());
            var priorities = backlog.GetPriorities();
            var newIssue = new NewIssueSettings(projectId, types[0].Id, priorities[0].Id, "remove shared file with key test");
            var issue = backlog.AddIssue(newIssue);
            Assert.True(issue.Id > 0);
            var added = backlog.AddIssueLinkedSharedFiles(issue.IssueKey, new[] { file });
            Assert.Equal(1, added.Count);
            var sharedFile = added[0];
            var actual = backlog.RemoveIssueLinkedSharedFile(issue.IssueKey, sharedFile.Id);
            Assert.Equal(sharedFile.Id, actual.Id);

            backlog.DeleteIssue(issue.IssueKey);
        }

        #endregion

        #endregion

        #region /api/v2/wikis

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPagesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetWikiPages(projectId);
            Assert.True(actual.Count > 0);
            Assert.Equal(projectId, actual[0].ProjectId);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPages_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetWikiPages(projectKey);
            Assert.True(actual.Count > 0);
            //// Assert.Equal(projectKey, actual[0].ProjectId);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPagesCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var pages = backlog.GetWikiPages(projectId);
            Assert.True(pages.Count > 0);
            var actual = backlog.GetWikiPagesCount(projectId);
            Assert.Equal(pages.Count, actual.Count);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPagesCount_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var pages = backlog.GetWikiPages(projectKey);
            Assert.True(pages.Count > 0);
            var actual = backlog.GetWikiPagesCount(projectKey);
            Assert.Equal(pages.Count, actual.Count);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageTagsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var actual = backlog.GetWikiPageTags(projectId);
            Assert.True(actual.Count > 0);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageTags_with_key_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var actual = backlog.GetWikiPageTags(projectKey);
            Assert.True(actual.Count > 0);
            Assert.True(actual[0].Id > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddWikiPageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var r = new Random();
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("name.{0}", DateTime.Now);
            var content = string.Format("content.{0}", DateTime.UtcNow);
            var mailNotify = r.Next() % 2 == 0;
            var addWikiPageOptions = new AddWikiPageOptions("[xunit] " + name, content, mailNotify);
            var actual = backlog.AddWikiPage(projectId, addWikiPageOptions);
            Assert.Equal(projectId, actual.ProjectId);
            Assert.Equal(name, actual.Name);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var pages = backlog.GetWikiPages(projectId);
            var r = new Random();
            var wikiId = pages[r.Next(pages.Count - 1)].Id;
            var actual = backlog.GetWikiPage(wikiId);
            Assert.Equal(wikiId, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateWikiPageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var pages = backlog.GetWikiPages(projectId);
            var r = new Random();
            var wikiId = pages[r.Next(pages.Count - 1)].Id;
            var name = string.Format("name.{0}", DateTime.Now);
            var content = string.Format("content.{0}", DateTime.UtcNow);
            var mailNotify = r.Next() % 2 == 0;
            var updateOptions = new UpdateWikiPageOptions { Name = "[xunit] " + name, Content = content, Notify = mailNotify };
            var actual = backlog.UpdateWikiPage(wikiId, updateOptions);
            Assert.True(actual.Id > 0); // XXX: wikiId != updated.Id ???
            Assert.Equal(name, actual.Name);
            Assert.Equal(content, actual.Content);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteWikiPageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var r = new Random();
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("name.{0}", DateTime.Now);
            var content = string.Format("content.{0}", DateTime.UtcNow);
            var mailNotify = r.Next() % 2 == 0;
            var addWikiPageOptions = new AddWikiPageOptions("[delete] " + name, content, mailNotify);
            var added = backlog.AddWikiPage(projectId, addWikiPageOptions);
            Assert.Equal(projectId, added.ProjectId);
            Assert.Equal(name, added.Name);
            Assert.Equal(content, added.Content);
            var actual = backlog.DeleteWikiPage(added.Id, mailNotify);
            Assert.Equal(added.Id, actual.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageAttachmentsTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support wiki page attachment"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddWikiPageAttachmentsTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support wiki page attachment"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageAttachmentTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support wiki page attachment"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void RemoveWikiPageAttachmentTest()
        {
            SkipIfSettingIsBroken();
            Assert.True(true, "Free plan does not support wiki page attachment"); // TODO:
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageSharedFilesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            long wikiId = 67261;
            var actual = backlog.GetWikiPageSharedFiles(wikiId);
            Assert.Equal(1, actual.Count);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddWikiPageSharedFilesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("AddWikiPageSharedFilesTest.{0}", DateTime.Now.Ticks);
            var options = new AddWikiPageOptions(name, "AddWikiPageSharedFilesTest");
            var added = backlog.AddWikiPage(projectId, options);
            Assert.True(added.Id > 0);

            var sharedFiles = backlog.GetProjectSharedFiles(projectId);
            Assert.True(sharedFiles.Count > 0);
            var fileId = sharedFiles.First(s => s.Type == "file").Id;
            var actual = backlog.AddWikiPageSharedFiles(added.Id, new[] { fileId });
            Assert.Equal(1, actual.Count);
            Assert.Equal(fileId, actual[0].Id);
            backlog.DeleteWikiPage(added.Id, false);
        }

        /// <inheritdoc/>
        [Fact]
        public override void RemoveWikiPageSharedFileTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            // Link Shared Files to WikiPage API (POST /api/v2/wikis/:wikiId/sharedFiles) is broken?
            var projectId = backlog.GetProjects()[0].Id;
            var name = string.Format("AddWikiPageSharedFilesTest.{0}", DateTime.Now.Ticks);
            var options = new AddWikiPageOptions(name, "AddWikiPageSharedFilesTest");
            var wiki = backlog.AddWikiPage(projectId, options);
            Assert.True(wiki.Id > 0);

            var sharedFiles = backlog.GetProjectSharedFiles(projectId);
            Assert.True(sharedFiles.Count > 0);
            var fileId = sharedFiles.First(s => s.Type == "file").Id;
            var files = backlog.AddWikiPageSharedFiles(wiki.Id, new[] { fileId });
            Assert.Equal(1, files.Count);

            // link shared file manualy
            long wikiId = wiki.Id; // Home:67261; //
            long sharedFileId = files[0].Id; // /dir1/26476.png:2585042
            var actual = backlog.RemoveWikiPageSharedFile(wikiId, sharedFileId);
            Assert.Equal(sharedFileId, actual.Id);

            ////backlog.DeleteIssue(added.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageHistoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var wikiPages = backlog.GetWikiPages(projectId);
            Assert.True(wikiPages.Count > 0);
            var pageId = wikiPages[0].Id;
            var actual = backlog.GetWikiPageHistory(pageId);
            Assert.True(actual.Count > 0);
            Assert.Equal(pageId, actual[0].PageId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageHistory_with_filter_Test()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var wikiPages = backlog.GetWikiPages(projectId);
            Assert.True(wikiPages.Count > 0);
            var pageId = wikiPages[0].Id;
            var filter = new ResultPagingOptions { Count = 2 };
            var actual = backlog.GetWikiPageHistory(pageId, filter);
            Assert.True(actual.Count > 0);
            Assert.True(actual.Count <= 2);
            Assert.Equal(pageId, actual[0].PageId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetWikiPageStarsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var wikiPages = backlog.GetWikiPages(projectId);
            Assert.True(wikiPages.Count > 0);
            var pageId = wikiPages.First(w => w.Name == "Stars").Id; // 80122
            var actual = backlog.GetWikiPageStars(pageId);
            Assert.True(actual.Count > 0);
            Assert.Equal("https://bl4n.backlog.jp/alias/wiki/80122", actual[0].Url);
        }

        #endregion

        #region /api/v2/stars

        /// <inheritdoc/>
        [Fact]
        public override void AddStarToIssueTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[new Random().Next() % issues.Count].Id;
            backlog.AddStarToIssue(issueId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddStarToCommentTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var issues = backlog.GetIssues(new[] { projectId }, new IssueSearchConditions());
            Assert.True(issues.Count > 0);
            var issueId = issues[new Random().Next() % issues.Count].Id;
            var comment = backlog.AddIssueComment(issueId, new CommentAddContent("star test"));
            Assert.True(comment.Id > 0);
            backlog.AddStarToIssue(comment.Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddStarToWikiPageTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            var pages = backlog.GetWikiPages(projectId);
            Assert.True(pages.Count > 0);
            var pageId = pages[(int)(DateTime.Now.Ticks % pages.Count)].Id;
            backlog.AddStarToWikiPage(pageId);
        }

        #endregion

        #region /api/v2/notifications

        /// <inheritdoc/>
        [Fact]
        public override void GetNotificationsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var notifications = backlog.GetNotifications();
            Assert.True(notifications.Count > 0);
            Assert.NotNull(notifications[0]);
            Assert.NotNull(notifications[0].Issue);
            Assert.NotNull(notifications[0].Project);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetNotificationsCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var optinos = new NotificationsCountOptions();
            var actual = backlog.GetNotificationsCount(optinos);
            Assert.True(actual.Count > 0);
        }

        /// <inheritdoc/>
        [Fact]
        public override void ResetUnreadNotificationCountTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.ResetUnreadNotificationCount();
            Assert.True(actual.Count > 0); // XXX: ???
        }

        /// <inheritdoc/>
        [Fact]
        public override void ReadNotificationTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var notifications = backlog.GetNotifications();
            var notification = notifications.FirstOrDefault(n => !n.AlreadyRead);
            var nid = (notification == null) ? notifications[0].Id : notification.Id;
            backlog.ReadNotification(nid);
        }

        #endregion

        #region /api/v2/git/repositories

        /// <inheritdoc/>
        [Fact]
        public override void GetGitRepositoriesTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectId = backlog.GetProjects()[0].Id;
            Assert.True(projectId > 0);
            var actual = backlog.GetGitRepositories(projectId);
            Assert.True(actual.Count > 0);
            Assert.Equal(projectId, actual[0].ProjectId);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetGitRepositories_with_key_Test()
        {
            var backlog = new Backlog(Settings);
            var project = backlog.GetProjects()[0];
            Assert.True(project.Id > 0);
            var actual = backlog.GetGitRepositories(project.ProjectKey);
            Assert.True(actual.Count > 0);
            Assert.Equal(project.Id, actual[0].ProjectId);
        }

        #endregion

        #region error handling

        /// <inheritdoc/>
        [Fact(Skip = "for mock server only")]
        public override void InternalErrorResponseTest()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [Fact(Skip = "for mock server only")]
        public override void LicenceErrorResponseTest()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}