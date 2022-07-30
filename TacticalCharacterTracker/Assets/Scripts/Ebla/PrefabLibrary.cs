using Ebla.Pooling;
using Ebla.Selection;
using Ebla.UI;
using Ebla.UI.Slots;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        public ConfigDragIcon ConfigDragIcon => configDragIcon;
        
        [Header("Slot Pools")] 
        [SerializeField] private FolderSlotPool folderSlotPool;
        [SerializeField] private DungeonSlotPool dungeonSlotPool;
        [SerializeField] private EncounterSlotPool encounterSlotPool;
        [SerializeField] private HeroSlotPool heroSlotPool;
        [SerializeField] private AbilitySlotPool abilitySlotPool;
        [SerializeField] private EnemySlotPool enemySlotPool;
        
        [Header("Option Pools")]
        [SerializeField] private AbilityOptionPool abilityOptionPool;

        [SerializeField] private ConfigDragIcon configDragIcon;
        
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
            Debug.Log("Get AbilitySlot");
            return abilitySlotPool.Get();
        }

        public AbilityOption AbilityOption()
        {
            return abilityOptionPool.Get();
        }
    }
}
