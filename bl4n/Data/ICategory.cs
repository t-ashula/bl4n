// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategory.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface ICategory
    {
        long Id { get; }

        int DisplayOrder { get; }

        string Name { get; }
    }

    [DataContract]
    internal sealed class Category : ICategory
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }
    }
}