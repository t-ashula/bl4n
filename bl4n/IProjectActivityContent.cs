// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> content for type 15, 16 </summary>
    public interface IProjectActivityContent : IActivityContent
    {
        IList<IUser> Users { get; }

        IList<IGroupProjectActivity> GroupProjectActivites { get; }

        string Comment { get; }
    }
}