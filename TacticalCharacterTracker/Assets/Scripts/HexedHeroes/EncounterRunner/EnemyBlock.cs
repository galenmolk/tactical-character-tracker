using System;
using System.Collections.Generic;
using Ebla.Models;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyBlock : MonoBehaviour
    {
        public event Action<EnemyBlock> OnDelete;

        public EnemyConfig Config { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private AbilityRow abilityRowPrefab;
        [SerializeField] private EnemyInstance enemyInstancePrefab;

        [Header("Other")] 
        [SerializeField] private Transform instanceParent;
        [SerializeField] private TMP_InputField enemyTypeTitleText;

        [SerializeField] private TMP_InputField defense;
        [SerializeField] private TMP_InputField health;
        [SerializeField] private TMP_InputField notes;
        [SerializeField] private Transform abilityRowParent;

        private readonly List<AbilityRow> abilityRows = new();

        private readonly List<EnemyInstance> enemyInstances = new();

        public void DeleteButtonClicked()
        {
            Config.TryDeleteConfig();
        }
        
        public void UpdateName(string newName)
        {
            Config.UpdateName(newName);
        }

        public void OnEndEditNotes(string newNotes)
        {
            Config.UpdateDescription(newNotes);
        }

        public void UpdateHealth(string newHealth)
        {
            if (!int.TryParse(newHealth, out int value))
            {
                return;
            }
            
            Config.UpdateHealth(value);
        }

        public void UpdateDefense(string newDefense)
        {
            if (!int.TryParse(newDefense, out int value))
            {
                return;
            }
            
            Config.UpdateDefense(value);
        }
        
        public void IncreaseEnemyQuantity()
        {
            CharacterInstanceConfig characterInstanceConfig = new(Config);
            Config.AddInstance(characterInstanceConfig);
            CreateEnemy(Config.CharacterInstances.Count - 1);
        }

        public void AddAbility()
        {
            AbilityConfig abilityConfig = new();
            Config.AddAbility(abilityConfig);
            CreateAbilityRow(abilityConfig);
            
            foreach (EnemyInstance enemyInstance in enemyInstances)
            {
                enemyInstance.AddAbility(abilityConfig);
            }
        }
        
        public void Initialize(EnemyConfig enemyConfig)
        {
            enemyConfig.OnConfigRemoved += HandleConfigDeleted;
            Config = enemyConfig;
            enemyTypeTitleText.text = Config.Name;
            notes.text = Config.Description;
            health.text = enemyConfig.Health.ToString();
            defense.text = enemyConfig.Defense.ToString();
            CreateAbilityRows();
            CreateEnemyInstances();
        }

        private void HandleConfigDeleted(BaseConfig baseConfig)
        {
            OnDelete?.Invoke(this);
        }

        private void CreateAbilityRows()
        {
            foreach (AbilityConfig abilityConfig in Config.Abilities)
            {
                CreateAbilityRow(abilityConfig);
            }   
        }

        private void CreateAbilityRow(AbilityConfig abilityConfig)
        {
            AbilityRow row = Instantiate(abilityRowPrefab, abilityRowParent);
            row.OnDelete += HandleDeleteAbility;
            abilityRows.Add(row);
            row.Initialize(abilityConfig);
        }

        private void HandleDeleteAbility(AbilityRow row)
        {
            row.OnDelete -= HandleDeleteAbility;
            Config.RemoveAbility(row.Config);
            abilityRows.Remove(row);
            Destroy(row.gameObject);
        }
        
        private void CreateEnemyInstances()
        {
            for (int i = 0, count = Config.CharacterInstances.Count; i < count; i++)
            {
                CreateEnemy(i);
            }
        }
        
        private void CreateEnemy(int index)
        {
            EnemyInstance instance = Instantiate(enemyInstancePrefab, instanceParent);
            enemyInstances.Add(instance);
            instance.OnDelete += HandleDeleteInstance;
            instance.Configure(Config.CharacterInstances[index], index);
        }

        private void HandleDeleteInstance(EnemyInstance enemyInstance)
        {
            enemyInstances.Remove(enemyInstance);
            Destroy(enemyInstance.gameObject);
            Config.RemoveInstance(enemyInstance.Config);
        }

        private void OnDisable()
        {
            OnDelete = null;
            
            if (Config != null)
            {
                Config.OnConfigRemoved -= HandleConfigDeleted;
            }
        }
    }
}
