using DG.Tweening;
using Ebla.Models;
using Ebla.UI;
using Ebla.Utils;
using MolkExtras;
using UnityEngine;

namespace Ebla.Editing
{
    public class EditingController : Singleton<EditingController>
    {
        [SerializeField] private Transform controlsParent;
        [SerializeField] private ControlsPrefabLibrary controlsPrefabLibrary;
        [SerializeField] private float openXPos;
        [SerializeField] private float closeXPos;
        [SerializeField] private float toggleDuration;

        private ConfigType activeType;
        private BaseConfig activeConfig;
        
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
        private GameObject activeControls;
        private AbilityControls abilityControls;

        public void Close()
        {
            activeType = ConfigType.None;
            activeConfig = null;
            RectTransform.DOAnchorPosX(closeXPos, toggleDuration, true);
            ClearActiveControls();
        }

        private void OpenEditingControls<TConfig, TControls>(
            TConfig config, 
            ref TControls controlsInstance, 
            TControls controlsPrefab, 
            ConfigType type) 
            where TConfig : BaseConfig
            where TControls : EditingControls<TConfig>
        {
            if (activeConfig == config)
            {
                return;
            }
            
            if (activeType != type)
                controlsInstance = GetNewControlsInstance<TConfig, TControls>(controlsPrefab);
            
            activeType = type;
            activeConfig = config;
            controlsInstance.Initialize(config);
        }

        private TControls GetNewControlsInstance<TConfig, TControls>(TControls controlsPrefab)
            where TConfig : BaseConfig
            where TControls : EditingControls<TConfig>
        {
            ClearActiveControls();
            TControls instance = Instantiate(controlsPrefab, controlsParent);
            activeControls = instance.gameObject;
            RectTransform.DOAnchorPosX(openXPos, toggleDuration, true);
            return instance;
        }
        
        private void ClearActiveControls()
        {
            if (activeControls == null)
                return;
         
            Destroy(activeControls);
            activeControls = null;
        }
        
        private void OnEnable()
        {
            EnemySlot.OnEditConfigSlot += HandleEditEnemySlot;
            AbilitySlot.OnEditConfigSlot += HandleEditAbilitySlot;
        }

        private void OnDisable()
        {
            EnemySlot.OnEditConfigSlot -= HandleEditEnemySlot;
            AbilitySlot.OnEditConfigSlot -= HandleEditAbilitySlot;
        }

        private void HandleEditAbilitySlot(AbilityConfig abilityConfig)
        {
            OpenEditingControls(
                abilityConfig, 
                ref abilityControls, 
                controlsPrefabLibrary.AbilityControls, 
                ConfigType.Ability);

        }

        private void HandleEditEnemySlot(EnemyConfig enemyConfig)
        {
            
        }
    }
}
