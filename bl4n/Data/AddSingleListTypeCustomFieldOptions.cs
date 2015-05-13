// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddSingleListTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 単一選択リスト形式のカスタムフィールドの追加用のオプションの基底クラスを表します </summary>
    public class AddSingleListTypeCustomFieldOptions : AddListTypeCustomFieldOptions
    {
        /// <summary> <see cref="AddSingleListTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public AddSingleListTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.SingleList, name)
        {
        }
    }
}