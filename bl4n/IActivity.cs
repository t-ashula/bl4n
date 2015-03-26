// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IActivity.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> activity </summary>
    public interface IActivity
    {
        long Id { get; }

        IProject Project { get; }

        int Type { get; }

        IActivityContent Content { get; }

        IList<INotification> Notifications { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }
    }
}