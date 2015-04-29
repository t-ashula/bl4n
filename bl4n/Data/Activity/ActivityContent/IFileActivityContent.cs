// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> content for type  8, 9, 10 </summary>
    public interface IFileActivityContent : IActivityContent
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> ディレクトリ名を取得します． </summary>
        string Dir { get; }

        /// <summary> 名前を取得します． </summary>
        string Name { get; }

        /// <summary> ファイルサイズを取得します． </summary>
        long Size { get; }
    }

    [DataContract]
    internal sealed class FileActivityContent : ActivityContent, IFileActivityContent
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "dir")]
        public string Dir { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "size")]
        public long Size { get; private set; }
    }
}