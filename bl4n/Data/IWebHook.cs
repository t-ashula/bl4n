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

        string HookUrl { get; }

        bool AllEvent { get; }

        IList<int> ActivityTypeIds { get; }

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

        [DataMember(Name = "hookUrl")]
        public string HookUrl { get; set; }

        [DataMember(Name = "allEvent")]
        public bool AllEvent { get; set; }

        [DataMember(Name = "activityTypeIds")]
        private List<int> _activityTypeIds;

        [IgnoreDataMember]
        public IList<int> ActivityTypeIds
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

        /// <summary> add activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
        /// <remarks> update <see cref="AllEvent"/> flag</remarks>
        public void AddActivityTypes(IEnumerable<ActivityType> types)
        {
            if (_activityTypeIds == null)
            {
                _activityTypeIds = new List<int>();
            }

            _activityTypeIds = _activityTypeIds.Select(t => (ActivityType)t).Union(types.Where(t => t != ActivityType.Unknown).OrderBy(_ => _)).Select(t => (int)t).ToList();
            AllEvent = !_activityTypeIds.Any();
        }

        /// <summary> remove activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
        /// <remarks> update <see cref="AllEvent"/> flag</remarks>
        public void RemoveActivityTypes(IEnumerable<ActivityType> types)
        {
            if (_activityTypeIds == null)
            {
                return;
            }

            foreach (var t in types.Distinct())
            {
                _activityTypeIds.Remove((int)t);
            }

            AllEvent = !_activityTypeIds.Any();
        }
    }
}