// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateNumberCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 数値タイプのカスタムフィールドの更新オプションを表します </summary>
    public class UpdateNumberCustomFieldOptions : UpdateCustomFieldOptions
    {
        private const string MinValueProperty = "min";
        private const string MaxValueProperty = "max";
        private const string InitialValueProperty = "initialValue";
        private const string UnitProperty = "unit";

        private double _minValue;
        private double _maxValue;
        private double _initialValue;
        private string _unit;

        /// <summary> <see cref="UpdateNumberCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        public UpdateNumberCustomFieldOptions(string name)
            : base(name)
        {
            AddTrackingKeys(MinValueProperty, MaxValueProperty, InitialValueProperty, UnitProperty);
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = CoreKeyValuePairs();
            if (IsPropertyChanged(MinValueProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MinValueProperty, string.Format("{0}", MinValue)));
            }

            if (IsPropertyChanged(MaxValueProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MaxValueProperty, string.Format("{0}", MaxValue)));
            }

            if (IsPropertyChanged(InitialValueProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(InitialValueProperty, string.Format("{0}", InitialValue)));
            }

            if (IsPropertyChanged(UnitProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(UnitProperty, Unit));
            }

            return pairs;
        }

        /// <summary> 最小値を取得または設定します </summary>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                PropertyChanged(MinValueProperty);
            }
        }

        /// <summary> 最大値を取得または設定します </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                PropertyChanged(MaxValueProperty);
            }
        }

        /// <summary> 初期値を取得または設定します </summary>
        public double InitialValue
        {
            get { return _initialValue; }
            set
            {
                _initialValue = value;
                PropertyChanged(InitialValueProperty);
            }
        }

        /// <summary> 単位の文字列を取得または設定します </summary>
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                PropertyChanged(UnitProperty);
            }
        }
    }
}