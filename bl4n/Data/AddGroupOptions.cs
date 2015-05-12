// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddGroupOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加するグループのオプションを表します </summary>
    public class AddGroupOptions : OptionalParams
    {
        private List<long> _members;

        /// <summary> <see cref="AddGroupOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">グループ名</param>
        public AddGroupOptions(string name)
            : base("members[]")
        {
            Name = name;
        }

        /// <summary> グループ名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> グループのメンバーのユーザー ID の一覧を取得します </summary>
        public List<long> Memebers
        {
            get { return _members; }
            private set
            {
                _members = value;
                PropertyChanged("members[]");
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name)
            };

            if (IsPropertyChanged("members[]"))
            {
                pairs.AddRange(Memebers.ToKeyValuePairs("members[]"));
            }

            return pairs;
        }

        /// <summary>
        /// メンバーを追加します
        /// </summary>
        /// <param name="newMembers"></param>
        public void AddMembers(IEnumerable<long> newMembers)
        {
            var mem = new List<long>(newMembers);
            if (Memebers != null)
            {
                mem.AddRange(Memebers);
            }

            Memebers = mem.Distinct().ToList();
        }
    }
}