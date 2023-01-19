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

        [JsonProperty(ConfigKeys.ENEMIES)]
        private List<EnemyConfig> Enemies { get; set; } = new();

        [JsonProperty(ConfigKeys.DEFAULT_ENEMIES)]
        private List<EnemyConfig> DefaultEnemies { get; set; }

        [JsonProperty(ConfigKeys.IS_DEFAULT_LOADED)]
        public bool IsDefaultLoaded { get; private set; }

        public bool HasDefault => DefaultEnemies != null;

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

        public void LoadDefault()
        {
            Enemies.Clear();

            foreach (EnemyConfig enemy in DefaultEnemies)
            {
                EnemyConfig config = (EnemyConfig)enemy.Clone();
                config.AssociateAbilityInstances();
                Enemies.Add(config);
            }

            InvokeConfigModified();
        }

        public void SaveDefault()
        {
            DefaultEnemies = new List<EnemyConfig>();

            foreach (EnemyConfig enemyConfig in Enemies)
            {
                DefaultEnemies.Add((EnemyConfig)enemyConfig.Clone());
            }

            InvokeConfigModified();
        }

        public void SetIsDefaultLoaded(bool isLoaded)
        {
            if (isLoaded == IsDefaultLoaded)
            {
                return;
            }

            IsDefaultLoaded = isLoaded;
            InvokeConfigModified();
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
