using System;
using Ebla.Models;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.LibraryControllers
{
    public class Librarian : Singleton<Librarian>
    {
        public static event Action<BaseConfig> OnConfigAdded;
        public static event Action<BaseConfig> OnConfigRemoved;
        
        private AbilityLibraryController Abilities { get; set; }
        
        [SerializeField] private TextAsset abilitiesJson;

        public void Add(BaseConfig baseConfig)
        {
            Debug.Log("Librarian.Add");
            bool isAdding = true;
            
            switch (baseConfig)
            {
                case AbilityConfig config:
                    Debug.Log("Ability Added");
                    Abilities.Add(config);
                    break;
                default:
                    isAdding = false;
                    break;
            }
            
            Debug.Log("Librarian.Add" + isAdding);

            if (isAdding)
                OnConfigAdded?.Invoke(baseConfig);
        }
        
        public void Remove(BaseConfig baseConfig)
        {
            bool isRemoving = true;
            
            switch (baseConfig)
            {
                case AbilityConfig config:
                    Abilities.Remove(config);
                    break;
                default:
                    isRemoving = false;
                    break;
            }
            
            if (isRemoving)
                OnConfigRemoved?.Invoke(baseConfig);
        }
        
        private void Awake()
        {
            CreateControllers();
        }

        private void CreateControllers()
        {
            var abilityLibraryConfig = JsonConvert.DeserializeObject<AbilityLibraryConfig>(abilitiesJson.text);
            Abilities = new AbilityLibraryController(abilityLibraryConfig ?? new AbilityLibraryConfig());
        }

        private static void HandleConfigAdded<TConfig>(TConfig config) where TConfig : BaseConfig
        {
            OnConfigAdded?.Invoke(config);
        }
        
        private static void HandleConfigRemoved<TConfig>(TConfig config) where TConfig : BaseConfig
        {
            OnConfigRemoved?.Invoke(config);
        }
    }
}
