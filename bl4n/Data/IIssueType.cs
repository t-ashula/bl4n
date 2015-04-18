// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssueType.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IIssueType
    {
        long Id { get; }

        long ProjectId { get; }

        string Name { get; }

        string Color { get; }

        int DisplayOrder { get; }
    }

    [DataContract]
    internal sealed class IssueType : IIssueType
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        // TODO: colors = { "#e30000", "#990000", "#934981", "#814fbc", "#2779ca", "#007e9a", "#7ea800", "#ff9200", "#ff3265", "#666665" };
        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }
    }
}