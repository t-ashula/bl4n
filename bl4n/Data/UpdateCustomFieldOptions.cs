// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 更新するカスタムフィールドのオプションの基底クラスを表します </summary>
    public abstract class UpdateCustomFieldOptions : OptionalParams
    {
        private bool _required;
        private string _description;
        private long[] _applicableIssueTypes;
        private const string ApplicableIssueTypesProperty = "applicableIssueTypes[]";
        private const string DescriptionProperty = "description";
        private const string RequiredProperty = "required";

        /// <summary> <see cref="UpdateCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name"> フィールド名 </param>
        protected UpdateCustomFieldOptions(string name)
            : base(ApplicableIssueTypesProperty, DescriptionProperty, RequiredProperty)
        {
            Name = name;
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public abstract IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs();

        /// <summary> カスタムフィールド規定のリクエスト用 key-value ペアの一覧を得ます </summary>
        /// <returns> 規定のペア一覧 </returns>
        protected List<KeyValuePair<string, string>> CoreKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name)
            };

            if (IsPropertyChanged(ApplicableIssueTypesProperty))
            {
                if (ApplicableIssueTypes == null || ApplicableIssueTypes.Length == 0)
                {
                    pairs.Add(new KeyValuePair<string, string>(ApplicableIssueTypesProperty, string.Empty));
                }
                else
                {
                    pairs.AddRange(ApplicableIssueTypes.ToKeyValuePairs(ApplicableIssueTypesProperty));
                }
            }

            if (IsPropertyChanged(DescriptionProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(DescriptionProperty, Description));
            }

            if (IsPropertyChanged(RequiredProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(RequiredProperty, Required ? "true" : "false"));
            }

            return pairs;
        }

        /// <summary> フィールド名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> 適用対象の課題種別 ID を取得または設定します </summary>
        public long[] ApplicableIssueTypes
        {
            get { return _applicableIssueTypes; }
            set
            {
                _applicableIssueTypes = value;
                PropertyChanged(ApplicableIssueTypesProperty);
            }
        }

        /// <summary> 説明を取得または設定します </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(DescriptionProperty);
            }
        }

        /// <summary> 必須かどうかを取得または設定します </summary>
        public bool Required
        {
            get { return _required; }
            set
            {
                _required = value;
                PropertyChanged(RequiredProperty);
            }
        }
    }
}