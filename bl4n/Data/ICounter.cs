// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICounter.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> カウンターを表します． </summary>
    public interface ICounter
    {
        /// <summary> カウント値を取得します． </summary>
        long Count { get; }
    }

    [DataContract]
    internal class Counter : ICounter
    {
        [DataMember(Name = "count")]
        public long Count { get; private set; }
    }
}