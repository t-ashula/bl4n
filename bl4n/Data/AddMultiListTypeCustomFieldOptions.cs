// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddMultiListTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 複数選択リスト形式のカスタムフィールドの追加用のオプションの基底クラスを表します </summary>
    public class AddMultiListTypeCustomFieldOptions : AddListTypeCustomFieldOptions
    {
        /// <summary> <see cref="AddMultiListTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public AddMultiListTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.MultipleList, name)
        {
        }
    }
}