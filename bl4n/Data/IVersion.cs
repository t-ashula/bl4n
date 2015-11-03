// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVersion.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> ヴァージョン情報を表します </summary>
    public interface IVersion
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクトID を取得します． </summary>
        long ProjectId { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> 開始日を取得します． </summary>
        DateTime StartDate { get; }

        /// <summary> 期限日を取得します． </summary>
        DateTime? ReleaseDueDate { get; }

        /// <summary> アーカイブ済かどうかを取得します． </summary>
        bool Archived { get; }

        /// <summary> 表示順を取得します． </summary>
        int DisplayOrder { get; }
    }

    [DataContract]
    internal sealed class Version : ExtraJsonPropertyReadableObject, IVersion
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "releaseDueDate")]
        public DateTime? ReleaseDueDate { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }
    }
}