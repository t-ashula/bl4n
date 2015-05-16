// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> 期間のオプションを表します </summary>
    public class TermOptions
    {
        /// <summary> 開始日を取得または設定します </summary>
        public DateTime? Since { get; set; }

        /// <summary> 最終日を取得または設定します </summary>
        public DateTime? Until { get; set; }

        /// <summary> <see cref="TermOptions"/> のインスタンスを初期化します </summary>
        /// <param name="since">開始日</param>
        /// <param name="until">最終日</param>
        public TermOptions(DateTime? since = null, DateTime? until = null)
        {
            Since = since;
            Until = until;
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (Since.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("since", Since.Value.ToString(Backlog.DateFormat)));
            }

            if (Until.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("until", Until.Value.ToString(Backlog.DateFormat)));
            }

            return pairs;
        }
    }
}