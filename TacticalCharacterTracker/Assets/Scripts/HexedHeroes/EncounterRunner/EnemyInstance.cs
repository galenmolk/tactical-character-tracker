using System;
using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyInstance : MonoBehaviour
    {
        public event Action<EnemyInstance> OnDelete;

        public EnemyConfig Config { get; private set; }

        [SerializeField] private LabelCell nameCell;
        [SerializeField] private AbilityCell abilityCellPrefab;
        [SerializeField] private Transform abilityCellParent;
        [SerializeField] private NumberCell healthCell;
        [SerializeField] private NumberCell defenseCell;

        private readonly List<AbilityCell> abilityCells = new();

        public void Delete()
        {
            OnDelete?.Invoke(this);
        }
        
        public void Configure(EnemyConfig enemyConfig, int index)
        {
            Config = enemyConfig;
            nameCell.SetString($"{Config.Name} {++index}");   
            healthCell.SetInt(Config.Health);
            defenseCell.SetInt(Config.Defense);
            CreateAbilityCells();
        }
        
        public void CreateAbilityCell(AbilityConfig abilityConfig)
        {
            AbilityCell abilityCell = Instantiate(abilityCellPrefab, abilityCellParent);
            abilityCell.OnDelete += HandleDeleteAbilityCell;
            abilityCells.Add(abilityCell);
            abilityCell.SetAbility(abilityConfig);
        }

        private void HandleDeleteAbilityCell(AbilityCell cell)
        {
            abilityCells.Remove(cell);
            Destroy(cell.gameObject);
        }
        
        private void CreateAbilityCells()
        {
            for (int i = 0, count = Config.Abilities.Count; i < count; i++)
            {
                CreateAbilityCell(Config.Abilities[i]);
            }
        }
        
        private void OnDisable()
        {
            OnDelete = null;
        }
    }
}
