// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddTextTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> テキストタイプのカスタムフィールドの追加のオプションを表します </summary>
    public class AddTextTypeCustomFieldOptions : AddCustomFieldOptions
    {
        /// <summary> <see cref="AddTextTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public AddTextTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.Text, name)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return CoreKeyValuePairs();
        }
    }
}