// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddProjectVersionOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// Parameters for AddProjectVersion
    /// </summary>
    public class AddProjectVersionOptions : OptionalParams
    {
        /// <summary>
        /// <see cref="AddProjectVersionOptions"/> �̃C���X�^���X�����������܂�
        /// </summary>
        /// <param name="versionName"> �o�[�W������ </param>
        public AddProjectVersionOptions(string versionName)
            : base(DescriptionProperty, StartDateProperty, ReleaseDueDateProperty)
        {
            Name = versionName;
        }

        /// <summary> �o�[�W�����̖��̂��擾�܂��͐ݒ肵�܂� </summary>
        public string Name { get; set; }

        /// <summary> �o�[�W�����̐������擾�܂��͐ݒ肵�܂� </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(DescriptionProperty);
            }
        }

        private string _description;
        private const string DescriptionProperty = "description";

        /// <summary> �J�n�����擾�܂��͐ݒ肵�܂� </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                PropertyChanged(StartDateProperty);
            }
        }

        private DateTime? _startDate;
        private const string StartDateProperty = "startDate";

        /// <summary> �����[�X�����擾�܂��͐ݒ肵�܂� </summary>
        public DateTime? ReleaseDueDate
        {
            get { return _releaseDueDate; }
            set
            {
                _releaseDueDate = value;
                PropertyChanged(ReleaseDueDateProperty);
            }
        }

        private DateTime? _releaseDueDate;
        private const string ReleaseDueDateProperty = "releaseDueDate";

        /// <summary> HTTP Request �p�� Key-value �y�A�̈ꗗ���擾���܂� </summary>
        /// <returns> key-value �y�A�̈ꗗ </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name)
            };

            if (IsPropertyChanged(DescriptionProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(DescriptionProperty, Description));
            }

            if (IsPropertyChanged(StartDateProperty))
            {
                var item = StartDate.HasValue
                    ? StartDate.Value.ToString(Backlog.DateFormat)
                    : string.Empty;
                pairs.Add(new KeyValuePair<string, string>(StartDateProperty, item));
            }

            if (IsPropertyChanged(ReleaseDueDateProperty))
            {
                var item = ReleaseDueDate.HasValue
                    ? ReleaseDueDate.Value.ToString(Backlog.DateFormat)
                    : string.Empty;
                pairs.Add(new KeyValuePair<string, string>(ReleaseDueDateProperty, item));
            }

            return pairs;
        }
    }
}