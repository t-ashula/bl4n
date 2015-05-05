// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// The number type custom field.
    /// </summary>
    public class NumberTypeCustomField : TypedCustomField
    {
        /// <summary> <see cref="MultiListTypeCustomField"/> のインスタンスを初期化します． </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        /// <param name="minValue">最小値</param>
        /// <param name="maxValue">最大値</param>
        /// <param name="initialValue">初期値</param>
        /// <param name="unit">単位</param>
        public NumberTypeCustomField(
            string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            double? minValue = null, double? maxValue = null, double? initialValue = null, string unit = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            InitailValue = initialValue;
            Unit = unit;
        }

        /// <inheritdoc/>
        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Number; }
        }

        /// <summary> 最小値を取得または設定します． </summary>
        public double? MinValue { get; set; }

        /// <summary> 最小値を取得または設定します． </summary>
        public double? MaxValue { get; set; }

        /// <summary> 初期値を取得または設定します． </summary>
        public double? InitailValue { get; set; }

        /// <summary> 単位を取得または設定します． </summary>
        public string Unit { get; set; }
    }
}