using System;
using System.Collections.Generic;
using Ebla.Libraries;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig
    {
        public static event Action<EnemyConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Enemy";

        public void AddInstance(CharacterInstanceConfig instanceConfig)
        {
            CharacterInstances.Add(instanceConfig);
            InvokeConfigModified();
        }

        public void RemoveInstance(CharacterInstanceConfig instanceConfig)
        {
            CharacterInstances.Remove(instanceConfig);
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
