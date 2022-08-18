using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class CharacterInstanceConfig : BaseConfig
    {
        [JsonIgnore] public CharacterConfig Character { get; }

        [JsonProperty(ConfigKeys.CURRENT_HEALTH_KEY)]
        public int CurrentHealth { get; private set; }
        
        [JsonProperty(ConfigKeys.CURRENT_DEFENSE_KEY)]
        public int CurrentDefense { get; private set; }
                
        [JsonProperty(ConfigKeys.ABILITY_INSTANCES_KEY)]
        public List<AbilityInstanceConfig> AbilityInstances { get; private set; }

        private readonly Dictionary<AbilityConfig, AbilityInstanceConfig> abilityInstanceRegistry = new();

        public CharacterInstanceConfig(CharacterConfig character)
        {
            Character = character;
            
            AbilityInstances = new List<AbilityInstanceConfig>();

            foreach (AbilityConfig characterAbility in character.Abilities)
            {
                AbilityInstances.Add(new AbilityInstanceConfig(characterAbility));
            }
            
            CurrentHealth = character.Health;
            CurrentDefense = character.Defense;
        }

        public CharacterInstanceConfig()
        {
            
        }

        public AbilityInstanceConfig AddAbility(AbilityConfig abilityConfig)
        {
            AbilityInstanceConfig abilityInstanceConfig = new(abilityConfig);
            abilityInstanceRegistry.Add(abilityConfig, abilityInstanceConfig);
            AbilityInstances.Add(abilityInstanceConfig);
            InvokeConfigModified();
            return abilityInstanceConfig;
        }
        
        public void RemoveAbility(AbilityConfig abilityConfig)
        {
            if (!abilityInstanceRegistry.TryGetValue(abilityConfig, out AbilityInstanceConfig config))
            {
                return;
            }

            abilityInstanceRegistry.Remove(abilityConfig);
            AbilityInstances.Remove(config);
            InvokeConfigModified();
        }
    }
}
