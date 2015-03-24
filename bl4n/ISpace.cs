// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForRealServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface ISpace
    {
        string SpaceKey { get; set; }
    }

    [DataContract]
    public class Space : ISpace
    {
        [DataMember(Name = "spaceKey")]
        public string SpaceKey { get; set; }
    }
}