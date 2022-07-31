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
        protected TController Controller { get; set; }

        [SerializeField] protected TextAsset libraryJson;

        private List<TConfig> configs;

        public void Add(TConfig config)
        {
            Controller.Add(config);
            OnConfigAdded?.Invoke(config);
        }
        
        public void Remove(TConfig config)
        {
            Debug.Log($"Librarian Remove {config.Name}");
            Controller.Remove(config);
            OnConfigRemovedFromLibrary?.Invoke(config);
        }

        public List<TConfig> GetAbilities()
        {
            return Controller.All();
        }

        public void LoadIntoController()
        {
            Controller.LoadInConfigs(configs);
        }

        public void InitializeFolders()
        {
            foreach (TConfig baseConfig in Controller.All())
            {
                baseConfig.GetParentFromPath();
            }
        }

        protected override void OnAwake()
        {
            DeserializeJson();
            CreateController();
            LoadIntoController();
        }

        private void CreateController()
        {
            Controller = new TController();
        }

        private void DeserializeJson()
        {
            configs = JsonConvert.DeserializeObject<List<TConfig>>(libraryJson.text);
        }

        private void OnDisable()
        {
            OnConfigAdded = null;
        }
    }
}
