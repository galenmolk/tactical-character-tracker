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
        [SerializeField] private FolderSlotPool folderSlotPool;
        [SerializeField] private DungeonSlotPool dungeonSlotPool;
        [SerializeField] private EncounterSlotPool encounterSlotPool;
        [SerializeField] private HeroSlotPool heroSlotPool;
        [SerializeField] private AbilitySlotPool abilitySlotPool;
        [SerializeField] private EnemySlotPool enemySlotPool;
        
        [Header("Option Pools")]
        [SerializeField] private AbilityOptionPool abilityOptionPool;

        public FolderSlot FolderSlot()
        {
            return folderSlotPool.Get();
        }
        
        public DungeonSlot DungeonSlot()
        {
            return dungeonSlotPool.Get();
        }

        public EncounterSlot EncounterSlot()
        {
            return encounterSlotPool.Get();
        }

        public HeroSlot HeroSlot()
        {
            return heroSlotPool.Get();
        }
              
        public EnemySlot EnemySlot()
        {
            return enemySlotPool.Get();
        }
        
        public AbilitySlot AbilitySlot()
        {
            return abilitySlotPool.Get();
        }

        public AbilityOption AbilityOption()
        {
            return abilityOptionPool.Get();
        }
    }
}
