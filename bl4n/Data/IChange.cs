// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChange.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> issue change info </summary>
    public interface IChange
    {
        /// <summary> 変更されたフィールド名を取得します． </summary>
        string Field { get; }

        /// <summary> 変更後の値を取得します． </summary>
        string NewValue { get; }

        /// <summary> 変更前の値を取得します． </summary>
        string OldValue { get; }

        /// <summary> 種別を取得します． </summary>
        string Type { get; }
    }

    /// <summary> </summary>
    [DataContract]
    internal sealed class Change : ExtraJsonPropertyReadableObject, IChange
    {
        [DataMember(Name = "field")]
        public string Field { get; private set; }

        [DataMember(Name = "new_value")]
        public string NewValue { get; private set; }

        [DataMember(Name = "old_value")]
        public string OldValue { get; private set; }

        [DataMember(Name = "type")]
        public string Type { get; private set; }
    }
}