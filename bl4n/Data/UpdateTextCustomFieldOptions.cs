// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateTextCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> テキストタイプのカスタムフィールドの更新用オプションを表します </summary>
    public class UpdateTextCustomFieldOptions : UpdateCustomFieldOptions
    {
        /// <summary> <see cref="UpdateTextCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public UpdateTextCustomFieldOptions(string name)
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