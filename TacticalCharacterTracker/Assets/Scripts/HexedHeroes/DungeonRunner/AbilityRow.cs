    using TMPro;
    using UnityEngine;

    namespace HexedHeroes.DungeonRunner
    {
        public class AbilityRow : MonoBehaviour
        {
            public Transform Content;
            
            [SerializeField] private TMP_InputField abilityTitleText;
            [SerializeField] private TMP_InputField cooldownText;
            [SerializeField] private Transform content;
            
            private AbilityConfig config;
            
            public void Initialize(AbilityConfig abilityConfig)
            {
                config = abilityConfig;
                abilityTitleText.text = config.name;
                cooldownText.text = config.cooldown.ToString();
            }
        }
    }
