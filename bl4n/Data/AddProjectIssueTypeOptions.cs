// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddProjectIssueTypeOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加する課題タイプのオプションを表します </summary>
    public class AddProjectIssueTypeOptions
    {
        /// <summary> <see cref="AddProjectIssueTypeOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">タイプ名</param>
        /// <param name="typeColor">色名</param>
        public AddProjectIssueTypeOptions(string name, IssueTypeColor typeColor)
        {
            Name = name;
            TypeColor = typeColor;
        }

        /// <summary> タイプ名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> 色名を取得または設定します </summary>
        public IssueTypeColor TypeColor { get; set; }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var kvs = new[]
            {
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("color", TypeColor.ColorCode)
            };

            return kvs;
        }
    }
}