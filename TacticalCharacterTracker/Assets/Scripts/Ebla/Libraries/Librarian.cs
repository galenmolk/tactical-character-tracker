using System;
using System.Collections.Generic;
using Ebla.API;
using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.Libraries
{
    public abstract class Librarian<TLibrarian, TConfig, TController> : Singleton<TLibrarian>
        where TConfig : BaseConfig
        where TLibrarian : Librarian<TLibrarian, TConfig, TController>
        where TController : LibraryController<TConfig>, new()
    {
        public static event Action<TConfig> OnConfigAdded;
        public static event Action<TConfig> OnConfigRemovedFromLibrary;

        protected virtual string ApiRoute => string.Empty;

        private readonly TController controller = new();

        public void Add(TConfig config)
        {
            controller.Add(config);
            config.OnConfigModified += HandleConfigModified;
            OnConfigAdded?.Invoke(config);
            StartCoroutine(ApiUtils.PostConfig(ApiRoute, config));
        }
        
        public void Remove(TConfig config)
        {
            controller.Remove(config);
            config.OnConfigModified -= HandleConfigModified;
            OnConfigRemovedFromLibrary?.Invoke(config);
        }

        public void LoadInConfigs(Dictionary<string, TConfig> configs)
        {
            foreach (KeyValuePair<string,TConfig> config in configs)
            {
                config.Value.OnConfigModified += HandleConfigModified;
            }
            
            controller.LoadInConfigs(configs);
        }
        
        public void InitializeFolders()
        {
            foreach (KeyValuePair<string,TConfig> config in controller.All())
            {
                config.Value.GetParentFromPath();
            }
        }

        private void HandleConfigModified(BaseConfig baseConfig)
        {
            Debug.Log($"HandleConfigModified {baseConfig.Name}");
            StartCoroutine(ApiUtils.UpdateConfig(ApiRoute, baseConfig));
        }

        private void OnDisable()
        {
            OnConfigAdded = null;
        }
    }
}
