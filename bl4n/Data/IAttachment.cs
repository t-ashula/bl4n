// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttachment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BL4N.Data
{
    /// <summary> attachments, </summary>
    public interface IAttachment
    {
        long Id { get; }

        string Name { get; }

        long Size { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }
    }
}