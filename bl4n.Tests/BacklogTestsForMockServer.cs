// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForMockServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
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
            Assert.Equal(4, activities.Count);
        }
    }
}