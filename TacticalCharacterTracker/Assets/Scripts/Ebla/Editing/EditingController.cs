using DG.Tweening;
using Ebla.Models;
using Ebla.UI;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingController<TControls, TConfig> : Window
        where TControls : EditingControls<TConfig>
        where TConfig : BaseConfig
    {
        [SerializeField] private TControls controlsPrefab;
        [SerializeField] private EditingControllerSettings settings;

        private TConfig activeConfig;

        private TControls controlsInstance;
        
        public override void Close()
        {
            if (activeConfig == null)
            {
                return;
            }

            BackController.Instance.UnregisterWindow(this);
            ClearWindow();
        }

        public override void ClearWindow()
        {
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

            BackController.Instance.RegisterWindow(this);
            controlsInstance.OnClose += Close;
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
