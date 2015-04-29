// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 文字列形式のカスタムフィールドを表します． </summary>
    public class TextTypeCustomField : TypedCustomField
    {
        /// <summary> <see cref="ListTypeCustomField"/> のインスタンスを初期化します </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        public TextTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false)
            : base(fieldname, applicableIssueTypes, description, required)
        {
        }

        /// <inheritdoc/>
        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Text; }
        }
    }
}