// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultPagingOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> ���ʂ͈̔͂���C�������w�肷��I�v�V������\���܂� </summary>
    public class ResultPagingOptions : OptionalParams
    {
        private const string MinIdProperty = "minId";
        private const string MaxIdProperty = "maxId";
        private const string CountProperty = "count";
        private const string OrderProperty = "order";

        private long _minId;
        private long _maxId;
        private int _count;
        private bool _ascending;

        /// <summary> <see cref="RecentUpdateFilterOptions"/> �̃C���X�^���X�����������܂� </summary>
        public ResultPagingOptions()
            : base(MinIdProperty, MaxIdProperty, CountProperty, OrderProperty)
        {
        }

        /// <summary> �ŏ���ID ���擾�܂��͐ݒ肵�܂��D </summary>
        public long MinId
        {
            get { return _minId; }
            set
            {
                _minId = value;
                PropertyChanged(MinIdProperty);
            }
        }

        /// <summary> �ő��ID ���擾�܂��͐ݒ肵�܂��D </summary>
        public long MaxId
        {
            get { return _maxId; }
            set
            {
                _maxId = value;
                PropertyChanged(MaxIdProperty);
            }
        }

        /// <summary> �擾�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> 1����100 �܂Łi�w�薳���̏ꍇ 20)</remarks>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                if (_count > 100)
                {
                    _count = 100;
                }

                if (_count < 1)
                {
                    _count = 1;
                }

                PropertyChanged(CountProperty);
            }
        }

        /// <summary> �����Ŏ擾���邩�ǂ������擾�܂��͐ݒ肵�܂� </summary>
        /// <remarks> �w�薳���̏ꍇ �~��</remarks>
        public bool Ascending
        {
            get { return _ascending; }
            set
            {
                _ascending = value;
                PropertyChanged(OrderProperty);
            }
        }

        /// <summary> HTTP Request �p�� Key-value �y�A�̈ꗗ���擾���܂� </summary>
        /// <returns> key-value �y�A�̈ꗗ </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(MinIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MinIdProperty, string.Format("{0}", MinId)));
            }

            if (IsPropertyChanged(MaxIdProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MaxIdProperty, string.Format("{0}", MaxId)));
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