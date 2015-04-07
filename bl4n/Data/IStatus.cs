// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatus.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IStatus
    {
        long Id { get; }

        string Name { get; }
    }

    [DataContract]
    internal sealed class Status : IStatus
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}