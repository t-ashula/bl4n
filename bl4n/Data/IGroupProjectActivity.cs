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
    /// <summary> </summary>
    public interface IGroupProjectActivity
    {
        long Id { get; }

        int Type { get; }
    }

    [DataContract]
    internal sealed class GroupProjectActivity : IGroupProjectActivity
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "type")]
        public int Type { get; private set; }
    }
}