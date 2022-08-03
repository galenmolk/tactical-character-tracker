using System;
using Ebla.Libraries;
using Ebla.Models;
using Ebla.UI.Slots;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ebla.UI
{
    public class ConfigBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        private void Awake()
        {
            FolderLibrarian.OnConfigAdded += CreateFolderSlot;
            DungeonLibrarian.OnConfigAdded += CreateDungeonSlot;
            EncounterLibrarian.OnConfigAdded += CreateEncounterSlot;
            HeroLibrarian.OnConfigAdded += CreateHeroSlot;
            EnemyLibrarian.OnConfigAdded += CreateEnemySlot;
            AbilityLibrarian.OnConfigAdded += CreateAbilitySlot;

            FolderConfig.OnLoadIntoFolder += CreateFolderSlot;
            DungeonConfig.OnLoadIntoFolder += CreateDungeonSlot;
            EncounterConfig.OnLoadIntoFolder += CreateEncounterSlot;
            EnemyConfig.OnLoadIntoFolder += CreateEnemySlot;
            HeroConfig.OnLoadIntoFolder += CreateHeroSlot;
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
            InitializeSlot(PrefabLibrary.Instance.AbilitySlot, abilityConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(Func<TSlot> getInstance, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
            where TConfig : BaseConfig
        {
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
