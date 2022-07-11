using System;
using System.Collections.Generic;
using Ebla.Models;
using Ebla.Utils;
using UnityEngine;

namespace Ebla.Selection
{
    public abstract class ConfigSelector<TConfig, TParentConfig, TOption> : MonoBehaviour 
        where TConfig : BaseConfig
        where TParentConfig : BaseConfig
        where TOption : BaseOption<TConfig, TOption>
    {
        public event Action OnClose;
        
        [SerializeField] protected Transform optionParent;
        
        protected TParentConfig parentConfig;
        
        protected List<TConfig> selectedConfigs = new();

        protected readonly List<TOption> options = new();
        protected readonly EventShuttle onFiltersUpdated = new();

        public void Initialize(TParentConfig config)
        {
            parentConfig = config;
            CreateOptions();
        }

        protected abstract void CreateOptions();
    }
}
