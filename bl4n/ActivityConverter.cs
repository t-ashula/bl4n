// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityConverter.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BL4N.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BL4N
{
    internal class ActivityConverter : AbstractJsonConverter<Activity>
    {
        protected override Activity Create(Type objectType, JObject jObject)
        {
            if (!FieldExists(jObject, "type", JTokenType.Integer))
            {
                throw new InvalidOperationException();
            }

            var activityType = jObject["type"].Value<int>();

            switch (activityType)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return new IssueActivity();

                case 5:
                case 6:
                case 7:
                    return new WikiActivity();

                case 14:
                    return new BulkUpdateActivity();

                case 15:
                case 16:
                    return new ProjectActivity();
            }

            throw new InvalidOperationException();
        }
    }

    internal abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected static bool FieldExists(JObject jObject, string name, JTokenType type)
        {
            JToken token;
            return jObject.TryGetValue(name, out token) && token.Type == type;
        }
    }
}