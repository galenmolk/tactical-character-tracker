using System;
using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyInstance : MonoBehaviour
    {
        public event Action<EnemyInstance> OnDelete;

        public EnemyInstanceConfig Config { get; private set; }

        [Header("Components")]
        [SerializeField] private LabelCell nameCell;
        [SerializeField] private Transform abilityCellParent;
        [SerializeField] private NumberCell healthCell;
        [SerializeField] private NumberCell defenseCell;
        
        [Header("Prefabs")]
        [SerializeField] private AbilityCell abilityCellPrefab;

        private readonly List<AbilityCell> abilityCells = new();

        public void Delete()
        {
            OnDelete?.Invoke(this);
        }
        
        public void Configure(EnemyInstanceConfig instanceConfig, int index)
        {
            Config = instanceConfig;
            nameCell.SetString($"{Config.Name} {++index}");   
            healthCell.SetInt(Config.CurrentHealth);
            defenseCell.SetInt(Config.CurrentDefense);
            CreateAbilityCells();
        }
        
        public void CreateAbilityCell(AbilityInstanceConfig instance)
        {
            AbilityCell abilityCell = Instantiate(abilityCellPrefab, abilityCellParent);
            abilityCell.OnDelete += HandleDeleteAbilityCell;
            abilityCells.Add(abilityCell);
            abilityCell.SetAbility(instance);
        }

        private void HandleDeleteAbilityCell(AbilityCell cell)
        {
            abilityCells.Remove(cell);
            Destroy(cell.gameObject);
        }
        
        private void CreateAbilityCells()
        {
            for (int i = 0, count = Config.Enemy.Abilities.Count; i < count; i++)
            {
                CreateAbilityCell(Config.Enemy.Abilities[i].AbilityInstances[i]);
            }
        }
        
        private void OnDisable()
        {
            OnDelete = null;
        }
    }
}
