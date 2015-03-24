// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForRealServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Xunit;

namespace BL4N.Tests
{
    /// <summary> 実サーバを使ったテスト </summary>
    public class BacklogTestsForRealServer : BacklogTests
    {
        /// <summary> </summary>
        public BacklogTestsForRealServer()
            : base((BacklogJPConnectionSettings)BacklogConnectionSettings.Load("bl4n.json"))
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

            var backlog = new Backlog(Settings);
            var spaceInfo = backlog.GetSpace();
            Assert.Equal("bl4n", spaceInfo.SpaceKey);
        }
    }
}