// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateListTypeCustomFieldOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> リストタイプのカスタムフィールドの更新オプションを表します </summary>
    public abstract class UpdateListTypeCustomFieldOptions : UpdateCustomFieldOptions
    {
        private const string ItemsProperty = "items[]";
        private const string AllowInputProperty = "allowInput";
        private const string AllowAddItemProperty = "allowAddItem";

        private string[] _items;
        private bool _allowInput;
        private bool _allowAddItem;

        /// <summary> <see cref="UpdateListTypeCustomFieldOptions"/> のインスタンスを初期化します </summary>
        /// <param name="name">フィールド名</param>
        protected UpdateListTypeCustomFieldOptions(string name)
            : base(name)
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