using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace webapi_format.App_Start
{
    public class JsonFormatter
    {
        public static void Setup(HttpConfiguration configuration)
        {
            configuration.Formatters[0] = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new OptionalModelConverter() }
                }
            };
        }
    }

    public class OptionalModelConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var genericArguments = objectType.GetGenericArguments();
            var value = serializer.Deserialize(reader, genericArguments[0]);
            return Activator.CreateInstance(objectType, value);
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    [JsonConverter(typeof(OptionalModelConverter))]
    public class Optional<T>
    {
        public Optional()
        {
            HasNewValue = false;
        }

        public Optional(T value)
        {
            HasNewValue = true;
            Value = value;
        }

        public bool HasNewValue { get; protected set; }
        public T Value { get; protected set; }

        public void Write(Action<T> func)
        {
            if (HasNewValue)
            {
                func(Value);
            }
        }
    }
}