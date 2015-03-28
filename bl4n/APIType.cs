// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIType.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N
{
    /// <summary>API type </summary>
    public enum APIType
    {
        /// <summary> use API Key </summary>
        APIKey,

        /// <summary> use OAuth2 </summary>
        OAuth2
    }
}