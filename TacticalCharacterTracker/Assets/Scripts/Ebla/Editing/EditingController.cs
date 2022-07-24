using DG.Tweening;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingController<TControls, TConfig> : MonoBehaviour
        where TControls : EditingControls<TConfig>
        where TConfig : BaseConfig
    {
        [SerializeField] private TControls controlsPrefab;
        [SerializeField] private EditingControllerSettings settings;

        private TConfig activeConfig;

        private TControls controlsInstance;
        
        public void Close()
        {
            if (activeConfig == null)
            {
                return;
            }
            
            activeConfig = null;
            settings.ControlsArea.DOAnchorPosX(settings.CloseXPos, settings.ToggleDuration, true);
            ClearActiveControls();
            settings.Overlay.SetActive(false);
        }

        public void DeleteConfig()
        {
            activeConfig?.TryDeleteConfig();
        }

        protected abstract void SubscribeEditSlot();
        protected abstract void UnsubscribeEditSlot();

        protected void HandleEditSlot(TConfig config)
        {
            OpenControls(config);
        }
        
        private void OpenControls(TConfig config)
        {
            if (activeConfig == config)
            {
                return;
            }
            
            if (controlsInstance == null)
            {
                controlsInstance = GetNewControlsInstance();
            }

            activeConfig = config;
            controlsInstance.Initialize(config);
            settings.ControlsArea.DOAnchorPosX(settings.OpenXPos, settings.ToggleDuration, true);
            settings.Overlay.SetActive(true);
        }

        private TControls GetNewControlsInstance()
        {
            ClearActiveControls();
            TControls instance = Instantiate(controlsPrefab, settings.ControlsParent);
            instance.OnClose += Close;
            controlsInstance = instance;
            return instance;
        }
        
        private void ClearActiveControls()
        {
            if (controlsInstance == null)
            {
                return;
            }

            Destroy(controlsInstance.gameObject);
            controlsInstance = null;
        }
        
        private void OnEnable()
        {
            SubscribeEditSlot();
        }

        private void OnDisable()
        {
            UnsubscribeEditSlot();
        }
    }
}
