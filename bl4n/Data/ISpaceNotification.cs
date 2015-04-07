// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpaceNotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface ISpaceNotification
    {
        string Content { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class SpaceNotification : ISpaceNotification
    {
        [DataMember(Name = "content")]
        public string Content { get; private set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}