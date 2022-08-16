using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EnemyInstanceConfig : BaseConfig
    {
        [JsonIgnore] public EnemyConfig Enemy { get; }

        [JsonProperty(ConfigKeys.CURRENT_HEALTH_KEY)]
        public int CurrentHealth { get; private set; }
        
        [JsonProperty(ConfigKeys.CURRENT_DEFENSE_KEY)]
        public int CurrentDefense { get; private set; }

        public EnemyInstanceConfig(EnemyConfig enemy)
        {
            Enemy = enemy;
            CurrentHealth = enemy.Health;
            CurrentDefense = enemy.Defense;
        }
    }
}
