using System.Collections.Generic;
using Ebla.Models;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyTypeBlock : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private AbilityRow abilityRowPrefab;
        [SerializeField] private NumberCell statCellPrefab;
        [SerializeField] private AbilityCell abilityCellPrefab;
        [SerializeField] private LabelCell labelCellPrefab;
        
        [Header("Other")]
        [SerializeField] private TMP_InputField enemyTypeTitleText;

        [SerializeField] private Transform headerRow;
        [SerializeField] private Transform abilityRowParent;
        [SerializeField] private Transform healthRow;
        [SerializeField] private Transform defenseRow;
        
        private EnemyTypeConfig config;
        private readonly List<AbilityRow> abilityRows = new();

        public void UpdateName(string newName)
        {
            Debug.Log($"New Name {newName}");
            config.Enemy.UpdateName(newName);
        }
        
        public void UpdateHealth(string health)
        {
            if (!int.TryParse(health, out int value))
            {
                return;
            }
            
            config.Enemy.UpdateHealth(value);
        }

        public void UpdateDefense(string defense)
        {
            if (!int.TryParse(defense, out int value))
            {
                return;
            }
            
            config.Enemy.UpdateDefense(value);
        }
        
        public void IncreaseEnemyQuantity()
        {
            int newQuantity = config.Quantity + 1;
            config.UpdateQuantity(newQuantity);
            CreateEnemy(newQuantity - 1);
        }

        public void AddAbility()
        {
            AbilityConfig abilityConfig = new();
            config.Enemy.AddAbility(abilityConfig);
            CreateAbility(abilityConfig);
            
            int abilityIndex = config.Enemy.Abilities.Count - 1;
            
            for (int i = 0; i < config.Quantity; i++)
            {
                CreateAbilityCell(abilityIndex);
            }
        }
        
        public void Initialize(EnemyTypeConfig enemyTypeConfig)
        {
            config = enemyTypeConfig;
            enemyTypeTitleText.text = $"{config.Enemy.Name}";
            CreateAbilityRows();
            CreateEnemyInstances();
        }

        private void CreateAbilityRows()
        {
            foreach (var abilityConfig in config.Enemy.Abilities)
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
            for (int i = 0; i < config.Quantity; i++)
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
            Debug.Log($"Create Header Cell {config.Enemy.Name}");
            LabelCell titleCell = Instantiate(labelCellPrefab, headerRow);
            string title = $"{config.Enemy.Name} {++index}";
            titleCell.SetString(title);
        }
        
        private void CreateStatCells()
        {
            NumberCell healthCell = Instantiate(statCellPrefab, healthRow);
            healthCell.SetInt(config.Enemy.Health);
            
            NumberCell defenseCell = Instantiate(statCellPrefab, defenseRow);
            defenseCell.SetInt(config.Enemy.Defense);
        }

        private void CreateAbilityCells()
        {
            for (int i = 0; i < config.Enemy.Abilities.Count; i++)
            {
                CreateAbilityCell(i);
            }
        }

        private void CreateAbilityCell(int index)
        {
            AbilityCell abilityCell = Instantiate(abilityCellPrefab, abilityRows[index].Content);
            abilityCell.SetAbility(config.Enemy.Abilities[index]);
        }
    }
}
