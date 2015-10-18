// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiskUsage.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 総ディスク使用量を表します． </summary>
    public interface IDiskUsage
    {
        /// <summary> キャパシティを取得します． </summary>
        long Capacity { get; }

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

        /// <summary> プロジェクトごとの内訳のリストを取得します． </summary>
        IList<IDiskUsageDetail> Details { get; }
    }

    [DataContract]
    internal sealed class DiskUsage : ExtraJsonPropertyReadableObject, IDiskUsage
    {
        [DataMember(Name = "capacity")]
        public long Capacity { get; private set; }

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

        [DataMember(Name = "details")]
        private List<DiskUsageDetail> _details;

        public IList<IDiskUsageDetail> Details
        {
            get { return _details.ToList<IDiskUsageDetail>(); }
        }
    }
}