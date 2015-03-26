// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProject.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BL4N.Data
{
    /// <summary> project  </summary>
    public interface IProject
    {
        int Id { get; }

        string ProjectKey { get; }

        string Name { get; }

        bool ChartEnabled { get; }

        bool SubtaskingEnabled { get; }

        string TextFormattingRule { get; }

        bool Archived { get; }

        int DisplayOrder { get; }
    }
}