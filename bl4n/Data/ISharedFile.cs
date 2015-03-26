// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISharedFile.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace BL4N.Data
{
    /// <summary> shared_files, </summary>
    public interface ISharedFile
    {
        long Id { get; }

        string Type { get; }

        string Name { get; }

        string Dir { get; }

        long Size { get; }

        IUser CreatedUser { get; }

        DateTime Created { get; }
    }
}