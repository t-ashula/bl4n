// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityConverter.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BL4N.Data;
using Newtonsoft.Json.Linq;

namespace BL4N
{
    /// <summary> <see cref="Activity"/> へのコンバータを表します </summary>
    internal class ActivityConverter : AbstractJsonConverter<Activity>
    {
        /// <inheritdoc/>
        protected override Activity Create(Type objectType, JObject jobj)
        {
            if (!FieldExists(jobj, "type", JTokenType.Integer))
            {
                throw new InvalidOperationException();
            }

            var activityType = jobj["type"].Value<int>();

            // XXX: use enum?
            switch (activityType)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return new IssueActivity();

                case 5:
                case 6:
                case 7:
                    return new WikiActivity();

                case 8:
                case 9:
                case 10:
                    return new FileActivity();

                case 11:
                    return new SVNRepositoryActivity();

                case 12:
                case 13:
                    return new GitRepositoryActivity();

                case 14:
                    return new BulkUpdateActivity();

                case 15:
                case 16:
                    return new ProjectActivity();

                case 17:
                    return new NotifyActivity();
            }

            throw new InvalidOperationException();
        }
    }
}