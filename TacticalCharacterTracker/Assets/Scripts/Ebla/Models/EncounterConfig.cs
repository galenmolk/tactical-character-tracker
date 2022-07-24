using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    [Serializable]
    public class EncounterConfig : BaseConfig
    {
        [Serializable]
        public class EnemyTypeConfig
        {
            [JsonProperty(ConfigKeys.ENEMY_KEY)]
            public EnemyConfig Enemy { get; private set; }
            
            [JsonProperty(ConfigKeys.QUANTITY_KEY)]
            public int Quantity { get; private set; }
        }
        
        public EncounterConfig()
        {
        }

        public override string BaseName => "Untitled Encounter";

        [JsonProperty(ConfigKeys.ENEMY_TYPES_KEY)]
        public List<EnemyTypeConfig> EnemyTypes { get; private set; }

        protected override void RemoveConfigFromLibrary()
        {
            EncounterLibrarian.Instance.Remove(this);
        }
    }
}
