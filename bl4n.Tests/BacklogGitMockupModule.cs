// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogGitMockupModule.cs">
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
    /// /api/v2/git roting module
    /// </summary>
    public class BacklogGitMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/git routing
        /// </summary>
        public BacklogGitMockupModule() :
            base("/api/v2/git")
        {
            #region /api/v2/git/repositories

            Get["/repositories"] = p =>
            {
                string o = Request.Query["projectIdOrKey"];
                long pid;
                if (!long.TryParse(o, out pid))
                {
                    pid = 151;
                }

                return Response.AsJson(new[]
                {
                    new
                    {
                        id = 1,
                        projectId = pid,
                        name = "app",
                        description = string.Empty,
                        hookUrl = string.Empty,
                        httpUrl = "https://xx.backlogtool.com/git/BLG/app.git",
                        sshUrl = "xx@xx.git.backlogtool.com:/BLG/app.git",
                        displayOrder = 0,
                        //// pushedAt = null,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2013-05-30T09:11:36Z",
                        updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        updated = "2013-05-30T09:11:36Z"
                    }
                });
            };

            #endregion
        }
    }
}