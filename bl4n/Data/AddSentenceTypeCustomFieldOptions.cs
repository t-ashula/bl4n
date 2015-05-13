// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddSentenceTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 複数行文字列タイプのカスタムフィールドの追加のオプションを表します </summary>
    public class AddSentenceTypeCustomFieldOptions : AddCustomFieldOptions
    {
        /// <summary> <see cref="AddSentenceTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public AddSentenceTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.Sentence, name)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return CoreKeyValuePairs();
        }
    }
}