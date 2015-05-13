// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddListTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> リスト形式のカスタムフィールドの追加用のオプションの基底クラスを表します </summary>
    public abstract class AddListTypeCustomFieldOptions : AddCustomFieldOptions
    {
        private string[] _items;
        private bool _allowInput;
        private bool _allowAddItem;
        private const string ItemsProperty = "items[]";
        private const string AllowInputProperty = "allowInput";
        private const string AllowAddItemProperty = "allowAddItem";

        /// <summary>
        /// <see cref="AddListTypeCustomFieldOptions"/>
        /// </summary>
        /// <param name="typeId">種別</param>
        /// <param name="name">名称</param>
        protected AddListTypeCustomFieldOptions(CustomFieldTypes typeId, string name)
            : base(typeId, name)
        {
            AddTrackingKeys(ItemsProperty, AllowInputProperty, AllowAddItemProperty);
        }

        /// <inheritdoc/>
        public override IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var opt = CoreKeyValuePairs();
            if (IsPropertyChanged(ItemsProperty))
            {
                opt.AddRange(Items.ToKeyValuePairs(ItemsProperty));
            }

            if (IsPropertyChanged(AllowInputProperty))
            {
                opt.Add(new KeyValuePair<string, string>(AllowInputProperty, AllowInput ? "true" : "false"));
            }

            if (IsPropertyChanged(AllowAddItemProperty))
            {
                opt.Add(new KeyValuePair<string, string>(AllowAddItemProperty, AllowAddItem ? "true" : "false"));
            }

            return opt;
        }

        /// <summary> 項目一覧を取得または設定します </summary>
        public string[] Items
        {
            get { return _items; }
            set
            {
                _items = value;
                PropertyChanged(ItemsProperty);
            }
        }

        /// <summary> 入力可能にするかどうかを取得または設定します </summary>
        public bool AllowInput
        {
            get { return _allowInput; }
            set
            {
                _allowInput = value;
                PropertyChanged(AllowInputProperty);
            }
        }

        /// <summary> 項目の追加を可能にするかどうかを取得または設定します </summary>
        public bool AllowAddItem
        {
            get { return _allowAddItem; }
            set
            {
                _allowAddItem = value;
                PropertyChanged(AllowAddItemProperty);
            }
        }
    }
}