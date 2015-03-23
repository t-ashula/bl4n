// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogConnectionSettings.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N
{
    public class BacklogConnectionSettings : IBacklogConnectionSettings
    {
        public BacklogConnectionSettings(string spaceName, string hostName, string apikey)
        {
            APIType = APIType.APIKey;
            APIKey = apikey;
            HostName = hostName;
            SpaceName = spaceName;
        }

        public string SpaceName { get; private set; }

        public string HostName { get; private set; }

        public string APIKey { get; private set; }

        public APIType APIType { get; private set; }
    }
}