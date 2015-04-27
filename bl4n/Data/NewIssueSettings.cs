// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewIssueSettings.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 課題追加用のオプションを表します </summary>
    public class NewIssueSettings
    {
        private readonly List<KeyValuePair<string, string>> _customFieldValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewIssueSettings"/> class.
        /// </summary>
        public NewIssueSettings()
            : this(0, 0, 0, string.Empty)
        {
        }

        /// <summary> initialize <see cref="NewIssueSettings"/> </summary>
        /// <param name="projectId">project id</param>
        /// <param name="issueTypeId">issue type id</param>
        /// <param name="priorityId">priority id</param>
        /// <param name="summary">summary</param>
        public NewIssueSettings(long projectId, long issueTypeId, long priorityId, string summary)
        {
            _customFieldValues = new List<KeyValuePair<string, string>>();
            ProjectId = projectId;
            IssueTypeId = issueTypeId;
            PriorityId = priorityId;
            Summary = summary;
            AttachmentIds = new List<long>();
            NotifiedUserIds = new List<long>();
            MilestoneIds = new List<long>();
            VersionIds = new List<long>();
            CategoryIds = new List<long>();
        }

        /// <summary> get options as list of KeyValuePair </summary>
        /// <returns> list of KeyValuePair{string, string} </returns>
        public List<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectId", string.Format("{0}", ProjectId)),
                new KeyValuePair<string, string>("summary", Summary),
                new KeyValuePair<string, string>("issueTypeId", string.Format("{0}", IssueTypeId)),
                new KeyValuePair<string, string>("priorityId", string.Format("{0}", PriorityId))
            };
            if (ParentIssueId.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("parentIssueId", string.Format("{0}", ParentIssueId.Value)));
            }

            if (!string.IsNullOrEmpty(Description))
            {
                pairs.Add(new KeyValuePair<string, string>("description", Description));
            }

            if (StartDate.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("startDate", StartDate.Value.ToString(Backlog.DateFormat)));
            }

            if (DueDate.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("dueDate", DueDate.Value.ToString(Backlog.DateFormat)));
            }

            if (EstimatedHours.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("estimatedHours", string.Format("{0}", EstimatedHours.Value)));
            }

            if (ActualHours.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("actualHours", string.Format("{0}", ActualHours.Value)));
            }

            pairs.AddRange(CategoryIds.ToKeyValuePairs("categoryId[]"));
            pairs.AddRange(VersionIds.ToKeyValuePairs("versionId[]"));
            pairs.AddRange(MilestoneIds.ToKeyValuePairs("milestoneId[]"));
            if (AssigneeId.HasValue)
            {
                pairs.Add(new KeyValuePair<string, string>("assigneeId", string.Format("{0}", AssigneeId.Value)));
            }

            pairs.AddRange(NotifiedUserIds.ToKeyValuePairs("notifiedUserId[]"));
            pairs.AddRange(AttachmentIds.ToKeyValuePairs("attachmentId[]"));

            pairs.AddRange(_customFieldValues);
            return pairs;
        }

        /// <summary> gets or sets ProjectId </summary>
        /// <remarks> projectId </remarks>
        public long ProjectId { get; set; }

        /// <summary> gets or sets IssueTypeId </summary>
        /// <remarks> issueTypeId </remarks>
        public long IssueTypeId { get; set; }

        /// <summary> gets or sets PriorityId </summary>
        /// <remarks> priorityId </remarks>
        public long PriorityId { get; set; }

        /// <summary> gets or sets Summary </summary>
        /// <remarks> summary </remarks>
        public string Summary { get; set; }

        /// <summary> gets or sets Summary </summary>
        /// <remarks> parentIssueId </remarks>
        public long? ParentIssueId { get; set; }

        /// <summary> gets or sets Description </summary>
        /// <remarks> description </remarks>
        public string Description { get; set; }

        /// <summary> gets or sets StartDate </summary>
        /// <remarks> startDate </remarks>
        public DateTime? StartDate { get; set; }

        /// <summary> gets or sets DueDate </summary>
        /// <remarks> dueDate </remarks>
        public DateTime? DueDate { get; set; }

        /// <summary> gets or sets EstimatedHours </summary>
        /// <remarks> estimatedHours </remarks>
        public double? EstimatedHours { get; set; }

        /// <summary> gets or sets ActualHours </summary>
        /// <remarks> actualHours </remarks>
        public double? ActualHours { get; set; }

        /// <summary> gets CategoryIds </summary>
        /// <remarks> categoryId[] </remarks>
        public List<long> CategoryIds { get; private set; }

        /// <summary> gets VersionIds </summary>
        /// <remarks> versionId[] </remarks>
        public List<long> VersionIds { get; private set; }

        /// <summary> gets or sets MilestoneIds </summary>
        /// <remarks> milestoneId[] </remarks>
        public List<long> MilestoneIds { get; private set; }

        /// <summary> gets or sets AssigneeId </summary>
        /// <remarks> assigneeId </remarks>
        public long? AssigneeId { get; set; }

        /// <summary> gets NotifiedUserIds </summary>
        /// <remarks> notifiedUserId[] </remarks>
        public List<long> NotifiedUserIds { get; private set; }

        /// <summary> gets or sets AttachmentIds </summary>
        /// <remarks> attachmetId[] </remarks>
        public List<long> AttachmentIds { get; private set; }

        /// <summary> add custom field value </summary>
        /// <param name="fieldId"> field id </param>
        /// <param name="value"> value </param>
        public void AddCustomFieldValue(long fieldId, double value)
        {
            _customFieldValues.Add(new KeyValuePair<string, string>(string.Format("customField_{0}", fieldId), string.Format("{0}", value)));
        }

        /// <summary> add custom field value </summary>
        /// <param name="fieldId"> field id </param>
        /// <param name="value"> value </param>
        public void AddCustomFieldValue(long fieldId, DateTime value)
        {
            _customFieldValues.Add(new KeyValuePair<string, string>(string.Format("customField_{0}", fieldId), value.ToString(Backlog.DateFormat)));
        }

        /// <summary> add custom field value </summary>
        /// <param name="fieldId"> field </param>
        /// <param name="value"> value </param>
        public void AddCustomFieldValue(long fieldId, string value)
        {
            _customFieldValues.Add(new KeyValuePair<string, string>(string.Format("customField_{0}", fieldId), value));
        }

        /// <summary> add custom field other value </summary>
        /// <param name="fieldId"> field </param>
        /// <param name="value"> value </param>
        public void AddCustomFieldOtherValue(long fieldId, string value)
        {
            _customFieldValues.Add(new KeyValuePair<string, string>(string.Format("customField_{0}_otherValue", fieldId), value));
        }
    }
}