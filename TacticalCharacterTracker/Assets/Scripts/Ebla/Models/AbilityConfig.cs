using System;
using Ebla.Libraries;
using Ebla.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    public class AbilityConfig : BaseConfig
    {
        public static event Action<AbilityConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Ability";
        
        [JsonProperty(ConfigKeys.COOLDOWN_KEY)]
        public int CooldownTurns { get; private set; }
        
        [JsonProperty(ConfigKeys.PASSIVE_KEY)]
        public bool IsPassive { get; private set; }
        
        [JsonProperty(ConfigKeys.INTERRUPT_KEY)]
        public bool IsInterrupt { get; private set; }

        public override void InvokeLoadIntoFolder()
        {
            OnLoadIntoFolder?.Invoke(this);
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

        protected override void RemoveConfigFromLibrary()
        {
            AbilityLibrarian.Instance.Remove(this);
        }
    }
}
