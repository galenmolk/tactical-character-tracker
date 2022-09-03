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

            PropertyAsset[] properties = form.Properties;
            for (int i = 0, length = properties.Length; i < length; i++)
            {
                PropertyAsset property = properties[i];
                property.WriteProperty(writer);
            }
            
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object obj = serializer.Deserialize(reader);

            if (obj == null)
            {
                return null;
            }

            Form form = JsonConvert.DeserializeObject<Form>(obj.ToString());
            return form;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Form);
        }
    }
}