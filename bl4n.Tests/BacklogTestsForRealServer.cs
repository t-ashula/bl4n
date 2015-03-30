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
    }
}