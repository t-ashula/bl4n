// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISVNRepositoryActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BL4N.Data
{
    /// <summary> content for type 11 </summary>
    public interface ISVNRepositoryActivityContent : IActivityContent
    {
        long Rev { get; }

        string Comment { get; }
    }
}