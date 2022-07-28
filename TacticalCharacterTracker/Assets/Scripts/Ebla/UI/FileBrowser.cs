using System;
using Ebla.Libraries;
using Ebla.Models;
using Ebla.UI.Slots;
using MolkExtras;
using UnityEngine;

namespace Ebla.UI
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        private void OnEnable()
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
            InitializeSlot(() => PrefabLibrary.Instance.FolderSlot(), folderConfig);
        }
        
        private void HandleDungeonAdded(DungeonConfig dungeonConfig)
        {
            InitializeSlot(() => PrefabLibrary.Instance.DungeonSlot(), dungeonConfig);
        }

        private void HandleEncounterAdded(EncounterConfig encounterConfig)
        {
            InitializeSlot(() => PrefabLibrary.Instance.EncounterSlot(), encounterConfig);
        }

        private void HandleHeroAdded(HeroConfig heroConfig)
        {
            InitializeSlot(() => PrefabLibrary.Instance.HeroSlot(), heroConfig);
        }
        
        private void HandleEnemyAdded(EnemyConfig enemyConfig)
        {
            InitializeSlot(() => PrefabLibrary.Instance.EnemySlot(), enemyConfig);
        }
        
        private void HandleAbilityAdded(AbilityConfig abilityConfig)
        {
            InitializeSlot(() => PrefabLibrary.Instance.AbilitySlot(), abilityConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(Func<TSlot> getInstance, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>
            where TConfig : BaseConfig
        {
            if (config.Parent != ScopeController.Instance.CurrentFolder)
            {
                return;
            }
            
            Debug.Log("init slot");
            TSlot slot = getInstance();
            slot.Configure(config);
            Debug.Log("InitializeSlot Scale: " + slot.transform.localScale);

            slot.Transform.SetParent(fileArea);
            slot.Transform.localScale = Vector3.one;
        }
    }
}
