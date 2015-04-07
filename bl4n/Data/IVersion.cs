// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVersion.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IVersion
    {
        long Id { get; }

        long ProjectId { get; }

        string Name { get; }

        string Description { get; }

        DateTime StartDate { get; }

        DateTime ReleaseDueDate { get; }

        bool Archived { get; }

        int DisplayOrder { get; }
    }

    [DataContract]
    internal sealed class Version : IVersion
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; private set; }

        [DataMember(Name = "releaseDueDate")]
        public DateTime ReleaseDueDate { get; private set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; private set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; private set; }
    }
}