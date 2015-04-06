// --------------------------------------------------------------------------------------------------------------------
// <copyright content="Backlog.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
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
    }
}