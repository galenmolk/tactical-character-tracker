using System;
using HexedHeroes.Models;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.DungeonRunner
{
    public class AbilityCell : NumberCell
    {
        [SerializeField] private Image image;

        private Color activatedColor = new Color(0.75f, 0.75f, 0.75f, 1f);
        private AbilityConfig config;
        
        public void SetAbility(AbilityConfig abilityConfig)
        {
            config = abilityConfig;
            
            if (config.cooldown > 0)
                SetInt(0);
            else
                SetString(String.Empty);
        }

        public void OnEndEdit()
        {
            if (GetNumber() <= 0)
                image.color = Color.white;
        }

        public void Activate()
        {
            image.color = activatedColor;
            
            if (config.cooldown > 0)
                SetInt(config.cooldown);
        }

        private void ReduceCooldown()
        {
            if (config.cooldown <= 0)
            {
                image.color = Color.white;
                return;
            }
            
            if (GetNumber() <= 0)
                return;

            Decrement();
            
            if (GetNumber() <= 0)
                image.color = Color.white;
        }

        private void OnEnable()
        {
            CooldownController.reduceActive += ReduceCooldown;
        }

        private void OnDisable()
        {
            CooldownController.reduceActive -= ReduceCooldown;
        }
    }
}
