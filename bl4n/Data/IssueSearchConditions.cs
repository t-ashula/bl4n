// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueSearchConditions.cs">
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
    /// Issue Search Conditions
    /// </summary>
    public class IssueSearchConditions
    {
        /// <summary>
        /// Initialize <see cref="IssueSearchConditions"/> instance.
        /// </summary>
        public IssueSearchConditions()
        {
            VersionIds = new List<long>();
            StatusIds = new List<long>();
            ParentIssueId = new List<long>();
            Ids = new List<long>();
            ResolutionIds = new List<long>();
            CreateUserIds = new List<long>();
            AssignerIds = new List<long>();
            PriorityIds = new List<long>();
            MileStoneIds = new List<long>();
            IssueTypeIds = new List<long>();
            CategoryIds = new List<long>();
            _searchConditions = new List<KeyValuePair<string, string>>();
        }

        private readonly List<KeyValuePair<string, string>> _searchConditions;

        /// <summary> convert search conditions to KeyValuePair </summary>
        /// <returns> key value pairs </returns>
        public List<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            _searchConditions.AddRange(IssueTypeIds.ToKeyValuePairs("issueTypeId[]"));
            _searchConditions.AddRange(CategoryIds.ToKeyValuePairs("categoryId[]"));
            _searchConditions.AddRange(VersionIds.ToKeyValuePairs("versionId[]"));
            _searchConditions.AddRange(MileStoneIds.ToKeyValuePairs("milestoneId[]"));
            _searchConditions.AddRange(StatusIds.ToKeyValuePairs("statusId[]"));
            _searchConditions.AddRange(PriorityIds.ToKeyValuePairs("priorityId[]"));
            _searchConditions.AddRange(AssignerIds.ToKeyValuePairs("assignerId[]"));
            _searchConditions.AddRange(CreateUserIds.ToKeyValuePairs("createdUserId[]"));
            _searchConditions.AddRange(ResolutionIds.ToKeyValuePairs("resolutionId[]"));
            _searchConditions.AddRange(Ids.ToKeyValuePairs("id[]"));
            _searchConditions.AddRange(ParentIssueId.ToKeyValuePairs("parentIssueId[]"));
            if (ParentChild != ParentChildType.All)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("parentChild", string.Format("{0}", (int)ParentChild)));
            }

            if (HasAttachment.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("attachment", HasAttachment.Value ? "true" : "false"));
            }

            if (HasSharedFile.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("sharedFile", HasSharedFile.Value ? "true" : "false"));
            }

            if (SortKey.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("sort", GetSortKeyFieldName(SortKey.Value)));
            }

            if (OrderByAsc)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("order", "asc"));
            }

            if (Offset.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("offset", string.Format("{0}", Offset.Value)));
            }

            if (Count.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("count", string.Format("{0}", Count.Value)));
            }

            if (CreatedSince.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("createdSince", CreatedSince.Value.ToString(Backlog.DateFormat)));
            }

            if (CreatedUntil.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("createdUntil", CreatedUntil.Value.ToString(Backlog.DateFormat)));
            }

            if (UpdatedSince.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("updatedSince", UpdatedSince.Value.ToString(Backlog.DateFormat)));
            }

            if (UpdatedUntil.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("updatedUntil", UpdatedUntil.Value.ToString(Backlog.DateFormat)));
            }

            if (StartDateSince.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("startDateSince", StartDateSince.Value.ToString(Backlog.DateFormat)));
            }

            if (StartDateUntil.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("startDateUntil", StartDateUntil.Value.ToString(Backlog.DateFormat)));
            }

            if (DueDateSince.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("dueDateSince", DueDateSince.Value.ToString(Backlog.DateFormat)));
            }

            if (DueDateUntil.HasValue)
            {
                _searchConditions.Add(new KeyValuePair<string, string>("dueDateUntil", DueDateUntil.Value.ToString(Backlog.DateFormat)));
            }

            if (!string.IsNullOrEmpty(Keyword))
            {
                _searchConditions.Add(new KeyValuePair<string, string>("keyword", Keyword));
            }

            return _searchConditions;
        }

        /// <summary> �����Ώۂ̉ۑ�̎�ʂ�ID�̃��X�g���擾���܂� </summary>
        /// <remarks> issueTypeId[] </remarks>
        public List<long> IssueTypeIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̃J�e�S���[��ID�̃��X�g���擾���܂� </summary>
        /// <remarks> categoryId[] </remarks>
        public List<long> CategoryIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̃��@�[�W������ID�̃��X�g���擾���܂� </summary>
        /// <remarks> versionId[] </remarks>
        public List<long> VersionIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̃}�C���X�g�[����ID�̃��X�g���擾���܂� </summary>
        /// <remarks> milestoneId[] </remarks>
        public List<long> MileStoneIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̃X�e�[�^�X��ID�̃��X�g���擾���܂� </summary>
        /// <remarks> statusId[] </remarks>
        public List<long> StatusIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̗D��x��ID�̃��X�g���擾���܂� </summary>
        /// <remarks> priorityId[] </remarks>
        public List<long> PriorityIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̒S���҂�ID�̃��X�g���擾���܂� </summary>
        /// <remarks> assignerId[] </remarks>
        public List<long> AssignerIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̍쐬�҂�ID�̃��X�g���擾���܂� </summary>
        /// <remarks> createdUserId[] </remarks>
        public List<long> CreateUserIds { get; private set; }

        /// <summary> �����Ώۂ̉ۑ�̊������R��ID�̃��X�g���擾���܂� </summary>
        /// <remarks> resolutionId[] </remarks>
        public List<long> ResolutionIds { get; private set; }

        /// <summary> �e�q�ۑ�̃p�^�[����\���܂� </summary>
        public enum ParentChildType
        {
            /// <summary> ���ׂāi��ʂȂ��j </summary>
            All = 0,

            /// <summary> �q�ۑ�ȊO </summary>
            ExcludeChild = 1,

            /// <summary> �q�ۑ� </summary>
            Child = 2,

            /// <summary> �e�ۑ���q�ۑ���w�肳��Ă��Ȃ��ۑ� </summary>
            NeitherParentNorChild = 3,

            /// <summary> �e�ۑ� </summary>
            Parent = 4
        }

        /// <summary> �����Ώۂ̉ۑ�̐e�q�ۑ�̃p�^�[�����擾�܂��͐ݒ肵�܂� </summary>
        /// <remarks> parentChild </remarks>
        public ParentChildType ParentChild { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̓Y�t�t�@�C���̗L�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> attachment </remarks>
        public bool? HasAttachment { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̋��L�t�@�C���ւ̃����N�̗L�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> sharedFile </remarks>
        public bool? HasSharedFile { get; set; }

        /// <summary> �\�[�g�Ώۂ�\���܂� </summary>
        public enum SortKeyField
        {
            IssueType,
            Category,
            Version,
            MileStone,
            Summary,
            Status,
            Priority,
            Attachment,
            SharedFile,
            CreatedDateTime,
            CreatedUser,
            UpdatedDateTime,
            UpdatedUser,
            Assignee,
            StartDate,
            DueDate,
            EstimatedHours,
            ActualHours,
            ChildIssue,
            CustomField
        }

        /// <summary> �\�[�g�Ώۂ��擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> sort </remarks>
        public SortKeyField? SortKey { get; set; }

        /// <summary> �\�[�g�ΏۂɃJ�X�^���t�B�[���h��p����Ƃ��̑Ώ�ID ���擾�܂��͐ݒ肵�܂��D </summary>
        public int SortCustomFieldId { get; set; }

        /// <summary> �\�[�g�����������ǂ������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> order </remarks>
        public bool OrderByAsc { get; set; }

        /// <summary> �������ʂ̌��̃I�t�Z�b�g���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> offset </remarks>
        public long? Offset { get; set; }

        /// <summary> �������ʂ̍ő�擾�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> count </remarks>
        public long? Count { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̓o�^���̊��Ԃ̊J�n�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> createdSince </remarks>
        public DateTime? CreatedSince { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̓o�^���̊��Ԃ̍ŏI�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> createdUntil </remarks>
        public DateTime? CreatedUntil { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̍X�V���̊��Ԃ̊J�n�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> updatedSince </remarks>
        public DateTime? UpdatedSince { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̍X�V���̊��Ԃ̏I�������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> updatedUntil </remarks>
        public DateTime? UpdatedUntil { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̊J�n���̊��Ԃ̊J�n�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> startDateSince </remarks>
        public DateTime? StartDateSince { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̊J�n���̊��Ԃ̏I�������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> startDateUntil </remarks>
        public DateTime? StartDateUntil { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̊������̊��Ԃ̊J�n�����擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> dueDateSince </remarks>
        public DateTime? DueDateSince { get; set; }

        /// <summary> �����Ώۂ̉ۑ�̊������̊��Ԃ̏I�������擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> dueDateUntil </remarks>
        public DateTime? DueDateUntil { get; set; }

        /// <summary> �����Ώۂ̉ۑ��ID �̈ꗗ���擾���܂��D </summary>
        /// <remarks> id[] </remarks>
        public List<long> Ids { get; private set; }

        /// <summary> �����Ώۂ̐e�ۑ��ID �̈ꗗ���擾���܂��D </summary>
        /// <remarks> parentIssueId[] </remarks>
        public List<long> ParentIssueId { get; private set; }

        /// <summary> �����Ώۂ̃L�[���[�h���擾�܂��͐ݒ肵�܂��D </summary>
        /// <remarks> keyword </remarks>
        public string Keyword { get; set; }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̃L�[���[�h��ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="keyword"> �����L�[���[�h </param>
        /// <remarks> customField_${id} </remarks>
        public void SetCustomFieldKeyword(long fieldId, string keyword)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}", fieldId), keyword));
        }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̍ŏ��l��ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="min"> �ŏ��l </param>
        /// <remarks> customField_${id}_min </remarks>
        public void SetCustomFieldMin(long fieldId, double min)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_min", fieldId), string.Format("{0}", min)));
        }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̍ő�l��ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="max"> �ő�l </param>
        /// <remarks> customField_${id}_max </remarks>
        public void SetCustomFieldMax(long fieldId, double max)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_max", fieldId), string.Format("{0}", max)));
        }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̊J�n����ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="min"> �J�n�� </param>
        /// <remarks> customField_${id}_min </remarks>
        public void SetCustomFieldMin(long fieldId, DateTime min)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_min", fieldId), min.ToString(Backlog.DateFormat)));
        }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̍ŏI����ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="max"> �ŏI�� </param>
        /// <remarks> customField_${id}_max </remarks>
        public void SetCustomFieldMax(long fieldId, DateTime max)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_max", fieldId), max.ToString(Backlog.DateFormat)));
        }

        /// <summary> �����Ώۂ̃J�X�^���t�B�[���h�̃��X�g�̃A�C�e����ID��ݒ肵�܂� </summary>
        /// <param name="fieldId"> �J�X�^���t�B�[���h ID </param>
        /// <param name="itemIds"> ���X�g�̃A�C�e���� ID </param>
        /// <remarks> customField_${id}[] </remarks>
        public void SetCustomFieldItems(long fieldId, List<long> itemIds)
        {
            _searchConditions.AddRange(itemIds.ToKeyValuePairs(string.Format("customField_{0}[]", fieldId)));
        }

        /// <summary> �\�[�g�Ώۂ�\����������擾���܂��D </summary>
        /// <param name="key">�\�[�g�Ώ�</param>
        /// <returns>�\�[�g�Ώۂ�\��������</returns>
        private string GetSortKeyFieldName(SortKeyField key)
        {
            /*
             * "issueType", "category", "version", "milestone"
             * "summary", "status", "priority", "attachment", "sharedFile"
             * "created", "createdUser", "updated", "updatedUser"
             * "assignee", "startDate", "dueDate", "estimatedHours",
             * "actualHours", "childIssue", "customField_${id}"
             */
            switch (key)
            {
                case SortKeyField.IssueType:
                    return "issueType";

                case SortKeyField.Category:
                    return "category";

                case SortKeyField.Version:
                    return "version";

                case SortKeyField.MileStone:
                    return "milestone";

                case SortKeyField.Summary:
                    return "summary";

                case SortKeyField.Status:
                    return "status";

                case SortKeyField.Priority:
                    return "priority";

                case SortKeyField.Attachment:
                    return "attachment";

                case SortKeyField.SharedFile:
                    return "sharedFile";

                case SortKeyField.CreatedDateTime:
                    return "created";

                case SortKeyField.CreatedUser:
                    return "createdUser";

                case SortKeyField.UpdatedDateTime:
                    return "updated";

                case SortKeyField.UpdatedUser:
                    return "updatedUser";

                case SortKeyField.Assignee:
                    return "assignee";

                case SortKeyField.StartDate:
                    return "startDate";

                case SortKeyField.DueDate:
                    return "dueDate";

                case SortKeyField.EstimatedHours:
                    return "estimatedHours";

                case SortKeyField.ActualHours:
                    return "actualHours";

                case SortKeyField.ChildIssue:
                    return "childIssue";

                case SortKeyField.CustomField:
                    return string.Format("customField_{0}", SortCustomFieldId);
            }

            return string.Empty;
        }
    }
}