// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationInfo.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 通知情報を表します </summary>
    public interface INotificationInfo
    {
        /// <summary> 通知の種類を取得します </summary>
        string Type { get; }
    }

    [DataContract]
    internal class NofiticationInfo : ExtraJsonPropertyReadableObject, INotificationInfo
    {
        [DataMember(Name = "type")]
        public string Type { get; private set; }
    }
}