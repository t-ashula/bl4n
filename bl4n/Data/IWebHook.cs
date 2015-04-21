// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebHook.cs">
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
    public interface IWebHook
    {
        long Id { get; }

        string Name { get; }

        string Description { get; }

        bool AllEvent { get; }

        IList<long> ActivityTypeIds { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }

        IUser UpdatedUser { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal class WebHook : IWebHook
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "allEvent")]
        public bool AllEvent { get; set; }

        [DataMember(Name = "activityTypeIds")]
        private List<long> _activityTypeIds;

        [IgnoreDataMember]
        public IList<long> ActivityTypeIds
        {
            get { return _activityTypeIds; }
        }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}