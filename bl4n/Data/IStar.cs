// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStar.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> スターを表します </summary>
    public interface IStar
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> コメントを取得します． </summary>
        string Comment { get; }

        /// <summary> URL を取得します． </summary>
        string Url { get; }

        /// <summary> タイトルを取得します． </summary>
        string Title { get; }

        /// <summary> スターを付けたユーザーを取得します． </summary>
        IUser Presenter { get; }

        /// <summary> スターのついた日時を取得します． </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal class Star : IStar
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "comment")]
        public string Comment { get; private set; }

        [DataMember(Name = "url")]
        public string Url { get; private set; }

        [DataMember(Name = "title")]
        public string Title { get; private set; }

        [DataMember(Name = "presenter")]
        private User _presenter;

        public IUser Presenter
        {
            get { return _presenter; }
        }

        [DataMember(Name = "created")]
        public DateTime Created { get; private set; }
    }
}