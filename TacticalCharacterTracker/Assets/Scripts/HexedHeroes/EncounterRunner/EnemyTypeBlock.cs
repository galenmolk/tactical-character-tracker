using System;
using System.Collections.Generic;
using Ebla.Models;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyTypeBlock : MonoBehaviour
    {
        public event Action<EnemyTypeBlock> OnDelete;

        public EnemyTypeConfig Config { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private AbilityRow abilityRowPrefab;
        [SerializeField] private EnemyInstance enemyInstancePrefab;

        [Header("Other")] 
        [SerializeField] private Transform instanceParent;
        [SerializeField] private TMP_InputField enemyTypeTitleText;

        [SerializeField] private TMP_InputField defense;
        [SerializeField] private TMP_InputField health;
        
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
            Config.Enemy.UpdateName(newName);
        }
        
        public void UpdateHealth(string newHealth)
        {
            if (!int.TryParse(newHealth, out int value))
            {
                return;
            }
            
            Config.Enemy.UpdateHealth(value);
        }

        public void UpdateDefense(string newDefense)
        {
            if (!int.TryParse(newDefense, out int value))
            {
                return;
            }
            
            Config.Enemy.UpdateDefense(value);
        }
        
        public void IncreaseEnemyQuantity()
        {
            EnemyInstanceConfig enemyInstanceConfig = new(Config.Enemy);
            Config.Enemy.AddInstance(enemyInstanceConfig);
            CreateEnemy(Config.Enemy.EnemyInstances.Count - 1);
        }

        public void AddAbility()
        {
            AbilityConfig abilityConfig = new();
            Config.Enemy.AddAbility(abilityConfig);
            CreateAbilityRow(abilityConfig);
            
            foreach (EnemyInstance enemyInstance in enemyInstances)
            {
                AbilityInstanceConfig instanceConfig = new(abilityConfig);
                abilityConfig.AddInstance(instanceConfig);
                enemyInstance.CreateAbilityCell(instanceConfig);
            }
        }
        
        public void Initialize(EnemyTypeConfig enemyTypeConfig)
        {
            enemyTypeConfig.OnConfigRemoved += HandleConfigDeleted;
            Config = enemyTypeConfig;
            enemyTypeTitleText.text = $"{Config.Enemy.Name}";
            health.text = enemyTypeConfig.Enemy.Health.ToString();
            defense.text = enemyTypeConfig.Enemy.Defense.ToString();
            CreateAbilityRows();
            CreateEnemyInstances();
        }

        private void HandleConfigDeleted(BaseConfig baseConfig)
        {
            OnDelete?.Invoke(this);
        }

        private void CreateAbilityRows()
        {
            foreach (AbilityConfig abilityConfig in Config.Enemy.Abilities)
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
            Config.Enemy.RemoveAbility(row.Config);
            abilityRows.Remove(row);
            Destroy(row.gameObject);
        }
        
        private void CreateEnemyInstances()
        {
            for (int i = 0, count = Config.Enemy.EnemyInstances.Count; i < count; i++)
            {
                CreateEnemy(i);
            }
        }
        
        private void CreateEnemy(int index)
        {
            EnemyInstance instance = Instantiate(enemyInstancePrefab, instanceParent);
            enemyInstances.Add(instance);
            instance.OnDelete += HandleDeleteInstance;
            instance.Configure(Config.Enemy.EnemyInstances[index], index);
        }

        private void HandleDeleteInstance(EnemyInstance enemyInstance)
        {
            enemyInstances.Remove(enemyInstance);
            Destroy(enemyInstance.gameObject);
            Config.Enemy.RemoveInstance(enemyInstance.Config);

            foreach (var abilityConfig in Config.Enemy.Abilities)
            {
                AbilityInstanceConfig instance = abilityConfig.AbilityInstances[]
                abilityConfig.RemoveInstance();
            }
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
