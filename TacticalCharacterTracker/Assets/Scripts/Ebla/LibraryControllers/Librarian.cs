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
        public static event Action<AbilityConfig> OnAbilityAdded;
        public static event Action<EnemyConfig> OnEnemyAdded;
        
        private AbilityLibraryController Abilities { get; set; }
        private EnemyLibraryController Enemies { get; set; }

        [SerializeField] private TextAsset abilitiesJson;
        [SerializeField] private TextAsset enemiesJson;
        
        public void Add(AbilityConfig abilityConfig)
        {
            Abilities.Add(abilityConfig);
            OnAbilityAdded?.Invoke(abilityConfig);
        }

        public void Add(EnemyConfig enemyConfig)
        {
            Enemies.Add(enemyConfig);
            OnEnemyAdded?.Invoke(enemyConfig);
        }
        
        public void Remove(AbilityConfig abilityConfig)
        {
            Abilities.Remove(abilityConfig);
            abilityConfig.InvokeConfigRemoved();
        }

        public void Remove(EnemyConfig enemyConfig)
        {
            Enemies.Remove(enemyConfig);
            enemyConfig.InvokeConfigRemoved();
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
