// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
            get
            {
                if ((_settings.UseSSL && _settings.Port == 443)
                    || (!_settings.UseSSL && _settings.Port == 80))
                {
                    return _settings.HostName;
                }

                return string.Format("{0}:{1}", _settings.HostName, _settings.Port);
            }
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

        /// <summary> space 情報を取得します </summary>
        /// <returns> <see cref="ISpace"/>. </returns>
        public ISpace GetSpace()
        {
            var uri = GetApiUri("/space");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var space = GetApiResult<Space>(uri, jss);
            return space.Result;
        }

        public async Task<T> GetApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(s, jss);
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

        public ILogo GetSpaceImage()
        {
            var api = GetApiUri("/space/image");
            var res = GetApiResultAsFile(api).Result;
            return new Logo(res.Item1, res.Item2);
        }

        public ISpaceNotification GetSpaceNotifiacation()
        {
            var api = GetApiUri("/space/notification");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<SpaceNotification>(api, jss);
            return res.Result;
        }
    }
}