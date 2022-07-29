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

        private void Awake()
        {
            FolderLibrarian.Instance.OnConfigAdded += CreateFolderSlot;
            DungeonLibrarian.Instance.OnConfigAdded += CreateDungeonSlot;
            EncounterLibrarian.Instance.OnConfigAdded += CreateEncounterSlot;
            HeroLibrarian.Instance.OnConfigAdded += CreateHeroSlot;
            EnemyLibrarian.Instance.OnConfigAdded += CreateEnemySlot;
            AbilityLibrarian.Instance.OnConfigAdded += CreateAbilitySlot;

            FolderConfig.OnLoadIntoFolder += CreateFolderSlot;
            AbilityConfig.OnLoadIntoFolder += CreateAbilitySlot;
        }
        
        private void CreateFolderSlot(FolderConfig folderConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.FolderSlot, folderConfig);
        }
        
        private void CreateDungeonSlot(DungeonConfig dungeonConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.DungeonSlot, dungeonConfig);
        }

        private void CreateEncounterSlot(EncounterConfig encounterConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.EncounterSlot, encounterConfig);
        }

        private void CreateHeroSlot(HeroConfig heroConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.HeroSlot, heroConfig);
        }
        
        private void CreateEnemySlot(EnemyConfig enemyConfig)
        {
            InitializeSlot(PrefabLibrary.Instance.EnemySlot, enemyConfig);
        }
        
        private void CreateAbilitySlot(AbilityConfig abilityConfig)
        {
            Debug.Log($"HandleAbilityAdded {abilityConfig.Name}");
            InitializeSlot(PrefabLibrary.Instance.AbilitySlot, abilityConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(Func<TSlot> getInstance, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>
            where TConfig : BaseConfig
        {
            Debug.Log($"InitializeSlot {config.Name}");
            Debug.Log($"InitializeSlot parent {config.Parent}");
            Debug.Log($"InitializeSlot current folder {ScopeController.Instance.CurrentFolder.Name}");
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
