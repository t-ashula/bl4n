// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddWebHookOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加する WebHook のオプションを表します </summary>
    public class AddWebHookOptions : OptionalParams
    {
        private string _description;
        private string _hookUrl;
        private bool _allEvent;
        private List<ActivityType> _activityTypeIds;

        /// <summary> WebHook 名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> <see cref="AddWebHookOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name"> WebHook 名</param>
        public AddWebHookOptions(string name)
        {
            Name = name;
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("name", Name) };
            if (IsPropertyChanged("description"))
            {
                pairs.Add(new KeyValuePair<string, string>("description", Description));
            }

            if (IsPropertyChanged("hookUrl"))
            {
                pairs.Add(new KeyValuePair<string, string>("hookUrl", HookUrl));
            }

            if (IsPropertyChanged("allEvent"))
            {
                pairs.Add(new KeyValuePair<string, string>("allEvent", AllEvent ? "true" : "false"));
            }

            if (!AllEvent && IsPropertyChanged("activityTypeIds[]"))
            {
                pairs.AddRange(ActivityTypeIds.Select(a => (int)a).ToKeyValuePairs("activityTypeIds[]"));
            }

            return pairs;
        }

        /// <summary> add activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
        /// <remarks> update <see cref="AllEvent"/> flag</remarks>
        public void AddActivityTypes(IEnumerable<ActivityType> types)
        {
            var ids = new List<ActivityType>(types);
            if (_activityTypeIds != null)
            {
                ids.AddRange(_activityTypeIds);
            }

            ActivityTypeIds = ids.Distinct().ToList();
            AllEvent = !ActivityTypeIds.Any();
        }

        /// <summary> remove activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
        /// <remarks> update <see cref="AllEvent"/> flag</remarks>
        public void RemoveActivityTypes(IEnumerable<ActivityType> types)
        {
            if (ActivityTypeIds == null)
            {
                return;
            }

            var ids = ActivityTypeIds.Distinct().ToList();
            foreach (var t in types.Distinct())
            {
                ids.Remove(t);
            }

            ActivityTypeIds = ids;
            AllEvent = !ActivityTypeIds.Any();
        }

        /// <summary> 通知するイベントの一覧を取得します </summary>
        public List<ActivityType> ActivityTypeIds
        {
            get { return _activityTypeIds; }
            private set
            {
                _activityTypeIds = value;
                PropertyChanged("activityTypeIds[]");
            }
        }

        /// <summary> 全部のイベントに反応するかどうかを取得または設定します </summary>
        public bool AllEvent
        {
            get { return _allEvent; }
            set
            {
                _allEvent = value;
                PropertyChanged("allEvent");
            }
        }

        /// <summary> Hook URL を取得または設定します </summary>
        public string HookUrl
        {
            get { return _hookUrl; }
            set
            {
                _hookUrl = value;
                PropertyChanged("hookUrl");
            }
        }

        /// <summary> WebHook の説明を取得または設定します </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged("description");
            }
        }
    }
}