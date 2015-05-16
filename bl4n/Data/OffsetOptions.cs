// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OffsetOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> offset/order/count のオプションを表します </summary>
    public class OffsetOptions : OptionalParams
    {
        private const string OffsetProperty = "offset";
        private const string CountProperty = "count";
        private const string OrderProperty = "order";

        private int _offset;
        private int _count;
        private bool _ascending;

        /// <summary> <see cref="OffsetOptions"/> のインスタンスを初期化します </summary>
        public OffsetOptions()
            : base(OffsetProperty, CountProperty, OrderProperty)
        {
        }

        /// <summary> オフセットを取得または設定します </summary>
        public int Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                PropertyChanged(OffsetProperty);
            }
        }

        /// <summary> 取得個数を取得または設定します </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                PropertyChanged(CountProperty);
            }
        }

        /// <summary> 昇順で取得するかどうかを取得または設定します </summary>
        public bool Ascending
        {
            get { return _ascending; }
            set
            {
                _ascending = value;
                PropertyChanged(OrderProperty);
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(OffsetProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(OffsetProperty, string.Format("{0}", Offset)));
            }

            if (IsPropertyChanged(CountProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(CountProperty, string.Format("{0}", Count)));
            }

            if (IsPropertyChanged(OrderProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(OrderProperty, Ascending ? "asc" : "desc"));
            }

            return pairs;
        }
    }
}