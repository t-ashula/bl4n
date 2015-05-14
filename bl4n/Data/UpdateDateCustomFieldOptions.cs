// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateDateCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 日付タイプのカスタムフィールドの更新オプションを表します </summary>
    public class UpdateDateCustomFieldOptions : UpdateCustomFieldOptions
    {
        private const string FirstDateProperty = "min";
        private const string LastDateProperty = "max";
        private const string InitialDateProperty = "initialDate";
        private const string InitialShiftProperty = "initialShift";
        private const string InitialValueTypeProperty = "initialValueType";

        private DateTime _firstDate;
        private DateTime _lastDate;
        private DateTime _initialDate;
        private int _initialShift;
        private int _initialValueType;

        /// <summary> <see cref="UpdateDateCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public UpdateDateCustomFieldOptions(string name)
            : base(name)
        {
            AddTrackingKeys(FirstDateProperty, LastDateProperty, InitialValueTypeProperty, InitialDateProperty, InitialShiftProperty);
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = CoreKeyValuePairs();
            if (IsPropertyChanged(FirstDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(FirstDateProperty, string.Format("{0}", FirstDate.ToString(Backlog.DateFormat))));
            }

            if (IsPropertyChanged(LastDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(LastDateProperty, string.Format("{0}", LastDate.ToString(Backlog.DateFormat))));
            }

            if (IsPropertyChanged(InitialDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(InitialDateProperty, string.Format("{0}", InitialDate.ToString(Backlog.DateFormat))));
            }

            if (IsPropertyChanged(InitialShiftProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(InitialShiftProperty, string.Format("{0}", InitialShift)));
            }

            if (IsPropertyChanged(InitialValueTypeProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(InitialValueTypeProperty, string.Format("{0}", InitialValueType)));
            }

            return pairs;
        }

        /// <summary> 開始日を取得または設定します </summary>
        public DateTime FirstDate
        {
            get { return _firstDate; }
            set
            {
                _firstDate = value;
                PropertyChanged(FirstDateProperty);
            }
        }

        /// <summary> 最終日を取得または設定します </summary>
        public DateTime LastDate
        {
            get { return _lastDate; }
            set
            {
                _lastDate = value;
                PropertyChanged(LastDateProperty);
            }
        }

        /// <summary> 初期値を取得または設定します </summary>
        public DateTime InitialDate
        {
            get { return _initialDate; }
            set
            {
                _initialDate = value;
                PropertyChanged(InitialDateProperty);
            }
        }

        /// <summary> 初期シフト日数を取得または設定します </summary>
        public int InitialShift
        {
            get { return _initialShift; }
            set
            {
                _initialShift = value;
                PropertyChanged(InitialShiftProperty);
            }
        }

        /// <summary> 初期値設定方法を取得または設定します </summary>
        public int InitialValueType
        {
            get { return _initialValueType; }
            set
            {
                _initialValueType = value;
                PropertyChanged(InitialValueTypeProperty);
            }
        }
    }
}