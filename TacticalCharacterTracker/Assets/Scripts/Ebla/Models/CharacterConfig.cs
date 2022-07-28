using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public abstract class CharacterConfig : BaseConfig
    {
        public CharacterConfig(FolderConfig parent) : base(parent)
        {
            
        }
        
        [JsonProperty(ConfigKeys.HEALTH_KEY)]
        public int Health { get; private set; }
        
        [JsonProperty(ConfigKeys.DEFENSE_KEY)]
        public int Defense { get; private set; }
        
        [JsonProperty(ConfigKeys.SPEED_KEY)]
        public int Speed { get; private set; }
        
        [JsonProperty(ConfigKeys.ABILITIES_KEY)]
        public List<AbilityConfig> Abilities { get; private set; }

        public void UpdateHealth(int health)
        {
            Health = health;
            InvokeConfigModified();
        }

        public void UpdateDefense(int defense)
        {
            Defense = defense;
            InvokeConfigModified();
        }

        public void UpdateSpeed(int speed)
        {
            Speed = speed;
            InvokeConfigModified();
        }

        public void UpdateAbilities(List<AbilityConfig> abilityConfigs)
        {
            Abilities = abilityConfigs;
            InvokeConfigModified();
        }
    }
}
