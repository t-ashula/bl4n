// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectUpdate.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IProjectUpdate
    {
        IProject Project { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class ProjectUpdate : IProjectUpdate
    {
        [DataMember(Name = "project")]
        private Project _project;

        [IgnoreDataMember]
        public IProject Project
        {
            get { return _project; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}