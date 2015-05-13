// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCheckBoxTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> チェックボックス形式のカスタムフィールドの追加用のオプションの基底クラスを表します </summary>
    public class AddCheckBoxTypeCustomFieldOptions : AddListTypeCustomFieldOptions
    {
        /// <summary> <see cref="AddCheckBoxTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public AddCheckBoxTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.Checkbox, name)
        {
        }
    }
}