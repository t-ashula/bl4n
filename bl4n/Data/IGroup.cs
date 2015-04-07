using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IGroup
    {
        long Id { get; }

        string Name { get; }

        IList<IUser> Members { get; }

        IUser CreatedUser { get; }

        int DisplayOrder { get; }

        DateTime Created { get; }

        IUser UpdatedUser { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class Group : IGroup
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "members")]
        private List<User> _members;

        [IgnoreDataMember]
        public IList<IUser> Members
        {
            get { return _members.ToList<IUser>(); }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }

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