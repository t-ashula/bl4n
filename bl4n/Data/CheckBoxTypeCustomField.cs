// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> チェックボックスのカスタムフィールドを表します </summary>
    public class CheckBoxTypeCustomField : ListTypeCustomField
    {
        /// <summary> <see cref="CheckBoxTypeCustomField"/> のインスタンスを初期化します． </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別の ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        /// <param name="items">選択肢一覧文字列</param>
        /// <param name="allowInput">その他直接入力を許可のとき true</param>
        /// <param name="allowAddItem">項目の追加を許可のとき true</param>
        public CheckBoxTypeCustomField(
            string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            List<string> items = null, bool? allowInput = null, bool? allowAddItem = null)
            : base(fieldname, applicableIssueTypes, description, required, items, allowInput, allowAddItem)
        {
        }

        /// <inheritdoc/>
        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Checkbox; }
        }
    }
}