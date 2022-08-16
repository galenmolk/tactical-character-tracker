using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig
    {
        public static event Action<EnemyConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Enemy";
        
                
        [JsonProperty(ConfigKeys.ENEMY_INSTANCES_KEY)]
        public List<EnemyInstanceConfig> EnemyInstances { get; private set; }

        public void AddInstance(EnemyInstanceConfig instanceConfig)
        {
            EnemyInstances.Add(instanceConfig);
            InvokeConfigModified();
        }

        public void RemoveInstance(EnemyInstanceConfig instanceConfig)
        {
            EnemyInstances.Remove(instanceConfig);
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
