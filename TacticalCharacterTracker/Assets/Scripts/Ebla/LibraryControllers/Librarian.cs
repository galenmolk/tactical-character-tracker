using System;
using System.Collections.Generic;
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
        private EnemyLibraryController Enemies { get; set; }

        [SerializeField] private TextAsset abilitiesJson;
        [SerializeField] private TextAsset enemiesJson;

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
                case EnemyConfig config:
                    Debug.Log("Enemy Added");
                    Enemies.Add(config);
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
                case EnemyConfig config:
                    Enemies.Remove(config);
                    break;
                default:
                    isRemoving = false;
                    break;
            }
            
            if (isRemoving)
                OnConfigRemoved?.Invoke(baseConfig);
        }

        public List<AbilityConfig> GetAbilities()
        {
            return Abilities.All();
        }

        private void Awake()
        {
            CreateControllers();
        }

        private void CreateControllers()
        {
            var abilityLibraryConfig = JsonConvert.DeserializeObject<List<AbilityConfig>>(abilitiesJson.text);
            Abilities = new AbilityLibraryController(abilityLibraryConfig ?? new List<AbilityConfig>());

            var enemyLibraryConfig = JsonConvert.DeserializeObject<List<EnemyConfig>>(enemiesJson.text);
            Enemies = new EnemyLibraryController(enemyLibraryConfig ?? new List<EnemyConfig>());
        }
    }
}
