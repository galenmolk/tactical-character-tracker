using Ebla.Libraries;
using Ebla.Models;

namespace Ebla.Utils
{
    public static class ConfigFactory
    {
        public static void Dungeon()
        {
            DungeonConfig dungeonConfig = new DungeonConfig();
            DungeonLibrarian.Instance.Add(dungeonConfig);
        }
        
        public static void Encounter()
        {
            EncounterConfig encounterConfig = new EncounterConfig();
            EncounterLibrarian.Instance.Add(encounterConfig);
        }
        
        public static void Enemy()
        {
            EnemyConfig enemyConfig = new EnemyConfig();
            EnemyLibrarian.Instance.Add(enemyConfig);
        }

        public static void Hero()
        {
            HeroConfig heroConfig = new HeroConfig();
            HeroLibrarian.Instance.Add(heroConfig);
        }
        
        public static void Ability()
        {
            AbilityConfig abilityConfig = new AbilityConfig();
            AbilityLibrarian.Instance.Add(abilityConfig);
        }
    }
}
