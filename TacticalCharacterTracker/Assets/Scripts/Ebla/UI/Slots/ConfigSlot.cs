using System;
using DG.Tweening;
using Ebla.Models;
using Ebla.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ebla.UI.Slots
{
    public abstract class ConfigSlot<TSlot, TConfig> : BaseBehaviour<TSlot>
    where TConfig : BaseConfig
    where TSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public static event Action<TConfig> OnEditConfigSlot;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text pathText;
        [SerializeField] private Image borderImage;
        [SerializeField] private ConfigParams configParams;
        [SerializeField] private Image icon;

        [SerializeField] private SlotSettings settings;
        private ConfigDragIcon dragInstance;
        private Vector2 dragOrigin;
        
        public TConfig Config { get; private set; }

        public void Configure(TConfig myConfig)
        {
            Config = myConfig;
            icon.sprite = configParams.Icon;
            ScopeController.OnScopeChanged += ReleaseObject;
            Config.OnConfigModified += ApplyConfigToSlot;
            Config.OnConfigRemoved += HandleConfigRemoved;
            ApplyConfigToSlot(Config);
        }

        public override void ResetObject()
        {
            ScopeController.OnScopeChanged -= ReleaseObject;
            Config.OnConfigModified -= ApplyConfigToSlot;
            Config.OnConfigRemoved -= HandleConfigRemoved;
            Config = null;
        }

        public void DeleteConfig()
        {
            if (Input.GetKey(HotKeys.ForceExecute))
            {
                Config.DeleteConfig();
            }
            else
            {
                Config.TryDeleteConfig();
            }
        }

        public void EditConfig()
        {
            OnEditConfigSlot?.Invoke(Config);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            dragOrigin = Input.mousePosition;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            dragInstance = Instantiate(PrefabLibrary.Instance.ConfigDragIcon, Transform.parent);
            dragInstance.OnDroppedOnFolder += HandleDroppedOnFolder;
            dragInstance.Configure(Config, configParams);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragInstance == null)
            {
                return;
            }
            
            dragInstance.Move();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragInstance == null)
            {
                return;
            }

            dragInstance.Transform.DOMove(dragOrigin, settings.DragIconReturnDuration).OnComplete(() =>
            {
                dragInstance.OnDroppedOnFolder -= HandleDroppedOnFolder;
                Destroy(dragInstance.gameObject);
                dragInstance = null;
            });
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"Config Slot On Drop {eventData.pointerDrag.name}");
        }

        protected virtual void HandleConfigRemoved(BaseConfig baseConfig)
        {
            ReleaseObject();
        }

        private void HandleDroppedOnFolder(FolderSlot folderSlot)
        {
            Debug.Log($"Config Slot dropped on {folderSlot.Config.Name}");
        }
        
        private void ApplyConfigToSlot(BaseConfig baseConfig)
        {
            nameText.text = Config != null ? Config.Name : string.Empty;
            pathText.text = Config != null ? Config.Path : string.Empty;
        }

        private void Awake()
        {
            borderImage.color = configParams.Color;
        }
    }
}
