using Ebla.Models;

namespace Ebla
{
    public static class ConfigFactory
    {
        public static AbilityConfig NewAbility()
        {
            AbilityConfig abilityConfig = new AbilityConfig();
            string uniqueName = Librarian.Instance.Abilities.GetUniqueName(abilityConfig.BaseName);
            abilityConfig.UpdateName(uniqueName);
            return abilityConfig;
        }
    }
}
