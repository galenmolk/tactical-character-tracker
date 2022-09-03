using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EncounterConfig : BaseConfig
    {
        public static event Action<EncounterConfig> OnEncounterDeleted; 
        [UsedImplicitly]
        public static event Action<EncounterConfig> OnLoadIntoFolder;
        
        public override string BaseName => "Untitled Encounter";

        [JsonIgnore]
        public string FilePath { get; set; }

        public EncounterConfig()
        {
            Identify();
        }

        [JsonProperty(ConfigKeys.ENEMIES)] private List<EnemyConfig> Enemies { get; set; } = new();

        public void AddEnemy(EnemyConfig enemyConfig)
        {
            Enemies.Add(enemyConfig);
            InvokeConfigModified();
        }

        public void RemoveEnemy(EnemyConfig enemyConfig)
        {
            bool wasRemoved = Enemies.Remove(enemyConfig);

            if (wasRemoved)
            {
                InvokeConfigModified();
            }
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
