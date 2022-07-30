using System;
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
    where TSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event Action<TConfig> OnEditConfigSlot;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text pathText;
        [SerializeField] private Image borderImage;
        [SerializeField] private ConfigParams configParams;

        private ConfigDragIcon dragInstance;
        
        public TConfig Config { get; private set; }

        public void Configure(TConfig myConfig)
        {
            Config = myConfig;
            ScopeController.OnScopeChanged += ReleaseObject;
            Config.OnConfigModified += ApplyConfigToSlot;
            Config.OnConfigRemoved += HandleConfigRemoved;
            ApplyConfigToSlot();
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
            Debug.Log($"Delete Config {Config.Name}");
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragInstance = Instantiate(PrefabLibrary.Instance.ConfigDragIcon, Transform.parent);
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
            
            Destroy(dragInstance.gameObject);
        }

        protected virtual void HandleConfigRemoved(BaseConfig baseConfig)
        {
            ReleaseObject();
        }

        private void ApplyConfigToSlot()
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
