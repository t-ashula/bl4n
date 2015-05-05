// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleListTypeCustomeField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 単数選択リスト形式のカスタムフィールドを表します． </summary>
    public class SingleListTypeCustomeField : ListTypeCustomField
    {
        /// <summary> <see cref="MultiListTypeCustomField"/> のインスタンスを初期化します． </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        /// <param name="items">選択肢一覧</param>
        /// <param name="allowInput">その他直接入力を許可のとき true</param>
        /// <param name="allowAddItem">項目の追加を許可のとき true</param>
        public SingleListTypeCustomeField(
            string fieldname, long[] applicableIssueTypes, string description, bool required,
            List<string> items = null, bool? allowInput = null, bool? allowAddItem = null)
            : base(fieldname, applicableIssueTypes, description, required, items, allowInput, allowAddItem)
        {
        }

        /// <inheritdoc/>
        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.SingleList; }
        }
    }
}