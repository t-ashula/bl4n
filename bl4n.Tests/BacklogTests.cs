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

        #region /api/v2/users

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

        /// <summary> GET /api/v2/users/:userId/stars/count のテスト </summary>
        public abstract void CountUserReceivedStarsTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedIssues のテスト </summary>
        public abstract void GetListOfRecentlyViewedIssuesTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedProjects のテスト </summary>
        public abstract void GetListOfRecentlyViewedProjectsTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedWikis のテスト </summary>
        public abstract void GetListOfRecentlyViewedWikisTest();

        #endregion

        #region /api/v2/groups

        /// <summary> GET /api/v2/groups のテスト </summary>
        public abstract void GetGroupsTest();

        /// <summary> POST /api/v2/groups のテスト </summary>
        public abstract void AddGroupTest();

        /// <summary> GET /api/v2/groups/:groupId </summary>
        public abstract void GetGroupTest();

        /// <summary> PATCH /api/v2/groups/:groupId </summary>
        public abstract void UpdateGroupTest();

        /// <summary> DELETE /api/v2/groups/:groupId </summary>
        public abstract void DeleteGroupTest();

        #endregion

        #region /api/v2/statuses

        public abstract void GetStatusesTest();

        #endregion

        #region /api/v2/resolutions

        public abstract void GetResolutionsTest();

        #endregion

        #region /api/v2/priorities

        /// <summary> GET /api/v2/priorities </summary>
        public abstract void GetPrioritiesTest();

        #endregion

        #region /api/v2/projects

        /// <summary> GET /api/v2/projects </summary>
        public abstract void GetProjectsTest();

        /// <summary> GET /api/v2/projects </summary>
        public abstract void AddProjectTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey </summary>
        public abstract void GetProjectTest();

        /// <summary> PATCH /api/v2/project/:projectIdOrKey </summary>
        public abstract void UpdateProjectTest();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey </summary>
        public abstract void DeleteProjectTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/image </summary>
        public abstract void GetProjectIconTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/activities </summary>
        public abstract void GetProjectRecentUpdatesTest();

        /// <summary> POST /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void AddProjectUserTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void GetProjectUsersTest();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void DeleteProjectUserTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void AddProjectAdministorTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void GetProjectAdministratorsTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void DeleteProjectAdministratorTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void GetProjectIssueTypesTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void AddProjectIssueTypeTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void UpdateProjectIssueTypeTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void DeleteProjectIssueTypeTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void GetProjectCategoriesTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void AddProjectCategoryTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void UpdateProjectCategoryTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void DeleteProjectCategoryTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void GetProjectVersionsTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void AddProjectVersionTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void UpdateProjectVersionTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void DeleteProjectVersionTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void GetProjectCustomFieldsTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void AddProjectCustomFieldsTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void UpdateCustomFieldTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void DeleteCustomFieldTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields/:id/items </summary>
        public abstract void AddProjectCustomFieldListItemTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void UpdateProjectCustomFieldListItemTest();

        #endregion
    }
}