// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWikiPageUpdate.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> Wiki ページの更新情報を表します </summary>
    public interface IWikiPageUpdate
    {
        /// <summary> Wiki ページを取得します． </summary>
        IWikiPage WikiPage { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class WikiPageUpdate : ExtraJsonPropertyReadableObject, IWikiPageUpdate
    {
        [DataMember(Name = "page")]
        private WikiPage _wikipage;

        [IgnoreDataMember]
        public IWikiPage WikiPage
        {
            get { return _wikipage; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}