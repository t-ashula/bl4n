// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> ユーザーの追加用のオプションを表します </summary>
    public class AddUserOptions
    {
        /// <summary> <see cref="AddUserOptions"/> のインスタンスを初期化します </summary>
        /// <param name="userId">user id</param>
        /// <param name="pass">password</param>
        /// <param name="name">name</param>
        /// <param name="mailAddress">mail address</param>
        /// <param name="roleType">role type</param>
        public AddUserOptions(string userId, string pass, string name, string mailAddress, int roleType)
        {
            UserId = userId;
            PassWord = pass;
            Name = name;
            MailAddress = mailAddress;
            RoleType = roleType;
        }

        /// <summary> ユーザー識別名を取得または設定します </summary>
        public string UserId { get; set; }

        /// <summary> パスワードを取得または設定します </summary>
        public string PassWord { get; set; }

        /// <summary> 表示名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> メールアドレスを取得または設定します </summary>
        public string MailAddress { get; set; }

        /// <summary> 権限を取得または設定します </summary>
        public int RoleType { get; set; }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var kvs = new[]
            {
                new KeyValuePair<string, string>("userId", UserId),
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("mailAddress", MailAddress),
                new KeyValuePair<string, string>("roleType", RoleType.ToString()),
                new KeyValuePair<string, string>("password", PassWord)
            };
            return kvs;
        }
    }
}