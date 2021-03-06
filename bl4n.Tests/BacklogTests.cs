// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTests.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using DynamicSkipExample;

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
                throw new SkipTestException("skip this test, real setting is not valid.");
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

        /// <summary> GET /api/v2/space/activities のテスト </summary>
        public abstract void GetSpaceActivities_with_filter_Test();

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

        /// <summary> GET /api/v2/users/:userId/activities のテスト </summary>
        public abstract void GetUserRecentUpdates_with_filter_Test();

        /// <summary> GET /api/v2/users/:userId/stars のテスト </summary>
        public abstract void GetReceivedStarListTest();

        /// <summary> GET /api/v2/users/:userId/stars のテスト </summary>
        public abstract void GetReceivedStarList_with_filter_Test();

        /// <summary> GET /api/v2/users/:userId/stars/count のテスト </summary>
        public abstract void CountUserReceivedStarsTest();

        /// <summary> GET /api/v2/users/:userId/stars/count のテスト </summary>
        public abstract void CountUserReceivedStars_with_term_Test();

        /// <summary> GET /api/v2/users/myself/recentlyViewedIssues のテスト </summary>
        public abstract void GetListOfRecentlyViewedIssuesTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedIssues のテスト </summary>
        public abstract void GetListOfRecentlyViewedIssues_with_offset_Test();

        /// <summary> GET /api/v2/users/myself/recentlyViewedProjects のテスト </summary>
        public abstract void GetListOfRecentlyViewedProjectsTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedProjects のテスト </summary>
        public abstract void GetListOfRecentlyViewedProjects_with_offset_Test();

        /// <summary> GET /api/v2/users/myself/recentlyViewedWikis のテスト </summary>
        public abstract void GetListOfRecentlyViewedWikisTest();

        /// <summary> GET /api/v2/users/myself/recentlyViewedWikis のテスト </summary>
        public abstract void GetListOfRecentlyViewedWikis_with_offset_Test();

        #endregion

        #region /api/v2/groups

        /// <summary> GET /api/v2/groups のテスト </summary>
        public abstract void GetGroupsTest();

        /// <summary> GET /api/v2/groups のテスト </summary>
        public abstract void GetGroups_with_offset_Test();

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

        /// <summary> GET /api/v2/projects?archived </summary>
        public abstract void GetProjects_archived_Test();

        /// <summary> GET /api/v2/projects?all </summary>
        public abstract void GetProjects_all_Test();

        /// <summary> GET /api/v2/projects </summary>
        public abstract void AddProjectTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey </summary>
        public abstract void GetProjectTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey </summary>
        public abstract void GetProject_with_key_Test();

        /// <summary> PATCH /api/v2/project/:projectIdOrKey </summary>
        public abstract void UpdateProjectTest();

        /// <summary> PATCH /api/v2/project/:projectIdOrKey </summary>
        public abstract void UpdateProject_with_key_Test();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey </summary>
        public abstract void DeleteProjectTest();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey </summary>
        public abstract void DeleteProject_with_key_Test();

        /// <summary> GET /api/v2/project/:projectIdOrKey/image </summary>
        public abstract void GetProjectIconTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/image </summary>
        public abstract void GetProjectIcon_with_key_Test();

        /// <summary> GET /api/v2/project/:projectIdOrKey/activities </summary>
        public abstract void GetProjectRecentUpdatesTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/activities </summary>
        public abstract void GetProjectRecentUpdates_with_key_Test();

        /// <summary> GET /api/v2/project/:projectIdOrKey/activities </summary>
        public abstract void GetProjectRecentUpdates_with_filter_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/diskUsage </summary>
        public abstract void GetProjectDiskUsageTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/diskUsage </summary>
        public abstract void GetProjectDiskUsage_with_key_Test();

        /// <summary> POST /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void AddProjectUserTest();

        /// <summary> POST /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void AddProjectUser_with_key_Test();

        /// <summary> GET /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void GetProjectUsersTest();

        /// <summary> GET /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void GetProjectUsers_with_key_Test();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void DeleteProjectUserTest();

        /// <summary> DELETE /api/v2/project/:projectIdOrKey/users </summary>
        public abstract void DeleteProjectUser_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void AddProjectAdministratorTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void AddProjectAdministrator_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void GetProjectAdministratorsTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void GetProjectAdministrators_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void DeleteProjectAdministratorTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/administrators </summary>
        public abstract void DeleteProjectAdministrator_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void GetProjectIssueTypesTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void GetProjectIssueTypes_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void AddProjectIssueTypeTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/issueTypes </summary>
        public abstract void AddProjectIssueType_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void UpdateProjectIssueTypeTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void UpdateProjectIssueType_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void DeleteProjectIssueTypeTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/issueTypes/:id </summary>
        public abstract void DeleteProjectIssueType_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void GetProjectCategoriesTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void GetProjectCategories_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void AddProjectCategoryTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/categories </summary>
        public abstract void AddProjectCategory_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void UpdateProjectCategoryTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void UpdateProjectCategory_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void DeleteProjectCategoryTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/categories/:id </summary>
        public abstract void DeleteProjectCategory_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void GetProjectVersionsTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void GetProjectVersions_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void AddProjectVersionTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/versions </summary>
        public abstract void AddProjectVersion_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void UpdateProjectVersionTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void UpdateProjectVersion_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void DeleteProjectVersionTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/versions/:id </summary>
        public abstract void DeleteProjectVersion_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void GetProjectCustomFieldsTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void GetProjectCustomFields_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void AddProjectCustomFieldsTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields </summary>
        public abstract void AddProjectCustomFields_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void UpdateProjectCustomFieldTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void UpdateProjectCustomField_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void DeleteProjectCustomFieldTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id </summary>
        public abstract void DeleteProjectCustomField_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields/:id/items </summary>
        public abstract void AddProjectCustomFieldListItemTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/customFields/:id/items </summary>
        public abstract void AddProjectCustomFieldListItem_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void UpdateProjectCustomFieldListItemTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void UpdateProjectCustomFieldListItem_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void DeleteProjectCustomFieldListItemTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/customFields/:id/items/:itemId </summary>
        public abstract void DeleteProjectCustomFieldListItem_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/metadata/:path </summary>
        public abstract void GetProjectSharedFilesTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/metadata/:path </summary>
        public abstract void GetProjectSharedFiles_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/:sharedFileId </summary>
        public abstract void GetProjectSharedFileTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/files/:sharedFileId </summary>
        public abstract void GetProjectSharedFile_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void GetProjectWebHooksTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void GetProjectWebHooks_with_key_Test();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void AddProjectWebHookTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/webhooks </summary>
        public abstract void AddProjectWebHook_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void GetProjectWebHookTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void GetProjectWebHook_with_key_Test();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/webhooks/:id</summary>
        public abstract void UpdateProjectWebHookTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/webhooks/:id</summary>
        public abstract void UpdateProjectWebHook_with_key_Test();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void DeleteProjectWebHookTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/webhooks/:id </summary>
        public abstract void DeleteProjectWebHook_with_key_Test();

        #region projects/git

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories </summary>
        public abstract void GetProjectGitRepositoriesTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories </summary>
        public abstract void GetProjectGitRepositories_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName </summary>
        public abstract void GetProjectGitRepositoryTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName </summary>
        public abstract void GetProjectGitRepository_with_name_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName </summary>
        public abstract void GetProjectGitRepository_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName </summary>
        public abstract void GetProjectGitRepository_with_name_with_key_Test();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests </summary>
        public abstract void GetProjectGitRepositoryPullRequestsTest();

        /// <summary> GET /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/count </summary>
        public abstract void GetProjectGitRepositoryPullRequestsCountTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests </summary>
        public abstract void AddProjectGitRepositoryPullRequestTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number </summary>
        public abstract void GetProjectGitRepositoryPullRequestTest();

        /// <summary> PATCH /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number </summary>
        public abstract void UpdateProjectGitRepositoryPullRequestTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments </summary>
        public abstract void GetProjectGitRepositoryPullRequestCommentsTest();

        /// <summary> POST /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments </summary>
        public abstract void AddProjectGitRepositoryPullRequestCommentTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments/count </summary>
        public abstract void GetProjectGitRepositoryPullRequestCommentsCountTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/comments/:comment </summary>
        public abstract void UpdateProjectGitRepositoryPullRequestCommentTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/attachments </summary>
        public abstract void GetProjectGitRepositoryPullRequestAttachmentsTest();

        /// <summary> /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/attachments/:aid </summary>
        public abstract void DownloadProjectGitRepositoryPullRequestAttachmentTest();

        /// <summary> DELETE /api/v2/projects/:projectIdOrKey/git/repositories/:repoIdOrName/pullRequests/:number/attachments/:aid </summary>
        public abstract void DeleteProjectGitRepositoryPullRequestAttachmentTest();

        #endregion

        #endregion

        #region /api/v2/issues

        /// <summary> GET /api/v2/issues </summary>
        public abstract void GetIssuesTest();

        /// <summary> GET /api/v2/issues </summary>
        public abstract void GetIssues_all_Test();

        /// <summary> GET /api/v2/issues/count </summary>
        public abstract void GetIssuesCountTest();

        /// <summary> GET /api/v2/issues/count </summary>
        public abstract void GetIssuesCount_all_Test();

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

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void GetIssueComments_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void GetIssueComments_with_filter_Test();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void AddIssueCommentTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments </summary>
        public abstract void AddIssueComment_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/count </summary>
        public abstract void GetIssueCommentCountTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/count </summary>
        public abstract void GetIssueCommentCount_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void GetIssueCommentTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void GetIssueCommentTest_with_key_Test();

        /// <summary> PATCH /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void UpdateIssueCommentTest();

        /// <summary> PATCH /api/v2/issues/:issueIdOrKey/comments/:id </summary>
        public abstract void UpdateIssueComment_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void GetIssueCommentNotificationsTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void GetIssueCommentNotifications_with_key_Test();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void AddIssueCommentNotificationTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/comments/:commentId/notifications </summary>
        public abstract void AddIssueCommentNotification_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments </summary>
        public abstract void GetIssueAttachmentsTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments </summary>
        public abstract void GetIssueAttachments_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void GetIssueAttachmentTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void GetIssueAttachment_with_key_Test();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void DeleteIssueAttachmentTest();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/attachments/:attachmentId </summary>
        public abstract void DeleteIssueAttachment_with_key_Test();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void GetIssueLinkedSharedFilesTest();

        /// <summary> GET /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void GetIssueLinkedSharedFiles_with_key_Test();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void AddIssueLinkedSharedFilesTest();

        /// <summary> POST /api/v2/issues/:issueIdOrKey/sharedFiles </summary>
        public abstract void AddIssueLinkedSharedFiles_with_key_Test();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/sharedFiles/:id </summary>
        public abstract void RemoveIssueLinkedSharedFileTest();

        /// <summary> DELETE /api/v2/issues/:issueIdOrKey/sharedFiles/:id </summary>
        public abstract void RemoveIssueLinkedSharedFile_with_key_Test();

        #endregion

        #region /api/v2/wikis

        /// <summary> GET /api/v2/wikis </summary>
        public abstract void GetWikiPagesTest();

        /// <summary> GET /api/v2/wikis </summary>
        public abstract void GetWikiPages_with_key_Test();

        /// <summary> GET /api/v2/wikis/count </summary>
        public abstract void GetWikiPagesCountTest();

        /// <summary> GET /api/v2/wikis/count </summary>
        public abstract void GetWikiPagesCount_with_key_Test();

        /// <summary> GET /api/v2/wikis/tags </summary>
        public abstract void GetWikiPageTagsTest();

        /// <summary> GET /api/v2/wikis/tags </summary>
        public abstract void GetWikiPageTags_with_key_Test();

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

        /// <summary> GET /api/v2/wikis/:wikiId/history </summary>
        public abstract void GetWikiPageHistory_with_filter_Test();

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

        /// <summary> POST /api/v2/stars (prid) </summary>
        public abstract void AddStarToPullRequestTest();

        /// <summary> POST /api/v2/stars (prcid) </summary>
        public abstract void AddStarToPullRequestCommentTest();

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

        /// <summary> GET /api/v2/git/repositories </summary>
        public abstract void GetGitRepositories_with_key_Test();

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