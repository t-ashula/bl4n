// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 日付のカスタムフィールドを表します </summary>
    public class DateTypeCustomField : TypedCustomField
    {
        /// <summary> <see cref="CheckBoxTypeCustomField"/> のインスタンスを初期化します． </summary>
        /// <param name="fieldname">フィールド名</param>
        /// <param name="applicableIssueTypes">適用可能な課題種別の ID</param>
        /// <param name="description">説明</param>
        /// <param name="required">必須のとき true</param>
        /// <param name="firstDate">開始日</param>
        /// <param name="lastDate">最終日</param>
        /// <param name="initialValueType">初期値種別（1:当日 2: 当日 + initialShift 3:指定日）</param>
        /// <param name="initialDate">初期値</param>
        /// <param name="initialShift">差分日数</param>
        public DateTypeCustomField(
            string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            string firstDate = null, string lastDate = null, int? initialValueType = null, string initialDate = null, int? initialShift = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            FirstDate = firstDate;
            LastDate = lastDate;
            InitialValueType = initialValueType;
            InitialDate = initialDate;
            InitailShift = initialShift;
        }

        /// <summary> カスタムフィールドの種別を取得します． </summary>
        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Date; }
        }

        /// <summary> 開始日を取得します． </summary>
        public string FirstDate { get; set; }

        /// <summary> 最終日を取得します． </summary>
        public string LastDate { get; set; }

        /// <summary> 初期値種別を取得します． </summary>
        public int? InitialValueType { get; set; }

        /// <summary> 初期値を取得します． </summary>
        public string InitialDate { get; set; }

        /// <summary> 差分日数を取得します． </summary>
        public int? InitailShift { get; set; }
    }
}