using Ebla.Pooling;
using Ebla.Selection;
using Ebla.UI;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        [SerializeField] private ConfigSlotPool configSlotPool;
        [SerializeField] private AbilityOptionPool abilityOptionPool;
        
        public ConfigSlot GetConfigSlot()
        {
            return configSlotPool.Get();
        }

        public AbilityOption GetAbilityOption()
        {
            return abilityOptionPool.Get();
        }
    }
}
