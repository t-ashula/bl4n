// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestUtils.cs" company="">
//   bl4n - Backlog.jp API Client library
//   //   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Tests
{
    internal static class RequestUtils
    {
        /// <summary> change  comma separated request value into list of number (long)  </summary>
        /// <param name="req"> request value </param>
        /// <returns> list of long or empty list </returns>
        public static IEnumerable<long> ToIds(string req)
        {
            return string.IsNullOrEmpty(req) ? new long[0] : req.Split(',').Select(long.Parse);
        }
    }
}