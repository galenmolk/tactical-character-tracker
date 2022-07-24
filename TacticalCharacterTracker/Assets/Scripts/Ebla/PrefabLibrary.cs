using Ebla.Pooling;
using Ebla.Selection;
using Ebla.UI.Slots;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        [Header("Slot Pools")]
        [SerializeField] private DungeonSlotPool dungeonSlotPool;
        [SerializeField] private EncounterSlotPool encounterSlotPool;
        [SerializeField] private HeroSlotPool heroSlotPool;
        [SerializeField] private AbilitySlotPool abilitySlotPool;
        [SerializeField] private EnemySlotPool enemySlotPool;
        
        [Header("Option Pools")]
        [SerializeField] private AbilityOptionPool abilityOptionPool;

        public DungeonSlot GetDungeonSlot()
        {
            return dungeonSlotPool.Get();
        }

        public EncounterSlot GetEncounterSlot()
        {
            return encounterSlotPool.Get();
        }

        public HeroSlot GetHeroSlot()
        {
            return heroSlotPool.Get();
        }
              
        public EnemySlot GetEnemySlot()
        {
            return enemySlotPool.Get();
        }
        
        public AbilitySlot GetAbilitySlot()
        {
            return abilitySlotPool.Get();
        }

        public AbilityOption GetAbilityOption()
        {
            return abilityOptionPool.Get();
        }
    }
}
