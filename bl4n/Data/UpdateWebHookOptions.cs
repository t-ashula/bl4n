// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateWebHookOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 更新する WebHook のオプションを表します </summary>
    public class UpdateWebHookOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string DescriptionProperty = "description";
        private const string HookUrlProperty = "hookUrl";
        private const string AllEventProperty = "allEvent";
        private const string ActivityTypeIdsProperty = "activityTypeIds[]";

        private string _name;
        private string _description;
        private string _hookUrl;
        private bool _allEvent;
        private List<ActivityType> _activityTypeIds;

        /// <summary> <see cref="UpdateWebHookOptions"/> のインスタンスを初期化します </summary>
        public UpdateWebHookOptions()
            : base(NameProperty, DescriptionProperty, HookUrlProperty, AllEventProperty, ActivityTypeIdsProperty)
        {
        }

        /// <summary> WebHook 名を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> WebHook の説明を取得または設定します </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(DescriptionProperty);
            }
        }

        /// <summary> Hook URL を取得または設定します </summary>
        public string HookUrl
        {
            get { return _hookUrl; }
            set
            {
                _hookUrl = value;
                PropertyChanged(HookUrlProperty);
            }
        }

        /// <summary> 全部のイベントに反応するかどうかを取得または設定します </summary>
        public bool AllEvent
        {
            get { return _allEvent; }
            set
            {
                _allEvent = value;
                PropertyChanged(AllEventProperty);
            }
        }

        /// <summary> 通知するイベントの一覧を取得します </summary>
        public List<ActivityType> ActivityTypeIds
        {
            get { return _activityTypeIds; }
            private set
            {
                _activityTypeIds = value;
                PropertyChanged(ActivityTypeIdsProperty);
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(NameProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NameProperty, Name));
            }

            if (IsPropertyChanged(DescriptionProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(DescriptionProperty, Description));
            }

            if (IsPropertyChanged(HookUrlProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(HookUrlProperty, HookUrl));
            }

            if (IsPropertyChanged(AllEventProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(AllEventProperty, AllEvent ? "true" : "false"));
            }

            if (!AllEvent && IsPropertyChanged(ActivityTypeIdsProperty))
            {
                pairs.AddRange(ActivityTypeIds.Select(a => (int)a).ToKeyValuePairs(ActivityTypeIdsProperty));
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
    }
}