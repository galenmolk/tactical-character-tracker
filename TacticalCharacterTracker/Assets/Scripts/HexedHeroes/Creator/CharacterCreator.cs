using HexedHeroes.Utils;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace HexedHeroes.Creator
{
    public class CharacterCreator : Singleton<CharacterCreator>
    {
        [SerializeField] private TMP_InputField nameText;
        [SerializeField] private TMP_InputField defenseText;
        [SerializeField] private TMP_InputField healthText;
        [SerializeField] private TMP_InputField speedText;

        [SerializeField] private AbilityCard abilityCardPrefab;
        [SerializeField] private Transform abilityParent;

        private CharacterConfig config;
        private AbilityCard activeCard;

        protected override void OnAwake()
        {
            base.OnAwake();
            OpenCharacter(new CharacterConfig());
        }

        public void OpenCharacter(CharacterConfig _config)
        {
            config = _config;
            DisplayStats();
            DisplayAbilities();
        }

        public void AddAbilityButtonClicked()
        {
            AbilityConfig abilityConfig = new AbilityConfig();
            config.abilities.Add(abilityConfig);
            AbilityCreator.Instance.Open(abilityConfig);
        }
    
        private void AbilityButtonClicked(AbilityCard abilityCard)
        {
            activeCard = abilityCard;
            AbilityCreator.Instance.Open(abilityCard.AbilityConfig);
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(nameText.text))
                return;
        
            config.name = nameText.text;
            config.defense = int.Parse(defenseText.text);
            config.health = int.Parse(healthText.text);
            config.speed = int.Parse(speedText.text);
            string json = JsonConvert.SerializeObject(config);
            Debug.Log(json);
        }

        public void UpdateActiveAbilityButton(AbilityConfig abilityConfig)
        {
            if (activeCard == null || activeCard.AbilityConfig != abilityConfig)
                CreateActiveButton(abilityConfig);
            else
                activeCard.Display(abilityConfig);
        }
    
        public void ClearActiveAbilityButton()
        {
            activeCard = null;
        }

        private void DisplayStats()
        {
            nameText.text = config.name;
            defenseText.text = config.defense.ToString();
            healthText.text = config.health.ToString();
            speedText.text = config.speed.ToString();
        }
    
        private void DisplayAbilities()
        {
            foreach (var ability in config.abilities)
            {
                CreateButton(ability);
            }
        }

        private void CreateActiveButton(AbilityConfig config)
        {
            activeCard = CreateButton(config);
        }

        private AbilityCard CreateButton(AbilityConfig config)
        {
            AbilityCard card = Instantiate(abilityCardPrefab, abilityParent);
            card.Initialize(config);
            return card;
        }
    }
}
