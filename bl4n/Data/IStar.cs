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
    public interface IStar
    {
        long Id { get; }

        string Comment { get; }

        string Url { get; }

        string Title { get; }

        IUser Presenter { get; }

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