// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Space.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for Space API </summary>
    public partial class Backlog
    {
        /// <summary> space �����擾���܂� </summary>
        /// <returns> <see cref="ISpace"/>. </returns>
        public ISpace GetSpace()
        {
            var uri = GetApiUri(new[] { "space" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var space = GetApiResult<Space>(uri, jss);
            return space.Result;
        }

        /// <summary> �ŋ߂̍X�V�̈ꗗ���擾���܂� </summary>
        /// <param name="filter">activity filtering option</param>
        /// <returns> <see cref="IActivity"/> �̃��X�g</returns>
        public List<IActivity> GetSpaceActivities(RecentUpdateFilterOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "space", "activities" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
        }

        /// <summary> �X�y�[�X�̃��S���擾���܂� </summary>
        /// <returns> <see cref="ILogo"/> </returns>
        public ILogo GetSpaceImage()
        {
            var api = GetApiUri(new[] { "space", "image" });
            var res = GetApiResultAsFile(api).Result;
            return new Logo(res.Item1, res.Item2);
        }

        /// <summary> �X�y�[�X�̂��m�点���擾���܂� </summary>
        /// <returns> <see cref="ISpaceNotification"/> </returns>
        public ISpaceNotification GetSpaceNotifiacation()
        {
            var api = GetApiUri(new[] { "space", "notification" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<SpaceNotification>(api, jss);
            return res.Result;
        }

        /// <summary> �X�y�[�X�̂��m�点���X�V���܂� </summary>
        /// <param name="content"> ���m�点�Ƃ��Đݒ肷�镶���� </param>
        /// <returns> <see cref="IActivity"/> �̃��X�g</returns>
        public ISpaceNotification UpdateSpaceNotification(string content)
        {
            var api = GetApiUri(new[] { "space", "notification" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var hc = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("content", content) });
            var res = PutApiResult<SpaceNotification>(api, hc, jss);
            return res.Result;
        }

        /// <summary> Get Space Disk Usage </summary>
        /// <returns> <see cref="IDiskUsage"/> </returns>
        public IDiskUsage GetSpaceDiskUsage()
        {
            var api = GetApiUri(new[] { "space", "diskUsage" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<DiskUsage>(api, jss);
            return res.Result;
        }

        /// <summary> add sttachment to space </summary>
        /// <param name="name">file name</param>
        /// <param name="content">file content</param>
        /// <returns> <see cref="IAttachment"/>(without CreatedUser and Created ) </returns>
        public IAttachment AddAttachment(string name, Stream content)
        {
            var api = GetApiUri(new[] { "space", "attachment" });
            var hc = new MultipartFormDataContent();
            var filename = name.Contains(Path.PathSeparator) ? Path.GetFileName(name) : name;
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = "content.dat";
            }

            hc.Add(new StreamContent(content), @"""file""", "\"" + filename + "\"");
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = PostApiResult<Attachment>(api, hc, jss);
            return res.Result;
        }
    }
}