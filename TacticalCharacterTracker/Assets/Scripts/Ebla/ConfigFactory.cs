using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla
{
    public static class ConfigFactory
    {
        public static void Ability()
        {
            Debug.Log("Ability");
            AbilityConfig abilityConfig = new AbilityConfig();
            AbilityLibrarian.Instance.Add(abilityConfig);
        }

        public static void Enemy()
        {
            Debug.Log("Enemy");

            EnemyConfig enemyConfig = new EnemyConfig();
            EnemyLibrarian.Instance.Add(enemyConfig);
        }
    }
}
