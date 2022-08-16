using System;
using Ebla.Models;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class AbilityCell : NumberCell
    {
        public event Action<AbilityCell> OnDelete; 

        [SerializeField] private Image image;

        private readonly Color activatedColor = new(0.75f, 0.75f, 0.75f, 1f);
        private AbilityInstanceConfig config;
        
        public void SetAbility(AbilityInstanceConfig abilityInstance)
        {
            config = abilityInstance;
            config.OnConfigRemoved += HandleDeleteAbility;
            ResetValue();
        }

        public void OnEndEdit()
        {
            bool isCoolingDown = GetNumber() > 0;
            image.color = isCoolingDown ? activatedColor : Color.white;
        }

        public void Activate()
        {
            image.color = activatedColor;
            
            if (config.Ability.CooldownTurns > 0)
            {
                SetInt(config.Ability.CooldownTurns);
            }
        }

        private void HandleDeleteAbility(BaseConfig baseConfig)
        {
            OnDelete?.Invoke(this);
        }
        
        private void ResetValue()
        {
            image.color = Color.white;
            SetString(string.Empty);
        }
        
        private void ReduceCooldown()
        {
            if (GetNumber() <= 0)
            {
                ResetValue();
                return;
            }

            Decrement();
            
            if (GetNumber() <= 0)
            {
                ResetValue();
            }
        }

        private void OnEnable()
        {
            CooldownController.ReduceActive += ReduceCooldown;
        }

        private void OnDisable()
        {
            CooldownController.ReduceActive -= ReduceCooldown;
        }
    }
}
