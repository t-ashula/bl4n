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

        /// <summary> 検索対象の課題の種別のIDのリストを取得します </summary>
        /// <remarks> issueTypeId[] </remarks>
        public List<long> IssueTypeIds { get; private set; }

        /// <summary> 検索対象の課題のカテゴリーのIDのリストを取得します </summary>
        /// <remarks> categoryId[] </remarks>
        public List<long> CategoryIds { get; private set; }

        /// <summary> 検索対象の課題のヴァージョンのIDのリストを取得します </summary>
        /// <remarks> versionId[] </remarks>
        public List<long> VersionIds { get; private set; }

        /// <summary> 検索対象の課題のマイルストーンのIDのリストを取得します </summary>
        /// <remarks> milestoneId[] </remarks>
        public List<long> MileStoneIds { get; private set; }

        /// <summary> 検索対象の課題のステータスのIDのリストを取得します </summary>
        /// <remarks> statusId[] </remarks>
        public List<long> StatusIds { get; private set; }

        /// <summary> 検索対象の課題の優先度のIDのリストを取得します </summary>
        /// <remarks> priorityId[] </remarks>
        public List<long> PriorityIds { get; private set; }

        /// <summary> 検索対象の課題の担当者のIDのリストを取得します </summary>
        /// <remarks> assignerId[] </remarks>
        public List<long> AssignerIds { get; private set; }

        /// <summary> 検索対象の課題の作成者のIDのリストを取得します </summary>
        /// <remarks> createdUserId[] </remarks>
        public List<long> CreateUserIds { get; private set; }

        /// <summary> 検索対象の課題の完了理由のIDのリストを取得します </summary>
        /// <remarks> resolutionId[] </remarks>
        public List<long> ResolutionIds { get; private set; }

        /// <summary> 親子課題のパターンを表します </summary>
        public enum ParentChildType
        {
            /// <summary> すべて（区別なし） </summary>
            All = 0,

            /// <summary> 子課題以外 </summary>
            ExcludeChild = 1,

            /// <summary> 子課題 </summary>
            Child = 2,

            /// <summary> 親課題も子課題も指定されていない課題 </summary>
            NeitherParentNorChild = 3,

            /// <summary> 親課題 </summary>
            Parent = 4
        }

        /// <summary> 検索対象の課題の親子課題のパターンを取得または設定します </summary>
        /// <remarks> parentChild </remarks>
        public ParentChildType ParentChild { get; set; }

        /// <summary> 検索対象の課題の添付ファイルの有無を取得または設定します． </summary>
        /// <remarks> attachment </remarks>
        public bool? HasAttachment { get; set; }

        /// <summary> 検索対象の課題の共有ファイルへのリンクの有無を取得または設定します． </summary>
        /// <remarks> sharedFile </remarks>
        public bool? HasSharedFile { get; set; }

        /// <summary> ソート対象を表します </summary>
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

        /// <summary> ソート対象を取得または設定します． </summary>
        /// <remarks> sort </remarks>
        public SortKeyField? SortKey { get; set; }

        /// <summary> ソート対象にカスタムフィールドを用いるときの対象ID を取得または設定します． </summary>
        public int SortCustomFieldId { get; set; }

        /// <summary> ソート順が昇順かどうかを取得または設定します． </summary>
        /// <remarks> order </remarks>
        public bool OrderByAsc { get; set; }

        /// <summary> 検索結果の個数のオフセットを取得または設定します． </summary>
        /// <remarks> offset </remarks>
        public long? Offset { get; set; }

        /// <summary> 検索結果の最大取得個数を取得または設定します． </summary>
        /// <remarks> count </remarks>
        public long? Count { get; set; }

        /// <summary> 検索対象の課題の登録日の期間の開始日を取得または設定します． </summary>
        /// <remarks> createdSince </remarks>
        public DateTime? CreatedSince { get; set; }

        /// <summary> 検索対象の課題の登録日の期間の最終日を取得または設定します． </summary>
        /// <remarks> createdUntil </remarks>
        public DateTime? CreatedUntil { get; set; }

        /// <summary> 検索対象の課題の更新日の期間の開始日を取得または設定します． </summary>
        /// <remarks> updatedSince </remarks>
        public DateTime? UpdatedSince { get; set; }

        /// <summary> 検索対象の課題の更新日の期間の終了日を取得または設定します． </summary>
        /// <remarks> updatedUntil </remarks>
        public DateTime? UpdatedUntil { get; set; }

        /// <summary> 検索対象の課題の開始日の期間の開始日を取得または設定します． </summary>
        /// <remarks> startDateSince </remarks>
        public DateTime? StartDateSince { get; set; }

        /// <summary> 検索対象の課題の開始日の期間の終了日を取得または設定します． </summary>
        /// <remarks> startDateUntil </remarks>
        public DateTime? StartDateUntil { get; set; }

        /// <summary> 検索対象の課題の期限日の期間の開始日を取得または設定します． </summary>
        /// <remarks> dueDateSince </remarks>
        public DateTime? DueDateSince { get; set; }

        /// <summary> 検索対象の課題の期限日の期間の終了日を取得または設定します． </summary>
        /// <remarks> dueDateUntil </remarks>
        public DateTime? DueDateUntil { get; set; }

        /// <summary> 検索対象の課題のID の一覧を取得します． </summary>
        /// <remarks> id[] </remarks>
        public List<long> Ids { get; private set; }

        /// <summary> 検索対象の親課題のID の一覧を取得します． </summary>
        /// <remarks> parentIssueId[] </remarks>
        public List<long> ParentIssueId { get; private set; }

        /// <summary> 検索対象のキーワードを取得または設定します． </summary>
        /// <remarks> keyword </remarks>
        public string Keyword { get; set; }

        /// <summary> 検索対象のカスタムフィールドのキーワードを設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="keyword"> 検索キーワード </param>
        /// <remarks> customField_${id} </remarks>
        public void SetCustomFieldKeyword(long fieldId, string keyword)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}", fieldId), keyword));
        }

        /// <summary> 検索対象のカスタムフィールドの最小値を設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="min"> 最小値 </param>
        /// <remarks> customField_${id}_min </remarks>
        public void SetCustomFieldMin(long fieldId, double min)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_min", fieldId), string.Format("{0}", min)));
        }

        /// <summary> 検索対象のカスタムフィールドの最大値を設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="max"> 最大値 </param>
        /// <remarks> customField_${id}_max </remarks>
        public void SetCustomFieldMax(long fieldId, double max)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_max", fieldId), string.Format("{0}", max)));
        }

        /// <summary> 検索対象のカスタムフィールドの開始日を設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="min"> 開始日 </param>
        /// <remarks> customField_${id}_min </remarks>
        public void SetCustomFieldMin(long fieldId, DateTime min)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_min", fieldId), min.ToString(Backlog.DateFormat)));
        }

        /// <summary> 検索対象のカスタムフィールドの最終日を設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="max"> 最終日 </param>
        /// <remarks> customField_${id}_max </remarks>
        public void SetCustomFieldMax(long fieldId, DateTime max)
        {
            _searchConditions.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_max", fieldId), max.ToString(Backlog.DateFormat)));
        }

        /// <summary> 検索対象のカスタムフィールドのリストのアイテムのIDを設定します </summary>
        /// <param name="fieldId"> カスタムフィールド ID </param>
        /// <param name="itemIds"> リストのアイテムの ID </param>
        /// <remarks> customField_${id}[] </remarks>
        public void SetCustomFieldItems(long fieldId, List<long> itemIds)
        {
            _searchConditions.AddRange(itemIds.ToKeyValuePairs(string.Format("customField_{0}[]", fieldId)));
        }

        /// <summary> ソート対象を表す文字列を取得します． </summary>
        /// <param name="key">ソート対象</param>
        /// <returns>ソート対象を表す文字列</returns>
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