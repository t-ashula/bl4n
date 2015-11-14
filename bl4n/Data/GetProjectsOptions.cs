// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetProjectsOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> プロジェクト一覧取得のオプションを表します </summary>
    public sealed class GetProjectsOptions : OptionalParams
    {
        private const string ArchivedProperty = "archived";
        private const string AllProperty = "all";

        /// <summary> <see cref="GetProjectsOptions"/> のインスタンスを初期化します </summary>
        public GetProjectsOptions()
            : base(ArchivedProperty, AllProperty)
        {
        }

        private bool _archived;

        /// <summary>
        /// アーカイブ済かどうかの指定を取得または設定します
        /// </summary>
        public bool Archived
        {
            get { return _archived; }
            set
            {
                if (_archived != value)
                {
                    _archived = value;
                    PropertyChanged(ArchivedProperty);
                }
            }
        }

        private bool _all;

        /// <summary>
        /// プロジェクトへの参加の有無の指定を取得または設定します
        /// </summary>
        public bool All
        {
            get { return _all; }
            set
            {
                if (_all != value)
                {
                    _all = value;
                    PropertyChanged(AllProperty);
                }
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var paris = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(ArchivedProperty))
            {
                paris.Add(new KeyValuePair<string, string>(ArchivedProperty, Archived ? "true" : "false"));
            }

            if (IsPropertyChanged(AllProperty))
            {
                paris.Add(new KeyValuePair<string, string>(AllProperty, All ? "true" : "false"));
            }

            return paris;
        }
    }
}