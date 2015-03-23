// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBacklogConnectionSettings.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BL4N
{
    public interface IBacklogConnectionSettings
    {
        string SpaceName { get; }

        string HostName { get; }

        string APIKey { get; }

        APIType APIType { get; }
    }
}