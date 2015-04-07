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
        string Field { get; }

        string NewValue { get; }

        string OldValue { get; }

        string Type { get; }
    }

    /// <summary> </summary>
    [DataContract]
    internal sealed class Change : IChange
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