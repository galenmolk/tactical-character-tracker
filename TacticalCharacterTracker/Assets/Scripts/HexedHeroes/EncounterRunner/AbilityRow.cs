    using HexedHeroes.Models;
    using TMPro;
    using UnityEngine;
    using AbilityConfig = Ebla.Models.AbilityConfig;

    namespace HexedHeroes.EncounterRunner
    {
        public class AbilityRow : MonoBehaviour
        {
            public Transform Content;
            
            [SerializeField] private TMP_InputField abilityTitleText;
            [SerializeField] private TMP_InputField cooldownText;
            [SerializeField] private Transform content;
            
            private AbilityConfig config;

            public void UpdateName(string newName)
            {
                config.UpdateName(newName);
            }
            
            public void UpdateCooldown(string cooldown)
            {
                if (!int.TryParse(cooldown, out int value))
                {
                    return;
                }
                
                config.UpdateCooldownTurns(value);
            }
            
            public void Initialize(AbilityConfig abilityConfig)
            {
                config = abilityConfig;
                abilityTitleText.text = config.Name;
                cooldownText.text = config.CooldownTurns.ToString();
            }
        }
    }
