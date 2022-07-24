using System;
using Ebla.Editing.Sections;
using Ebla.Models;
using Ebla.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.Editing
{
    public abstract class EditingControls<TConfig> : MonoBehaviour
        where TConfig : BaseConfig
    {
        public event Action OnClose;

        [SerializeField] private Image headerImage;
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private StringSection nameSection;
        [SerializeField] private StringSection descriptionSection;
        [SerializeField] private ConfigParams configParams;
        
        protected TConfig ActiveConfig { get; private set; }

        public void Initialize(TConfig config)
        {
            headerImage.color = configParams.Color;
            headerText.text = configParams.ConfigName;
            ActiveConfig = config;
            config.OnConfigRemoved += Close;
            config.OnConfigModified += Refresh;
            ApplyConfig(config);
        }

        public void TryUpdateName(string newName)
        {
            if (string.Equals(newName, ActiveConfig.Name))
            {
                return;
            }

            ActiveConfig.UpdateName(newName);
        }

        private void TryUpdateDescription(string newDescription)
        {
            if (string.Equals(newDescription, ActiveConfig.Description))
            {
                return;
            }

            ActiveConfig.UpdateDescription(newDescription);
        }

        private void Close()
        {
            OnClose?.Invoke();
        }

        protected virtual void ApplyConfig(TConfig config)
        {
            nameSection.TrySetValue(config.Name);
            descriptionSection.TrySetValue(config.Description);
        }

        protected virtual void SubscribeToSectionModifiedEvents()
        {
            nameSection.SubscribeToModifiedEvent(TryUpdateName);
            descriptionSection.SubscribeToModifiedEvent(TryUpdateDescription);
        }

        private void Awake()
        {
            SubscribeToSectionModifiedEvents();
        }

        private void Refresh()
        {
            ApplyConfig(ActiveConfig);
        }

        private void OnDisable()
        {
            OnClose = null;
            ActiveConfig.OnConfigModified -= Refresh;
            ActiveConfig.OnConfigRemoved -= Close;
        }
    }
}
