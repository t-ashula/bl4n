// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 追加するカスタムフィールドのオプションの基底クラスを表します </summary>
    public abstract class AddCustomFieldOptions : OptionalParams
    {
        private const string ApplicableIssueTypesProperty = "applicableIssueTypes[]";
        private const string DescriptionProperty = "description";
        private const string RequiredProperty = "required";
        private long[] _applicableIssueTypes;
        private string _description;
        private bool _required;

        /// <summary> <see cref="AddCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="typeId">カスタムフィールド種別</param>
        /// <param name="name">名称</param>
        protected AddCustomFieldOptions(CustomFieldTypes typeId, string name)
            : base(ApplicableIssueTypesProperty, DescriptionProperty, RequiredProperty)
        {
            Name = name;
            TypeId = typeId;
        }

        /// <summary> カスタムフィールド種別を取得します </summary>
        public CustomFieldTypes TypeId { get; private set; }

        /// <summary> カスタムフィールド名を取得または設定します </summary>
        public string Name { get; set; }

        /// <summary> 適用対象の課題種別のID一覧を取得または設定します </summary>
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

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public abstract IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs();

        /// <summary> カスタムフィールド規定のリクエスト用 key-value ペアの一覧を得ます </summary>
        /// <returns> 規定のペア一覧 </returns>
        protected List<KeyValuePair<string, string>> CoreKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("typeId", ((int)TypeId).ToString())
            };

            if (IsPropertyChanged(ApplicableIssueTypesProperty))
            {
                if (ApplicableIssueTypes != null)
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
    }
}