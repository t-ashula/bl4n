// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateProjectIssueTypeOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> 追加する課題種別のオプションを表します </summary>
    public class UpdateProjectIssueTypeOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string ColorProperty = "color";

        private string _name;
        private IssueTypeColor _color;

        /// <summary> <see cref="UpdateProjectIssueTypeOptions"/> のインスタンスを初期化します </summary>
        public UpdateProjectIssueTypeOptions() :
            base(NameProperty, ColorProperty)
        {
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

            if (IsPropertyChanged(ColorProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ColorProperty, Color.ColorCode));
            }

            return pairs;
        }

        /// <summary> 課題種別の名称を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> 課題種別の背景色を取得または設定します </summary>
        public IssueTypeColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                PropertyChanged(ColorProperty);
            }
        }
    }
}