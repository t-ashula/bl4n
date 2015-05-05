// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComment.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> comment with id for type 17 </summary>
    public interface IComment
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> コメント文字列を取得します． </summary>
        string Content { get; }
    }

    [DataContract]
    internal sealed class Comment : IComment
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "content")]
        public string Content { get; private set; }
    }
}