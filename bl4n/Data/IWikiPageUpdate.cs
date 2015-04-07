// --------------------------------------------------------------------------------------------------------------------
// <copyright content="IWikiPageUpdate.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    public interface IWikiPageUpdate
    {
        IWikiPage WikiPage { get; }

        DateTime Updated { get; }
    }

    [DataContract]
    internal sealed class WikiPageUpdate : IWikiPageUpdate
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