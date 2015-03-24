// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogTestsForRealServer.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> スペース情報の公開インタフェース </summary>
    public interface ISpace
    {
        string SpaceKey { get; set; }

        string Name { get; set; }

        long OwnerId { get; set; }

        string Lang { get; set; }

        // XXX: use System.TimeZone?
        string Timezone { get; set; }

        string ReportSendTime { get; set; }

        string TextFormattingRule { get; set; }

        DateTime Created { get; set; }

        DateTime Updated { get; set; }
    }

    /// <summary> スペース情報の内部用データクラス API との serialize 用 </summary>
    [DataContract]
    internal class Space : ISpace
    {
        [DataMember(Name = "spaceKey")]
        public string SpaceKey { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "ownerId")]
        public long OwnerId { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }

        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }

        [DataMember(Name = "reportSendTime")]
        public string ReportSendTime { get; set; }

        [DataMember(Name = "textFormattingRule")]
        public string TextFormattingRule { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}