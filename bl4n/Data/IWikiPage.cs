// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiPage.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IWikiPage
    {
        long Id { get; }

        long ProjectId { get; }

        string Name { get; }

        IList<ITag> Tags { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }

        IUser UpdatedUser { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class WikiPage : IWikiPage
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "tags")]
        private List<Tag> _tags;

        [IgnoreDataMember]
        public IList<ITag> Tags
        {
            get { return _tags.ToList<ITag>(); }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}