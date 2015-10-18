// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> カスタムフィールドを表します </summary>
    public interface ICustomField
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> フィールドの種別を取得します． </summary>
        long TypeId { get; }

        /// <summary> フィールド名を取得します． </summary>
        string Name { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> 必須かどうかを取得します． </summary>
        bool Required { get; }

        /// <summary> 適用可能な課題種別のリストを取得します． </summary>
        IList<long> ApplicableIssueTypes { get; }

        /// <summary> 追加可能かどうかを取得します． </summary>
        bool AllowAddItem { get; }

        /// <summary> カスタムフィールドの項目のリストを取得します． </summary>
        IList<ICustomFieldItem> Items { get; }
    }

    [DataContract]
    internal sealed class CustomField : ExtraJsonPropertyReadableObject, ICustomField
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "typeId")]
        public long TypeId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "required")]
        public bool Required { get; private set; }

        [DataMember(Name = "applicableIssueTypes")]
        private List<long> _applicableIssueTypes;

        [IgnoreDataMember]
        public IList<long> ApplicableIssueTypes
        {
            get { return _applicableIssueTypes; }
        }

        [DataMember(Name = "allowAddItem")]
        public bool AllowAddItem { get; private set; }

        [DataMember(Name = "items")]
        private List<CustomFieldItem> _items;

        [IgnoreDataMember]
        public IList<ICustomFieldItem> Items
        {
            get { return _items.ToList<ICustomFieldItem>(); }
        }
    }
}