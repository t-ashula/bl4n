// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N.Data
{
    /// <summary> comment with id for type 17 </summary>
    public interface IComment
    {
        long Id { get; }

        string Content { get; }
    }
}