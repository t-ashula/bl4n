// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomFieldItem.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface ICustomFieldItem
    {
        long Id { get; }

        string Name { get; }

        int DisplayOrder { get; }
    }

    [DataContract]
    internal class CustomFieldItem : ICustomFieldItem
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }
    }
}