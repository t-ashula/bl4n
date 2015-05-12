// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateGroupOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 更新するグループのオプションを表します </summary>
    public class UpdateGroupOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string MembersProperty = "members[]";

        private string _name;
        private List<long> _members;

        /// <summary> <see cref="UpdateGroupOptions"/> のインスタンスを初期化します </summary>
        public UpdateGroupOptions()
            : base(NameProperty, MembersProperty)
        {
        }

        /// <summary> グループ名を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> グループのメンバーのユーザー ID の一覧を取得します </summary>
        public List<long> Memebers
        {
            get { return _members; }
            private set
            {
                _members = value;
                PropertyChanged(MembersProperty);
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

            if (IsPropertyChanged(MembersProperty))
            {
                pairs.AddRange(Memebers.ToKeyValuePairs(MembersProperty));
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