using System;
using System.Collections.Generic;
using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class EnemyInstance : MonoBehaviour
    {
        public event Action<EnemyInstance> OnDelete;

        public CharacterInstanceConfig Config { get; private set; }

        public AutoFade Fade { get; private set; }
        public LayoutElement LayoutElement { get; private set; } 

        [Header("Components")]
        [SerializeField] private LabelCell nameCell;
        [SerializeField] private Transform abilityCellParent;
        [SerializeField] private NumberCell healthCell;
        [SerializeField] private NumberCell defenseCell;
        [SerializeField] private TMP_InputField notesCell;
        
        [Header("Prefabs")]
        [SerializeField] private AbilityCell abilityCellPrefab;

        private readonly List<AbilityCell> abilityCells = new();

        public void Delete()
        {
            OnDelete?.Invoke(this);
        }

        public void OnEndEditName(string newName)
        {
            Config.UpdateName(newName);
        }

        public void OnEndEditNotes(string newNotes)
        {
            Config.UpdateDescription(newNotes);
        }

        public void OnEndEditHealth(string newHealth)
        {
            if (int.TryParse(newHealth, out int health))
            {
                HealthModified(health);
            }
        }

        public void OnEndEditDefense(string newDefense)
        {
            if (int.TryParse(newDefense, out int defense))
            {
                DefenseModified(defense);
            }
        }
        
        public void HealthModified(int health)
        {
            Config.UpdateHealth(health);
        }

        public void DefenseModified(int defense)
        {
            Config.UpdateDefense(defense);   
        }

        public void Configure(CharacterInstanceConfig instanceConfig, int index)
        {
            Config = instanceConfig;
            nameCell.SetString(instanceConfig.GetOrUpdateName(index));  
            notesCell.text = instanceConfig.Description;
            healthCell.SetInt(Config.CurrentHealth);
            defenseCell.SetInt(Config.CurrentDefense);
            CreateAbilityCells();
        }

        public void AddAbility(AbilityConfig abilityConfig)
        {
            AbilityInstanceConfig config = Config.AddAbility(abilityConfig);
            CreateAbilityCell(config);
        }
                
        private void CreateAbilityCells()
        {
            for (int i = 0, count = Config.AbilityInstances.Count; i < count; i++)
            {
                CreateAbilityCell(Config.AbilityInstances[i]);
            }
        }
        
        private void CreateAbilityCell(AbilityInstanceConfig instance)
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

        private void OnDisable()
        {
            OnDelete = null;
        }

        private void Awake()
        {
            Fade = GetComponent<AutoFade>();
            LayoutElement = GetComponent<LayoutElement>();
        }
    }
}
