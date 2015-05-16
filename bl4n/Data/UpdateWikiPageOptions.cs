// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateWikiPageOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// wikipage の新規作成用のパラメータを表します
    /// </summary>
    public class UpdateWikiPageOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string ContentProperty = "content";
        private const string NotifyProperty = "mailNotify";

        private string _name;
        private string _content;
        private bool _notify;

        /// <summary> <see cref="UpdateWikiPageOptions"/> のインスタンスを初期化します </summary>
        public UpdateWikiPageOptions()
            : base(NameProperty, ContentProperty, NotifyProperty)
        {
        }

        /// <summary> ページ名を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> ページ内容を取得または設定します </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                PropertyChanged(ContentProperty);
            }
        }

        /// <summary> メール通知するかどうかを取得または設定します </summary>
        public bool Notify
        {
            get { return _notify; }
            set
            {
                _notify = value;
                PropertyChanged(NotifyProperty);
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

            if (IsPropertyChanged(ContentProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ContentProperty, Content));
            }

            if (IsPropertyChanged(NotifyProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NotifyProperty, Notify ? "true" : "false"));
            }

            return pairs;
        }
    }
}