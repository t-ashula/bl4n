// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractJsonConverter.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BL4N
{
    /// <summary> アブストラクトクラスの派生クラスのためのコンバータを表します </summary>
    /// <typeparam name="T">アブストラクトクラス</typeparam>
    internal abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jobj);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JObject.Load(reader);
            var target = Create(objectType, jobj);
            serializer.Populate(jobj.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary> フィールドが存在するかどうかを取得します </summary>
        /// <param name="jobj">Json.net オブジェクト</param>
        /// <param name="name">フィールド名</param>
        /// <param name="type">フィールドの型</param>
        /// <returns>存在するとき true</returns>
        protected static bool FieldExists(JObject jobj, string name, JTokenType type)
        {
            JToken token;
            return jobj.TryGetValue(name, out token) && token.Type == type;
        }
    }
}