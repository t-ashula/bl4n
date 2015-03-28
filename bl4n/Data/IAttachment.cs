// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttachment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> attachments, </summary>
    public interface IAttachment
    {
        long Id { get; }

        string Name { get; }

        long Size { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }
    }

    [DataContract]
    internal sealed class Attachement : IAttachment
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "id")]
        public string Name { get; private set; }

        [DataMember(Name = "id")]
        public long Size { get; private set; }

        [DataMember(Name = "id")]
        private User _createdUser;

        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "id")]
        public DateTime Created { get; private set; }
    }

    [CollectionDataContract]
    internal sealed class Attachments : List<Attachement>
    {
    }
}