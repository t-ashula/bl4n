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
    /// <summary> wiki page �̗�����\���܂� </summary>
    public interface IWikiPageHistory
    {
        /// <summary> �y�[�W ID ���擾���܂� </summary>
        long PageId { get; }

        /// <summary> �o�[�W�������擾���܂� </summary>
        long Version { get; }

        /// <summary> �y�[�W�� ���擾���܂� </summary>
        string Name { get; }

        /// <summary> �y�[�W���e���擾���܂� </summary>
        string Content { get; }

        /// <summary> �쐬���[�U�[���擾���܂� </summary>
        IUser CreatedUser { get; }

        /// <summary> �쐬�������擾���܂� </summary>
        DateTime Created { get; }
    }

    [DataContract]
    internal class WikiPageHistory : IWikiPageHistory
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