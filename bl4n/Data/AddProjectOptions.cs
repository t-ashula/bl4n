// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddProjectOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加するプロジェクトのオプションを表します </summary>
    public class AddProjectOptions
    {
        /// <summary> <see cref="AddProjectOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">プロジェクト名称</param>
        /// <param name="projectKey">プロジェクト識別子</param>
        /// <param name="chartEnabled">チャートを有効にします</param>
        /// <param name="subtaskingEnabled">サブタスクを有効にします</param>
        /// <param name="textFormattingRule">テキストのフォーマット</param>
        public AddProjectOptions(string name, string projectKey, bool chartEnabled, bool subtaskingEnabled, string textFormattingRule)
        {
            Name = name;
            ProjectKey = projectKey;
            ChartEnabled = chartEnabled;
            SubtaskingEnabled = subtaskingEnabled;
            TextFormattingRule = textFormattingRule;
        }

        /// <summary> プロジェクト名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> プロジェクト識別子を取得または設定します </summary>
        public string ProjectKey { get; set; }

        /// <summary> チャートを有効にするかどうかを取得または設定します </summary>
        public bool ChartEnabled { get; set; }

        /// <summary> サブタスクを有効にするかどうかを取得または設定します </summary>
        public bool SubtaskingEnabled { get; set; }

        /// <summary> テキストのフォーマットを取得または設定します </summary>
        public string TextFormattingRule { get; set; }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("key", ProjectKey),
                new KeyValuePair<string, string>("chartEnabled", ChartEnabled ? "true" : "false"),
                new KeyValuePair<string, string>("subtaskingEnabled", SubtaskingEnabled ? "true" : "false"),
                new KeyValuePair<string, string>("textFormattingRule", TextFormattingRule),
            };
            return pairs;
        }
    }
}