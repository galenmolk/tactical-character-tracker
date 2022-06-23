using System;
using DG.Tweening;
using Ebla.AddButtons;
using Ebla.LibraryControllers;
using Ebla.Models;
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
            activeType = ConfigType.None;
            RectTransform.DOAnchorPosX(closeXPos, toggleDuration, true);
            ClearActiveControls();
        }

        private void BeginAbilityEditing(AbilityConfig abilityConfig)
        {
            Debug.Log("BeginAbilityEditing " + abilityConfig.Name );

            if (activeType != ConfigType.Ability)
            {
                Debug.Log("Resetting ");

                ClearActiveControls();
                abilityControls = Instantiate(controlsPrefabLibrary.AbilityControls, controlsParent);
                activeControls = abilityControls.gameObject;
            }
            
            if (!IsOpen)
                RectTransform.DOAnchorPosX(openXPos, toggleDuration, true);

            activeType = ConfigType.Ability;
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
            FileSlot.OnOpenFileForEditing += HandleOpenFileForEditing;
        }

        private void OnDisable()
        {
            AddAbilityButton.OnAddConfigButtonClicked -= BeginAbilityEditing;
            FileSlot.OnOpenFileForEditing -= HandleOpenFileForEditing;
        }

        private void HandleOpenFileForEditing(FileSlot fileSlot)
        {
            switch (fileSlot.File)
            {
                case AbilityConfig file:
                    BeginAbilityEditing(file);
                    break;
                default:
                    return;
            }
        }
    }
}
