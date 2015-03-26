// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChange.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N.Data
{
    /// <summary> issue change info </summary>
    public interface IChange
    {
        string Field { get; }

        string NewValue { get; }

        string OldValue { get; }

        string Type { get; }
    }
}