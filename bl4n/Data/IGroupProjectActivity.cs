// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGroupProjectActivity.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> グループの更新情報を表します </summary>
    public interface IGroupProjectActivity
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 種別を取得します． </summary>
        int Type { get; }
    }

    [DataContract]
    internal sealed class GroupProjectActivity : ExtraJsonPropertyReadableObject, IGroupProjectActivity
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "type")]
        public int Type { get; private set; }
    }
}