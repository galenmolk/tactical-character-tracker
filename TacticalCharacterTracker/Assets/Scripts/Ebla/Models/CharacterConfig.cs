using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public abstract class CharacterConfig : BaseConfig
    {
        [JsonProperty(ConfigKeys.HEALTH_KEY)]
        public int Health { get; private set; }
        
        [JsonProperty(ConfigKeys.DEFENSE_KEY)]
        public int Defense { get; private set; }
        
        [JsonProperty(ConfigKeys.SPEED_KEY)]
        public int Speed { get; private set; }
        
        [JsonProperty(ConfigKeys.ABILITIES_KEY)]
        public List<AbilityConfig> Abilities { get; private set; }
                
        [JsonProperty(ConfigKeys.CHARACTER_INSTANCES_KEY)]
        public List<CharacterInstanceConfig> CharacterInstances { get; private set; }

        protected CharacterConfig()
        {
            Abilities = new List<AbilityConfig>();
            CharacterInstances = new List<CharacterInstanceConfig>();
        }

        public void AddAbility(AbilityConfig abilityConfig)
        {
            Abilities.Add(abilityConfig);
            
            foreach (CharacterInstanceConfig characterInstanceConfig in CharacterInstances)
            {
                characterInstanceConfig.AddAbility(abilityConfig);
            }
            
            InvokeConfigModified();
        }

        public void RemoveAbility(AbilityConfig abilityConfig)
        {
            Abilities.Remove(abilityConfig);
            
            foreach (CharacterInstanceConfig characterInstanceConfig in CharacterInstances)
            {
                characterInstanceConfig.RemoveAbility(abilityConfig);
            }
            
            InvokeConfigModified();
        }
        
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

        
    }
}
