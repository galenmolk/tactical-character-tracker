using System.Collections.Generic;
using HexedHeroes.Creator;
using TMPro;
using UnityEngine;

namespace HexedHeroes.DungeonRunner
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
        private List<AbilityRow> abilityRows = new List<AbilityRow>();

        public void Initialize(EnemyTypeConfig enemyTypeConfig)
        {
            config = enemyTypeConfig;
            enemyTypeTitleText.text = $"{config.character.name}s";
            CreateAbilityRows();
            CreateEnemyInstances();
        }

        private void CreateAbilityRows()
        {
            foreach (var abilityConfig in config.character.abilities)
            {
                AbilityRow row = Instantiate(abilityRowPrefab, abilityRowParent);
                abilityRows.Add(row);
                row.Initialize(abilityConfig);
            }   
        }

        private void CreateEnemyInstances()
        {
            for (int i = 0; i < config.quantity; i++)
            {
                CreateHeaderCell(i);
                CreateStatCells();
                CreateAbilityCells();
            }
        }

        private void CreateHeaderCell(int index)
        {
            LabelCell titleCell = Instantiate(labelCellPrefab, headerRow);
            string title = $"{config.character.name} {++index}";
            titleCell.SetString(title);
        }
        
        private void CreateStatCells()
        {
            NumberCell healthCell = Instantiate(statCellPrefab, healthRow);
            healthCell.SetInt(config.character.health);
            
            NumberCell defenseCell = Instantiate(statCellPrefab, defenseRow);
            defenseCell.SetInt(config.character.defense);
        }

        private void CreateAbilityCells()
        {
            for (int i = 0; i < config.character.abilities.Count; i++)
            {
                AbilityCell abilityCell = Instantiate(abilityCellPrefab, abilityRows[i].Content);
                abilityCell.SetAbility(config.character.abilities[i]);
            }
        }
    }
}
