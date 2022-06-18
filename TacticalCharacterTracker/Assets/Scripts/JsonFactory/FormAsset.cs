using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace JsonFactory
{
    [CreateAssetMenu]
    public class FormAsset : ScriptableObject, ISerializable
    {
        public PropertyAsset[] Properties => properties;
        [SerializeField] private PropertyAsset[] properties;
        
        public string Serialize()
        {
            return JsonConvert.SerializeObject(new Form(properties), new FormConverter());
        }
    }
}
