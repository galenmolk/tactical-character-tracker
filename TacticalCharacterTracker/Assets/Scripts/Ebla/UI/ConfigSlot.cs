using System;
using Ebla.LibraryControllers;
using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public abstract class ConfigSlot<TSlot, TConfig> : BaseBehaviour<TSlot>
    where TConfig : BaseConfig
    where TSlot : MonoBehaviour
    {
        public static event Action<TConfig> OnEditConfigSlot;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image backgroundImage;
        
        public TConfig Config { get; private set; }

        public void Configure(TConfig myConfig)
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

        public void ApplyConfigToSlot()
        {
            nameText.text = Config != null ? Config.Name : string.Empty;
        }

        public void DeleteConfig()
        {
            RemoveConfig();
            Config = null;
            ReleaseObject();
        }

        public void EditConfig()
        {
            OnEditConfigSlot?.Invoke(Config);
        }

        protected abstract void RemoveConfig();
    }
}
