using Ebla.Libraries;
using Ebla.Models;

namespace Ebla.Utils
{
    public static class ConfigFactory
    {
        public static void Folder()
        {
            FolderConfig folderConfig = new(ScopeController.Instance.CurrentFolder);
            FolderLibrarian.Instance.Add(folderConfig);
        }
        
        public static void Dungeon()
        {
            DungeonConfig dungeonConfig = new(ScopeController.Instance.CurrentFolder);
            DungeonLibrarian.Instance.Add(dungeonConfig);
        }
        
        public static void Encounter()
        {
            EncounterConfig encounterConfig = new(ScopeController.Instance.CurrentFolder);
            EncounterLibrarian.Instance.Add(encounterConfig);
        }
        
        public static void Enemy()
        {
            EnemyConfig enemyConfig = new(ScopeController.Instance.CurrentFolder);
            EnemyLibrarian.Instance.Add(enemyConfig);
        }

        public static void Hero()
        {
            HeroConfig heroConfig = new(ScopeController.Instance.CurrentFolder);
            HeroLibrarian.Instance.Add(heroConfig);
        }
        
        public static void Ability()
        {
            AbilityConfig abilityConfig = new(ScopeController.Instance.CurrentFolder);
            AbilityLibrarian.Instance.Add(abilityConfig);
        }
    }
}
