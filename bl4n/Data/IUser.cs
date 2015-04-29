// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUser.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> user </summary>
    public interface IUser
    {
        /// <summary> ID を取得します． </summary>
        long Id { get; }

        /// <summary> ユーザーID を取得します． </summary>
        string UserId { get; }

        /// <summary> 名前を取得します． </summary>
        string Name { get; }

        /// <summary> 権限を取得します． </summary>
        int RoleType { get; }

        /// <summary> UI の言語を取得します． </summary>
        string Lang { get; }

        /// <summary> メールアドレスを取得します． </summary>
        string MailAddress { get; }
    }

    /// <summary> user </summary>
    [DataContract]
    internal sealed class User : IUser
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "roleType")]
        public int RoleType { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }

        [DataMember(Name = "mailAddress")]
        public string MailAddress { get; set; }
    }
}