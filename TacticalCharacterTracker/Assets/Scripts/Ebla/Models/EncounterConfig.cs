using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EncounterConfig : BaseConfig
    {
        public static event Action<EncounterConfig> OnEncounterDeleted; 
        public static event Action<EncounterConfig> OnLoadIntoFolder;
        
        public override string BaseName => "Untitled Encounter";

        public EncounterConfig()
        {
            Identify();
            Enemies = new List<EnemyConfig>();
        }
        
        [JsonProperty(ConfigKeys.ENEMIES)]
        private List<EnemyConfig> Enemies { get; set; }

        public void AddEnemy(EnemyConfig enemyConfig)
        {
            Enemies.Add(enemyConfig);
            InvokeConfigModified();
        }

        public bool RemoveEnemy(EnemyConfig enemyConfig)
        {
            bool wasRemoved = Enemies.Remove(enemyConfig);

            if (wasRemoved)
            {
                InvokeConfigModified();
            }

            return wasRemoved;
        }

        public IEnumerable<EnemyConfig> GetEnemies()
        {
            return Enemies;
        }

        protected override void RemoveConfigFromLibrary()
        {
            EncounterLibrarian.Instance.Remove(this);
        }

        protected override void InvokeTypedOnConfigRemoved()
        {
            OnEncounterDeleted?.Invoke(this);
        }
    }
}
