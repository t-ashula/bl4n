// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttachment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> attachments, </summary>
    public interface IAttachment
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> ファイル名を取得します． </summary>
        string Name { get; }

        /// <summary> ファイルサイズを取得します． </summary>
        long Size { get; }

        /// <summary> 作成者を表す <see cref="IUser"/> を取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal sealed class Attachment : IAttachment
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "size")]
        public long Size { get; private set; }

        [DataMember(Name = "createdUser")]
        private User _createdUser;

        [IgnoreDataMember]
        public IUser CreatedUser
        {
            get { return _createdUser; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }
    }
}