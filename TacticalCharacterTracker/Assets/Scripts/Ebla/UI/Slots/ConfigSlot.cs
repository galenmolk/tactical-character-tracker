using System;
using Ebla.Models;
using Ebla.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI.Slots
{
    public abstract class ConfigSlot<TSlot, TConfig> : BaseBehaviour<TSlot>
    where TConfig : BaseConfig
    where TSlot : MonoBehaviour
    {
        public static event Action<TConfig> OnEditConfigSlot;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text pathText;
        [SerializeField] private Image borderImage;
        [SerializeField] private ConfigParams configParams;
        
        public TConfig Config { get; private set; }

        public void Configure(TConfig myConfig)
        {
            ScopeController.OnScopeChanged += HandleScopeChanged;
            Config = myConfig;
            Config.OnConfigModified += ApplyConfigToSlot;
            Config.OnConfigRemoved += HandleConfigRemoved;
            ApplyConfigToSlot();
        }

        public override void ResetObject()
        {
            ApplyConfigToSlot();
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

        private void HandleScopeChanged()
        {
            Debug.Log($"{Config.Name} handle scope changed");
            ScopeController.OnScopeChanged -= HandleScopeChanged;
            ReleaseObject();
        }
        
        private void ApplyConfigToSlot()
        {
            nameText.text = Config != null ? Config.Name : string.Empty;
            pathText.text = Config != null ? Config.Path : string.Empty;
        }
        
        private void HandleConfigRemoved()
        {
            Config.OnConfigModified -= ApplyConfigToSlot;
            Config.OnConfigRemoved -= HandleConfigRemoved;
            Config = null;
            ReleaseObject();
        }

        private void Awake()
        {
            borderImage.color = configParams.Color;
        }
    }
}
