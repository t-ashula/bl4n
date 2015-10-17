// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtraJsonPropertyReadableObject.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BL4N.Data
{
    /// <summary> クラスのメンバーにデシリアライズされなかった JSON のプロパティを取得可能なオブジェクトを表します </summary>
    public abstract class ExtraJsonPropertyReadableObject
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> extraProperties = new Dictionary<string, JToken>();

        /// <summary> 余分なプロパティとその値を文字列のペアで得ます </summary>
        /// <returns> 余分なプロパティの一覧 </returns>
        public IDictionary<string, string> GetExtraProperties()
        {
            return extraProperties.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
        }

        /// <summary>
        /// 余分なプロパティがあるかどうかを取得します
        /// </summary>
        /// <returns> 余分なプロパティがあるとき true </returns>
        public bool HasExtraProperty()
        {
            return extraProperties.Any();
        }
    }
}