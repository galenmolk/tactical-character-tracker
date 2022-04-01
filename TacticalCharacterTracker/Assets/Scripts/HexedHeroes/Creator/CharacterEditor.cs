using System.Collections.Generic;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexedHeroes.Creator
{
    public class CharacterEditor : MainPanel<CharacterEditor>
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_InputField nameText;
        [SerializeField] private TMP_InputField defenseText;
        [SerializeField] private TMP_InputField healthText;
        [SerializeField] private TMP_InputField speedText;

        [SerializeField] private AbilityCard abilityCardPrefab;
        [SerializeField] private Transform abilityParent;

        private CharacterCard characterCard;
        private readonly List<AbilityCard> abilityCards = new();

        public override void Open()
        {
            canvasGroup.SetIsActive(true);
        }

        public override void Close()
        {
            canvasGroup.SetIsActive(false);
        }
    
        public void Back()
        {
            CharacterDisplay.Instance.Open();
            Close();
        }
    
        public void Initialize(CharacterCard card)
        {
            characterCard = card;
            DisplayStats();
            DisplayAbilities();
        }

        public void AddAbilityButtonClicked()
        {
            AbilitySelector.Instance.Initialize(characterCard);
            AbilitySelector.Instance.Open();
            
            // AbilityConfig abilityConfig = new AbilityConfig();
            // characterCard.Config.abilities.Add(abilityConfig);
            // AbilityCreator.Instance.Open(abilityConfig);
        }
        
        public void DeleteAbilityCard(AbilityCard abilityCard)
        {
            characterCard.Config.abilities.Remove(abilityCard.AbilityConfig);
            AbilitySelector.Instance.Initialize(characterCard);
            abilityCards.Remove(abilityCard);
            Destroy(abilityCard.gameObject);
        }

        public void UpdateName()
        {
            characterCard.Config.name = nameText.text;
            CharactersModified();
        }

        public void UpdateDefense()
        {
            characterCard.Config.defense = int.Parse(defenseText.text);
            CharactersModified();
        }

        public void UpdateHealth()
        {
            characterCard.Config.health = int.Parse(healthText.text);
            CharactersModified();
        }
        
        public void UpdateSpeed()
        {
            characterCard.Config.speed = int.Parse(speedText.text);
            CharactersModified();
        }

        private void CharactersModified()
        {
            DataManager.Instance.SaveCharacters();
        }

        private void DisplayStats()
        {
            nameText.text = characterCard.Config.name;
            defenseText.text = characterCard.Config.defense.ToString();
            healthText.text = characterCard.Config.health.ToString();
            speedText.text = characterCard.Config.speed.ToString();
        }
    
        private void DisplayAbilities()
        {
            foreach (var ability in characterCard.Config.abilities)
            {
                CreateButton(ability);
            }
        }

        private void CreateButton(AbilityConfig config)
        {
            AbilityCard card = Instantiate(abilityCardPrefab, abilityParent);
            card.Initialize(config);
            AbilitySelector.Instance.Initialize(characterCard);
        }
    }
}
