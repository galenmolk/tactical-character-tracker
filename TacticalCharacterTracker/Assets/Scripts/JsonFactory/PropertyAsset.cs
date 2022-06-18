using Newtonsoft.Json;
using UnityEngine;

namespace JsonFactory
{
    public abstract class PropertyAsset : ScriptableObject
    {
        // public abstract string Serialize();

        public abstract JsonWriter WriteProperty(JsonWriter writer);
    }
    
    public abstract class PropertyAsset<T> : PropertyAsset
    {
        [SerializeField] protected string propertyName;

        [SerializeField] protected T value;

        // private Property<T> property;

        public PropertyBinding<T> BindingPrefab => bindingPrefab;
        [SerializeField] private PropertyBinding<T> bindingPrefab;

        private PropertyConverter<Property<T>, T> Converter => converter ??= new PropertyConverter<Property<T>, T>();
        private PropertyConverter<Property<T>, T> converter;

        // public override string Serialize()
        // {
        //     // property = new Property<T>(propertyName, value);
        //     // return JsonConvert.SerializeObject(property, Converter);
        // }

        public override JsonWriter WriteProperty(JsonWriter writer)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteValue(value);
            return writer;
        }
    }
}
