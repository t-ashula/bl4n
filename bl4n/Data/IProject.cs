// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProject.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> project; library -> user app  </summary>
    public interface IProject
    {
        long Id { get; }

        string ProjectKey { get; }

        string Name { get; }

        bool ChartEnabled { get; }

        bool SubtaskingEnabled { get; }

        string TextFormattingRule { get; }

        bool Archived { get; }

        int DisplayOrder { get; }
    }

    /// <summary> project; api -> library </summary>
    [DataContract]
    internal sealed class Project : IProject
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectKey")]
        public string ProjectKey { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "chartEnabled")]
        public bool ChartEnabled { get; set; }

        [DataMember(Name = "subtaskingEnabled")]
        public bool SubtaskingEnabled { get; set; }

        [DataMember(Name = "textFormattingRule")]
        public string TextFormattingRule { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }
    }
}