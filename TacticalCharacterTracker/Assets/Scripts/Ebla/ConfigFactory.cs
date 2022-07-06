using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla
{
    public static class ConfigFactory
    {
        public static AbilityConfig NewAbility()
        {
            Debug.Log("NewAbility");
            AbilityConfig abilityConfig = new AbilityConfig();
            Librarian.Instance.Add(abilityConfig);
            return abilityConfig;
        }
    }
}
