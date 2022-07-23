using System;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingControls<TConfig> : MonoBehaviour where TConfig : BaseConfig
    {
        public event Action OnConfigRemoved;
        
        [SerializeField] private StringSection nameSection;

        protected TConfig ActiveConfig { get; private set; }

        public void Initialize(TConfig config)
        {
            ActiveConfig = config;
            config.OnConfigRemoved += HandleConfigRemoved;
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

        public void DeleteConfig()
        {
            RemoveConfig();
        }

        protected abstract void RemoveConfig();
        
        protected virtual void ApplyConfig(TConfig config)
        {
            nameSection.TrySetValue(config.Name);
        }

        protected virtual void SubscribeToSectionModifiedEvents()
        {
            nameSection.SubscribeToModifiedEvent(TryUpdateName);
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
            OnConfigRemoved = null;
            ActiveConfig.OnConfigModified -= Refresh;
            ActiveConfig.OnConfigRemoved -= HandleConfigRemoved;
        }

        private void HandleConfigRemoved()
        {
            OnConfigRemoved?.Invoke();
        }
    }
}
