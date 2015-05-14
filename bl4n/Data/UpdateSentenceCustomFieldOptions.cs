// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateSentenceCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 文字列タイプのカスタムフィールドの更新オプションを表します </summary>
    public class UpdateSentenceCustomFieldOptions : UpdateCustomFieldOptions
    {
        /// <summary> <see cref="UpdateSentenceCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name"> フィールド名 </param>
        public UpdateSentenceCustomFieldOptions(string name)
            : base(name)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return CoreKeyValuePairs();
        }
    }
}