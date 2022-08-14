using System;
using TMPro;
using UnityEngine;
using  Ebla.Models;

namespace HexedHeroes.EncounterRunner
{
    public class AbilityRow : MonoBehaviour
    {
        public event Action<AbilityRow> OnDelete; 

        [SerializeField] private TMP_InputField abilityTitleText;
        [SerializeField] private TMP_InputField cooldownText;

        public AbilityConfig Config { get; private set; }

        public void TryDelete()
        {
            Config.TryDeleteConfig();
        }
        
        public void UpdateName(string newName)
        {
            Config.UpdateName(newName);
        }
        
        public void UpdateCooldown(string cooldown)
        {
            if (!int.TryParse(cooldown, out int value))
            {
                return;
            }
            
            Config.UpdateCooldownTurns(value);
        }
        
        public void Initialize(AbilityConfig abilityConfig)
        {
            Config = abilityConfig;
            Config.OnConfigRemoved += HandleConfigRemoved;
            abilityTitleText.text = Config.Name;
            cooldownText.text = Config.CooldownTurns.ToString();
        }

        private void HandleConfigRemoved(BaseConfig baseConfig)
        {
            OnDelete?.Invoke(this);
        }

        private void OnDisable()
        {
            OnDelete = null;
        }
    }
}
