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
    /// <summary> カテゴリーを表します． </summary>
    public interface ICategory
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 表示順を取得します． </summary>
        int DisplayOrder { get; }

        /// <summary> 名前を取得します． </summary>
        string Name { get; }
    }

    [DataContract]
    internal sealed class Category : ICategory
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }
    }
}