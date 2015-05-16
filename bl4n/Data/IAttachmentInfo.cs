// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttachmentInfo.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 添付ファイル情報を表します </summary>
    public interface IAttachmentInfo
    {
        /// <summary> ID を取得します </summary>
        long Id { get; }

        /// <summary> ファイル名を取得します </summary>
        string Name { get; }
    }

    [DataContract]
    internal class AttachmentInfo : IAttachmentInfo
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}