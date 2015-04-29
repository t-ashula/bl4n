// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// リスト形式のカスタムフィールドの基底クラスを表します．
    /// </summary>
    public abstract class ListTypeCustomField : TypedCustomField
    {
        /// <summary> <see cref="ListTypeCustomField"/> のインスタンスを初期化します </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        /// <param name="items">選択肢一覧文字列</param>
        /// <param name="allowInput">その他直接入力を許可のとき true</param>
        /// <param name="allowAddItem">項目の追加を許可のとき true</param>
        protected ListTypeCustomField(
            string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            List<string> items = null, bool? allowInput = null, bool? allowAddItem = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            Items = items;
            AllowInput = allowInput;
            AllowAddItem = allowAddItem;
        }

        /// <summary> 選択肢一覧文字列を取得します． </summary>
        public List<string> Items { get; private set; }

        /// <summary> その他直接入力を許可するかどうかを取得または設定します． </summary>
        public bool? AllowInput { get; set; }

        /// <summary> 項目の追加を許可するかどうかを取得または設定します． </summary>
        public bool? AllowAddItem { get; set; }
    }
}