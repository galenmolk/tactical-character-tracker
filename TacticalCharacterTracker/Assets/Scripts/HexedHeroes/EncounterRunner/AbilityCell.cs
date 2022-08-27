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
            abilityInstance.Ability.OnConfigRemoved += HandleDeleteAbility;

            if (config.CurrentCooldownTurns != 0)
            {
                SetInt(config.CurrentCooldownTurns);
            }
            else
            {
                SetString(string.Empty);
            }

            image.color = config.IsActivated ? activatedColor : Color.white;
        }

        public void OnEndEdit()
        {
            int value = GetNumber();
            SetColorBasedOnCooldown(value);
            ChangeCurrentCooldown(value);
        }

        public void Activate()
        {
            image.color = activatedColor;
            config.UpdateIsActivated(true);
            
            int cooldown = config.Ability.CooldownTurns;
            
            if (cooldown <= 0)
            {
                return;
            }

            ChangeCurrentCooldown(cooldown);
            SetInt(cooldown);
        }

        private void SetColorBasedOnCooldown(int value)
        {
            image.color = value > 0 ? activatedColor : Color.white;
        }
        
        private void HandleDeleteAbility(BaseConfig baseConfig)
        {
            OnDelete?.Invoke(this);
        }
        
        private void ResetValue()
        {
            image.color = Color.white;
            SetString(string.Empty);
            ChangeCurrentCooldown(0);
        }
        
        private void ReduceCooldown()
        {
            int currentNumber = GetNumber();

            if (currentNumber <= 0)
            {
                ResetValue();
                return;
            }
            
            int newValue = DecrementAndReturnValue();
            ChangeCurrentCooldown(newValue);

            if (newValue <= 0)
            {
                ResetValue();
            }
        }

        private void ChangeCurrentCooldown(int newCooldown)
        {
            config.UpdateCurrentCooldownTurns(newCooldown);
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
