// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using BL4N.Data;

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
            var serset = new DataContractJsonSerializerSettings { DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ") };
            var space = GetApiResult<Space>(uri, serset);
            return space.Result;
        }

        private async Task<T> GetApiResult<T>(Uri apiEndPoint, DataContractJsonSerializerSettings serializerSettings) where T : class
        {
            var ua = new HttpClient();
            var stream = await ua.GetStreamAsync(apiEndPoint);
            var ser = new DataContractJsonSerializer(typeof(T), serializerSettings);
            return (T)ser.ReadObject(stream);
        }

        private Uri GetApiUri(string apiname)
        {
            var endpoint = GetApiEndPointBase() + apiname;
            return _settings.APIType == APIType.APIKey
                ? new Uri(endpoint + "?apiKey=" + APIKey)
                : new Uri(endpoint);
        }

        private string GetApiEndPointBase()
        {
            return string.Format("http{0}://{1}/api/v2", _settings.UseSSL ? "s" : string.Empty, Host);
        }
    }
}