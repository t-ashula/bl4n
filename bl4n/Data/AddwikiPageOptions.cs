// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddWikiPageOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// wikipage の新規作成用のパラメータを表します
    /// </summary>
    public class AddWikiPageOptions
    {
        /// <summary> ページ名を取得します </summary>
        public string Name { get; private set; }

        /// <summary> ページ内容を取得します </summary>
        public string Content { get; private set; }

        /// <summary> メール通知するかどうかを取得します． </summary>
        public bool Notify { get; private set; }

        /// <summary>
        /// <see cref="AddWikiPageOptions"/> のインスタンスを初期化します
        /// </summary>
        /// <param name="name">wiki page name</param>
        /// <param name="content">wiki page content</param>
        /// <param name="mailNotify">true: do notify </param>
        public AddWikiPageOptions(string name, string content, bool mailNotify = false)
        {
            Name = name;
            Content = content;
            Notify = mailNotify;
        }

        /// <summary>
        /// HTTP Request 用の key-value pair のリストを取得します
        /// </summary>
        /// <returns>parameters as key-value pairs</returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("content", Content)
            };

            if (Notify)
            {
                pairs.Add(new KeyValuePair<string, string>("mailNotify", "true"));
            }

            return pairs;
        }
    }
}