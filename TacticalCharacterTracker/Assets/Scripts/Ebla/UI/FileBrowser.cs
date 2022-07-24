using Ebla.Libraries;
using Ebla.Models;
using Ebla.UI.Slots;
using UnityEngine;

namespace Ebla.UI
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        private void OnEnable()
        {
            DungeonLibrarian.Instance.OnConfigAdded += HandleDungeonAdded;
            EncounterLibrarian.Instance.OnConfigAdded += HandleEncounterAdded;
            HeroLibrarian.Instance.OnConfigAdded += HandleHeroAdded;
            EnemyLibrarian.Instance.OnConfigAdded += HandleEnemyAdded;
            AbilityLibrarian.Instance.OnConfigAdded += HandleAbilityAdded;
        }

        private void HandleDungeonAdded(DungeonConfig dungeonConfig)
        {
            DungeonSlot slot = PrefabLibrary.Instance.GetDungeonSlot();
            InitializeSlot(slot, dungeonConfig);
        }

        private void HandleEncounterAdded(EncounterConfig encounterConfig)
        {
            EncounterSlot encounterSlot = PrefabLibrary.Instance.GetEncounterSlot();
            InitializeSlot(encounterSlot, encounterConfig);
        }

        private void HandleHeroAdded(HeroConfig heroConfig)
        {
            HeroSlot heroSlot = PrefabLibrary.Instance.GetHeroSlot();
            InitializeSlot(heroSlot, heroConfig);
        }
        
        private void HandleEnemyAdded(EnemyConfig enemyConfig)
        {
            EnemySlot slot = PrefabLibrary.Instance.GetEnemySlot();
            InitializeSlot(slot, enemyConfig);
        }
        
        private void HandleAbilityAdded(AbilityConfig abilityConfig)
        {
            AbilitySlot slot = PrefabLibrary.Instance.GetAbilitySlot();
            InitializeSlot(slot, abilityConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(TSlot slot, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>
            where TConfig : BaseConfig
        {
            slot.Configure(config);
            slot.transform.SetParent(fileArea);
        }
    }
}
