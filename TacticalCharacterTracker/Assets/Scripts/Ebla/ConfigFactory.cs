using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla
{
    public static class ConfigFactory
    {
        public static AbilityConfig Ability()
        {
            Debug.Log("NewAbility");
            AbilityConfig abilityConfig = new AbilityConfig();
            Librarian.Instance.Add(abilityConfig);
            return abilityConfig;
        }

        public static EnemyConfig Enemy()
        {
            EnemyConfig enemyConfig = new EnemyConfig();
            Librarian.Instance.Add(enemyConfig);
            return enemyConfig;
        }
    }
}
