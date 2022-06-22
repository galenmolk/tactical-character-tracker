using System.Collections.Generic;
using HexedHeroes.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace JsonFactory
{
    [CreateAssetMenu]
    public class AbilityListPropertyAsset : PropertyAsset<List<AbilityConfig>>
    {
        public override JsonWriter WriteProperty(JsonWriter writer)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteStartArray();

            for (int i = 0, length = value.Count; i < length; i++)
            {
                
            }
            
            writer.WriteEndArray();
            return writer;
        }
    }
}
