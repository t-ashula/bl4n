// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateProjectCategoryOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 更新するカテゴリのオプションを表します </summary>
    public class UpdateProjectCategoryOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private string _name;

        /// <summary> <see cref="UpdateProjectCategoryOptions"/> のインスタンスを初期化します </summary>
        public UpdateProjectCategoryOptions()
            : base(NameProperty)
        {
        }

        /// <summary> カテゴリ名を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
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

            return pairs;
        }
    }
}