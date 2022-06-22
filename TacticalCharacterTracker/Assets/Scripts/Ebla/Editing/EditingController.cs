using System.Runtime.CompilerServices;
using DG.Tweening;
using Ebla.Models;
using Ebla.Utils;
using UnityEngine;

namespace Ebla.Editing
{
    public class EditingController : MonoBehaviour
    {
        [SerializeField] private Transform controlsParent;
        [SerializeField] private ControlsPrefabLibrary controlsPrefabLibrary;
        [SerializeField] private float openXPos;
        [SerializeField] private float closeXPos;
        [SerializeField] private float toggleDuration;

        private ConfigType activeType;
        

        private bool IsOpen => Mathf.Approximately(RectTransform.anchoredPosition.x, openXPos);
        
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
            RectTransform.DOAnchorPosX(closeXPos, toggleDuration, true);
            ClearActiveControls();
        }

        private void BeginEditingConfig<TControls, TConfig>(TConfig config)
            where TControls : EditingControls<TConfig>
            where TConfig : BaseConfig
        {
            
        }

        private TControls GetControls<TControls, TConfig>(TConfig config)
            where TControls : EditingControls<TConfig>
            where TConfig : BaseConfig
        {
            switch (config)
            {
                case AbilityConfig:
                    return abilityControls as TControls;
                
            }
        }

        private void BeginAbilityEditing(AbilityConfig abilityConfig)
        {
            if (activeType != ConfigType.Ability)
            {
                ClearActiveControls();
                abilityControls = Instantiate(controlsPrefabLibrary.AbilityControls, controlsParent);
            }
            
            if (!IsOpen)
                RectTransform.DOAnchorPosX(openXPos, toggleDuration, true);
            
            activeControls = abilityControls.gameObject;
            abilityControls.Initialize(abilityConfig);
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
            AddAbilityButton.OnAddConfigButtonClicked += BeginAbilityEditing;
        }

        private void OnDisable()
        {
            AddAbilityButton.OnAddConfigButtonClicked -= BeginAbilityEditing;
        }
    }
}
