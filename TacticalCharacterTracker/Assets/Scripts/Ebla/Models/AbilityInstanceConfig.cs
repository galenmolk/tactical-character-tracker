using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class AbilityInstanceConfig : AbilityConfig
    {
        [JsonProperty(ConfigKeys.CURRENT_COOLDOWN_TURNS)]
        public int CurrentCooldownTurns { get; private set; }
    }
}
