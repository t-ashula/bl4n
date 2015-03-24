// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTests.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Xunit;

namespace BL4N.Tests
{
    public class BacklogTests
    {
        private readonly BacklogJPConnectionSettings _realSettings;
        private readonly BacklogConnectionSettings _mockSettings;

        public BacklogTests()
        {
            _realSettings = (BacklogJPConnectionSettings)BacklogConnectionSettings.Load("bl4n.json");
            _mockSettings = new BacklogConnectionSettings("mock", APIType.APIKey, "dummyapikey", "localhost", 34567, false);
        }

        [Fact]
        public void BacklogConstructorTest_ForRealSettings()
        {
            if (_realSettings == null || !_realSettings.IsValid())
            {
                Assert.True(true, "skip this test, real setting is not valid.");
                return;
            }

            var realClient = new Backlog(_realSettings);
            Assert.Equal(_realSettings.SpaceName, realClient.SpaceName);
        }

        [Fact]
        public void BacklogConstructorTest_ForMockSettings()
        {
            var mockClient = new Backlog(_mockSettings);
            Assert.Equal(_mockSettings.SpaceName, mockClient.SpaceName);
        }
    }
}