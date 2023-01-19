using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ebla.Utils;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    public abstract class CharacterConfig : BaseConfig 
    {
        [JsonProperty(ConfigKeys.HEALTH_KEY)]
        public int Health { get; protected set; }
        
        [JsonProperty(ConfigKeys.DEFENSE_KEY)]
        public int Defense { get; protected set; }
        
        [JsonProperty(ConfigKeys.SPEED_KEY)]
        public int Speed { get; protected set; }
        
        [JsonProperty(ConfigKeys.ABILITIES_KEY)]
        public List<AbilityConfig> Abilities { get; protected set; }
                
        [JsonProperty(ConfigKeys.CHARACTER_INSTANCES_KEY)]
        public List<CharacterInstanceConfig> CharacterInstances { get; protected set; }

        protected CharacterConfig()
        {
            Abilities = new List<AbilityConfig>();
            CharacterInstances = new List<CharacterInstanceConfig>();
        }

        public void AddInstance(CharacterInstanceConfig instanceConfig)
        {
            CharacterInstances.Add(instanceConfig);
            InvokeConfigModified();
        }

        public void RemoveInstance(CharacterInstanceConfig instanceConfig)
        {
            CharacterInstances.Remove(instanceConfig);
            InvokeConfigModified();
        }
        
        public void AddAbility(AbilityConfig abilityConfig)
        {
            Abilities.Add(abilityConfig);
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

        public void AssociateAbilityInstances()
        {
            for (int abilityIndex = 0, abilityCount = Abilities.Count; abilityIndex < abilityCount; abilityIndex++)
            {
                foreach (CharacterInstanceConfig instance in CharacterInstances)
                {
                    List<AbilityInstanceConfig> abilityInstances = instance.AbilityInstances;

                    if (abilityInstances.Count != abilityCount)
                    {
                        Debug.LogError($"{Name}: Mismatch between Character Instance Abilities and Character Abilities.");
                        return;
                    }

                    abilityInstances[abilityIndex].Ability = Abilities[abilityIndex];
                }
            }
        }

        [OnDeserialized]
        private void OnDeserializedCallback(StreamingContext context)
        {
            if (!CharacterInstances.HasItems() || !Abilities.HasItems())
            {
                return;
            }

            AssociateAbilityInstances();
        }
    }
}
