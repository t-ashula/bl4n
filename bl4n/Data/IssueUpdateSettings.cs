// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueUpdateSettings.cs">
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
    /// �ۑ�̍X�V�̂��߂̃p�����[�^��\���܂�
    /// </summary>
    public class IssueUpdateSettings
    {
        /// <summary>
        /// <see cref="IssueUpdateSettings"/> �̃C���X�^���X�����������܂�
        /// </summary>
        /// <param name="issueTypeId">�ۑ���ID</param>
        /// <param name="priorityId">�D��xID</param>
        public IssueUpdateSettings(long issueTypeId, long priorityId)
        {
            PriorityId = priorityId;
            IssueTypeId = issueTypeId;
        }

        /// <summary> �ۑ���ID���擾���܂��D </summary>
        /// <remarks> issueTypeId </remarks>
        public long IssueTypeId { get; private set; }

        /// <summary> �D��xID���擾���܂��D </summary>
        /// <remarks> priorityId </remarks>
        public long PriorityId { get; private set; }

        /// <summary> �T�}���[���擾�܂��͐ݒ肵�܂��D</summary>
        /// <remarks> summary </remarks>
        public string Summary { get; set; }

        /// <summary> �e�ۑ��ID���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> parentIssueId </remarks>
        public long? ParentIssueId
        {
            get { return _parentIssueId; }
            set
            {
                _parentIssueIdChanging = true;
                _parentIssueId = value;
            }
        }

        private long? _parentIssueId;
        private bool _parentIssueIdChanging;

        /// <summary> �������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> description </remarks>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                _descriptionChanging = true;
            }
        }

        private string _description;
        private bool _descriptionChanging;

        /// <summary> �X�e�[�^�XID���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> statusId </remarks>
        public long? StatusId
        {
            get { return _statusId; }
            set
            {
                _statusId = value;
                _statusIdChanging = true;
            }
        }

        private long? _statusId;
        private bool _statusIdChanging;

        /// <summary> �������R��ID���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> resolutionId </remarks>
        public long? ResolutionId
        {
            get { return _resolutionId; }
            set
            {
                _resolutionId = value;
                _resolutionIdChanging = true;
            }
        }

        private long? _resolutionId;
        private bool _resolutionIdChanging;

        /// <summary> �J�n�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> startDate </remarks>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                _startDateChanging = true;
            }
        }

        private DateTime? _startDate;
        private bool _startDateChanging;

        /// <summary> ���������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> dueDate </remarks>
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                _dueDateChanging = true;
            }
        }

        private DateTime? _dueDate;
        private bool _dueDateChanging;

        /// <summary> �\���Ǝ��Ԃ��擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> estimatedHours </remarks>
        public double? EstimatedHours
        {
            get { return _estimatedHours; }
            set
            {
                _estimatedHours = value;
                _estimatedHoursChanging = true;
            }
        }

        private double? _estimatedHours;
        private bool _estimatedHoursChanging;

        /// <summary> ����Ǝ��Ԃ�ݒ肵�܂��D </summary>
        /// <remarks> actualHours </remarks>
        public double? ActualHour
        {
            get { return _actualHours; }
            set
            {
                _actualHours = value;
                _actualHoursChanging = true;
            }
        }

        private double? _actualHours;
        private bool _actualHoursChanging;

        /// <summary> �J�e�S��ID�̈ꗗ���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> categoryId[] </remarks>
        public List<long> CategoryIds
        {
            get { return _categoryIds; }
            set
            {
                _categoryIds = value;
                _categoryIdsChanging = true;
            }
        }

        private List<long> _categoryIds; // TODO:use observablecollection ?
        private bool _categoryIdsChanging;

        /// <summary> �o�[�W����ID�̈ꗗ���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> versionId[] </remarks>
        public List<long> VersionIds
        {
            get { return _versionIds; }
            set
            {
                _versionIds = value;
                _versionIdsChanging = true;
            }
        }

        private List<long> _versionIds;
        private bool _versionIdsChanging;

        /// <summary> �}�C���X�g�[��ID�̈ꗗ���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> milestoneId[] </remarks>
        public List<long> MilestoneIds
        {
            get { return _milestoneIds; }
            set
            {
                _milestoneIds = value;
                _milestoneIdsChanging = true;
            }
        }

        private List<long> _milestoneIds;
        private bool _milestoneIdsChanging;

        /// <summary> �S�����[�U�[��ID���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> assigneeId </remarks>
        public long? AssigneeId
        {
            get { return _assigneeId; }
            set
            {
                _assigneeId = value;
                _assigneeIdChanging = true;
            }
        }

        private long? _assigneeId;
        private bool _assigneeIdChanging;

        /// <summary> �ʒm�惆�[�U�[��ID�̈ꗗ���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> notifiedUserId[] </remarks>
        public List<long> NotifiedUserIds
        {
            get { return _notifiedUserIds; }
            set
            {
                _notifiedUserIds = value;
                _notifiedUserIdsChanging = true;
            }
        }

        private List<long> _notifiedUserIds;
        private bool _notifiedUserIdsChanging;

        /// <summary> �Y�t�t�@�C����ID�̈ꗗ���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> attachmentId[] </remarks>
        public List<long> AttachmentIds
        {
            get { return _attachmentIds; }
            set
            {
                _attachmentIds = value;
                _attachmentIdsChanging = true;
            }
        }

        private List<long> _attachmentIds;
        private bool _attachmentIdsChanging;

        /// <summary> �R�����g���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> comment </remarks>
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                _commentChanging = true;
            }
        }

        private string _comment;
        private bool _commentChanging;

        /// <summary> get options as list of KeyValuePair </summary>
        /// <returns> list of KeyValuePair{string, string} </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("priorityId", string.Format("{0}", PriorityId)),
                new KeyValuePair<string, string>("issueTypeId", string.Format("{0}", IssueTypeId))
            };

            if (!string.IsNullOrEmpty(Summary))
            {
                pairs.Add(new KeyValuePair<string, string>("summary", Summary));
            }

            if (_parentIssueIdChanging)
            {
                var v = _parentIssueId.HasValue ? string.Format("{0}", _parentIssueId.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("parentIssueId", v));
            }

            if (_descriptionChanging)
            {
                pairs.Add(new KeyValuePair<string, string>("description", _description ?? string.Empty));
            }

            if (_statusIdChanging)
            {
                var v = _statusId.HasValue ? string.Format("{0}", _statusId.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("statusId", string.Format("{0}", v)));
            }

            if (_resolutionIdChanging)
            {
                var v = _resolutionId.HasValue ? string.Format("{0}", _resolutionId.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("resolutionId", string.Format("{0}", v)));
            }

            if (_startDateChanging)
            {
                var v = _startDate.HasValue ? _startDate.Value.ToString(Backlog.DateFormat) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("startDate", v));
            }

            if (_dueDateChanging)
            {
                var v = _dueDate.HasValue ? _dueDate.Value.ToString(Backlog.DateFormat) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("dueDate", v));
            }

            if (_estimatedHoursChanging)
            {
                var v = _estimatedHours.HasValue ? string.Format("{0}", _estimatedHours.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("estimatedHours", v));
            }

            if (_actualHoursChanging)
            {
                var v = _actualHours.HasValue ? string.Format("{0}", _actualHours.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("actualHours", v));
            }

            if (_categoryIdsChanging)
            {
                if (_categoryIds == null)
                {
                    pairs.Add(new KeyValuePair<string, string>("categoryId[]", string.Empty));
                }
                else
                {
                    pairs.AddRange(_categoryIds.ToKeyValuePairs("categoryId[]"));
                }
            }

            if (_versionIdsChanging)
            {
                if (_versionIds == null)
                {
                    pairs.Add(new KeyValuePair<string, string>("versionId[]", string.Empty));
                }
                else
                {
                    pairs.AddRange(_versionIds.ToKeyValuePairs("versionId[]"));
                }
            }

            if (_milestoneIdsChanging)
            {
                if (_milestoneIds == null)
                {
                    pairs.Add(new KeyValuePair<string, string>("milestoneId[]", string.Empty));
                }
                else
                {
                    pairs.AddRange(_milestoneIds.ToKeyValuePairs("milestoneId[]"));
                }
            }

            if (_assigneeIdChanging)
            {
                var v = _assigneeId.HasValue ? string.Format("{0}", _assigneeId.Value) : string.Empty;
                pairs.Add(new KeyValuePair<string, string>("assigneeId", v));
            }

            if (_notifiedUserIdsChanging)
            {
                if (_notifiedUserIds == null)
                {
                    pairs.Add(new KeyValuePair<string, string>("notifiedUserId[]", string.Empty));
                }
                else
                {
                    pairs.AddRange(_notifiedUserIds.ToKeyValuePairs("notifiedUserId[]"));
                }
            }

            if (_attachmentIdsChanging)
            {
                if (_attachmentIds == null)
                {
                    pairs.Add(new KeyValuePair<string, string>("attachmentId[]", string.Empty));
                }
                else
                {
                    pairs.AddRange(_attachmentIds.ToKeyValuePairs("attachmentId[]"));
                }
            }

            if (_commentChanging)
            {
                pairs.Add(new KeyValuePair<string, string>("comment", _comment));
            }

            return pairs;
        }
    }
}