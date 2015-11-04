// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Issue.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for Issue API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Issue List
        /// Returns list of issues.
        /// </summary>
        /// <param name="projectIds">Project Ids</param>
        /// <param name="conditions">search option</param>
        /// <returns>list of <see cref="IIssue"/></returns>
        public IList<IIssue> GetIssues(long[] projectIds, IssueSearchConditions conditions)
        {
            var query = new List<KeyValuePair<string, string>>();
            query.AddRange(projectIds.ToKeyValuePairs("projectId[]"));
            query.AddRange(conditions.ToKeyValuePairs());

            var api = GetApiUri(new[] { "issues" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Issue>>(api, jss);
            return res.Result.ToList<IIssue>();
        }

        /// <summary>
        /// Count Issue
        /// Returns number of issues.
        /// </summary>
        /// <param name="projectIds">Project Ids</param>
        /// <param name="conditions">search condtions</param>
        /// <returns>issue count as <see cref="ICounter"/></returns>
        public ICounter GetIssuesCount(long[] projectIds, IssueSearchConditions conditions)
        {
            var query = new List<KeyValuePair<string, string>>();
            query.AddRange(projectIds.ToKeyValuePairs("projectId[]"));
            query.AddRange(conditions.ToKeyValuePairs());

            var api = GetApiUri(new[] { "issues", "count" }, query);
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Add Issue
        /// Adds new issue.
        /// </summary>
        /// <param name="settings">new issue setting</param>
        /// <returns>created <see cref="IIssue"/></returns>
        public IIssue AddIssue(NewIssueSettings settings)
        {
            var api = GetApiUri(new[] { "issues" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var hc = new FormUrlEncodedContent(settings.ToKeyValuePairs());
            var res = PostApiResult<Issue>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Issue
        /// Returns information about issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns><see cref="IIssue"/></returns>
        public IIssue GetIssue(long issueId)
        {
            return GetIssue(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Get Issue
        /// Returns information about issue.
        /// </summary>
        /// <param name="issueKey">issue key to get (like XXX-123)</param>
        /// <returns><see cref="IIssue"/></returns>
        public IIssue GetIssue(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<Issue>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Issue
        /// Updates information about issue.
        /// </summary>
        /// <param name="issueId">issue id to update</param>
        /// <param name="issueUpdateSettings">update settings</param>
        /// <returns>updated <see cref="IIssue"/></returns>
        public IIssue UpdateIssue(long issueId, IssueUpdateSettings issueUpdateSettings)
        {
            return UpdateIssue(string.Format("{0}", issueId), issueUpdateSettings);
        }

        /// <summary>
        /// Update Issue
        /// Updates information about issue.
        /// </summary>
        /// <param name="issueKey">issue key to update (like XXX-123)</param>
        /// <param name="issueUpdateSettings">update settings</param>
        /// <returns>updated <see cref="IIssue"/></returns>
        public IIssue UpdateIssue(string issueKey, IssueUpdateSettings issueUpdateSettings)
        {
            var api = GetApiUri(new[] { "issues", issueKey });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = issueUpdateSettings.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Issue>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Issue
        /// Deletes issue.
        /// </summary>
        /// <param name="issueId">issue id to delete</param>
        /// <returns>deleted <see cref="IIssue"/></returns>
        public IIssue DeleteIssue(long issueId)
        {
            return DeleteIssue(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Delete Issue
        /// Deletes issue.
        /// </summary>
        /// <param name="issueKey">issue key to delete (like XXX-123)</param>
        /// <returns>deleted <see cref="IIssue"/></returns>
        public IIssue DeleteIssue(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<Issue>(api, jss);
            return res.Result;
        }

        #region issues/comments

        /// <summary>
        /// Get Comment List
        /// Returns list of comments in issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="filter">result paging option</param>
        /// <returns>list of <see cref="IIssueComment"/></returns>
        public IList<IIssueComment> GetIssueComments(long issueId, ResultPagingOptions filter = null)
        {
            return GetIssueComments(string.Format("{0}", issueId), filter);
        }

        /// <summary>
        /// Get Comment List
        /// Returns list of comments in issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="filter">result paging option</param>
        /// <returns>list of <see cref="IIssueComment"/></returns>
        public IList<IIssueComment> GetIssueComments(string issueKey, ResultPagingOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "issues", issueKey, "comments" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = GetApiResult<List<IssueComment>>(api, jss);
            return res.Result.ToList<IIssueComment>();
        }

        /// <summary>
        /// Add Comment
        /// Adds a comment to the issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="options">comment content</param>
        /// <returns>list of <see cref="IIssueComment"/></returns>
        public IIssueComment AddIssueComment(long issueId, CommentAddContent options)
        {
            return AddIssueComment(string.Format("{0}", issueId), options);
        }

        /// <summary>
        /// Add Comment
        /// Adds a comment to the issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="options">comment content</param>
        /// <returns>list of <see cref="IIssueComment"/></returns>
        public IIssueComment AddIssueComment(string issueKey, CommentAddContent options)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<IssueComment>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Count Comment
        /// Returns number of comments in issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>number of comments</returns>
        public ICounter GetIssueCommentCount(long issueId)
        {
            return GetIssueCommentCount(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Count Comment
        /// Returns number of comments in issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <returns>number of comments</returns>
        public ICounter GetIssueCommentCount(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments", "count" });
            var res = GetApiResult<Counter>(api, new JsonSerializerSettings());
            return res.Result;
        }

        /// <summary>
        /// Get Comment
        /// Returns information about comment.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="commentId">comment id</param>
        /// <returns>comment <see cref="IIssueComment"/></returns>
        public IIssueComment GetIssueComment(long issueId, long commentId)
        {
            return GetIssueComment(string.Format("{0}", issueId), commentId);
        }

        /// <summary>
        /// Get Comment
        /// Returns information about comment.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="commentId">comment id</param>
        /// <returns>comment <see cref="IIssueComment"/></returns>
        public IIssueComment GetIssueComment(string issueKey, long commentId)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments", commentId.ToString("D") });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<IssueComment>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update comment
        /// Updates content of comment.
        /// User can update own comment.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="commentId">comment id</param>
        /// <param name="content">comment content</param>
        /// <returns>updated <see cref="IIssueComment"/></returns>
        public IIssueComment UpdateIssueComment(long issueId, long commentId, string content)
        {
            return UpdateIssueComment(string.Format("{0}", issueId), commentId, content);
        }

        /// <summary>
        /// Update comment
        /// Updates content of comment.
        /// User can update own comment.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="commentId">comment id</param>
        /// <param name="content">comment content</param>
        /// <returns>updated <see cref="IIssueComment"/></returns>
        public IIssueComment UpdateIssueComment(string issueKey, long commentId, string content)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments", commentId.ToString("D") });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new[] { new KeyValuePair<string, string>("content", content) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<IssueComment>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Comment Notifications
        /// Returns the list of comment notifications.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="commentId">comment id</param>
        /// <returns>list of <see cref="ICommentNotification"/></returns>
        public IList<ICommentNotification> GetIssueCommentNotifications(long issueId, long commentId)
        {
            var issueKey = issueId.ToString("D");
            return GetIssueCommentNotifications(issueKey, commentId);
        }

        /// <summary>
        /// Get List of Comment Notifications
        /// Returns the list of comment notifications.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="commentId">comment id</param>
        /// <returns>list of <see cref="ICommentNotification"/></returns>
        public IList<ICommentNotification> GetIssueCommentNotifications(string issueKey, long commentId)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments", commentId.ToString("D"), "notifications" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<CommentNotification>>(api, jss);
            return res.Result.ToList<ICommentNotification>();
        }

        /// <summary>
        /// Add Comment Notification
        /// Adds notifications to the comment.
        /// Only the user who added the comment can add notifications.
        /// </summary>
        /// <param name="issueId">issue id </param>
        /// <param name="commentId">comment id</param>
        /// <param name="userIds">user id who to notify</param>
        /// <returns>comment id <see cref="IIssueComment"/></returns>
        /// <remarks>
        /// <paramref name="userIds"/> shall not contain comment created user. </remarks>
        public IIssueComment AddIssueCommentNotification(long issueId, long commentId, List<long> userIds)
        {
            return AddIssueCommentNotification(string.Format("{0}", issueId), commentId, userIds);
        }

        /// <summary>
        /// Add Comment Notification
        /// Adds notifications to the comment.
        /// Only the user who added the comment can add notifications.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="commentId">comment id</param>
        /// <param name="userIds">user id who to notify</param>
        /// <returns>comment id <see cref="IIssueComment"/></returns>
        /// <remarks>
        /// <paramref name="userIds"/> shall not contain comment created user. </remarks>
        public IIssueComment AddIssueCommentNotification(string issueKey, long commentId, List<long> userIds)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments", commentId.ToString(), "notifications" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = userIds.ToKeyValuePairs("notifiedUserId[]");
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<IssueComment>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region issues/attachments

        /// <summary>
        /// Get List of Issue Attachments
        /// Returns the list of issue attachments.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>list of <see cref="IAttachment"/></returns>
        public IList<IAttachment> GetIssueAttachments(long issueId)
        {
            return GetIssueAttachments(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Get List of Issue Attachments
        /// Returns the list of issue attachments.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <returns>list of <see cref="IAttachment"/></returns>
        public IList<IAttachment> GetIssueAttachments(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "attachments" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Attachment>>(api, jss);
            return res.Result.ToList<IAttachment>();
        }

        /// <summary>
        /// Get Issue Attachment
        /// Downloads issue's attachment file.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="attachmentId">attachment id</param>
        /// <returns>file content and name</returns>
        public ISharedFileData GetIssueAttachment(long issueId, long attachmentId)
        {
            return GetIssueAttachment(string.Format("{0}", issueId), attachmentId);
        }

        /// <summary>
        /// Get Issue Attachment
        /// Downloads issue's attachment file.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="attachmentId">attachment id</param>
        /// <returns>file content and name</returns>
        public ISharedFileData GetIssueAttachment(string issueKey, long attachmentId)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "attachments", attachmentId.ToString("D") });
            var res = GetApiResultAsFile(api);
            var fileName = res.Result.Item1;
            var content = res.Result.Item2;
            return new SharedFileData(fileName, content);
        }

        /// <summary>
        /// Delete Issue Attachment
        /// Deletes an attachment of issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="attachmentId">attachment id</param>
        /// <returns>deleted <see cref="IAttachment"/></returns>
        public IAttachment DeleteIssueAttachment(long issueId, long attachmentId)
        {
            return DeleteIssueAttachment(string.Format("{0}", issueId), attachmentId);
        }

        /// <summary>
        /// Delete Issue Attachment
        /// Deletes an attachment of issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="attachmentId">attachment id</param>
        /// <returns>deleted <see cref="IAttachment"/></returns>
        public IAttachment DeleteIssueAttachment(string issueKey, long attachmentId)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "attachments", attachmentId.ToString("D") });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<Attachment>(api, jss);
            return res.Result;
        }

        #endregion

        #region issues/sharedfiles

        /// <summary>
        /// Get List of Linked Shared Files
        /// Returns the list of linked Shared Files to issues.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>list of <see cref="ISharedFile"/></returns>
        public IList<ISharedFile> GetIssueLinkedSharedFiles(long issueId)
        {
            return GetIssueLinkedSharedFiles(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Get List of Linked Shared Files
        /// Returns the list of linked Shared Files to issues.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <returns>list of <see cref="ISharedFile"/></returns>
        public IList<ISharedFile> GetIssueLinkedSharedFiles(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "sharedFiles" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<SharedFile>>(api, jss);
            return res.Result.ToList<ISharedFile>();
        }

        /// <summary>
        /// Link Shared Files to Issue
        /// Links shared files to issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="fileIds">shared file ids</param>
        /// <returns>list of linked <see cref="ISharedFile"/></returns>
        public IList<ISharedFile> AddIssueLinkedSharedFiles(long issueId, IEnumerable<long> fileIds)
        {
            return AddIssueLinkedSharedFiles(string.Format("{0}", issueId), fileIds);
        }

        /// <summary>
        /// Link Shared Files to Issue
        /// Links shared files to issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="fileIds">shared file ids</param>
        /// <returns>list of linked <see cref="ISharedFile"/></returns>
        public IList<ISharedFile> AddIssueLinkedSharedFiles(string issueKey, IEnumerable<long> fileIds)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "sharedFiles" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = fileIds.ToKeyValuePairs("fileId[]");
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<List<SharedFile>>(api, hc, jss);
            return res.Result.ToList<ISharedFile>();
        }

        /// <summary>
        /// Remove Link to Shared File from Issue
        /// Removes link to shared file from issue.
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <param name="fileId">shared file id</param>
        /// <returns>removed <see cref="ISharedFile"/></returns>
        public ISharedFile RemoveIssueLinkedSharedFile(long issueId, long fileId)
        {
            return RemoveIssueLinkedSharedFile(string.Format("{0}", issueId), fileId);
        }

        /// <summary>
        /// Remove Link to Shared File from Issue
        /// Removes link to shared file from issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <param name="fileId">shared file id</param>
        /// <returns>removed <see cref="ISharedFile"/></returns>
        public ISharedFile RemoveIssueLinkedSharedFile(string issueKey, long fileId)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "sharedFiles", fileId.ToString("D") });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<SharedFile>(api, jss);
            return res.Result;
        }

        #endregion
    }
}