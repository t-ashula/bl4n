// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Wiki.cs">
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
    /// <summary> The backlog. for Wiki API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Wiki Page List
        /// Returns list of Wiki pages.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IWikiPage"/></returns>
        public IList<IWikiPage> GetWikiPages(long projectId)
        {
            return GetWikiPages(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Wiki Page List
        /// Returns list of Wiki pages.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="IWikiPage"/></returns>
        public IList<IWikiPage> GetWikiPages(string projectKey)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectKey)
            };
            var api = GetApiUri(new[] { "wikis" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<WikiPage>>(api, jss);
            return res.Result.ToList<IWikiPage>();
        }

        /// <summary>
        /// Count Wiki Page
        /// Returns number of Wiki pages.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IWikiPage"/></returns>
        public ICounter GetWikiPagesCount(long projectId)
        {
            return GetWikiPagesCount(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Count Wiki Page
        /// Returns number of Wiki pages.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="IWikiPage"/></returns>
        public ICounter GetWikiPagesCount(string projectKey)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectKey)
            };
            var api = GetApiUri(new[] { "wikis", "count" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Wiki Page Tag List
        /// Returns list of tags that are used in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="ITag"/></returns>
        public IList<ITag> GetWikiPageTags(long projectId)
        {
            return GetWikiPageTags(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Wiki Page Tag List
        /// Returns list of tags that are used in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="ITag"/></returns>
        public IList<ITag> GetWikiPageTags(string projectKey)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectKey)
            };
            var api = GetApiUri(new[] { "wikis", "tags" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Tag>>(api, jss);
            return res.Result.ToList<ITag>();
        }

        /// <summary>
        /// Add Wiki Page
        /// Adds new Wiki page.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="addWikiPageOptions">adding wikipage info</param>
        /// <returns>created <see cref="IWikiPage"/></returns>
        public IWikiPage AddWikiPage(long projectId, AddWikiPageOptions addWikiPageOptions)
        {
            var api = GetApiUri(new[] { "wikis" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectId", string.Format("{0}", projectId))
            };
            kvs.AddRange(addWikiPageOptions.ToKeyValuePairs());

            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<WikiPage>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Wiki Page
        /// Returns information about Wiki page.
        /// </summary>
        /// <param name="pageId">wiki page id</param>
        /// <returns>wiki page</returns>
        public IWikiPage GetWikiPage(long pageId)
        {
            var api = GetApiUri(new[] { "wikis", pageId.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<WikiPage>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Wiki Page
        /// Updates information about Wiki page.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="updateOptions">update option</param>
        /// <returns>updated <see cref="IWikiPage"/></returns>
        public IWikiPage UpdateWikiPage(long wikiId, UpdateWikiPageOptions updateOptions)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var hc = new FormUrlEncodedContent(updateOptions.ToKeyValuePairs());
            var res = PatchApiResult<WikiPage>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Wiki Page
        /// Deletes Wiki page.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="doNotify">true: do mail notify</param>
        /// <returns></returns>
        public IWikiPage DeleteWikiPage(long wikiId, bool doNotify)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("mailNotify", doNotify ? "true" : "false")
            };
            var hc = new FormUrlEncodedContent(kvs);
            var res = DeleteApiResult<WikiPage>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Wiki attachments
        /// Gets list of files attached to Wiki.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <returns>list of <see cref="IAttachment"/>(created/createdUser is not set)</returns>
        public IList<IAttachment> GetWikiPageAttachments(long wikiId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "attachments" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Attachment>>(api, jss);
            return res.Result.ToList<IAttachment>();
        }

        /// <summary>
        /// Attach File to Wiki
        /// Attaches file to Wiki
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="fileIds">attachment file ids</param>
        /// <returns>list of <see cref="IAttachment"/></returns>
        public IList<IAttachment> AddWikiPageAttachments(long wikiId, IEnumerable<long> fileIds)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "attachments" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = fileIds.ToKeyValuePairs("attachmentId[]");
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<List<Attachment>>(api, hc, jss);
            return res.Result.ToList<IAttachment>();
        }

        /// <summary>
        /// Get Wiki Page Attachment
        /// Downloads Wiki page's attachment file.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="fileId">attachment file id</param>
        /// <returns>downloaded file content and name as <see cref="ISharedFileData"/></returns>
        public ISharedFileData GetWikiPageAttachment(long wikiId, long fileId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "attachments", fileId.ToString() });
            var res = GetApiResultAsFile(api);
            var file = new SharedFileData(res.Result.Item1, res.Result.Item2);
            return file;
        }

        /// <summary>
        /// Remove Wiki Attachment
        /// Removes files attached to Wiki.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="fileId">attachment file id</param>
        /// <returns>removed file <see cref="IAttachment"/></returns>
        public IAttachment RemoveWikiPageAttachment(long wikiId, long fileId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "attachments", fileId.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<Attachment>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Shared Files on Wiki
        /// Returns the list of Shared Files on Wiki.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <returns>list of <see cref="ISharedFile"/></returns>
        public IList<ISharedFile> GetWikiPageSharedFiles(long wikiId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "sharedFiles" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<SharedFile>>(api, jss);
            return res.Result.ToList<ISharedFile>();
        }

        /// <summary>
        /// Link Shared Files to Wiki
        /// Links Shared Files to Wiki.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="fileIds">shared file id</param>
        /// <returns>list of linked <see cref="ISharedFile"/> </returns>
        public IList<ISharedFile> AddWikiPageSharedFiles(long wikiId, IEnumerable<long> fileIds)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "sharedFiles" });
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
        /// Remove Link to Shared File from Wiki
        /// Removes link to shared file from Wiki.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="fileId">shared file id</param>
        /// <returns>deleted <see cref="ISharedFile"/></returns>
        public ISharedFile RemoveWikiPageSharedFile(long wikiId, long fileId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "sharedFiles", fileId.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<SharedFile>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Wiki Page History
        /// Returns history of Wiki page.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <param name="filter">result paging option</param>
        /// <returns>list of <see cref="IWikiPageHistory"/></returns>
        public IList<IWikiPageHistory> GetWikiPageHistory(long wikiId, ResultPagingOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "history" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<WikiPageHistory>>(api, jss);
            return res.Result.ToList<IWikiPageHistory>();
        }

        /// <summary>
        /// Get Wiki Page Star
        /// Returns list of stars received on the Wiki page.
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        /// <returns>list of <see cref="IStar"/></returns>
        public IList<IStar> GetWikiPageStars(long wikiId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "stars" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Star>>(api, jss);
            return res.Result.ToList<IStar>();
        }
    }
}