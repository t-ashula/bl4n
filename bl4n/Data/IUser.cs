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
        long Id { get; }

        string UserId { get; }

        string Name { get; }

        int RoleType { get; }

        string Lang { get; }

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