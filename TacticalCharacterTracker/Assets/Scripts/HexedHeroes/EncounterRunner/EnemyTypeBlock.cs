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
        [SerializeField] private NumberCell statCellPrefab;
        [SerializeField] private AbilityCell abilityCellPrefab;
        [SerializeField] private LabelCell labelCellPrefab;
        
        [Header("Other")]
        [SerializeField] private TMP_InputField enemyTypeTitleText;

        [SerializeField] private TMP_InputField defense;
        [SerializeField] private TMP_InputField health;
        
        [SerializeField] private Transform headerRow;
        [SerializeField] private Transform abilityRowParent;
        [SerializeField] private Transform healthRow;
        [SerializeField] private Transform defenseRow;

        private readonly List<AbilityRow> abilityRows = new();

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
            int newQuantity = Config.Quantity + 1;
            Config.UpdateQuantity(newQuantity);
            CreateEnemy(newQuantity - 1);
        }

        public void AddAbility()
        {
            AbilityConfig abilityConfig = new();
            Config.Enemy.AddAbility(abilityConfig);
            CreateAbility(abilityConfig);
            
            int abilityIndex = Config.Enemy.Abilities.Count - 1;
            
            for (int i = 0; i < Config.Quantity; i++)
            {
                CreateAbilityCell(abilityIndex);
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
            foreach (var abilityConfig in Config.Enemy.Abilities)
            {
                CreateAbility(abilityConfig);
            }   
        }

        private void CreateAbility(AbilityConfig abilityConfig)
        {
            AbilityRow row = Instantiate(abilityRowPrefab, abilityRowParent);
            abilityRows.Add(row);
            row.Initialize(abilityConfig);
        }
        
        private void CreateEnemyInstances()
        {
            for (int i = 0; i < Config.Quantity; i++)
            {
                CreateEnemy(i);
            }
        }
        
        private void CreateEnemy(int index)
        {
            CreateHeaderCell(index);
            CreateStatCells();
            CreateAbilityCells();
        }

        private void CreateHeaderCell(int index)
        {
            LabelCell titleCell = Instantiate(labelCellPrefab, headerRow);
            string title = $"{Config.Enemy.Name} {++index}";
            titleCell.SetString(title);
        }
        
        private void CreateStatCells()
        {
            NumberCell healthCell = Instantiate(statCellPrefab, healthRow);
            healthCell.SetInt(Config.Enemy.Health);
            
            NumberCell defenseCell = Instantiate(statCellPrefab, defenseRow);
            defenseCell.SetInt(Config.Enemy.Defense);
        }

        private void CreateAbilityCells()
        {
            for (int i = 0; i < Config.Enemy.Abilities.Count; i++)
            {
                CreateAbilityCell(i);
            }
        }

        private void CreateAbilityCell(int index)
        {
            AbilityCell abilityCell = Instantiate(abilityCellPrefab, abilityRows[index].Content);
            abilityCell.SetAbility(Config.Enemy.Abilities[index]);
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
