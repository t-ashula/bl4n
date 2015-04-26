// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N
{
    internal static class CollectionExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs<T>(this IEnumerable<T> list, string key)
        {
            return list.Select(v => new KeyValuePair<string, string>(key, string.Format("{0}", v)));
        }
    }
}