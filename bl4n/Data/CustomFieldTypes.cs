// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomFieldTypes.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N.Data
{
    /// <summary> カスタムフィールドの種別を表します． </summary>
    public enum CustomFieldTypes
    {
        /// <summary> 文字列 </summary>
        Text = 1,

        /// <summary> 文章 </summary>
        Sentence = 2,

        /// <summary> 数値 </summary>
        Number = 3,

        /// <summary> 日付 </summary>
        Date = 4,

        /// <summary> 単一リスト </summary>
        SingleList = 5,

        /// <summary> 複数選択可リスト </summary>
        MultipleList = 6,

        /// <summary> チェックボックス </summary>
        Checkbox = 7,

        /// <summary> ラジオ </summary>
        Radio = 8
    }
}