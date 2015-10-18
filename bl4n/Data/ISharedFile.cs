// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISharedFile.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> shared_files; library / user app </summary>
    public interface ISharedFile
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 種類を取得します． </summary>
        string Type { get; }

        /// <summary> ファイル名を取得します． </summary>
        string Name { get; }

        /// <summary> ディレクトリ名を取得します． </summary>
        string Dir { get; }

        /// <summary> ファイルサイズを取得します． </summary>
        long Size { get; }

        /// <summary> 作成ユーザを取得します． </summary>
        IUser CreatedUser { get; }

        /// <summary> 作成日時を取得します． </summary>
        DateTime Created { get; }

        /// <summary> 更新ユーザを取得します． </summary>
        IUser UpdatedUser { get; }

        /// <summary> 更新日時を取得します． </summary>
        DateTime Updated { get; }
    }

    /// <summary> shared_file; api / library </summary>
    [DataContract]
    internal sealed class SharedFile : ExtraJsonPropertyReadableObject, ISharedFile
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "type")]
        public string Type { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "dir")]
        public string Dir { get; private set; }

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

        [DataMember(Name = "updatedUser")]
        private User _updatedUser;

        [IgnoreDataMember]
        public IUser UpdatedUser
        {
            get { return _updatedUser; }
        }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; private set; }
    }
}