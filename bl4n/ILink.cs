// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILink.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BL4N.Data
{
    /// <summary> bulk update linkage info </summary>
    public interface ILink
    {
        long Id { get; }

        long KeyId { get; }

        string Title { get; }
    }
}