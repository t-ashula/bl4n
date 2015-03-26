// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N.Data
{
    /// <summary> content for type  8, 9, 10 </summary>
    public interface IFileActivityContent : IActivityContent
    {
        long Id { get; }

        string Dir { get; }

        string Name { get; }

        long Size { get; }
    }
}