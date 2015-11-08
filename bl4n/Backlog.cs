// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. </summary>
    public partial class Backlog
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

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage s, JsonSerializerSettings jss)
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
            var obj = JsonConvert.DeserializeObject<T>(result, jss);
            if (_settings.StrictMode)
            {
                ThrowIfExtraProperty(obj);
            }

            return obj;
        }

        private static void ThrowIfExtraProperty<T>(T obj)
        {
            var exObj = obj as ExtraJsonPropertyReadableObject;
            if (exObj != null)
            {
                if (exObj.HasExtraProperty())
                {
                    var extraKeys = string.Join(",", exObj.GetExtraProperties().Keys);
                    throw new ApplicationException($"unkown property found. {extraKeys}");
                }
                return;
            }

            var list = obj as List<ExtraJsonPropertyReadableObject>;
            if (list != null)
            {
                if (list.Any(l => l.HasExtraProperty()))
                {
                    var extraKeys = string.Join(",", list.First(l => l.HasExtraProperty()).GetExtraProperties().Keys);
                    throw new ApplicationException($"unkown property found. {extraKeys}");
                }
            }
        }

        private async Task<T> GetApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            // TODO: default JSS
            var ua = new HttpClient();
            var s = await ua.GetAsync(uri);
            return await DeserializeResponse<T>(s, jss);
        }

        private async Task<T> PutApiResult<T>(Uri uri, HttpContent c, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.PutAsync(uri, c);
            return await DeserializeResponse<T>(s, jss);
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
            return await DeserializeResponse<T>(s, jss);
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
            return await DeserializeResponse<T>(s, jss);
        }

        private async Task<T> DeleteApiResult<T>(Uri uri, JsonSerializerSettings jss)
        {
            var ua = new HttpClient();
            var s = await ua.DeleteAsync(uri);
            return await DeserializeResponse<T>(s, jss);
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
            return await DeserializeResponse<T>(s, jss);
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

        internal async Task<T> DirectAccess<T>(Uri uri, HttpMethod method, HttpContent content = null)
        {
            var ua = new HttpClient();
            var request = new HttpRequestMessage(method, uri) { Content = content };
            var s = await ua.SendAsync(request);
            return await DeserializeResponse<T>(s, new JsonSerializerSettings());
        }

        internal static T DeserializeObj<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        internal static DateTime FromUnixTimeStamp(long timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timeStamp);
        }
    }
}