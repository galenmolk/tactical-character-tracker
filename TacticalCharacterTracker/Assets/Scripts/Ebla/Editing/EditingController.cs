using DG.Tweening;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingController<TControls, TConfig> : MonoBehaviour
        where TControls : EditingControls<TConfig>
        where TConfig : BaseConfig
    {
        [SerializeField] private Transform controlsParent;

        [SerializeField] private TControls controlsPrefab;
        
        [SerializeField] private float openXPos;
        [SerializeField] private float closeXPos;
        [SerializeField] private float toggleDuration;

        private TConfig activeConfig;
        
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = transform as RectTransform;
                }

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
        private TControls controlsInstance;
        
        public void Close()
        {
            if (activeConfig == null)
            {
                return;
            }
            
            activeConfig = null;
            RectTransform.DOAnchorPosX(closeXPos, toggleDuration, true);
            ClearActiveControls();
        }

        public void DeleteConfig()
        {
            activeConfig?.TryDeleteConfig();
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
            RectTransform.DOAnchorPosX(openXPos, toggleDuration, true);
        }

        private TControls GetNewControlsInstance()
        {
            ClearActiveControls();
            TControls instance = Instantiate(controlsPrefab, controlsParent);
            instance.OnConfigRemoved += Close;
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

        protected abstract void SubscribeEditSlot();
        protected abstract void UnsubscribeEditSlot();

        protected void HandleEditSlot(TConfig config)
        {
            OpenControls(config);
        }
    }
}
