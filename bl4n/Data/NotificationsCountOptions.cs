// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsCountOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// 通知の個数の検索条件を表します
    /// </summary>
    public class NotificationsCountOptions : OptionalParams
    {
        /// <summary>
        /// <see cref="NotificationsCountOptions"/> のインスタンスを初期化します
        /// </summary>
        public NotificationsCountOptions()
            : base(AlreadReadProperty, ResourceAlreadyReadProperty)
        {
        }

        /// <summary> 既読かどうかを取得します </summary>
        public bool AlreadyRead
        {
            get { return _alreadyRead; }
            set
            {
                _alreadyRead = value;
                PropertyChanged(AlreadReadProperty);
            }
        }

        private bool _alreadyRead;
        private const string AlreadReadProperty = "alreadRead";

        /// <summary> リソースが既読かどうかを取得します </summary>
        public bool ResourceAlreadyRead
        {
            get { return _resourceAlreadyRead; }
            set
            {
                _resourceAlreadyRead = value;
                PropertyChanged(ResourceAlreadyReadProperty);
            }
        }

        private bool _resourceAlreadyRead;
        private const string ResourceAlreadyReadProperty = "resourceAlreadyRead";

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(AlreadReadProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(AlreadReadProperty, _alreadyRead ? "true" : "false"));
            }

            if (IsPropertyChanged(ResourceAlreadyReadProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ResourceAlreadyReadProperty, _resourceAlreadyRead ? "true" : "false"));
            }

            return pairs;
        }
    }
}