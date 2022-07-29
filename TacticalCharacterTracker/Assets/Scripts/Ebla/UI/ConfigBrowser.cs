using System;
using Ebla.Libraries;
using Ebla.Models;
using Ebla.UI.Slots;
using UnityEngine;

namespace Ebla.UI
{
    public class ConfigBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        [UnityEngine.ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("Load");
            FolderLibrarian.Instance.LoadIntoController();
            AbilityLibrarian.Instance.LoadIntoController();
        }
        
        private void Awake()
        {
            FolderLibrarian.Instance.OnConfigAdded += HandleFolderAdded;
            DungeonLibrarian.Instance.OnConfigAdded += HandleDungeonAdded;
            EncounterLibrarian.Instance.OnConfigAdded += HandleEncounterAdded;
            HeroLibrarian.Instance.OnConfigAdded += HandleHeroAdded;
            EnemyLibrarian.Instance.OnConfigAdded += HandleEnemyAdded;
            AbilityLibrarian.Instance.OnConfigAdded += HandleAbilityAdded;
        }
        
        private void HandleFolderAdded(FolderConfig folderConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.FolderSlot, folderConfig);
        }
        
        private void HandleDungeonAdded(DungeonConfig dungeonConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.DungeonSlot, dungeonConfig);
        }

        private void HandleEncounterAdded(EncounterConfig encounterConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.EncounterSlot, encounterConfig);
        }

        private void HandleHeroAdded(HeroConfig heroConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.HeroSlot, heroConfig);
        }
        
        private void HandleEnemyAdded(EnemyConfig enemyConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.EnemySlot, enemyConfig);
        }
        
        private void HandleAbilityAdded(AbilityConfig abilityConfig)
        {
            Debug.Log($"HandleAbilityAdded {abilityConfig.Name}");
            InitializeSlot(PrefabLibrary.Instance.AbilitySlot, abilityConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(Func<TSlot> getInstance, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>
            where TConfig : BaseConfig
        {
            Debug.Log($"InitializeSlot name {config.Name}");
            Debug.Log($"InitializeSlot path {config.Path}");
            Debug.Log($"InitializeSlot parent {config.Parent}");
            Debug.Log($"InitializeSlot parent name {config.Parent.Name}");
            if (config.Parent != ScopeController.Instance.CurrentFolder)
            {
                return;
            }
            
            TSlot slot = getInstance();
            slot.Configure(config);

            slot.Transform.SetParent(fileArea);
            
            // TODO: UNCOVER WHY THIS HACK IS NECESSARY.
            slot.Transform.localScale = Vector3.one;
        }
    }
}
