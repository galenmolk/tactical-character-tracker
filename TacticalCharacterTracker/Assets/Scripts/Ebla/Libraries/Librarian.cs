using System;
using System.Collections.Generic;
using Ebla.Models;
using MolkExtras;
using Newtonsoft.Json;
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

        private readonly TController controller = new();

        [SerializeField] protected TextAsset libraryJson;

        private Dictionary<string, TConfig> configs;

        public void Add(TConfig config)
        {
            controller.Add(config);
            OnConfigAdded?.Invoke(config);
        }
        
        public void Remove(TConfig config)
        {
            Debug.Log($"Librarian Remove {config.Name}");
            controller.Remove(config);
            OnConfigRemovedFromLibrary?.Invoke(config);
        }

        public void LoadInConfigs(Dictionary<string, TConfig> configs)
        {
            controller.LoadInConfigs(configs);
        }
        
        public void InitializeFolders()
        {
            foreach (KeyValuePair<string,TConfig> config in controller.All())
            {
                config.Value.GetParentFromPath();
            }
        }

        protected override void OnAwake()
        {
            // DeserializeJson();
            // LoadIntoController();
        }

        private void DeserializeJson()
        {
            configs = JsonConvert.DeserializeObject<Dictionary<string, TConfig>>(libraryJson.text);
        }

        private void OnDisable()
        {
            OnConfigAdded = null;
        }
    }
}
