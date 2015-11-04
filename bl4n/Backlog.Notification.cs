// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Notification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for Notification API </summary>
    public partial class Backlog
    {
        /// <summary>
        /// Get Notification
        /// Returns own notifications.
        /// </summary>
        /// <returns>list of <see cref="INotification"/></returns>
        public IList<INotification> GetNotifications()
        {
            var api = GetApiUri(new[] { "notifications" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Notification>>(api, jss);
            return res.Result.ToList<INotification>();
        }

        /// <summary>
        /// Count Notification
        /// Returns number of Notifications.
        /// </summary>
        /// <param name="options">filter option</param>
        /// <returns>count of notifications</returns>
        public ICounter GetNotificationsCount(NotificationsCountOptions options)
        {
            var query = options.ToKeyValuePairs();
            var api = GetApiUri(new[] { "notifications", "count" }, query);
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<Counter>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Reset Unread Notification Count
        /// Resets unread Notification count.
        /// </summary>
        /// <returns>count of notifications</returns>
        public ICounter ResetUnreadNotificationCount()
        {
            var api = GetApiUri(new[] { "notifications", "markAsRead" });
            var jss = new JsonSerializerSettings();
            var hc = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
            var res = PostApiResult<Counter>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Read Notification
        /// Changes notifications read.
        /// </summary>
        /// <param name="id">notification id</param>
        /// <remarks>TODO: async? </remarks>
        public void ReadNotification(long id)
        {
            var api = GetApiUri(new[] { "notifications", id.ToString(), "markAsRead" });
            var ua = new HttpClient();
            var s = ua.PostAsync(api, new StringContent(string.Empty)); // no body
            var res = s.Result.Content;
            System.Diagnostics.Trace.Write(string.Format("ReadNotification({0}) = {1}", id, res));
        }
    }
}