// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateSingleListTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 単一選択リストタイプのカスタムフィールドの更新オプションを表します </summary>
    public class UpdateSingleListTypeCustomFieldOptions : UpdateListTypeCustomFieldOptions
    {
        /// <summary> <see cref="UpdateSingleListTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public UpdateSingleListTypeCustomFieldOptions(string name)
            : base(name)
        {
        }
    }
}