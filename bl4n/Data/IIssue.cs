// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIssue.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 課題を表します． </summary>
    public interface IIssue
    {
        /// <summary> IDを取得します． </summary>
        long Id { get; }

        /// <summary> プロジェクトIDを取得します． </summary>
        long ProjectId { get; }

        /// <summary> キー文字列を取得します． </summary>
        string IssueKey { get; }

        /// <summary> キーIDを取得します． </summary>
        long KeyId { get; }

        /// <summary> 課題種別を取得します． </summary>
        IIssueType IssueType { get; }

        /// <summary> サマリを取得します． </summary>
        string Summary { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }

        /// <summary> 解決理由を取得します． </summary>
        IResolution Resolutions { get; }

        /// <summary> 優先度を取得します． </summary>
        IPriority Priority { get; }

        /// <summary> ステータスを取得します． </summary>
        IStatus Status { get; }

        /// <summary> 担当者を取得します． </summary>
        IUser Assignee { get; }

        /// <summary> カテゴリーの一覧を取得します． </summary>
        IList<ICategory> Categories { get; }

        /// <summary> バージョンの一覧を取得します． </summary>
        IList<IVersion> Versions { get; }

        /// <summary> マイルストーンの一覧を取得します． </summary>
        IList<IVersion> Milestones { get; }

        /// <summary> 開始日を取得します． </summary>
        DateTime? StartDate { get; }

        /// <summary> 期限日を取得します． </summary>
        DateTime? DueDate { get; }

        /// <summary> 予想作業時間を取得します． </summary>
        double? EstimatedHours { get; }

        /// <summary> 実作業時間を取得します． </summary>
        double? ActualHours { get; }

        /// <summary> 親課題のIDを取得します． </summary>
        long? ParentIssueId { get; }

        /// <summary> 作成ユーザを取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }

        /// <summary> 更新ユーザを取得します． </summary>
        IUser UpdatedUser { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }

        /// <summary> カスタムフィールドの一覧を取得します． </summary>
        IList<ICustomField> CustomFields { get; }

        /// <summary> 添付ファイルの一覧を取得します． </summary>
        IList<IAttachment> Attachments { get; }

        /// <summary> 共有ファイルへの参照の一覧を取得します． </summary>
        IList<ISharedFile> SharedFiles { get; }

        /// <summary> スターの一覧を取得します． </summary>
        IList<IStar> Stars { get; }
    }

    [DataContract]
    internal sealed class Issue : IIssue
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "projectId")]
        public long ProjectId { get; set; }

        [DataMember(Name = "issueKey")]
        public string IssueKey { get; set; }

        [DataMember(Name = "keyId")]
        public long KeyId { get; set; }

        [DataMember(Name = "issueType")]
        private IssueType _issueType;

        [IgnoreDataMember]
        public IIssueType IssueType
        {
            get { return _issueType; }
        }

        [DataMember(Name = "summary")]
        public string Summary { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "resolutions")]
        private Resolution _resolutions;

        [IgnoreDataMember]
        public IResolution Resolutions
        {
            get { return _resolutions; }
        }

        [DataMember(Name = "priority")]
        private Priority _priority;

        [IgnoreDataMember]
        public IPriority Priority
        {
            get { return _priority; }
        }

        [DataMember(Name = "status")]
        private Status _status;

        [IgnoreDataMember]
        public IStatus Status
        {
            get { return _status; }
        }

        [DataMember(Name = "assignee")]
        private User _assignee;

        [IgnoreDataMember]
        public IUser Assignee
        {
            get { return _assignee; }
        }

        [DataMember(Name = "category")]
        private List<Category> _categories;

        [IgnoreDataMember]
        public IList<ICategory> Categories
        {
            get { return _categories.ToList<ICategory>(); }
        }

        [DataMember(Name = "versions")]
        private List<Version> _versions;

        [IgnoreDataMember]
        public IList<IVersion> Versions
        {
            get { return _versions.ToList<IVersion>(); }
        }

        [DataMember(Name = "milestone")]
        private List<Version> _milestones;

        [IgnoreDataMember]
        public IList<IVersion> Milestones
        {
            get { return _milestones.ToList<IVersion>(); }
        }

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; private set; }

        [DataMember(Name = "dueDate")]
        public DateTime? DueDate { get; private set; }

        [DataMember(Name = "estimatedHours")]
        public double? EstimatedHours { get; private set; }

        [DataMember(Name = "actualHours")]
        public double? ActualHours { get; private set; }

        [DataMember(Name = "parentIssueId")]
        public long? ParentIssueId { get; private set; }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }

        [DataMember(Name = "customFields")]
        private List<CustomField> _customFields;

        [IgnoreDataMember]
        public IList<ICustomField> CustomFields
        {
            get { return _customFields.ToList<ICustomField>(); }
        }

        [DataMember(Name = "attachments")]
        private List<Attachment> _attachments;

        [IgnoreDataMember]
        public IList<IAttachment> Attachments
        {
            get { return _attachments.ToList<IAttachment>(); }
        }

        [DataMember(Name = "sharedFiles")]
        private List<SharedFile> _sharedFiles;

        [IgnoreDataMember]
        public IList<ISharedFile> SharedFiles
        {
            get { return _sharedFiles.ToList<ISharedFile>(); }
        }

        [DataMember(Name = "stars")]
        private List<Star> _stars;

        [IgnoreDataMember]
        public IList<IStar> Stars
        {
            get { return _stars.ToList<IStar>(); }
        }
    }
}