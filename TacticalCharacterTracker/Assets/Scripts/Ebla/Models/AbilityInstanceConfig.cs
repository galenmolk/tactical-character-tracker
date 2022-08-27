using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class AbilityInstanceConfig : BaseConfig
    {
        [JsonIgnore] public AbilityConfig Ability { get; set; }

        [JsonProperty(ConfigKeys.CURRENT_COOLDOWN_TURNS_KEY)]
        public int CurrentCooldownTurns { get; private set; }

        [JsonProperty(ConfigKeys.IS_ACTIVATED_KEY)]
        public bool IsActivated { get; private set; }

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
