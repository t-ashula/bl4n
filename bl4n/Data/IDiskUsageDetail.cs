// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiskUsageDetail.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> プロジェクトごとのディスク使用量を表します． </summary>
    public interface IDiskUsageDetail
    {
        /// <summary> プロジェクトIDを取得します． </summary>
        long ProjectId { get; }

        /// <summary> 課題の総使用量を取得します． </summary>
        long Issue { get; }

        /// <summary> Wiki の総使用量を取得します． </summary>
        long Wiki { get; }

        /// <summary> ファイルの総使用量を取得します． </summary>
        long File { get; }

        /// <summary> Subversion リポジトリの総使用量を取得します． </summary>
        long Subversion { get; }

        /// <summary> git リポジトリの総使用量を取得します． </summary>
        long Git { get; }
    }

    [DataContract]
    internal sealed class DiskUsageDetail : IDiskUsageDetail
    {
        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "issue")]
        public long Issue { get; private set; }

        [DataMember(Name = "wiki")]
        public long Wiki { get; private set; }

        [DataMember(Name = "file")]
        public long File { get; private set; }

        [DataMember(Name = "subversion")]
        public long Subversion { get; private set; }

        [DataMember(Name = "git")]
        public long Git { get; private set; }
    }
}