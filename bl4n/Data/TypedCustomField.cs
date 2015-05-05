// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypedCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> カスタムフィールドの基底クラスを表します． </summary>
    public abstract class TypedCustomField
    {
        /// <summary> フィールド種別 <see cref="CustomFieldTypes"/> を取得します． </summary>
        public abstract CustomFieldTypes TypeId { get; }

        /// <summary> フィールド名を取得または設定します． </summary>
        public string Name { get; set; }

        /// <summary> 適用可能な課題種別ID を取得または設定します． </summary>
        public long[] ApplicableIssueTypes { get; set; }

        /// <summary> フィールドの説明文字列を取得または設定します． </summary>
        public string Description { get; set; }

        /// <summary> フィールドが必須かどうかを取得または設定します． </summary>
        public bool Required { get; set; }

        /// <summary> <see cref="TypedCustomField"/> のインスタンスを初期化します． </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true </param>
        protected TypedCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false)
        {
            Name = fieldname;
            ApplicableIssueTypes = applicableIssueTypes;
            Description = description;
            Required = required;
        }
    }
}