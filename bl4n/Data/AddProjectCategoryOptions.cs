// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddProjectCategoryOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> 追加する課題カテゴリのオプションを表します </summary>
    public class AddProjectCategoryOptions
    {
        /// <summary> <see cref="AddProjectCategoryOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">カテゴリ名</param>
        public AddProjectCategoryOptions(string name)
        {
            Name = name;
        }

        /// <summary> カテゴリ名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return new[] { new KeyValuePair<string, string>("name", Name) };
        }
    }
}