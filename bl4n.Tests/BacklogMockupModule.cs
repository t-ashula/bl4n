// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// Nancy の http hander module
    /// </summary>
    public class BacklogMockupModule : NancyModule
    {
        /// <summary> <see cref="BacklogMockupModule"/> を初期化します </summary>
        public BacklogMockupModule()
            : base("/api/v2")
        {
            #region /statuses

            Get["/statuses"] = _ => Response.AsJson(new[]
            {
                new { id = 1, name = "Open" },
                new { id = 2, name = "In Progress" },
                new { id = 3, name = "Resolved" },
                new { id = 4, name = "Closed" }
            });

            #endregion

            #region /resolutions

            Get["/resolutions"] = _ => Response.AsJson(new[]
            {
                new { id = 0, name = "Fixed" },
                new { id = 1, name = "Won't Fix" },
                new { id = 2, name = "Invalid" },
                new { id = 3, name = "Duplication" },
                new { id = 4, name = "Cannot Reproduce" }
            });

            #endregion

            #region /priorities

            Get["/priorities"] = _ => Response.AsJson(new[]
            {
                new { id = 2, name = "High" },
                new { id = 3, name = "Normal" },
                new { id = 4, name = "Low" }
            });

            #endregion
        }
    }
}