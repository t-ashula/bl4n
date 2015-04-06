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

        #region /api/v2/space

        /// <summary> GET /api/v2/space のテスト </summary>
        public abstract void GetSpaceTest();

        /// <summary> GET /api/v2/space/activities のテスト </summary>
        public abstract void GetSpaceActivitiesTest();

        /// <summary> GET /api/v2/space/image のテスト </summary>
        public abstract void GetSpaceLogoTest();

        /// <summary> GET /api/v2/space/notification のテスト </summary>
        public abstract void GetSpaceNotificationTest();

        /// <summary> PUT /api/v2/space/notification のテスト </summary>
        public abstract void UpdateSpaceNotificationTest();

        /// <summary> GET /api/v2/space/diskUsage のテスト </summary>
        public abstract void GetSpaceDiskUsageTest();

        /// <summary> POST /api/v2/space/attachment のテスト </summary>
        public abstract void AddAttachmentTest();

        #endregion

        /// <summary> GET /api/v2/users のテスト </summary>
        public abstract void GetUsersTest();

        /// <summary> GET /api/v2/users/:userId のテスト </summary>
        public abstract void GetUserTest();

        /// <summary> POST /api/v2/users のテスト </summary>
        public abstract void AddUserTest();

        /// <summary> PATCH /api/v2/users/:userId のテスト </summary>
        public abstract void UpdateUserTest();

        /// <summary> DELETE /api/v2/users/:userId のテスト </summary>
        public abstract void DeleteUserTest();

        /// <summary> GET /api/v2/users/myself のテスト </summary>
        public abstract void GetOwnUserTest();

        /// <summary> GET /api/v2/users/:userId/icon のテスト </summary>
        public abstract void GetUserIconTest();

        /// <summary> GET /api/v2/users/:userId/activities のテスト </summary>
        public abstract void GetUserRecentUpdatesTest();

        /// <summary> GET /api/v2/users/:userId/stars のテスト </summary>
        public abstract void GetReceivedStarListTest();
    }
}