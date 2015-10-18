// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogErrorResponse.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using BL4N.Data;

namespace BL4N
{
    /// <summary> エラー応答全体を表します </summary>
    [DataContract]
    public class BacklogErrorResponse : ExtraJsonPropertyReadableObject
    {
        /// <summary> HTTP のステータスコードを取得または設定します </summary>
        [IgnoreDataMember]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary> エラー情報の一覧を取得します </summary>
        [DataMember(Name = "errors")]
        public List<BacklogErrorInfo> Errors { get; private set; }
    }
}