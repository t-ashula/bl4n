// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogErrorResponse.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Runtime.Serialization;

namespace BL4N
{
    /// <summary> エラー応答を表します </summary>
    [DataContract]
    public class BacklogErrorResponse
    {
        /// <summary> HTTP のステータスコードを取得または設定します </summary>
        public HttpStatusCode Statuscode { get; set; }

        /// <summary> エラー情報の一覧を取得します </summary>
        [DataMember(Name = "errors")]
        public BacklogErrorInfo[] Errors { get; private set; }
    }
}