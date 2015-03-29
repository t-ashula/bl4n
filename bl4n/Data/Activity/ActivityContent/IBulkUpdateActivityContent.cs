// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBulkUpdateActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> content for type 14 </summary>
    public interface IBulkUpdateActivityContent : IActivityContent
    {
        long TxId { get; }

        // comment but no id
        IComment Comment { get; }

        IList<ILink> Link { get; }

        IList<IChange> Changes { get; }
    }

    [DataContract]
    internal sealed class BulkUpdateActivityContent : ActivityContent, IBulkUpdateActivityContent
    {
        [DataMember(Name = "tx_id")]
        public long TxId { get; private set; }

        [DataMember(Name = "comment")]
        private Comment _comment;

        [IgnoreDataMember]
        public IComment Comment
        {
            get { return _comment; }
        }

        [DataMember(Name = "link")]
        private List<Link> _links;

        [IgnoreDataMember]
        public IList<ILink> Link
        {
            get { return _links.ToList<ILink>(); }
        }

        [DataMember(Name = "changes")]
        private Changes _changes;

        [IgnoreDataMember]
        public IList<IChange> Changes
        {
            get { return _changes.ToList<IChange>(); }
        }
    }
}