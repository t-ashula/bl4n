// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BL4N.Data
{
    /// <summary> repository (git) </summary>
    public interface IRepository
    {
        long Id { get; }

        string Name { get; }

        string Description { get; }
    }
}