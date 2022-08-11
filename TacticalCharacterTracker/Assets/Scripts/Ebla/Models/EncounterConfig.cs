using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EncounterConfig : BaseConfig
    {
        public static event Action<EncounterConfig> OnLoadIntoFolder;
        
        
        public override string BaseName => "Untitled Encounter";

        public EncounterConfig()
        {
            EnemyTypes = new List<EnemyTypeConfig>();
        }
        
        [JsonProperty(ConfigKeys.ENEMY_TYPES_KEY)]
        private List<EnemyTypeConfig> EnemyTypes { get; set; }

        public void AddEnemyType(EnemyTypeConfig enemyTypeConfig)
        {
            EnemyTypes.Add(enemyTypeConfig);
            InvokeConfigModified();
        }

        public IReadOnlyList<EnemyTypeConfig> GetEnemyTypes()
        {
            return EnemyTypes;
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            EncounterLibrarian.Instance.Remove(this);
        }
    }
}
