// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionalParams.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 必須ではないパラメータの変更を追跡します </summary>
    public abstract class OptionalParams
    {
        private readonly Dictionary<string, bool> _changing;

        /// <summary> <see cref="OptionalParams"/> のインスタンスを初期化します </summary>
        /// <param name="keys"> 変更を追跡するプロパティ名 </param>
        protected OptionalParams(params string[] keys)
        {
            _changing = new Dictionary<string, bool>();
            AddTrackingKeys(keys);
        }

        /// <summary> 変更を追跡するプロパティ名を追加します </summary>
        /// <param name="keys"> 変更を追跡するプロパティ名 </param>
        protected void AddTrackingKeys(params string[] keys)
        {
            foreach (var key in keys)
            {
                _changing.Add(key, false);
            }
        }

        /// <summary> <paramref name="key"/> が変更されたかどうかを得ます </summary>
        /// <param name="key">プロパティ名</param>
        /// <returns> 変更されたら true </returns>
        protected bool IsPropertyChanged(string key)
        {
            return _changing[key];
        }

        /// <summary> <paramref name="key"/> が変更されたときに呼びます </summary>
        /// <param name="key"> 変更されたプロパティ名 </param>
        protected void PropertyChanged(string key)
        {
            _changing[key] = true;
        }
    }
}