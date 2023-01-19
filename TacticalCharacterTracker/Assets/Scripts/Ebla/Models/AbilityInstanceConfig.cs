using System;
using Ebla.Utils;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Ebla.Models
{
    public class AbilityInstanceConfig : BaseConfig, ICloneable
    {
        [JsonIgnore] public AbilityConfig Ability { get; set; }

        [JsonProperty(ConfigKeys.CURRENT_COOLDOWN_TURNS_KEY)]
        public int CurrentCooldownTurns { get; private set; }

        [JsonProperty(ConfigKeys.IS_ACTIVATED_KEY)]
        public bool IsActivated { get; private set; }

        public object Clone()
        {
            return new AbilityInstanceConfig(this);
        }

        [Preserve]
        public AbilityInstanceConfig()
        {

        }

        public AbilityInstanceConfig(AbilityInstanceConfig instance)
        {
            Ability = instance.Ability;
            CurrentCooldownTurns = instance.CurrentCooldownTurns;
            IsActivated = instance.IsActivated;
        }

        public AbilityInstanceConfig(AbilityConfig ability)
        {
            Ability = ability;
        }

        public void UpdateCurrentCooldownTurns(int currentCooldown)
        {
            if (CurrentCooldownTurns == currentCooldown)
            {
                return;
            }

            CurrentCooldownTurns = currentCooldown;
            UpdateIsActivated(CurrentCooldownTurns > 0);
            InvokeConfigModified();
        }

        public void UpdateIsActivated(bool isActivated)
        {
            if (isActivated == IsActivated)
            {
                return;
            }

            IsActivated = isActivated;
            InvokeConfigModified();
        }
    }
}
