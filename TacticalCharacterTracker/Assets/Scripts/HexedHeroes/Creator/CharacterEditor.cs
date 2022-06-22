using System.Collections.Generic;
using HexedHeroes.Models;
using HexedHeroes.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

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
        private readonly List<AbilityConfig> currentAbilityConfigs = new();
        
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
            AbilitySelector.Instance.Close();
            CharacterDisplay.Instance.Open();
            Close();
        }
    
        public void Initialize(CharacterCard card)
        {
            if (characterCard != card)
            {
                abilityParent.gameObject.DestroyChildrenOfType<AbilityCard>();
                abilityCards.Clear();
                currentAbilityConfigs.Clear();
            }

            characterCard = card;
            DisplayStats();
            DisplayAbilities();
        }

        public void AddAbilityButtonClicked()
        {
            AbilitySelector.Instance.Initialize(characterCard);
            AbilitySelector.Instance.Open();
        }
        
        public void DeleteAbilityCard(AbilityCard abilityCard)
        {
            characterCard.Config.abilities.Remove(abilityCard.AbilityConfig);
            AbilitySelector.Instance.Initialize(characterCard);
            abilityCards.Remove(abilityCard);
            currentAbilityConfigs.Remove(abilityCard.AbilityConfig);
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
                if (currentAbilityConfigs.Contains(ability))
                    continue;
                
                CreateButton(ability);
            }

            AbilitySelector.Instance.Initialize(characterCard);
        }

        private void CreateButton(AbilityConfig config)
        {
            var card = Instantiate(abilityCardPrefab, abilityParent);
            card.Initialize(config);
            abilityCards.Add(card);
            currentAbilityConfigs.Add(card.AbilityConfig);
        }
    }
}
