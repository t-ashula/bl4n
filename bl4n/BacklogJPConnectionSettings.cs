// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogJPConnectionSettings.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N
{
    [DataContract]
    public sealed class BacklogJPConnectionSettings : BacklogConnectionSettings
    {
        public BacklogJPConnectionSettings(string spaceName, APIType apiType, string apiKey)
            : base(spaceName, apiType, apiKey, spaceName + ".backlog.jp", 443, true)
        {
        }

        public override bool IsValid()
        {
            // only apikey type support, now
            return APIType == APIType.APIKey
                && (!string.IsNullOrWhiteSpace(SpaceName) && !string.IsNullOrWhiteSpace(APIKey));
        }
    }
}