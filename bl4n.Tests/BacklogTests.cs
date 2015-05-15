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
    /// <summary>
    /// test base class
    /// </summary>
    public abstract class BacklogTests
    {
        private readonly BacklogConnectionSettings _settings;

        /// <summary>
        /// gets ConnectionSettings
        /// </summary>
        public BacklogConnectionSettings Settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// initialzie <see cref="BacklogTests"/> instance
        /// </summary>
        /// <param name="settings"></param>
        protected BacklogTests(BacklogConnectionSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// skip test fact when connection setting is not valid
        /// </summary>
        protected void SkipIfSettingIsBroken()
        {
            if (Settings == null || !Settings.IsValid())
            {
                Assert.True(false, "skip this test, real setting is not valid.");
            }
        }

        /// <summary>
        /// get api endpoint uri ( http://server:port/api/v2 )
        /// </summary>
        /// <returns>api end point base uri as string</returns>
        protected string GetApiEndPointBase()
        {
            return string.Format("http{0}://{1}/api/v2", _settings.UseSSL ? "s" : string.Empty, _settings.Host);
        }

        /// <summary> test for new Backlog()</summary>
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

        /// <summary> GET /api/v2/statuses のテスト </summary>
        public abstract void GetStatusesTest();

        #endregion

        #region /api/v2/resolutions

        /// <summary> GET /api/v2/resolutions のテスト </summary>
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

        /// <summary> GET /api/v2/projects/:projectIdOrKey/diskUsage </summary>
        public abstract void GetProjectDiskUsageTest();

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
        public abstract void UpdateProjectCustomFieldTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void DeleteProjectCustomFieldTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields/:id/items </summary>
        public abstract void AddProjectCustomFieldListItemTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void UpdateProjectCustomFieldListItemTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void DeleteProjectCustomFieldListItemTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/metadata/:path </summary>
        public abstract void GetProjectSharedFilesTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/:sharedFileId </summary>
        public abstract void GetProjectSharedFileTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void GetProjectWebHooksTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void AddProjectWebHookTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void GetProjectWebHookTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/webhooks/:id</summary>
        public abstract void UpdateProjectWebHookTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void DeleteProjectWebHookTest();

        #endregion

        #region /api/v2/issues

        /// <summary> GET /api/v2/issues </summary>
        public abstract void GetIssuesTest();

        /// <summary> GET /api/v2/issues/count </summary>
        public abstract void GetIssuesCountTest();

        /// <summary> POST /api/v2/issues </summary>
        public abstract void AddIssueTest();

        /// <summary> GET /api/v2/issues/:idOrKey </summary>
        public abstract void GetIssueTest();

        /// <summary> GET /api/v2/issues/:idOrKey </summary>
        public abstract void GetIssue_with_key_Test();

        /// <summary> PATCH /api/v2/issues/:idOrKey </summary>
        public abstract void UpdateIssueTest();

        /// <summary> PATCH /api/v2/issues/:idOrKey </summary>
        public abstract void UpdateIssue_with_key_Test();

        /// <summary> DELETE /api/v2/issues/:idOrKey </summary>
        public abstract void DeleteIssueTest();

        /// <summary> DELETE /api/v2/issues/:idOrKey </summary>
        public abstract void DeleteIssue_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void GetIssueCommentsTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void AddIssueCommentTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/count </summary>
        public abstract void GetIssueCommentCountTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void GetIssueCommentTest();

        /// <summary> PATCH /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void UpdateIssueCommentTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void GetIssueCommentNotificationsTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void AddIssueCommentNotificationTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments </summary>
        public abstract void GetIssueAttachmentsTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void GetIssueAttachmentTest();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void DeleteIssueAttachmentTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void GetIssueLinkedSharedFilesTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void AddIssueLinkedSharedFilesTest();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/sharedFiles/:id </summary>
        public abstract void RemoveIssueLinkedSharedFileTest();

        #endregion

        #region /api/v2/wikis

        /// <summary> GET /api/v2/wikis </summary>
        public abstract void GetWikiPagesTest();

        /// <summary> GET /api/v2/wikis/count </summary>
        public abstract void GetWikiPagesCountTest();

        /// <summary> GET /api/v2/wikis/tags </summary>
        public abstract void GetWikiPageTagsTest();

        /// <summary> POST /api/v2/wikis/tags </summary>
        public abstract void AddWikiPageTest();

        /// <summary> GET /api/v2/wikis/:pageId </summary>
        public abstract void GetWikiPageTest();

        /// <summary> PATCH /api/v2/wikis/:pageId </summary>
        public abstract void UpdateWikiPageTest();

        /// <summary> DELETE /api/v2/wikis/:pageId </summary>
        public abstract void DeleteWikiPageTest();

        /// <summary> GET /api/v2/wikis/:wikiId/attachments </summary>
        public abstract void GetWikiPageAttachmentsTest();

        /// <summary> POST /api/v2/wikis/:wikiId/attachments </summary>
        public abstract void AddWikiPageAttachmentsTest();

        /// <summary> GET /api/v2/wikis/:wikiId/attachments/:attachmentId </summary>
        public abstract void GetWikiPageAttachmentTest();

        /// <summary> DELETE /api/v2/wikis/:wikiId/attachments/:attachmentId </summary>
        public abstract void RemoveWikiPageAttachmentTest();

        /// <summary> GET /api/v2/wikis/:wikiId/sharedFiles </summary>
        public abstract void GetWikiPageSharedFilesTest();

        /// <summary> POST /api/v2/wikis/:wikiId/sharedFiles </summary>
        public abstract void AddWikiPageSharedFilesTest();

        /// <summary> DELETE /api/v2/wikis/:wikiId/sharedFiles/:id </summary>
        public abstract void RemoveWikiPageSharedFileTest();

        /// <summary> GET /api/v2/wikis/:wikiId/history </summary>
        public abstract void GetWikiPageHistoryTest();

        /// <summary> GET /api/v2/wikis/:wikiId/stars </summary>
        public abstract void GetWikiPageStarsTest();

        #endregion

        #region /api/v2/stars

        /// <summary> POST /api/v2/stars (issueId) </summary>
        public abstract void AddStarToIssueTest();

        /// <summary> POST /api/v2/stars (commentId) </summary>
        public abstract void AddStarToCommentTest();

        /// <summary> POST /api/v2/stars (wikiId) </summary>
        public abstract void AddStarToWikiPageTest();

        #endregion

        #region /api/v2/notifications

        /// <summary> GET /api/v2/notifications </summary>
        public abstract void GetNotificationsTest();

        /// <summary> GET /api/v2/notifications/count </summary>
        public abstract void GetNotificationsCountTest();

        /// <summary> POST /api/v2/notifications/markAsRead </summary>
        public abstract void ResetUnreadNotificationCountTest();

        /// <summary> POST /api/v2/notifications/:id/markAsRead </summary>
        public abstract void ReadNotificationTest();

        #endregion

        #region /api/v2/git

        /// <summary> GET /api/v2/git/repositories </summary>
        public abstract void GetGitRepositoriesTest();

        #endregion

        #region error handling

        /// <summary> test for errors:[{code:1}], </summary>
        /// <remarks> for Mock Only </remarks>
        public abstract void InternalErrorResponseTest();

        /// <summary> test for errors:[{code:2}], </summary>
        public abstract void LicenceErrorResponseTest();

        #endregion
    }
}