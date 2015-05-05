// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpace.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> スペース情報の公開インタフェース </summary>
    public interface ISpace
    {
        /// <summary> スペースの識別文字列を取得します． </summary>
        string SpaceKey { get; set; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; set; }

        /// <summary> 所有者の ID を取得します． </summary>
        long OwnerId { get; set; }

        /// <summary> UI の言語を取得します． </summary>
        string Lang { get; set; }

        /// <summary> タイムゾーンを取得します． </summary>
        string Timezone { get; set; } // XXX: use System.TimeZone?

        /// <summary> レポートが送信される時刻を取得します． </summary>
        string ReportSendTime { get; set; }

        /// <summary> テキストのマークアップ方法を取得します． </summary>
        string TextFormattingRule { get; set; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; set; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; set; }
    }

    /// <summary> スペース情報の内部用データクラス API との serialize 用 </summary>
    [DataContract]
    internal class Space : ISpace
    {
        [DataMember(Name = "spaceKey")]
        public string SpaceKey { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "ownerId")]
        public long OwnerId { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }

        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }

        [DataMember(Name = "reportSendTime")]
        public string ReportSendTime { get; set; }

        [DataMember(Name = "textFormattingRule")]
        public string TextFormattingRule { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}