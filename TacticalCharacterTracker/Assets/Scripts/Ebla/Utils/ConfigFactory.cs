using Ebla.Libraries;
using Ebla.Models;

namespace Ebla.Utils
{
    public static class ConfigFactory
    {
        public static void Folder()
        {
            FolderConfig folderConfig = new();
            Initialize(folderConfig);
            FolderLibrarian.Instance.Add(folderConfig);
        }
        
        public static void Dungeon()
        {
            DungeonConfig dungeonConfig = new();
            Initialize(dungeonConfig);
            DungeonLibrarian.Instance.Add(dungeonConfig);
        }
        
        public static void Encounter()
        {
            EncounterConfig encounterConfig = new();
            Initialize(encounterConfig);
            EncounterLibrarian.Instance.Add(encounterConfig);
        }
        
        public static void Enemy()
        {
            EnemyConfig enemyConfig = new();
            Initialize(enemyConfig);
            EnemyLibrarian.Instance.Add(enemyConfig);
        }

        public static void Hero()
        {
            HeroConfig heroConfig = new();
            Initialize(heroConfig);
            HeroLibrarian.Instance.Add(heroConfig);
        }
        
        public static void Ability()
        {
            AbilityConfig abilityConfig = new();
            Initialize(abilityConfig);
            AbilityLibrarian.Instance.Add(abilityConfig);
        }

        private static void Initialize(BaseConfig config)
        {
            config.Initialize();
        }
    }
}
