// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogErrorInfo.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N
{
    /// <summary> エラー情報を表します </summary>
    [DataContract]
    public class BacklogErrorInfo
    {
        /// <summary> エラーメッセージを取得します </summary>
        [DataMember(Name = "message")]
        public string Message { get; private set; }

        /// <summary> 追加情報を取得します </summary>
        [DataMember(Name = "moreInfo")]
        public string MoreInfo { get; private set; }

        /// <summary> エラーコードを取得します </summary>
        [DataMember(Name = "code")]
        public int Code { get; private set; }
    }
}