using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla
{
    public static class ConfigFactory
    {
        public static AbilityConfig NewAbility()
        {
            AbilityConfig abilityConfig = new AbilityConfig();
            Librarian.Instance.Add(abilityConfig);
            return abilityConfig;
        }
    }
}
