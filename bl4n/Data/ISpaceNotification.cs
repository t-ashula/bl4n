// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpaceNotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> スペースのお知らせを表します </summary>
    public interface ISpaceNotification
    {
        /// <summary> お知らせ文字列を取得します． </summary>
        string Content { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class SpaceNotification : ISpaceNotification
    {
        [DataMember(Name = "content")]
        public string Content { get; private set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}