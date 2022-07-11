using System;
using System.Runtime.Serialization;
using Ebla.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    [Serializable]
    public class AbilityConfig : BaseConfig
    {
        public override string BaseName => "Untitled Ability";
        
        [JsonProperty(ConfigKeys.COOLDOWN_KEY)]
        public int CooldownTurns { get; private set; }
        
        [JsonProperty(ConfigKeys.PASSIVE_KEY)]
        public bool IsPassive { get; private set; }
        
        [JsonProperty(ConfigKeys.INTERRUPT_KEY)]
        public bool IsInterrupt { get; private set; }

        public AbilityConfig()
        {

        }

        public void UpdateCooldownTurns(int cooldownTurns)
        {
            CooldownTurns = cooldownTurns;
            InvokeConfigModified();
        }
        
        public void UpdateIsPassive(bool isPassive)
        {
            IsPassive = isPassive;
            InvokeConfigModified();
        }

        public void UpdateIsInterrupt(bool isInterrupt)
        {
            IsInterrupt = isInterrupt;
            InvokeConfigModified();
        }
        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Debug.Log($"Ability Deserialized: {Name}, {CooldownTurns}, {IsPassive}, {IsInterrupt}, {Description}");
        }
    }
}
