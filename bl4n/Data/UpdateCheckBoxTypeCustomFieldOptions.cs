// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCheckBoxTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> チェックボックスタイプのカスタムフィールドの更新オプションを表します </summary>
    public class UpdateCheckBoxTypeCustomFieldOptions : UpdateListTypeCustomFieldOptions
    {
        /// <summary> <see cref="UpdateCheckBoxTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public UpdateCheckBoxTypeCustomFieldOptions(string name)
            : base(name)
        {
        }
    }
}