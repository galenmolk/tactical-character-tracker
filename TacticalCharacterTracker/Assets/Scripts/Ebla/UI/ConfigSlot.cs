using System;
using Ebla.LibraryControllers;
using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class ConfigSlot : BaseBehaviour<ConfigSlot>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event Action<BaseConfig> OnEditConfigSlot;
        public override event Action<ConfigSlot> OnReleaseObject;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image backgroundImage;
        
        public BaseConfig Config { get; private set; }

        public void Configure(BaseConfig myConfig)
        {
            Config = myConfig;
            Config.OnConfigModified += ApplyConfigToSlot;
            ApplyConfigToSlot();
            backgroundImage.color = ConfigParamsRegistry.Get(myConfig).Color;
        }
        
        public override void ResetObject()
        {
            ApplyConfigToSlot();
        }

        public override void ReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }

        public void ApplyConfigToSlot()
        {
            nameText.text = Config != null ? Config.Name : string.Empty;
        }

        public void DeleteConfig()
        {
            Librarian.Instance.Remove(Config as AbilityConfig);
            Config = null;
            OnReleaseObject?.Invoke(this);
        }

        public void EditConfig()
        {
            OnEditConfigSlot?.Invoke(Config);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}
