// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueSearchOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// Issue Search Options
    /// </summary>
    public class IssueSearchOptions
    {
        /// <summary> convert search options to KeyValuePair </summary>
        /// <returns> key value pairs </returns>
        public List<KeyValuePair<string, string>> ToKeyValues()
        {
            return new List<KeyValuePair<string, string>>();
        }
    }
}