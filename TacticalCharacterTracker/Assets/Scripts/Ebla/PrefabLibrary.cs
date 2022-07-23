using Ebla.Pooling;
using Ebla.Selection;
using Ebla.UI;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        [SerializeField] private AbilitySlotPool abilitySlotPool;
        [SerializeField] private EnemySlotPool enemySlotPool;
        [SerializeField] private AbilityOptionPool abilityOptionPool;

        public AbilitySlot GetAbilitySlot()
        {
            return abilitySlotPool.Get();
        }
        
        public EnemySlot GetEnemySlot()
        {
            return enemySlotPool.Get();
        }

        public AbilityOption GetAbilityOption()
        {
            return abilityOptionPool.Get();
        }
    }
}
