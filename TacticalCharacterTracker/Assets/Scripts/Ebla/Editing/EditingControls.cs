using System;
using Ebla.Editing.Sections;
using Ebla.Models;
using Ebla.Utils;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingControls<TConfig> : MonoBehaviour, IUpdatable
        where TConfig : BaseConfig
    {
        public event Action OnClose;
        
        [SerializeField] private StringSection nameSection;
        [SerializeField] private StringSection descriptionSection;
        
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

        private void TryUpdateDescription(string newDescription)
        {
            if (string.Equals(newDescription, ActiveConfig.Description))
            {
                return;
            }
            
            ActiveConfig.UpdateDescription(newDescription);
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

        private void OnEnable()
        {
            SubscribeToUpdates();
        }

        private void OnDisable()
        {
            UnsubscribeToUpdates();
            OnClose = null;
            ActiveConfig.OnConfigModified -= Refresh;
            ActiveConfig.OnConfigRemoved -= HandleConfigRemoved;
        }

        private void HandleConfigRemoved()
        {
            OnClose?.Invoke();
        }

        public void ExecuteUpdate()
        {
            if (Input.GetKeyDown(HotKeys.Back))
            {
                OnClose?.Invoke();
            }
        }

        private void SubscribeToUpdates()
        {
            UpdateController.Instance.Subscribe(this);
        }

        private void UnsubscribeToUpdates()
        {
            if (UpdateController.Instance)
                UpdateController.Instance.Unsubscribe(this);
        }
    }
}
