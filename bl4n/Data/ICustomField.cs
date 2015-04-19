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
    public interface ICustomField
    {
        long Id { get; }

        long TypeId { get; }

        string Name { get; }

        string Description { get; }

        bool Required { get; }

        IList<long> ApplicableIssueTypes { get; }

        bool AllowAddItem { get; }

        IList<ICustomFieldItem> Items { get; }
    }

    [DataContract]
    internal sealed class CustomField : ICustomField
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