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
    }
}