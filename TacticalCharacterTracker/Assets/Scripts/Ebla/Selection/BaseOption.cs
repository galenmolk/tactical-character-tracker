using Ebla.Models;
using Ebla.Utils;
using TMPro;
using UnityEngine;

namespace Ebla.Selection
{
    public abstract class BaseOption<TConfig, TOption> : BaseBehaviour<TOption>
        where TConfig : BaseConfig
        where TOption : MonoBehaviour
    {
        public TConfig Config { get; private set; }

        [SerializeField] private TMP_Text nameText;

        private EventShuttle filterUpdateShuttle;
        
        public override void ResetObject()
        {
            
        }

        public void Initialize(TConfig config, EventShuttle onFiltersUpdated)
        {
            filterUpdateShuttle = onFiltersUpdated;
            onFiltersUpdated.Subscribe(HandleFiltersUpdated);
            Config = config;
            nameText.text = config.Name;
            PopulateOption();
        }

        protected abstract void PopulateOption();

        private void HandleFiltersUpdated()
        {
            filterUpdateShuttle.Unsubscribe(HandleFiltersUpdated);
            Debug.Log("BaseOption Handle Filters Updated");
            ReleaseObject();
        }
    }
}
