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
        where TController : LibraryController<TConfig>
    {
        public event Action<TConfig> OnConfigAdded;

        private TController Configs { get; set; }

        [SerializeField] protected TextAsset libraryJson;
        
        public void Add(TConfig config)
        {
            Configs.Add(config);
            OnConfigAdded?.Invoke(config);
        }
        
        public void Remove(TConfig config)
        {
            Configs.Remove(config);
        }

        public List<TConfig> GetAbilities()
        {
            return Configs.All();
        }

        private void Awake()
        {
            CreateControllers();
        }

        private void CreateControllers()
        {
            var libraryConfig = JsonConvert.DeserializeObject<List<TConfig>>(libraryJson.text);
            Configs = Activator.CreateInstance(typeof(TController), libraryConfig ?? new List<TConfig>()) as TController;
        }

        private void OnDisable()
        {
            OnConfigAdded = null;
        }
    }
}
