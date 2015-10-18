// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPriority.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 優先度を表します </summary>
    public interface IPriority
    {
        /// <summary> IDを取得します． </summary>
        long Id { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }
    }

    [DataContract]
    internal sealed class Priority : ExtraJsonPropertyReadableObject, IPriority
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}