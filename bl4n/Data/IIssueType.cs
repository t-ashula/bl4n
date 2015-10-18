// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueType.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 課題種別を表します </summary>
    public interface IIssueType
    {
        /// <summary> IDを取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクトIDを取得します． </summary>
        long ProjectId { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> 背景色を取得します． </summary>
        string Color { get; }

        /// <summary> 表示順を取得します． </summary>
        int DisplayOrder { get; }
    }

    [DataContract]
    internal sealed class IssueType : ExtraJsonPropertyReadableObject, IIssueType
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        // TODO: colors = { "#e30000", "#990000", "#934981", "#814fbc", "#2779ca", "#007e9a", "#7ea800", "#ff9200", "#ff3265", "#666665" };
        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }
    }
}