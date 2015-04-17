﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. </summary>
    public class Backlog
    {
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

        public async Task<T> GetApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            // TODO: default JSS
            var ua = new HttpClient();
            var s = await ua.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(s, jss);
        }

        private async Task<T> PutApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.PutAsync(uri, c);
            return JsonConvert.DeserializeObject<T>(await s.Content.ReadAsStringAsync(), jss);
        }

        private async Task<T> PostApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.PostAsync(uri, c);
            var res = await s.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(res, jss);
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
            var res = await s.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(res, jss);
        }

        private async Task<T> DeleteApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.DeleteAsync(uri);
            var res = await s.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(res, jss);
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
            var res = await s.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(res, jss);
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

        /// <summary> Adds new user to the space. </summary>
        /// <param name="user"> user (required: UserId, Name, MailAddress, RoleType)</param>
        /// <param name="pass"> password </param>
        /// <returns>created user</returns>
        public IUser AddUser(IUser user, string pass)
        {
            var api = GetApiUri("/users");
            var jss = new JsonSerializerSettings();
            var kvs = new[]
            {
                new KeyValuePair<string, string>("userId", user.UserId),
                new KeyValuePair<string, string>("name", user.Name),
                new KeyValuePair<string, string>("mailAddress", user.MailAddress),
                new KeyValuePair<string, string>("roleType", user.RoleType.ToString()),
                new KeyValuePair<string, string>("password", pass)
            };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IUser UpdateUser(IUser user, string password = "")
        {
            var userId = user.Id;
            var api = GetApiUri(string.Format("/users/{0}", userId));
            var jss = new JsonSerializerSettings();

            // XXX: only changing parameter ?
            var kvs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", user.Name),
                new KeyValuePair<string, string>("mailAddress", user.MailAddress),
                new KeyValuePair<string, string>("roleType", user.RoleType.ToString()),
            };

            if (!string.IsNullOrWhiteSpace(password))
            {
                kvs.Add(new KeyValuePair<string, string>("password", password));
            }

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
            var res = GetApiResult<List<Data.Group>>(api, jss);
            return res.Result.ToList<IGroup>();
        }

        /// <summary>
        /// Add Group.
        /// Adds new group.
        /// </summary>
        /// <param name="name">group name</param>
        /// <param name="members">group member id</param>
        /// <returns>created <see cref="IGroup"/></returns>
        public IGroup AddGroup(string name, IEnumerable<long> members)
        {
            var api = GetApiUri("/groups");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

            var kvs = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("name", name) };
            kvs.AddRange(members.Select(uid => new KeyValuePair<string, string>("members[]", uid.ToString())));

            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Data.Group>(api, hc, jss);
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
            var res = GetApiResult<Data.Group>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Group
        /// Updates information about group.
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="newname">new group name</param>
        /// <param name="member">User ID added to the group</param>
        /// <returns>changed <see cref="IGroup"/></returns>
        public IGroup UpdateGroup(long groupId, string newname, IEnumerable<long> member)
        {
            var api = GetApiUri(string.Format("/groups/{0}", groupId));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

            // XXX: only changing parameter ?
            var kvs = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(newname))
            {
                kvs.Add(new KeyValuePair<string, string>("name", newname));
            }

            kvs.AddRange(member.Select(uid => new KeyValuePair<string, string>("members[]", uid.ToString())));

            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Data.Group>(api, hc, jss);
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
            var res = DeleteApiResult<Data.Group>(api, jss);
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
        /// <param name="newProject">project to create</param>
        /// <returns>created <see cref="IProject"/></returns>
        public IProject AddProject(IProject newProject)
        {
            var api = GetApiUri("/projects");
            var jss = new JsonSerializerSettings();
            var kvs = new[]
            {
                new KeyValuePair<string,string>("name", newProject.Name),
                new KeyValuePair<string,string>("key", newProject.ProjectKey),
                new KeyValuePair<string,string>("chartEnabled", newProject.ChartEnabled.ToString().ToLower()),
                new KeyValuePair<string,string>("subtaskingEnabled", newProject.SubtaskingEnabled.ToString().ToLower()),
                new KeyValuePair<string,string>("textFormattingRule", newProject.TextFormattingRule),
            };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project
        /// Returns information about project.
        /// </summary>
        /// <param name="projectKey">projectKey</param>
        /// <returns><see cref="IProject"/></returns>
        public IProject GetProject(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}", projectKey));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<Project>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Project
        /// Updates information about project.
        /// </summary>
        /// <param name="newProject"></param>
        /// <returns>updated <see cref="IProject"/></returns>
        public IProject UpdateProject(IProject newProject)
        {
            var api = GetApiUri(string.Format("/projects/{0}", newProject.Id));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var current = GetProject(newProject.Id.ToString());

            var kvs = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(newProject.Name) && current.Name != newProject.Name)
            {
                kvs.Add(new KeyValuePair<string, string>("name", newProject.Name));
            }

            if (!string.IsNullOrEmpty(newProject.ProjectKey) && current.ProjectKey != newProject.ProjectKey)
            {
                kvs.Add(new KeyValuePair<string, string>("key", newProject.ProjectKey));
            }

            if (current.ChartEnabled != newProject.ChartEnabled)
            {
                kvs.Add(new KeyValuePair<string, string>("chartEnabled", newProject.ChartEnabled.ToString().ToLower()));
            }

            if (current.SubtaskingEnabled != newProject.SubtaskingEnabled)
            {
                kvs.Add(new KeyValuePair<string, string>("subtaskingEnabled", newProject.SubtaskingEnabled.ToString().ToLower()));
            }

            if (!string.IsNullOrEmpty(newProject.TextFormattingRule) && current.TextFormattingRule != newProject.TextFormattingRule)
            {
                kvs.Add(new KeyValuePair<string, string>("textFormattingRule", newProject.TextFormattingRule));
            }

            if (current.Archived != newProject.Archived)
            {
                kvs.Add(new KeyValuePair<string, string>("archived", newProject.Archived.ToString().ToLower()));
            }

            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Project
        /// Deletes project.
        /// </summary>
        /// <param name="test">project key</param>
        /// <returns>deleted project</returns>
        public IProject DeleteProject(string test)
        {
            var api = GetApiUri(string.Format("/projects/{0}", test));
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = DeleteApiResult<Project>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project Icon
        /// Downloads project icon.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>project icon</returns>
        public ILogo GetProjectImage(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/image", projectKey));
            var res = GetApiResultAsFile(api);

            return new Logo(res.Result.Item1, res.Result.Item2);
        }

        /// <summary>
        /// Get Project Recent Updates
        /// Returns recent update in the project.
        /// </summary>
        /// <remarks>TODO: more parameters </remarks>
        /// <returns>list of <see cref="IActivity"/></returns>
        public IList<IActivity> GetProjectRecentUpdates(string projectKey)
        {
            var api = GetApiUri(string.Format("/projects/{0}/activities", projectKey));
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
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
            var api = GetApiUri(string.Format("/projects/{0}/users", projectKey));
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        #endregion

        /// <summary>
        /// Delete Project User
        /// Removes user from list project members.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectUser(string projectKey, long uid)
        {
            var api = GetApiUri(string.Format("/projects/{0}/users", projectKey));
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = DeleteApiResult<User>(api, hc, jss);
            return res.Result;
        }
    }
}