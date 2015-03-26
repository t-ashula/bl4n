// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUser.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N.Data
{
    /// <summary> user  </summary>
    public interface IUser
    {
        long Id { get; }

        string UserId { get; }

        string Name { get; }

        int RoleType { get; }

        string Lang { get; }

        string MailAddress { get; }
    }
}