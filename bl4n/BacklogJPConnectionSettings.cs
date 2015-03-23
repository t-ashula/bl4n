// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogJPConnectionSettings.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N
{
    public sealed class BacklogJPConnectionSettings : IBacklogConnectionSettings
    {
        public BacklogJPConnectionSettings(string spaceName, string apiKey)
        {
            SpaceName = spaceName;
            APIKey = apiKey;
            APIType = APIType.APIKey;
        }

        public string SpaceName { get; private set; }

        public string HostName
        {
            get { return SpaceName + ".backlog.jp"; }
        }

        public string APIKey { get; private set; }

        public APIType APIType { get; private set; }
    }
}