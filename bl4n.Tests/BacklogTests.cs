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
    public abstract class BacklogTests
    {
        private readonly BacklogConnectionSettings _settings;

        public BacklogConnectionSettings Settings
        {
            get { return _settings; }
        }

        protected BacklogTests(BacklogConnectionSettings settings)
        {
            _settings = settings;
        }

        protected void SkipIfSettingIsBroken()
        {
            if (Settings == null || !Settings.IsValid())
            {
                Assert.True(false, "skip this test, real setting is not valid.");
            }
        }

        /// <summary> </summary>
        public abstract void BacklogConstructorTest();

        /// <summary> /api/v2/space のテスト </summary>
        public abstract void GetSpaceTest();

        /// <summary> /api/v2/space/activities のテスト </summary>
        public abstract void GetSpaceActivitiesTest();

        /// <summary> /api/v2/space/image のテスト </summary>
        public abstract void GetSpaceLogoTest();
    }
}