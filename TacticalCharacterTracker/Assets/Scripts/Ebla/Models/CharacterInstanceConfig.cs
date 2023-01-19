using System;
using System.Collections.Generic;
using Ebla.Utils;
using HexedHeroes.EncounterRunner;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Ebla.Models
{
    public class CharacterInstanceConfig : BaseConfig, ICloneable
    {
        [JsonIgnore] private CharacterConfig Character { get; set; }

        [JsonProperty(ConfigKeys.CURRENT_HEALTH_KEY)]
        public int CurrentHealth { get; private set; }
        
        [JsonProperty(ConfigKeys.CURRENT_DEFENSE_KEY)]
        public int CurrentDefense { get; private set; }
                
        [JsonProperty(ConfigKeys.ABILITY_INSTANCES_KEY)]
        public List<AbilityInstanceConfig> AbilityInstances { get; private set; }

        public object Clone()
        {
            return new CharacterInstanceConfig(this);
        }

        public CharacterInstanceConfig(CharacterInstanceConfig instance)
        {
            Name = instance.Name;
            Character = instance.Character;
            CurrentHealth = instance.CurrentHealth;
            CurrentDefense = instance.CurrentHealth;

            AbilityInstances = new List<AbilityInstanceConfig>();

            foreach (AbilityInstanceConfig abilityInstance in instance.AbilityInstances)
            {
                AbilityInstances.Add((AbilityInstanceConfig)abilityInstance.Clone());
            }
        }

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

        public void ConfigureInstance(CharacterConfig characterConfig)
        {
            SetCharacter(characterConfig);
            
            AbilityInstances = new List<AbilityInstanceConfig>();

            foreach (AbilityConfig characterAbility in characterConfig.Abilities)
            {
                AbilityInstances.Add(new AbilityInstanceConfig(characterAbility));
            }
            
            CurrentHealth = characterConfig.Health;
            CurrentDefense = characterConfig.Defense;
        }

        public void SetCharacter(CharacterConfig characterConfig)
        {
            Character = characterConfig;
        }

        [JsonConstructor]
        public CharacterInstanceConfig() { }

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
