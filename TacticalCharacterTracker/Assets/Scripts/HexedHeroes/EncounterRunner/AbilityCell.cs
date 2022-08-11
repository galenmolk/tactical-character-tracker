using Ebla.Models;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class AbilityCell : NumberCell
    {
        [SerializeField] private Image image;

        private readonly Color activatedColor = new(0.75f, 0.75f, 0.75f, 1f);
        private AbilityConfig config;
        
        public void SetAbility(AbilityConfig abilityConfig)
        {
            config = abilityConfig;
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
            
            if (config.CooldownTurns > 0)
            {
                SetInt(config.CooldownTurns);
            }
        }

        private void ResetValue()
        {
            if (config.CooldownTurns > 0)
            {
                SetInt(0);
            }
            else
            {
                SetString(string.Empty);
            }
        }
        
        private void ReduceCooldown()
        {
            if (config.CooldownTurns <= 0)
            {
                image.color = Color.white;
                ResetValue();
                return;
            }
            
            if (GetNumber() <= 0)
            {
                return;
            }

            Decrement();
            
            if (GetNumber() <= 0)
            {
                image.color = Color.white;
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
