// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForRealServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using BL4N.Data;
using BL4N.Tests.Properties;
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

        [Fact]
        public override void BacklogConstructorTest()
        {
            SkipIfSettingIsBroken();

            var realClient = new Backlog(Settings);
            Assert.Equal(Settings.SpaceName, realClient.SpaceName);
        }

        #region /api/v2/space

        [Fact]
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

            var ser = new DataContractJsonSerializer(typeof(Space), new DataContractJsonSerializerSettings { DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ") });
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(RealResult)))
            {
                var expected = (Space)ser.ReadObject(ms);
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
            // Assert.Equal(new DateTime(2015, 3, 26, 6, 37, 37, DateTimeKind.Utc), actual.Updated);
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
            // Assert.Equal(new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc), actual.Updated);
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
            // TODO: 冪等性
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var name = string.Format("bl4n.{0}", DateTime.Now.Ticks);
            var user = new User
            {
                UserId = name,
                Name = name,
                Lang = null,
                MailAddress = string.Format("t.ashula+{0}@{1}", name, "gmail.com"),
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
            // TODO: 冪等性
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var users = backlog.GetUsers();
            var oldUser = users.FirstOrDefault(u => u.RoleType == 6);
            if (oldUser == null)
            {
                Assert.False(true, "no user to test");
                return;
            }

            // var oldName = oldUser.Name;
            var newName = string.Format("bl4n.{0}", DateTime.Now.Ticks);

            var newUser = new User
            {
                Id = oldUser.Id,
                Lang = oldUser.Lang,
                Name = newName, // new name
                MailAddress = oldUser.MailAddress,
                RoleType = oldUser.RoleType,
                UserId = oldUser.UserId
            };

            var changed = backlog.UpdateUser(newUser);
            Assert.Equal(newName, changed.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteUserTest()
        {
            // TODO: 冪等性
            SkipIfSettingIsBroken();
            var backlog = new Backlog(Settings);
            var users = backlog.GetUsers();
            var del = users.FirstOrDefault(u => (u.RoleType != 1 && u.RoleType != 2));
            if (del == null)
            {
                Assert.False(true, "no user to test");
                return;
            }

            var deleted = backlog.DeleteUser(del.Id);
            Assert.Equal(del.Id, deleted.Id);
        }

        /// <inhertidoc/>
        [Fact]
        public override void GetOwnUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var myself = backlog.GetOwnUser();
            // {"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"}
            var dummySelf = new
            {
                id = 60965,
                userId = "bl4n.admin",
                name = "bl4n.admin",
                roleType = 1,
                // lang = null,
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
        public override void GetReceivedStarListTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var actual = backlog.GetReceivedStarList(uid);
            Assert.True(actual.Count > 0);
            Assert.Equal(null, actual[0].Comment);
            Assert.StartsWith("https://bl4n.backlog.jp/view/BL4N", actual[0].Url);
            Assert.StartsWith("[BL4N-", actual[0].Title);
            Assert.Equal(60965, actual[0].Presenter.Id);
            // Assert.Equal(new DateTime(2015, 4, 6, 5, 52, 46, DateTimeKind.Utc), actual[0].Created);
        }

        /// <inheritdoc/>
        [Fact]
        public override void CountUserReceivedStarsTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var uid = backlog.GetOwnUser().Id;
            var actual = backlog.CountUserReceivedStars(uid);
            Assert.Equal(2, actual.Count);
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
        public override void GetListOfRecentlyViewedWikisTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var actual = backlog.GetListOfRecentlyViewedWikis();

            Assert.InRange(actual.Count, 1, 20);
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
        public override void AddGroupTest()
        {
            // TODO: 冪等性
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var users = backlog.GetUsers().Select(u => u.Id).OrderBy(_ => _).ToArray();
            var name = string.Format("g.{0}", DateTime.Now.Ticks);

            var actual = backlog.AddGroup(name, users);
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

            var actual = backlog.GetGroup(3377);
            // {"id":3377,
            //  "name":"g1",
            //  "members":[{"id":60966,"userId":"t.ashula","name":"t.ashula","roleType":2,"lang":null,"mailAddress":"t.ashula@gmail.com"}],
            //  "displayOrder":-1,
            //  "createdUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //  "created":"2015-04-07T11:58:54Z",
            //  "updatedUser":{"id":60965,"userId":"bl4n.admin","name":"bl4n.admin","roleType":1,"lang":null,"mailAddress":"t.ashula+nulab@gmail.com"},
            //  "updated":"2015-04-07T11:58:54Z"}
            Assert.Equal(3377, actual.Id);
            Assert.StartsWith("g1", actual.Name);
            Assert.Equal(1, actual.Members.Count);
            Assert.Equal(60966, actual.Members[0].Id);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var g1 = backlog.GetGroups().FirstOrDefault(g => g.Name.StartsWith("g1"));
            if (g1 == null)
            {
                Assert.False(false, "group g1.x not found?");
                return;
            }

            var newName = string.Format("g1.{0:00000}", DateTime.Now.Ticks % 100000); // group.name.length must be less than 20
            var actual = backlog.UpdateGroup(g1.Id, newName, new long[] { });
            Assert.Equal(g1.Id, actual.Id);
            Assert.Equal(newName, actual.Name);
            Assert.Equal(g1.Members.Count, actual.Members.Count);
            Assert.Equal(g1.Created, actual.Created);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteGroupTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            // TODO: 冪等性
            var added = backlog.AddGroup("delete", new long[] { });
            var actual = backlog.DeleteGroup(added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.Name, actual.Name);
            Assert.Equal(added.Members.Count, actual.Members.Count);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.CreatedUser.Id, actual.CreatedUser.Id);
            // Assert.Equal(added.Created, actual.Created); // ???
            // Assert.Equal(1, actual.UpdatedUser.Id);
            // Assert.Equal(new DateTime(2013, 05, 30, 09, 11, 36, DateTimeKind.Utc), actual.Updated);
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
            var np = new Project
            {
                Name = "newproject",
                ProjectKey = "NEWPRO",
                ChartEnabled = false,
                SubtaskingEnabled = false,
                TextFormattingRule = "markdown"
            };
            var actual = backlog.AddProject(np);
            // プランの都合上プロジェクトの追加ができないのでテスト不可
            // Assert.Equal("newproject", actual.Name);
            Assert.True(true, "cant add new project on free plan.");
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

            var actual = backlog.GetProject(project.Id.ToString());
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
            var newProject = new Project
            {
                Id = current.Id,
                Name = current.Name + (new Random().Next() % 100),
                ProjectKey = current.ProjectKey,
                ChartEnabled = current.ChartEnabled,
                SubtaskingEnabled = current.SubtaskingEnabled,
                TextFormattingRule = current.TextFormattingRule,
                Archived = current.Archived
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
            // プランの都合上プロジェクトの追加ができないのでテスト不可
            // Assert.Equal("newproject", actual.Name);

            Assert.True(true, "cant del project on free plan.");
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectIconTest()
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
            var projectKey = backlog.GetProjects()[0].ProjectKey;

            var actual = backlog.GetProjectRecentUpdates(projectKey);

            Assert.InRange(actual.Count, 1, 20);
            Assert.Equal(projectKey, actual[0].Project.ProjectKey);
        }

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectUserTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var newUserInfo = new User
            {
                UserId = newUserName,
                Lang = "ja",
                Name = newUserName,
                MailAddress = newUserName + "@example.com",
                RoleType = 5, // guest reporter
            };
            var newUser = backlog.AddUser(newUserInfo, "hogehogehogehoge"); // TODO:
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
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var newUserInfo = new User
            {
                UserId = newUserName,
                Lang = "ja",
                Name = newUserName,
                MailAddress = newUserName + "@example.com",
                RoleType = 5, // guest reporter
            };
            var newUser = backlog.AddUser(newUserInfo, "hogehogehogehoge"); // TODO:
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

        /// <inheritdoc/>
        [Fact]
        public override void AddProjectAdministorTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var newUserInfo = new User
            {
                UserId = newUserName,
                Lang = "ja",
                Name = newUserName,
                MailAddress = newUserName + "@example.com",
                RoleType = 2 // Normal User
            };
            var newUser = backlog.AddUser(newUserInfo, "hogehogehogehoge"); // TODO: password
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
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var r = new Random();
            var newUserName = string.Format("g.{0:X}", r.Next());
            var newUserInfo = new User
            {
                UserId = newUserName,
                Lang = "ja",
                Name = newUserName,
                MailAddress = newUserName + "@example.com",
                RoleType = 2 // Normal User
            };

            var newUser = backlog.AddUser(newUserInfo, "hogehogehogehoge"); // TODO: password
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
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var colors = new[] { "#e30000", "#990000", "#934981", "#814fbc", "#2779ca", "#007e9a", "#7ea800", "#ff9200", "#ff3265", "#666665" };
            var issueType = new IssueType
            {
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };

            var actual = backlog.AddProjectIssueType(projectKey, issueType);
            Assert.True(actual.Id > 0);
            Assert.Equal(issueType.Color, actual.Color);
            Assert.Equal(issueType.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectIssueTypeTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var colors = new[] { "#e30000", "#990000", "#934981", "#814fbc", "#2779ca", "#007e9a", "#7ea800", "#ff9200", "#ff3265", "#666665" };
            var issueType = new IssueType
            {
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };
            var added = backlog.AddProjectIssueType(projectKey, issueType);
            Assert.True(added.Id > 0);
            var change = new IssueType
            {
                Id = added.Id,
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };

            var actual = backlog.UpdateProjectIssueType(projectKey, change);
            Assert.Equal(added.Id, change.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);

            Assert.Equal(change.Color, actual.Color);
            Assert.Equal(change.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectIssueTypeTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);

            var colors = new[] { "#e30000", "#990000", "#934981", "#814fbc", "#2779ca", "#007e9a", "#7ea800", "#ff9200", "#ff3265", "#666665" };
            var issueType = new IssueType
            {
                Color = colors[new Random().Next(colors.Length - 1)],
                Name = string.Format("is.{0}", new Random().Next(2000))
            };
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var substIssueType = backlog.GetProjectIssueTypes(projectKey)[0];
            var added = backlog.AddProjectIssueType(projectKey, issueType);
            Assert.NotEqual(0, added.Id);

            var actual = backlog.DeleteProjectIssueType(projectKey, added.Id, substIssueType.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.ProjectId, actual.ProjectId);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Color, actual.Color);
            Assert.Equal(added.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void GetProjectCategoriesTest()
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
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var cat = new Category
            {
                Name = string.Format("cat.{0}", new Random().Next(2000))
            };

            var actual = backlog.AddProjectCategory(projectKey, cat);
            Assert.True(actual.Id > 0);
            Assert.Equal(cat.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void UpdateProjectCategoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var cat = new Category
            {
                Name = string.Format("cat.{0}", new Random().Next(2000))
            };

            var added = backlog.AddProjectCategory(projectKey, cat);
            Assert.True(added.Id > 0);
            Assert.Equal(cat.Name, added.Name);
            var newCat = new Category
            {
                Id = added.Id,
                Name = added.Name + "1"
            };
            var actual = backlog.UpdateProjectCategory(projectKey, newCat);
            Assert.Equal(newCat.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(newCat.Name, actual.Name);
        }

        /// <inheritdoc/>
        [Fact]
        public override void DeleteProjectCategoryTest()
        {
            SkipIfSettingIsBroken();

            var backlog = new Backlog(Settings);
            var projectKey = backlog.GetProjects()[0].ProjectKey;
            var cat = new Category
            {
                Name = string.Format("cat.{0}", new Random().Next(2000))
            };

            var added = backlog.AddProjectCategory(projectKey, cat);
            Assert.True(added.Id > 0);
            Assert.Equal(cat.Name, added.Name);

            var actual = backlog.DeleteProjectCategory(projectKey, added.Id);
            Assert.Equal(added.Id, actual.Id);
            Assert.Equal(added.DisplayOrder, actual.DisplayOrder);
            Assert.Equal(added.Name, actual.Name);
        }

        #endregion
    }
}