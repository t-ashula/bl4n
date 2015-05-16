// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChangeLogDetail.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> Changelog のエントリを表します </summary>
    public interface IChangeLogDetail
    {
        /// <summary> 更新されたフィールドを取得します </summary>
        string Field { get; }

        /// <summary> 更新後の値を取得します </summary>
        string NewValue { get; }

        /// <summary> 更新前の値を取得します </summary>
        string OriginalValue { get; }

        /// <summary> 添付ファイル情報を取得します </summary>
        IAttachmentInfo AttachmentInfo { get; }

        /// <summary> 通知情報を取得します </summary>
        INotificationInfo NotificationInfo { get; }

        /// <summary> 属性情報を取得します </summary>
        IAttributeInfo AttributeInfo { get; }
    }

    [DataContract]
    internal class ChangeLogDetail : IChangeLogDetail
    {
        [DataMember(Name = "field")]
        public string Field { get; private set; }

        [DataMember(Name = "newValue")]
        public string NewValue { get; private set; }

        [DataMember(Name = "originalValue")]
        public string OriginalValue { get; private set; }

        [DataMember(Name = "attachmentInfo")]
        private AttachmentInfo _attachmentInfo;

        [IgnoreDataMember]
        public IAttachmentInfo AttachmentInfo
        {
            get { return _attachmentInfo; }
        }

        [DataMember(Name = "notificationInfo")]
        private NofiticationInfo _notificationInfo;

        [IgnoreDataMember]
        public INotificationInfo NotificationInfo
        {
            get { return _notificationInfo; }
        }

        [DataMember(Name = "attributeInfo")]
        private AttributeInfo _attributeInfo;

        [IgnoreDataMember]
        public IAttributeInfo AttributeInfo
        {
            get { return _attributeInfo; }
        }
    }
}