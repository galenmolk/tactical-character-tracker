using System;
using Newtonsoft.Json;

namespace JsonFactory
{
    public class FormConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Form form = (Form) value;
            
            writer.WriteStartObject();

            var properties = form.Properties;
            for (int i = 0, length = properties.Length; i < length; i++)
            {
                PropertyAsset property = properties[i];
                property.WriteProperty(writer);
            }
            
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Form>(t.ToString());
            return iv;        
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Form);
        }
    }
}