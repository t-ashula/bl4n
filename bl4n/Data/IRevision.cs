// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRevision.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> revision info (git) </summary>
    public interface IRevision
    {
        string Rev { get; }

        string Comment { get; }
    }

    [DataContract]
    internal sealed class Revision : IRevision
    {
        [DataMember(Name = "rev")]
        public string Rev { get; private set; }

        [DataMember(Name = "comment")]
        public string Comment { get; private set; }
    }
}