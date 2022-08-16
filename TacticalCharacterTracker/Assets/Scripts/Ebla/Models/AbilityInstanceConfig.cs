using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class AbilityInstanceConfig : BaseConfig
    {
        [JsonIgnore] public AbilityConfig Ability { get; private set; }

        [JsonProperty(ConfigKeys.CURRENT_COOLDOWN_TURNS)]
        public int CurrentCooldownTurns { get; private set; }

        public AbilityInstanceConfig(AbilityConfig ability)
        {
            Ability = ability;
            CurrentCooldownTurns = ability.CooldownTurns;
        }
    }
}
