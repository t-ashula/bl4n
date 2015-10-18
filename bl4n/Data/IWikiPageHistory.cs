// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiPageHistory.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> wiki page の履歴を表します </summary>
    public interface IWikiPageHistory
    {
        /// <summary> ページ ID を取得します </summary>
        long PageId { get; }

        /// <summary> バージョンを取得します </summary>
        long Version { get; }

        /// <summary> ページ名 を取得します </summary>
        string Name { get; }

        /// <summary> ページ内容を取得します </summary>
        string Content { get; }

        /// <summary> 作成ユーザーを取得します </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal class WikiPageHistory : ExtraJsonPropertyReadableObject, IWikiPageHistory
    {
        [DataMember(Name = "pageId")]
        public long PageId { get; private set; }

        [DataMember(Name = "version")]
        public long Version { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "content")]
        public string Content { get; private set; }

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