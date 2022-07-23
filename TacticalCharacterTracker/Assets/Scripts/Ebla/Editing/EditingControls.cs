using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Editing
{
    public abstract class EditingControls<TConfig> : MonoBehaviour where TConfig : BaseConfig
    {
        [SerializeField] private StringSection nameSection;

        protected TConfig ActiveConfig { get; private set; }

        public void Initialize(TConfig config)
        {
            Debug.Log("Initialize " + config.Name);

            ActiveConfig = config;
            config.OnConfigModified += Refresh;
            ApplyConfig(config);
        }

        public void TryUpdateName(string newName)
        {
            if (string.Equals(newName, ActiveConfig.Name))
                return;

            ActiveConfig.UpdateName(newName);
        }
        
        protected virtual void ApplyConfig(TConfig config)
        {
            nameSection.TrySetValue(config.Name);
        }

        private void Refresh()
        {
            ApplyConfig(ActiveConfig);
        }

        private void OnEnable()
        {
            Librarian.OnConfigRemoved += HandleConfigRemoved;
        }

        private void OnDisable()
        {
            ActiveConfig.OnConfigModified -= Refresh;
            Librarian.OnConfigRemoved -= HandleConfigRemoved;
        }

        private void HandleConfigRemoved(BaseConfig config)
        {
            if (config == ActiveConfig)
                EditingController.Instance.Close();
        }
    }
}
