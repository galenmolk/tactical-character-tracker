using System.Collections.Generic;
using Ebla.Utils;
using MolkExtras;
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

        public string GetOrUpdateName(int index)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }

            int adjustedIndex = ++index;
            string newName = string.IsNullOrWhiteSpace(Character.Name)
                ? $"{adjustedIndex}"
                : $"{Character.Name} {adjustedIndex}";
            
            UpdateName(newName);
            return newName;
        }
        
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

        public void UpdateHealth(int newHealth)
        {
            if (newHealth == CurrentHealth)
            {
                return;
            }

            CurrentHealth = newHealth;
            InvokeConfigModified();
        }

        public void UpdateDefense(int newDefense)
        {
            if (newDefense == CurrentDefense)
            {
                return;
            }

            CurrentDefense = newDefense;
            InvokeConfigModified();
        }
        
        public AbilityInstanceConfig AddAbility(AbilityConfig abilityConfig)
        {
            AbilityInstanceConfig abilityInstanceConfig = new(abilityConfig);
            AbilityInstances.Add(abilityInstanceConfig);
            InvokeConfigModified();
            return abilityInstanceConfig;
        }
        
        public void RemoveAbility(AbilityConfig abilityConfig)
        {
            AbilityInstances.RemoveFirstMatch(instance => instance.Ability == abilityConfig);
            InvokeConfigModified();
        }
    }
}
