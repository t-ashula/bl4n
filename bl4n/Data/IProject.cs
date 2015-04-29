// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProject.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> project; library -> user app  </summary>
    public interface IProject
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 識別文字列を取得します． </summary>
        string ProjectKey { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> グラフが有効かどうかを取得します． </summary>
        bool ChartEnabled { get; }

        /// <summary> サブタスクが有効かどうかを取得します． </summary>
        bool SubtaskingEnabled { get; }

        /// <summary> 文字列のマークアップ方法を取得します． </summary>
        string TextFormattingRule { get; }

        /// <summary> アーカイブ済かどうかを取得します． </summary>
        bool Archived { get; }

        /// <summary> 表示順を取得します． </summary>
        int DisplayOrder { get; }
    }

    /// <summary> project; api -> library </summary>
    [DataContract]
    internal sealed class Project : IProject
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectKey")]
        public string ProjectKey { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "chartEnabled")]
        public bool ChartEnabled { get; set; }

        [DataMember(Name = "subtaskingEnabled")]
        public bool SubtaskingEnabled { get; set; }

        [DataMember(Name = "textFormattingRule")]
        public string TextFormattingRule { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }
    }
}