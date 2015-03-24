// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogConnectionSettings.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BL4N
{
    [DataContract]
    [KnownType(typeof(BacklogJPConnectionSettings))]
    public class BacklogConnectionSettings
    {
        public BacklogConnectionSettings(string spaceName, APIType apiType, string apikey, string hostName, int port, bool ssl)
        {
            APIType = apiType;
            APIKey = apikey;

            HostName = hostName;
            SpaceName = spaceName;
            Port = port;
            UseSSL = ssl;
        }

        [DataMember]
        public string SpaceName { get; private set; }

        [DataMember]
        public string HostName { get; private set; }

        [DataMember]
        public int Port { get; private set; }

        [DataMember]
        public bool UseSSL { get; private set; }

        public APIType APIType { get; private set; }

        [DataMember(Name = "APIType")]
        private string APITypeString
        {
            get { return APIType.ToString(); }
            set { APIType = (APIType)Enum.Parse(typeof(APIType), value); }
        }

        [DataMember]
        public string APIKey { get; private set; }

        /// <summary> 妥当な設定かどうかを取得します </summary>
        /// <returns> 妥当な設定のとき true </returns>
        public virtual bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SpaceName)
                   && !string.IsNullOrWhiteSpace(HostName)
                   && !string.IsNullOrWhiteSpace(APIKey)
                   && (1 <= Port && Port <= 65535);
        }

        /// <summary> <paramref name="path"/> から接続設定を読み込みます </summary>
        /// <param name="path">設定ファイルのパス</param>
        /// <returns> 接続設定 </returns>
        public static BacklogConnectionSettings Load(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    return Load(sr.BaseStream);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary> <paramref name="stream"/> から接続設定を読み込みます </summary>
        /// <param name="stream">設定を読み込んでいるストリーム</param>
        /// <returns> 接続設定 </returns>
        public static BacklogConnectionSettings Load(Stream stream)
        {
            var ser = new DataContractJsonSerializer(typeof(BacklogConnectionSettings));
            return (BacklogConnectionSettings)ser.ReadObject(stream);
        }

        /// <summary> <paramref name="path"/> に接続設定を書き出します </summary>
        /// <param name="path">書き出すファイルのパス</param>
        public void Save(string path)
        {
            using (var wr = new StreamWriter(path))
            {
                Save(wr.BaseStream);
            }
        }

        /// <summary> <paramref name="stream"/> に接続設定を書き出します </summary>
        /// <param name="stream">書き出すストリーム</param>
        public void Save(Stream stream)
        {
            var serset = new DataContractJsonSerializerSettings { EmitTypeInformation = EmitTypeInformation.Never };
            var ser = new DataContractJsonSerializer(typeof(BacklogConnectionSettings), serset);
            ser.WriteObject(stream, this);
        }
    }
}