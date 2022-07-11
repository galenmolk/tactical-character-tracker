using System;
using DG.Tweening;
using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.Selection
{
    public class ConfigSelectorController : Singleton<ConfigSelectorController>
    {
        [SerializeField] private SelectorPrefabLibrary selectorPrefabLibrary;
        [SerializeField] private Transform selectorParent;
        [SerializeField] private float openPosX;
        [SerializeField] private float closedPosX;
        [SerializeField] private float toggleDuration;
        
        [SerializeField] private EnemyConfig enemyConfig = new();

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
        private GameObject selectorObject;
        
        public void OpenSelectorForConfig<TParentConfig>(TParentConfig config) where TParentConfig : BaseConfig
        {
            switch (config)
            {
                case CharacterConfig characterConfig:
                    OpenSelector<AbilitySelector, AbilityConfig, CharacterConfig, AbilityOption>(selectorPrefabLibrary.AbilitySelector, characterConfig);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OpenSelector<TSelector, TConfig, TParentConfig, TOption>(TSelector prefab, TParentConfig parentConfig)
            where TSelector : ConfigSelector<TConfig, TParentConfig, TOption>
            where TConfig : BaseConfig
            where TParentConfig : BaseConfig
            where TOption : BaseOption<TConfig, TOption>
        {
            TSelector selector = Instantiate(prefab, selectorParent);
            selector.Initialize(parentConfig);
            selectorObject = selector.gameObject;
            RectTransform.DOAnchorPosX(openPosX, toggleDuration, true);
            selector.OnClose += HandleSelectorClose;
        }

        private void HandleSelectorClose()
        {
            RectTransform.DOAnchorPosX(closedPosX, toggleDuration, true);
            Destroy(selectorObject);
        }

        [ContextMenu("Test")]
        public void Test()
        {
            OpenSelectorForConfig(enemyConfig);

        }
    }
}
