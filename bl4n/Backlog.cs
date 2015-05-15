// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL4N.Data;
using Newtonsoft.Json;
using Group = BL4N.Data.Group;
using Version = BL4N.Data.Version;

namespace BL4N
{
    /// <summary> The backlog. </summary>
    public class Backlog
    {
        /// <summary> 日付のフォーマット文字列を表します． </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary> API タイプを取得します</summary>
        public APIType APIType
        {
            get { return _settings.APIType; }
        }

        /// <summary> スペース名を取得します </summary>
        public string SpaceName
        {
            get { return _settings.SpaceName; }
        }

        /// <summary> ホスト名を取得します </summary>
        public string HostName
        {
            get { return _settings.HostName; }
        }

        /// <summary> ポート番号まで含めた host を取得します </summary>
        public string Host
        {
            get { return _settings.Host; }
        }

        /// <summary> API Key を取得します </summary>
        public string APIKey
        {
            get { return _settings.APIKey; }
        }

        private readonly BacklogConnectionSettings _settings;

        /// <summary> <see cref="Backlog"/> クラスを初期化します． </summary>
        public Backlog(BacklogConnectionSettings settings)
        {
            _settings = settings;
        }

        private async Task<T> DeserializeObject<T>(HttpResponseMessage s, JsonSerializerSettings jss)
        {
            var status = s.StatusCode;

            if (status == HttpStatusCode.BadRequest
                || status == HttpStatusCode.Unauthorized
                || status == HttpStatusCode.PaymentRequired
                || status == HttpStatusCode.Forbidden
                || status == HttpStatusCode.NotFound)
            {
                // TODO: only 400, 401, 402, 403, 404 ?
                var error = await s.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<BacklogErrorResponse>(error);
                errorResponse.StatusCode = status;
                throw new BacklogException(errorResponse);
            }

            var result = await s.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result, jss);
        }

