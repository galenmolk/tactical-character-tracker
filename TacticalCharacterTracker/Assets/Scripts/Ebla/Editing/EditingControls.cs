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
            ActiveConfig = config;
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
    }
}
