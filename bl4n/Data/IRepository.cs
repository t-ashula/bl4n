// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> repository (git) </summary>
    public interface IRepository
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> 名称を取得します． </summary>
        string Name { get; }

        /// <summary> 説明を取得します． </summary>
        string Description { get; }
    }

    [DataContract]
    internal class Repository : ExtraJsonPropertyReadableObject, IRepository
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "description")]
        public string Description { get; private set; }
    }
}