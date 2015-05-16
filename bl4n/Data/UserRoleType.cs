// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRoleType.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> ユーザー権限を表します </summary>
    public enum UserRoleType
    {
        /// <summary> Administrator(1) </summary>
        Administrator = 1,

        /// <summary> Normal User(2) </summary>
        NormalUser = 2,

        /// <summary> Reporter(3) </summary>
        Reporter = 3,

        /// <summary> Viewer(4) </summary>
        Viewer = 4,

        /// <summary> Guest Reporter(5) </summary>
        GuestReporter = 5,

        /// <summary> Guest Viewer(6) </summary>
        GuestViewer = 6
    }
}