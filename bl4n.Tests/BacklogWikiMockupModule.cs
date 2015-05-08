// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogWikiMockupModule.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BL4N.Tests.Properties;
using Nancy;

namespace BL4N.Tests
{
    /// <summary>
    /// /api/v2/wikis routing module
    /// </summary>
    public class BacklogWikiMockupModule : NancyModule
    {
        /// <summary>
        /// /api/v2/wikis routing
        /// </summary>
        public BacklogWikiMockupModule() :
            base("/api/v2/wikis")
        {
            #region /api/v2/wikis

            Get[string.Empty] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 112,
                    projectId = Request.Query["projectIdOrKey"], // TODO
                    name = "Home",
                    tags = new[] { new { id = 12, name = "proceedings" } },
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2013-05-30T09:11:36Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2013-05-30T09:11:36Z"
                }
            });

            #endregion

            #region /api/v2/wikis/count

            Get["/count"] = p => Response.AsJson(new { count = 5 });

            #endregion

            #region /api/v2/wikis/tags

            Get["/tags"] = p => Response.AsJson(new[] { new { id = 1, name = "test" } });

            #endregion

            #region POST /api/v2/wikis

            Post[string.Empty] = p =>
            {
                string projectId = Request.Form["projectId"];
                string name = Request.Form["name"];
                string content = Request.Form["content"];
                //// bool notify = Request.Form["mailNotify"] != null;
                return Response.AsJson(new
                {
                    id = 1,
                    projectId = projectId,
                    name = name,
                    content = content,
                    tags = new[] { new { id = 12, name = "prpceedings" } },
                    //// attachments = [],
                    //// sharedFiles = [],
                    //// stars = [],
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2012-07-23T06:09:48Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2012-07-23T06:09:48Z"
                });
            };

            #endregion

            #region GET /api/v2/wikis/:id

            Get["/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = "Home",
                content = "Content",
                tags = new[] { new { id = 12, name = "prpceedings" } },
                //// attachments = [],
                //// sharedFiles = [],
                //// stars = [],
                createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2012-07-23T06:09:48Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2012-07-23T06:09:48Z"
            });

            #endregion

            #region PATCH /api/v2/wikis/:id

            Patch["/{id}"] = p =>
            {
                string name = Request.Form["name"];
                string content = Request.Form["content"];
                //// bool notify = Request.Form["mailNotify"] != null;
                return Response.AsJson(new
                {
                    id = p.id,
                    projectId = 1,
                    name = name,
                    content = content,
                    tags = new[] { new { id = 12, name = "prpceedings" } },
                    //// attachments = [],
                    //// sharedFiles = [],
                    //// stars = [],
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2012-07-23T06:09:48Z",
                    updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    updated = "2012-07-23T06:09:48Z"
                });
            };

            #endregion

            #region DELETE /api/v2/wikis/:id

            Delete["/{id}"] = p => Response.AsJson(new
            {
                id = p.id,
                projectId = 1,
                name = "Home",
                content = "Content",
                tags = new[] { new { id = 12, name = "prpceedings" } },
                //// attachments = [],
                //// sharedFiles = [],
                //// stars = [],
                createdUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2012-07-23T06:09:48Z",
                updatedUser = new { id = 1, userId = "admin", name = "admin", roleTyoe = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                updated = "2012-07-23T06:09:48Z"
            });

            #endregion

            #region /api/v2/wikis/:id/attachments

            Get["/{id}/attachments"] = p => Response.AsJson(new[]
            {
                new { id = 1, name = "IMGP0088.JPG", size = 85079 }
            });

            #endregion

            #region POST /api/v2/wikis/:id/attachments

            Post["/{id}/attachments"] = p =>
            {
                string reqFileIds = Request.Form["attachmentId[]"];
                var ids = RequestUtils.ToIds(reqFileIds).ToList();
                return Response.AsJson(new[]
                {
                    new
                    {
                        id = ids[0],
                        name = "Duke.png",
                        size = 196186,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2014-07-11T06:26:05Z"
                    }
                });
            };

            #endregion

            #region GET /api/v2/wikis/:wikiId/attachments/:attachmentid

            Get["/{wikiid}/attachments/{attachmentid}"] = p =>
            {
                var fileName = string.Format("{0}.{1}.dat", (long)p.wikiid, (long)p.attachmentid);
                var response = new Response
                {
                    ContentType = "application/octet-stream",
                    Contents = stream =>
                    {
                        var logo = Resources.projectIcon;
                        using (var ms = new MemoryStream())
                        {
                            logo.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            using (var writer = new BinaryWriter(stream))
                            {
                                writer.Write(ms.GetBuffer());
                            }
                        }
                    }
                };

                response.Headers.Add("Content-Disposition", "attachment; filename*=UTF-8''" + fileName);
                return response;
            };

            #endregion

            #region DELETE /api/v2/wikis/:id/attachments/:attachmentid

            Delete["/{wikiid}/attachments/{attachmentid}"] = p => Response.AsJson(new
            {
                id = p.attachmentid,
                name = "Duke.png",
                size = 196186,
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2014-07-11T06:26:05Z"
            });

            #endregion

            #region /api/v2/wikis/:wikiId/sharedFiles

            Get["/{wikiid}/sharedFiles"] = p => Response.AsJson(new[]
            {
                new
                {
                    id = 825952,
                    type = "file",
                    dir = "/PressRelease/20091130/",
                    name = "20091130.txt",
                    size = 4836,
                    createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                    created = "2009-11-30T01:22:21Z",
                    //// updatedUser = null,
                    updated = "2009-11-30T01:22:21Z"
                }
            });

            #endregion

            #region POST /api/v2/wikis/:wikiid/sharedFiles

            Post["/{wikiid}/sharedFiles"] = p =>
            {
                string reqFileIds = Request.Form["fileId[]"];
                var ids = RequestUtils.ToIds(reqFileIds).ToList();
                return Response.AsJson(new[]
                {
                    new
                    {
                        id = ids[0],
                        type = "file",
                        dir = "/PressRelease/20091130/",
                        name = "20091130.txt",
                        size = 4836,
                        createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                        created = "2009-11-30T01:22:21Z",
                        //// updatedUser = null,
                        updated = "2009-11-30T01:22:21Z"
                    }
                });
            };

            #endregion

            #region DELETE /api/v2/wikis/:wikiid/sharedFiles/:id

            Delete["/{wikiid}/sharedFiles/{fileid}"] = p => Response.AsJson(new
            {
                id = p.fileid,
                type = "file",
                dir = "/PressRelease/20091130/",
                name = "20091130.txt",
                size = 4836,
                createdUser = new { id = 1, userId = "admin", name = "admin", roleType = 1, lang = "ja", mailAddress = "eguchi@nulab.example" },
                created = "2009-11-30T01:22:21Z",
                //// updatedUser = null,
                updated = "2009-11-30T01:22:21Z"
            });

            #endregion
        }
    }
}