// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddDateTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> ���t�`���̃J�X�^���t�B�[���h�̒ǉ��p�̃I�v�V������\���܂� </summary>
    public class AddDateTypeCustomFieldOptions : AddCustomFieldOptions
    {
        private DateTime _firstDate;
        private DateTime _lastDate;
        private DateTime _initialDate;
        private int _initialShift;
        private int _initialValueType;
        private const string FirstDateProperty = "min";
        private const string LastDateProperty = "max";
        private const string InitialDateProperty = "initialDate";
        private const string InitialShiftProperty = "initialShift";
        private const string InitialValueTypeProperty = "initialValueType";

        /// <summary> <see cref="AddDateTypeCustomFieldOptions"/> �̃C���X�^���X�����������܂� </summary>
        /// <param name="name">�t�B�[���h��</param>
        public AddDateTypeCustomFieldOptions(string name)
            : base(CustomFieldTypes.Date, name)
        {
            AddTrackingKeys(FirstDateProperty, LastDateProperty, InitialDateProperty, InitialShiftProperty, InitialValueTypeProperty);
        }

        /// <summary> �J�n�����擾�܂��͐ݒ肵�܂� </summary>
        public DateTime FirstDate
        {
            get { return _firstDate; }
            set
            {
                _firstDate = value;
                PropertyChanged(FirstDateProperty);
            }
        }

        /// <summary> �ŏI�����擾�܂��͐ݒ肵�܂� </summary>
        public DateTime LastDate
        {
            get { return _lastDate; }
            set
            {
                _lastDate = value;
                PropertyChanged(LastDateProperty);
            }
        }

        /// <summary> �����l���擾�܂��͐ݒ肵�܂� </summary>
        public DateTime InitialDate
        {
            get { return _initialDate; }
            set
            {
                _initialDate = value;
                PropertyChanged(InitialDateProperty);
            }
        }

        /// <summary> �����V�t�g�������擾�܂��͐ݒ肵�܂� </summary>
        public int InitialShift
        {
            get { return _initialShift; }
            set
            {
                _initialShift = value;
                PropertyChanged(InitialShiftProperty);
            }
        }

        /// <summary> �����l�ݒ���@���擾�܂��͐ݒ肵�܂� </summary>
        public int InitialValueType
        {
            get { return _initialValueType; }
            set
            {
                _initialValueType = value;
                PropertyChanged(InitialValueTypeProperty);
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = CoreKeyValuePairs();

            if (IsPropertyChanged(FirstDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(FirstDateProperty, FirstDate.ToString(Backlog.DateFormat)));
            }

            if (IsPropertyChanged(LastDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(LastDateProperty, LastDate.ToString(Backlog.DateFormat)));
            }

            if (IsPropertyChanged(InitialDateProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(InitialDateProperty, InitialDate.ToString(Backlog.DateFormat)));
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
    }
}