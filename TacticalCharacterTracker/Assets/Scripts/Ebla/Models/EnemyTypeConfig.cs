using System;
using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    [Serializable]
    public class EnemyTypeConfig : BaseConfig
    {
        public EnemyTypeConfig()
        {
            Enemy = new EnemyConfig();
        }
            
        [JsonProperty(ConfigKeys.ENEMY_KEY)]
        public EnemyConfig Enemy { get; private set; }
            
        [JsonProperty(ConfigKeys.QUANTITY_KEY)]
        public int Quantity { get; private set; }
        
        [JsonProperty(ConfigKeys.ENEMY_INSTANCES_KEY)]
        public List<EnemyInstanceConfig> EnemyInstances { get; private set; }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
            InvokeConfigModified();
        }

        public override string BaseName => "Untitled Enemy Type";

        public override string GetDeletionText()
        {
            return $"Delete all enemy instances of type \"{Enemy.Name}\"?";
        }

        protected override void RemoveConfigFromLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
