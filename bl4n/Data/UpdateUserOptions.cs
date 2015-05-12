// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> ユーザーの更新オプションを表します </summary>
    public class UpdateUserOptions : OptionalParams
    {
        private string _name;
        private string _mailAddress;
        private string _passWord;
        private int _roleType;
        private const string NameProperty = "name";
        private const string PassWordProperty = "password";
        private const string MailAddressProperty = "mailAddress";
        private const string RoleTypeProperty = "roleType";

        /// <summary> <see cref="UpdateUserOptions"/> のインスタンスを初期化します </summary>
        public UpdateUserOptions()
            : base(NameProperty, PassWordProperty, MailAddressProperty, RoleTypeProperty)
        {
        }

        /// <summary> ユーザー名を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> メールアドレスを取得または設定します </summary>
        public string MailAddress
        {
            get { return _mailAddress; }
            set
            {
                _mailAddress = value;
                PropertyChanged(MailAddressProperty);
            }
        }

        /// <summary> 権限を取得または設定します </summary>
        public int RoleType
        {
            get { return _roleType; }
            set
            {
                _roleType = value;
                PropertyChanged(RoleTypeProperty);
            }
        }

        /// <summary> パスワードを取得または設定します </summary>
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                PropertyChanged(PassWordProperty);
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();

            if (IsPropertyChanged(NameProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NameProperty, Name));
            }

            if (IsPropertyChanged(MailAddressProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(MailAddressProperty, MailAddress));
            }

            if (IsPropertyChanged(RoleTypeProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(RoleTypeProperty, RoleType.ToString()));
            }

            if (IsPropertyChanged(PassWordProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(PassWordProperty, PassWord));
            }

            return pairs;
        }
    }
}