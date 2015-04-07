// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILink.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> bulk update linkage info </summary>
    public interface ILink
    {
        long Id { get; }

        long KeyId { get; }

        string Title { get; }
    }

    [DataContract]
    internal sealed class Link : ILink
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "key_id")]
        public long KeyId { get; private set; }

        [DataMember(Name = "title")]
        public string Title { get; private set; }
    }
}