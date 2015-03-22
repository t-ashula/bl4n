// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogJP.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N
{
    /// <summary> The backlog. </summary>
    public class BacklogJP
    {
        /// <summary> API タイプを取得します</summary>
        public APIType APIType { get; private set; }

        /// <summary> スペース名を取得します </summary>
        public string SpaceName { get; private set; }

        /// <summary> ホスト名を取得します </summary>
        public string HostName
        {
            get { return "backlog.jp"; }
        }

        /// <summary>
        /// API Key を取得します
        /// </summary>
        public string APIKey { get; private set; }

        /// <summary> <see cref="BacklogJP"/> クラスを初期化します． </summary>
        /// <param name="spaceName"> スペース名 </param>
        /// <param name="apiType"> API タイプ </param>
        public BacklogJP(string spaceName, APIType apiType = APIType.APIKey)
        {
            APIType = apiType;
            SpaceName = spaceName;
        }

        /// <summary>API Key を設定します </summary>
        /// <param name="apiKey"> 個人設定で取得した API Key </param>
        public void SetAPIKey(string apiKey)
        {
            APIKey = apiKey;
        }
    }
}