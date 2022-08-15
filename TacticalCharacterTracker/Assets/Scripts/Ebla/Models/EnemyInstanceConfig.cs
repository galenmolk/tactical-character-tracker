using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EnemyInstanceConfig : EnemyConfig
    {
        [JsonProperty(ConfigKeys.CURRENT_HEALTH_KEY)]
        public int CurrentHealth { get; private set; }
        
        [JsonProperty(ConfigKeys.CURRENT_DEFENSE_KEY)]
        public int CurrentDefense { get; private set; }

        [JsonProperty(ConfigKeys.ABILITY_INSTANCES_KEY)]
        public List<AbilityInstanceConfig> AbilityInstances { get; private set; }
    }
}
