using Ebla.Libraries;
using Ebla.Models;

namespace Ebla.Utils
{
    public static class ConfigFactory
    {
        public static void Dungeon()
        {
            DungeonLibrarian.Instance.Add(CreateNewConfig<DungeonConfig>());
        }
        
        public static void Encounter()
        {
            EncounterLibrarian.Instance.Add(CreateNewConfig<EncounterConfig>());
        }
        
        public static void Enemy()
        {
            EnemyLibrarian.Instance.Add(CreateNewConfig<EnemyConfig>());
        }

        public static void Hero()
        {
            HeroLibrarian.Instance.Add(CreateNewConfig<HeroConfig>());
        }
        
        public static void Ability()
        {
            AbilityLibrarian.Instance.Add(CreateNewConfig<AbilityConfig>());
        }

        private static TConfig CreateNewConfig<TConfig>() where TConfig : BaseConfig, new()
        {
            TConfig config = new TConfig();
            config.UpdatePath(ScopeController.Instance.GetPathForCurrentScope());
            return config;
        }
    }
}