        private async Task<T> GetApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            // TODO: default JSS
            var ua = new HttpClient();
            var s = await ua.GetAsync(uri);
            return await DeserializeObject<T>(s, jss);
        }

        private async Task<T> PutApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.PutAsync(uri, c);
            return await DeserializeObject<T>(s, jss);
        }

        private async Task<string> PostApiResult(Uri uri, HttpContent c)
        {
            var ua = new HttpClient();
            var s = await ua.PostAsync(uri, c);
            return await s.Content.ReadAsStringAsync();
        }

        private async Task<T> PostApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.PostAsync(uri, c);
            return await DeserializeObject<T>(s, jss);
        }

        private async Task<T> PatchApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = uri,
                Content = c
            };
            var s = await ua.SendAsync(req);
            return await DeserializeObject<T>(s, jss);
        }

        private async Task<T> DeleteApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.DeleteAsync(uri);
            return await DeserializeObject<T>(s, jss);
        }

        private async Task<T> DeleteApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var req = new HttpRequestMessage
            {
                Method = new HttpMethod("DELETE"),
                RequestUri = uri,
                Content = c
            };
            var s = await ua.SendAsync(req);
            return await DeserializeObject<T>(s, jss);
        }

        private string GetApiEndPointBase()
        {
            return string.Format("http{0}://{1}/api/v2", _settings.UseSSL ? "s" : string.Empty, Host);
        }

        private Uri GetApiUri(string apiname)
        {
            var endpoint = GetApiEndPointBase() + apiname;
            return _settings.APIType == APIType.APIKey
                ? new Uri(endpoint + "?apiKey=" + APIKey)
                : new Uri(endpoint);
        }

        private Uri GetApiUri(string[] subjects, IEnumerable<KeyValuePair<string, string>> query = null)
        {
            var kvs = new List<KeyValuePair<string, string>>();
            if (query != null)
            {
                kvs.AddRange(query);
            }

            if (_settings.APIType == APIType.APIKey)
            {
                kvs.Add(new KeyValuePair<string, string>("apiKey", APIKey));
            }

            var builder = new UriBuilder
            {
                Scheme = _settings.UseSSL ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                Host = _settings.HostName,
                Port = _settings.Port,
                Path = "/api/v2/" + string.Join("/", subjects),
                Query = string.Join("&", kvs.Select(kv => string.Format("{0}={1}", Uri.EscapeUriString(kv.Key), Uri.EscapeUriString(kv.Value))))
            };
            return builder.Uri;
        }

        private async Task<Tuple<string, byte[]>> GetApiResultAsFile(Uri uri)
        {
            var ua = new HttpClient();
            var res = await ua.GetAsync(uri);
            IEnumerable<string> headers;
            if (res.Content.Headers.TryGetValues("Content-Disposition", out headers)
                || res.Headers.TryGetValues("Content-Disposition", out headers))
            {
                // sample: Content-Disposition:attachment;filename="logo_mark.png"
                // chrome: Content-Disposition:attachment; filename*=UTF-8''logo_mark.png
                foreach (var header in headers)
                {
                    var r = new Regex(@"filename\*=UTF-8''(?<name>.+)$");
                    var m = r.Match(header);
                    if (m.Success)
                    {
                        return Tuple.Create(m.Groups["name"].Value, await res.Content.ReadAsByteArrayAsync());
                    }
                }
            }

            return Tuple.Create(string.Empty, await res.Content.ReadAsByteArrayAsync());
        }

        #region Space API

        /// <summary> space 情報を取得します </summary>
        /// <returns> <see cref="ISpace"/>. </returns>
        public ISpace GetSpace()
        {
            var uri = GetApiUri("/space");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var space = GetApiResult<Space>(uri, jss);
            return space.Result;
        }

        /// <summary> 最近の更新の一覧を取得します </summary>
        /// <returns> <see cref="IActivity"/> のリスト</returns>
        public List<IActivity> GetSpaceActivities()
        {
            var api = GetApiEndPointBase() + "/space/activities" + "?apiKey=" + APIKey;
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };

            return GetApiResult<List<Activity>>(new Uri(api), jss).Result.ToList<IActivity>();
        }

        /// <summary> スペースのロゴを取得します </summary>
        /// <returns> <see cref="ILogo"/> </returns>
        public ILogo GetSpaceImage()
        {
            var api = GetApiUri("/space/image");
            var res = GetApiResultAsFile(api).Result;
            return new Logo(res.Item1, res.Item2);
        }

        /// <summary> スペースのお知らせを取得します </summary>
        /// <returns> <see cref="ISpaceNotification"/> </returns>
        public ISpaceNotification GetSpaceNotifiacation()
        {
            var api = GetApiUri("/space/notification");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<SpaceNotification>(api, jss);
            return res.Result;
        }

        /// <summary> スペースのお知らせを更新します </summary>
        /// <param name="content"> お知らせとして設定する文字列 </param>
        /// <returns> <see cref="IActivity"/> のリスト</returns>
        public ISpaceNotification UpdateSpaceNotification(string content)
        {
            var api = GetApiUri("/space/notification");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var hc = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("content", content) });
            var res = PutApiResult<SpaceNotification>(api, hc, jss);
            return res.Result;
        }

        /// <summary> Get Space Disk Usage </summary>
        /// <returns> <see cref="IDiskUsage"/> </returns>
        public IDiskUsage GetSpaceDiskUsage()
        {
            var api = GetApiUri("/space/diskUsage");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<DiskUsage>(api, jss);
            return res.Result;
        }

        /// <summary> add sttachment to space </summary>
        /// <param name="name">file name</param>
        /// <param name="content">file content</param>
        /// <returns> <see cref="IAttachment"/>(without CreatedUser and Created ) </returns>
        public IAttachment AddAttachment(string name, Stream content)
        {
            var api = GetApiUri("/space/attachment");
            var hc = new MultipartFormDataContent();
            var filename = name.Contains(Path.PathSeparator) ? Path.GetFileName(name) : name;
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = "content.dat";
            }

            hc.Add(new StreamContent(content), @"""file""", "\"" + filename + "\"");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = PostApiResult<Attachment>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region User API

        /// <summary> Get User List Returns list of users in your space. </summary>
        /// <returns> List of <see cref="IUser"/>. </returns>
        public IList<IUser> GetUsers()
        {
            var api = GetApiUri("/users");
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            var users = res.Result;
            return users.ToList<IUser>();
        }

        /// <summary> Get User returns information about user. </summary>
        /// <param name="userId">user id ( not nickname )</param>
        /// <returns><see cref="IUser"/></returns>
        public IUser GetUser(int userId)
        {
            var api = GetApiUri(string.Format("/users/{0}", userId));
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Adds new user to the space.
        /// "Project Administrator" cannot add "Admin" user.
        /// </summary>
        /// <param name="options"> new user options </param>
        /// <returns></returns>
        public IUser AddUser(AddUserOptions options)
        {
            var api = GetApiUri(new[] { "users" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update User
        /// Updates information about user.
        /// </summary>
        /// <param name="userId">user id (number)</param>
        /// <param name="options">update options</param>
        /// <returns>updated <see cref="IUser"/></returns>
        public IUser UpdateUser(long userId, UpdateUserOptions options)
        {
            var api = GetApiUri(new[] { "users", string.Format("{0}", userId) });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete User. Deletes user from the space.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>deleted user</returns>
        public IUser DeleteUser(long uid)
        {
            var api = GetApiUri(string.Format("/users/{0}", uid));
            var jss = new JsonSerializerSettings();
            var res = DeleteApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Own User. Returns own information about user.
        /// /users/myself
        /// </summary>
        /// <returns> <see cref="IUser"/> of myself </returns>
        public IUser GetOwnUser()
        {
            var api = GetApiUri("/users/myself");
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<User>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get User Icon. Downloads user icon.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>user icon</returns>
        public ILogo GetUserIcon(long uid)
        {
            var api = GetApiUri(string.Format("/users/{0}/icon", uid));
            var res = GetApiResultAsFile(api);

            return new Logo(res.Result.Item1, res.Result.Item2);
        }

        /// <summary>
        /// Get User Recent Updates. Returns user's recent updates
        /// </summary>
        /// <remarks>TODO: more parameters </remarks>
        /// <returns> List of <see cref="IActivity"/>. </returns>
        public IList<IActivity> GetUserRecentUpdates(long uid)
        {
            var api = GetApiUri(string.Format("/users/{0}/activities", uid));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
        }

        /// <summary>
        /// Get Received Star List.
        /// Returns the list of stars that user received.
        /// </summary>
        /// <remarks>TODO: more parameters</remarks>
        /// <param name="uid">user id</param>
        /// <returns>list of <see cref="IStar"/></returns>
        public IList<IStar> GetReceivedStarList(long uid)
        {
            var api = GetApiUri(string.Format("/users/{0}/stars", uid));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
            };
            var res = GetApiResult<List<Star>>(api, jss);
            return res.Result.ToList<IStar>();
        }

        /// <summary>
        /// Count User Received Stars
        /// Returns number of stars that user received.
        /// </summary>
        /// <remarks>TODO: more parameters</remarks>
        /// <param name="uid">user id</param>
        /// <returns><see cref="ICounter"/></returns>
        public ICounter CountUserReceivedStars(long uid)
        {
            var api = GetApiUri(string.Format("/users/{0}/stars/count", uid));
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Recently Viewed Issues
        /// Returns list of issues which the user viewed recently.
        /// </summary>
        /// <returns> list of <see cref="IIssue"/>. </returns>
        public IList<IIssueUpdate> GetListOfRecentlyViewedIssues()
        {
            var api = GetApiUri("/users/myself/recentlyViewedIssues");
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<IssueUpdate>>(api, jss);
            return res.Result.ToList<IIssueUpdate>();
        }

        /// <summary>
        /// Get List of Recently Viewed Projects
        /// Returns list of projects which the user viewed recently.
        /// </summary>
        /// <remarks>TODO: more parameters</remarks>
        /// <returns>return list of <see cref="IProjectUpdate"/></returns>
        public IList<IProjectUpdate> GetListOfRecentlyViewedProjects()
        {
            var api = GetApiUri("/users/myself/recentlyViewedProjects");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<ProjectUpdate>>(api, jss);
            return res.Result.ToList<IProjectUpdate>();
        }

        /// <summary>
        /// Get List of Recently Viewed Wikis
        /// Returns list of Wikis which the user viewed recently.
        /// </summary>
        /// <remarks>TODO: more parameters</remarks>
        /// <returns> return list of <see cref="IWikiPageUpdate"/> </returns>
        public IList<IWikiPageUpdate> GetListOfRecentlyViewedWikis()
        {
            var api = GetApiUri("/users/myself/recentlyViewedWikis");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<WikiPageUpdate>>(api, jss);
            return res.Result.ToList<IWikiPageUpdate>();
        }

        #endregion

        #region Group API

        /// <summary>
        /// Get List of Groups
        /// Returns list of groups.
        /// </summary>
        /// <remarks>TODO: more parameters</remarks>
        /// <returns>retur list of <see cref="IGroup"/></returns>
        public IList<IGroup> GetGroups()
        {
            var api = GetApiUri("/groups");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<Group>>(api, jss);
            return res.Result.ToList<IGroup>();
        }

        /// <summary>
        /// Add Group.
        /// Adds new group.
        /// </summary>
        /// <param name="options">group options</param>
        /// <returns>created <see cref="IGroup"/></returns>
        public IGroup AddGroup(AddGroupOptions options)
        {
            var api = GetApiUri(new[] { "groups" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Group>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Group
        /// Returns information about group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <returns><see cref="IGroup"/></returns>
        public IGroup GetGroup(long groupId)
        {
            var api = GetApiUri(string.Format("/groups/{0}", groupId));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<Group>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Group
        /// Updates information about group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="options">update group options</param>
        /// <returns>changed <see cref="IGroup"/></returns>
        public IGroup UpdateGroup(long groupId, UpdateGroupOptions options)
        {
            var api = GetApiUri(new[] { "groups", string.Format("{0}", groupId) });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Group>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Group
        /// Deletes group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <returns>deleted <see cref="IGroup"/></returns>
        public IGroup DeleteGroup(long groupId)
        {
            var api = GetApiUri(string.Format("/groups/{0}", groupId));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = DeleteApiResult<Group>(api, jss);
            return res.Result;
        }

        #endregion

        #region Statuses API

        /// <summary>
        /// Get Status List
        /// Returns list of statuses.
        /// </summary>
        /// <returns>list of <see cref="IStatus"/> </returns>
        public IList<IStatus> GetStatuses()
        {
            var api = GetApiUri("/statuses");
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Status>>(api, jss);
            return res.Result.ToList<IStatus>();
        }

        #endregion

        #region Resolutions API

        /// <summary>
        /// Get Resolution List
        /// Returns list of resolutions.
        /// </summary>
        /// <returns>list of <see cref="IResolution"/> </returns>
        public IList<IResolution> GetResolutions()
        {
            var api = GetApiUri("/resolutions");
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Resolution>>(api, jss);
            return res.Result.ToList<IResolution>();
        }

        #endregion

        #region Priorities API

        /// <summary>
        /// Get Priority List
        /// Returns list of priorities.
        /// </summary>
        /// <returns>list of <see cref="IPriority"/></returns>
        public IList<IPriority> GetPriorities()
        {
            var api = GetApiUri("/priorities");
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Priority>>(api, jss);
            return res.Result.ToList<IPriority>();
        }

        #endregion

        #region Projects API

        #region project

        /// <summary>
        /// Get Project List
        /// Returns list of projects.
        /// </summary>
        /// <returns>list of <see cref="IProject"/> </returns>
        public IList<IProject> GetProjects()
        {
            var api = GetApiUri("/projects");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<Project>>(api, jss);
            return res.Result.ToList<IProject>();
        }

        /// <summary>
        /// Add Project
        /// Adds new project.
        /// </summary>
        /// <param name="options">project to create</param>
        /// <returns>created <see cref="IProject"/></returns>
        public IProject AddProject(AddProjectOptions options)
        {
            var api = GetApiUri(new[] { "projects" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project
        /// Returns information about project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns><see cref="IProject"/></returns>
        public IProject GetProject(long projectId)
        {
            return GetProject(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project
        /// Returns information about project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns><see cref="IProject"/></returns>
        public IProject GetProject(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<Project>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Project
        /// Updates information about project.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="options">update option</param>
        /// <returns>updated <see cref="IProject"/></returns>
        public IProject UpdateProject(long projectId, UpdateProjectOptions options)
        {
            return UpdateProject(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Update Project
        /// Updates information about project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="options">update option</param>
        /// <returns>updated <see cref="IProject"/></returns>
        public IProject UpdateProject(string projectKey, UpdateProjectOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValurPairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Project
        /// Deletes project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>deleted project</returns>
        public IProject DeleteProject(long projectId)
        {
            return DeleteProject(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Delete Project
        /// Deletes project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>deleted project</returns>
        public IProject DeleteProject(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = DeleteApiResult<Project>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/misc

        /// <summary>
        /// Get Project Icon
        /// Downloads project icon.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <returns>project icon</returns>
        public ILogo GetProjectImage(long projectId)
        {
            return GetProjectImage(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project Icon
        /// Downloads project icon.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>project icon</returns>
        public ILogo GetProjectImage(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "image" });
            var res = GetApiResultAsFile(api);

            return new Logo(res.Result.Item1, res.Result.Item2);
        }

        /// <summary>
        /// Get Project Recent Updates
        /// Returns recent update in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <remarks>TODO: more parameters </remarks>
        /// <returns>list of <see cref="IActivity"/></returns>
        public IList<IActivity> GetProjectRecentUpdates(long projectId)
        {
            return GetProjectRecentUpdates(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project Recent Updates
        /// Returns recent update in the project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <remarks>TODO: more parameters </remarks>
        /// <returns>list of <see cref="IActivity"/></returns>
        public IList<IActivity> GetProjectRecentUpdates(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "activities" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
        }

        /// <summary>
        /// Get Project Disk Usage
        /// Returns information about project disk usage.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>project's <see cref="IDiskUsageDetail"/></returns>
        public IDiskUsageDetail GetProjectDiskUsage(long projectId)
        {
            return GetProjectDiskUsage(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project Disk Usage
        /// Returns information about project disk usage.
        /// </summary>
        /// <param name="projectkey"></param>
        /// <returns>project's <see cref="IDiskUsageDetail"/></returns>
        public IDiskUsageDetail GetProjectDiskUsage(string projectkey)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "diskUsage" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<DiskUsageDetail>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/user

        /// <summary>
        /// Add Project User
        /// Adds user to list of project members.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="uid">user id</param>
        /// <returns>added <see cref="IUser"/></returns>
        public IUser AddProjectUser(long projectId, long uid)
        {
            return AddProjectUser(string.Format("{0}", projectId), uid);
        }

        /// <summary>
        /// Add Project User
        /// Adds user to list of project members.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>added <see cref="IUser"/></returns>
        public IUser AddProjectUser(string projectKey, long uid)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project User List
        /// Returns list of project members.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IUser"/></returns>
        public IList<IUser> GetProjectUsers(long projectId)
        {
            return GetProjectUsers(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project User List
        /// Returns list of project members.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IUser"/></returns>
        public IList<IUser> GetProjectUsers(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            return res.Result.ToList<IUser>();
        }

        /// <summary>
        /// Delete Project User
        /// Removes user from list project members.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectUser(long projectId, long uid)
        {
            return DeleteProjectUser(string.Format("{0}", projectId), uid);
        }

        /// <summary>
        /// Delete Project User
        /// Removes user from list project members.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectUser(string projectKey, long uid)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = DeleteApiResult<User>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/admin

        /// <summary>
        /// Add Project Administrator
        /// Adds "Project Administrator" role to user
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="userId">user id</param>
        /// <returns>changed <see cref="IUser"/></returns>
        public IUser AddProjectAdministrator(long projectId, long userId)
        {
            return AddProjectAdministrator(string.Format("{0}", projectId), userId);
        }

        /// <summary>
        /// Add Project Administrator
        /// Adds "Project Administrator" role to user
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="userId">user id</param>
        /// <returns>changed <see cref="IUser"/></returns>
        public IUser AddProjectAdministrator(string projectKey, long userId)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "administrators" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", userId.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Project Administrators
        /// Returns list of users who has Project Administrator role
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of administrator <see cref="IUser"/></returns>
        public IList<IUser> GetProjectAdministrators(long projectId)
        {
            return GetProjectAdministrators(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get List of Project Administrators
        /// Returns list of users who has Project Administrator role
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of administrator <see cref="IUser"/></returns>
        public IList<IUser> GetProjectAdministrators(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "administrators" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            return res.Result.ToList<IUser>();
        }

        /// <summary>
        /// Delete Project Administrator
        /// Removes Project Administrator role from user
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectAdministrator(string projectKey, long uid)
        {
            var api = GetApiUri(string.Format("/projects/{0}/administrators", projectKey));
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = DeleteApiResult<User>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/issueType

        /// <summary>
        /// Get Issue Type List
        /// Returns list of Issue Types in the project.
        /// </summary>
        /// <param name="projectkey"></param>
        /// <returns></returns>
        public IList<IIssueType> GetProjectIssueTypes(string projectkey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/issueTypes", projectkey));
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<IssueType>>(api, jss);
            return res.Result.ToList<IIssueType>();
        }

        /// <summary>
        /// Add Issue Type
        /// Adds new Issue Type to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">issue type to add</param>
        /// <returns>added <see cref="IIssueType"/></returns>
        public IIssueType AddProjectIssueType(string projectKey, AddProjectIssueTypeOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Issue Type
        /// Updates information about Issue Type.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">issue type id</param>
        /// <param name="options">issue type to change</param>
        /// <returns>updated <see cref="IIssueType"/></returns>
        public IIssueType UpdateProjectIssueType(string projectKey, long id, UpdateProjectIssueTypeOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Issue Type
        /// Deletes Issue Type.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="issueId">delete issueType id</param>
        /// <param name="substituteIssueTypeId">Substitute Issue Type Id</param>
        /// <returns>deleted <see cref="IIssueType"/></returns>
        public IIssueType DeleteProjectIssueType(string projectKey, long issueId, long substituteIssueTypeId)
        {
            var api = GetApiUri(string.Format("/projects/{0}/issueTypes/{1}", projectKey, issueId));
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("substituteIssueTypeId", substituteIssueTypeId.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = DeleteApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/category

        /// <summary>
        /// Get Category List
        /// Returns list of Categories in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of project's <see cref="ICategory"/></returns>
        public IList<ICategory> GetProjectCategories(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/categories", projectKey));
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Category>>(api, jss);
            return res.Result.ToList<ICategory>();
        }

        /// <summary>
        /// Add Category
        /// Adds new Category to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">category to add</param>
        /// <returns>added <see cref="ICategory"/></returns>
        public ICategory AddProjectCategory(string projectKey, AddProjectCategoryOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Category>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Category
        /// Updates information about Category.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="categoryId">category id</param>
        /// <param name="options">update category option</param>
        /// <returns>updated <see cref="ICategory"/></returns>
        public ICategory UpdateProjectCategory(string projectKey, long categoryId, UpdateProjectCategoryOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories", string.Format("{0}", categoryId) });
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var jss = new JsonSerializerSettings();
            var res = PatchApiResult<Category>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Category
        /// Deletes Category.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="categoryId">category id to del</param>
        /// <returns>deletec <see cref="ICategory"/></returns>
        public ICategory DeleteProjectCategory(string projectKey, long categoryId)
        {
            var api = GetApiUri(string.Format("/projects/{0}/categories/{1}", projectKey, categoryId));
            var jss = new JsonSerializerSettings();
            var res = DeleteApiResult<Category>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/version

        /// <summary>
        /// Get Version List
        /// Returns list of Versions in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="IVersion"/></returns>
        public IList<IVersion> GetProjectVersions(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/versions", projectKey));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Version>>(api, jss);
            return res.Result.ToList<IVersion>();
        }

        /// <summary>
        /// Add Version
        /// Adds new Version to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">adding version options</param>
        /// <returns>added <see cref="IVersion"/></returns>
        public IVersion AddProjectVersion(string projectKey, AddProjectVersionOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Version>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Version
        /// Updates information about Version.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">version id</param>
        /// <param name="options">new version info </param>
        /// <returns>updated <see cref="IVersion"/></returns>
        public IVersion UpdateProjectVersion(string projectKey, long id, UpdateProjectVersionOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions", id.ToString() });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Version>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Version
        /// Deletes Version.
        /// </summary>
        /// <param name="projectKey">project key </param>
        /// <param name="id">version id to delete</param>
        /// <returns>deleted <see cref="IVersion"/></returns>
        public IVersion DeleteProjectVersion(string projectKey, long id)
        {
            var api = GetApiUri(string.Format("/projects/{0}/versions/{1}", projectKey, id));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<Version>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/customfield

        /// <summary>
        /// Get Custom Field List
        /// Returns list of Custom Fields in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="ICustomField"/></returns>
        public IList<ICustomField> GetProjectCustomFields(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/customFields", projectKey));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<CustomField>>(api, jss);
            return res.Result.ToList<ICustomField>();
        }

        /// <summary>
        /// Add Custom Field
        /// Adds new Custom Field to the project.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="options">custom field options to add</param>
        /// <returns>created <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomField(string projectkey, AddCustomFieldOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "customFields" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Custom Field
        /// Updates Custom Field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">custom filed id</param>
        /// <param name="options">field to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomField(string projectKey, long id, UpdateCustomFieldOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Custom Field
        /// Deletes Custom Field.
        /// </summary>
        /// <param name="projectkey"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICustomField DeleteProjectCustomField(string projectkey, long id)
        {
            var api = GetApiUri(string.Format("/projects/{0}/customFields/{1}", projectkey, id));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<CustomField>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Add List Item for List Type Custom Field
        /// Adds new list item for list type custom field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">custom field id</param>
        /// <param name="name">name to add</param>
        /// <returns>udated <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomFieldListItem(string projectKey, long id, string name)
        {
            var api = GetApiUri(string.Format("/projects/{0}/customFields/{1}/items", projectKey, id));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new[] { new KeyValuePair<string, string>("name", name) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update List Item for List Type Custom Field
        /// Updates list item for list type custom field.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <param name="name">name to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomFieldListItem(string projectkey, long fieldId, long itemId, string name)
        {
            var api = GetApiUri(string.Format("/projects/{0}/customFields/{1}/items/{2}", projectkey, fieldId, itemId));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new[] { new KeyValuePair<string, string>("name", name) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete List Item for List Type Custom Field
        /// Deletes list item for list type custom field.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <returns>deleted <see cref="ICustomField"/></returns>
        public ICustomField DeleteProjectCustomFieldListItem(string projectkey, long fieldId, long itemId)
        {
            var api = GetApiUri(string.Format("/projects/{0}/customFields/{1}/items/{2}", projectkey, fieldId, itemId));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<CustomField>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/file

        /// <summary>
        /// Get List of Shared Files
        /// Gets list of Shared Files ( meta data only )
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="path">path </param>
        /// <remarks>TODO: more parameters</remarks>
        /// <returns>list of <see cref="ISharedFile"/> in path</returns>
        public IList<ISharedFile> GetProjectSharedFiles(string projectkey, string path = "")
        {
            var apig = GetApiUri(string.Format("/projects/{0}/files/metadata/{1}", projectkey, path));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = GetApiResult<List<SharedFile>>(apig, jss);
            return res.Result.ToList<ISharedFile>();
        }

        /// <summary>
        /// Get File
        /// Downloads the file.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="id">shared file id</param>
        /// <returns>downloaded <see cref="ISharedFileData"/></returns>
        public ISharedFileData GetProjectSharedFile(string projectkey, long id)
        {
            var api = GetApiUri(string.Format("/projects/{0}/files/{1}", projectkey, id));
            var res = GetApiResultAsFile(api);
            var file = new SharedFileData(res.Result.Item1, res.Result.Item2);
            return file;
        }

        #endregion

        #region project/webhook

        /// <summary>
        /// Get List of Webhooks
        /// Returns list of webhooks.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <returns>list of <see cref="IWebHook"/></returns>
        public IList<IWebHook> GetProjectWebHooks(string projectkey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/webhooks", projectkey));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<WebHook>>(api, jss);
            return res.Result.ToList<IWebHook>();
        }

        /// <summary>
        /// Add Webhook
        /// Adds new webhook.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">new webhook options</param>
        /// <returns>added <see cref="IWebHook"/></returns>
        public IWebHook AddProjectWebHook(string projectKey, AddWebHookOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "webhooks" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<WebHook>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Webhook
        /// Returns information about webhook.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">webhook id</param>
        /// <returns><see cref="IWebHook"/></returns>
        public IWebHook GetProjectWebHook(string projectKey, long id)
        {
            var api = GetApiUri(string.Format("/projects/{0}/webhooks/{1}", projectKey, id));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<WebHook>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Webhook
        /// Updates information about webhook.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="hookId">webhook id</param>
        /// <param name="options">webhook to update (Required:Id)</param>
        /// <returns>updated <see cref="IWebHook"/></returns>
        public IWebHook UpdateProjectWebHook(string projectkey, long hookId, UpdateWebHookOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "webhooks", string.Format("{0}", hookId) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<WebHook>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Webhook
        /// Deletes webhook.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="hookId">web hook id</param>
        /// <returns>deleted <see cref="IWebHook"/></returns>
        public IWebHook DeleteProjectWebHook(string projectkey, long hookId)
        {
            var api = GetApiUri(string.Format("/projects/{0}/webhooks/{1}", projectkey, hookId));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<WebHook>(api, jss);
            return res.Result;
        }

        #endregion

        #endregion

        #region Issues API

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
        /// <returns>list of <see cref="IIssueComment"/></returns>
        /// <remarks>TODO: add more parameters</remarks>
        public IList<IIssueComment> GetIssueComments(long issueId)
        {
            return GetIssueComments(string.Format("{0}", issueId));
        }

        /// <summary>
        /// Get Comment List
        /// Returns list of comments in issue.
        /// </summary>
        /// <param name="issueKey">issue key (like XXX-123)</param>
        /// <returns>list of <see cref="IIssueComment"/></returns>
        /// <remarks>TODO: add more parameters</remarks>
        public IList<IIssueComment> GetIssueComments(string issueKey)
        {
            var api = GetApiUri(new[] { "issues", issueKey, "comments" });
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

        #endregion

        #region Wikis API

        /// <summary>
        /// Get Wiki Page List
        /// Returns list of Wiki pages.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IWikiPage"/></returns>
        /// <remarks>TODO: add project Key type api</remarks>
        public IList<IWikiPage> GetWikiPages(long projectId)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectId.ToString())
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
        /// <remarks>TODO: add project Key type api</remarks>
        public ICounter GetWikiPagesCount(long projectId)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectId.ToString())
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
        /// <remarks>TODO: add project Key type api</remarks>
        public IList<ITag> GetWikiPageTags(long projectId)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectId.ToString())
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
        public IWikiPage UpdateWikiPage(long wikiId, AddWikiPageOptions updateOptions)
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
        /// <param name="wikiId">wiki [age id</param>
        /// <returns>list of <see cref="IWikiPageHistory"/></returns>
        /// <remarks>TODO: more parameters </remarks>
        public IList<IWikiPageHistory> GetWikiPageHistory(long wikiId)
        {
            var api = GetApiUri(new[] { "wikis", wikiId.ToString(), "history" });
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

        #endregion

        #region Stars API

        private void AddStar(string key, long id)
        {
            var api = GetApiUri(new[] { "stars" });
            var kvs = new[] { new KeyValuePair<string, string>(key, id.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult(api, hc);
            System.Diagnostics.Trace.WriteLine(res);
        }

        /// <summary>
        /// Add Star
        /// Adds star to issue
        /// </summary>
        /// <param name="issueId">issue id</param>
        public void AddStarToIssue(long issueId)
        {
            AddStar("issueKey", issueId);
        }

        /// <summary>
        /// Add Star
        /// Adds star to comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        public void AddStarToComment(long commentId)
        {
            AddStar("commentId", commentId);
        }

        /// <summary>
        /// Add Star
        /// Adds star to wiki page
        /// </summary>
        /// <param name="wikiId">wiki page id</param>
        public void AddStarToWikiPage(long wikiId)
        {
            AddStar("wikiId", wikiId);
        }

        #endregion

        #region Notifications API

        /// <summary>
        /// Get Notification
        /// Returns own notifications.
        /// </summary>
        /// <returns>list of <see cref="INotification"/></returns>
        public IList<INotification> GetNotifications()
        {
            var api = GetApiUri(new[] { "notifications" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Notification>>(api, jss);
            return res.Result.ToList<INotification>();
        }

        /// <summary>
        /// Count Notification
        /// Returns number of Notifications.
        /// </summary>
        /// <param name="options">filter option</param>
        /// <returns>count of notifications</returns>
        public ICounter GetNotificationsCount(NotificationsCountOptions options)
        {
            var query = options.ToKeyValuePairs();
            var api = GetApiUri(new[] { "notifications", "count" }, query);
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Reset Unread Notification Count
        /// Resets unread Notification count.
        /// </summary>
        /// <returns>count of notifications</returns>
        public ICounter ResetUnreadNotificationCount()
        {
            var api = GetApiUri(new[] { "notifications", "markAsRead" });
            var jss = new JsonSerializerSettings();
            var hc = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
            var res = PostApiResult<Counter>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Read Notification
        /// Changes notifications read.
        /// </summary>
        /// <param name="id">notification id</param>
        /// <remarks>TODO: async? </remarks>
        public void ReadNotification(long id)
        {
            var api = GetApiUri(new[] { "notifications", id.ToString(), "markAsRead" });
            var ua = new HttpClient();
            var s = ua.PostAsync(api, new StringContent(string.Empty)); // no body
            var res = s.Result.Content;
            System.Diagnostics.Trace.Write(string.Format("ReadNotification({0}) = {1}", id, res));
        }

        #endregion

        #region Git API

        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IList<IRepositoryDetail> GetGitRepositories(long projectId)
        {
            return GetGitRepositories(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IList<IRepositoryDetail> GetGitRepositories(string projectKey)
        {
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectIdOrKey", projectKey)
            };
            var api = GetApiUri(new[] { "git", "repositories" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<RepositoryDetail>>(api, jss);
            return res.Result.ToList<IRepositoryDetail>();
        }

        #endregion

        internal async Task<T> DirectAccess<T>(Uri uri, HttpMethod method, HttpContent content = null)
        {
            var ua = new HttpClient();
            var request = new HttpRequestMessage(method, uri) { Content = content };
            var s = await ua.SendAsync(request);
            return await DeserializeObject<T>(s, new JsonSerializerSettings());
        }
    }
}