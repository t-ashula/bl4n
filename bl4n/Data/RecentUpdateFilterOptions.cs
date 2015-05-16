// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentUpdateFilterOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// 最近の更新のフィルタ用オプションを表します
    /// </summary>
    public class RecentUpdateFilterOptions : OptionalParams
    {
        private const string MinIdProperty = "minId";
        private const string MaxIdProperty = "maxId";
        private const string CountProperty = "count";
        private const string OrderProperty = "order";
        private const string ActivityTypeIdsProperty = "activityTypeId[]";

        private long _minId;
        private long _maxId;
        private int _count;
        private bool _ascending;
        private List<ActivityType> _activityTypeIds;

        /// <summary> <see cref="RecentUpdateFilterOptions"/> のインスタンスを初期化します </summary>
        public RecentUpdateFilterOptions()
            : base(MinIdProperty, MaxIdProperty, CountProperty, OrderProperty, ActivityTypeIdsProperty)
        {
        }

        /// <summary> 最小のID を取得または設定します． </summary>
        public long MinId
        {
            get { return _minId; }
            set
            {
                _minId = value;
                PropertyChanged(MinIdProperty);
            }
        }

        /// <summary> 最大のID を取得または設定します． </summary>
        public long MaxId
        {
            get { return _maxId; }
            set
            {
                _maxId = value;
                PropertyChanged(MaxIdProperty);
            }
        }

        /// <summary> 取得個数を取得または設定します． </summary>
        /// <remarks> 1から100 まで（指定無しの場合 20)</remarks>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                if (_count > 100)
                {
                    _count = 100;
                }

                if (_count < 1)
                {
                    _count = 1;
                }

                PropertyChanged(CountProperty);
            }
        }

        /// <summary> 昇順で取得するかどうかを取得または設定します </summary>
        /// <remarks> 指定無しの場合 降順</remarks>
        public bool Ascending
        {
            get { return _ascending; }
            set
            {
                _ascending = value;
                PropertyChanged(OrderProperty);
            }
        }

        /// <summary> 対象のイベントの一覧を取得します </summary>
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
            if (IsPropertyChanged(MinIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MinIdProperty, string.Format("{0}", MinId)));
            }

            if (IsPropertyChanged(MaxIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MaxIdProperty, string.Format("{0}", MaxId)));
            }

            if (IsPropertyChanged(CountProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(CountProperty, string.Format("{0}", Count)));
            }

            if (IsPropertyChanged(OrderProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(OrderProperty, Ascending ? "asc" : "desc"));
            }

            if (IsPropertyChanged(ActivityTypeIdsProperty))
            {
                pairs.AddRange(ActivityTypeIds.Select(a => (int)a).ToKeyValuePairs(ActivityTypeIdsProperty));
            }

            return pairs;
        }

        /// <summary> add activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
        public void AddActivityTypes(IEnumerable<ActivityType> types)
        {
            var ids = new List<ActivityType>(types);
            if (_activityTypeIds != null)
            {
                ids.AddRange(_activityTypeIds);
            }

            ActivityTypeIds = ids.Distinct().ToList();
        }

        /// <summary> remove activity types  </summary>
        /// <param name="types"> list of <see cref="ActivityType"/> </param>
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
        }
    }
}