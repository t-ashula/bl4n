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
    /// <summary> �A�u�X�g���N�g�N���X�̔h���N���X�̂��߂̃R���o�[�^��\���܂� </summary>
    /// <typeparam name="T">�A�u�X�g���N�g�N���X</typeparam>
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

        /// <summary> �t�B�[���h�����݂��邩�ǂ������擾���܂� </summary>
        /// <param name="jobj">Json.net �I�u�W�F�N�g</param>
        /// <param name="name">�t�B�[���h��</param>
        /// <param name="type">�t�B�[���h�̌^</param>
        /// <returns>���݂���Ƃ� true</returns>
        protected static bool FieldExists(JObject jobj, string name, JTokenType type)
        {
            JToken token;
            return jobj.TryGetValue(name, out token) && token.Type == type;
        }
    }
}