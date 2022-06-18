using System;
using Newtonsoft.Json;

namespace JsonFactory
{
    public class PropertyConverter<TEntry, TValue> : JsonConverter where TEntry : Property<TValue>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TEntry t = (TEntry)value;
            
            writer.WritePropertyName(t.Key);
            writer.WriteValue(t.Value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<TEntry>(t.ToString());
            return iv;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TEntry);
        }
    }
}
