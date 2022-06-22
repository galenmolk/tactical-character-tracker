using System.Collections.Generic;
using HexedHeroes.Models;
using HexedHeroes.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.Creator
{
    public class AbilitySelector : MainPanel<AbilitySelector>
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform abilityOptionParent;
        [SerializeField] private AbilityOptionCard abilityOptionCardPrefab;
        [SerializeField] private GameObject noAbilitiesWindow;
        [SerializeField] private Button addButton;

        private CharacterCard characterCard;
        private string SearchInput => searchInput.text.ToUpper();
        
        [SerializeField] private TMP_InputField searchInput;
        
        private readonly List<AbilityOptionCard> abilityCards = new();
        private readonly List<AbilityOptionCard> ownedCards = new();
        [SerializeField] private List<AbilityOptionCard> selectedCards = new();
        
        public override void Open()
        {
            canvasGroup.SetIsActive(true);
        }
    
        public override void Close()
        {
            canvasGroup.SetIsActive(false);
        }
        
        public void AddSelection()
        {
            characterCard.Config.abilities.AddRange(GetAbilityConfigsFromOptionCards());
            foreach (var card in selectedCards)
            {
                card.IsSelected = false;
            }
            selectedCards.Clear();
            CharacterEditor.Instance.Initialize(characterCard);
        }
        
        public void Back()
        {
            Close();
        }

        public void Initialize(CharacterCard _characterCard)
        {
            // if card is new, uncheck all abilities
            characterCard = _characterCard;
            ToggleAddButton();
            FilterContentForCharacter();
        }

        public void SwitchToAbilityCreator()
        {
            Debug.Log("Enemy Builder");
        }

        private List<AbilityConfig> GetAbilityConfigsFromOptionCards()
        {
            var list = new List<AbilityConfig>();
            foreach (var card in selectedCards)
            {
                list.Add(card.Config);
            }

            return list;
        }

        private void FilterContentForCharacter()
        {
            ownedCards.Clear();
            for (int i = 0, length = abilityCards.Count; i < length; i++)
            {
                var card = abilityCards[i];
                bool isAbilityOwned = characterCard.Config.abilities.Contains(card.Config);
                card.GameObject.TrySetActive(!isAbilityOwned);
                
                if (isAbilityOwned)
                    ownedCards.Add(card);
            }
        }
        
        public void FilterContentForSearch()
        {
            var isSearchEmpty = string.IsNullOrWhiteSpace(SearchInput);
            
            for (int i = 0, length = abilityCards.Count; i < length; i++)
            {
                var card = abilityCards[i];
                
                if (ownedCards.Contains(card))
                    continue;
                
                if (isSearchEmpty)
                {
                    card.GameObject.SetActive(true);
                    continue;
                }

                var abilityNameToUpper = card.Config.name.ToUpper();
                var isValidResult = abilityNameToUpper.Contains(SearchInput);
                card.GameObject.TrySetActive(isValidResult);
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Close();
        }

        private void CreateAbilityOptions(List<AbilityConfig> abilities)
        {
            var abilityCount = abilities.Count;

            if (abilityCount < 1)
            {
                noAbilitiesWindow.SetActive(true);
                return;
            }
        
            for (int i = 0, count = abilities.Count; i < count; i++)
            {
                CreateAbilityOption(abilities[i]);
            }
        }

        private void CreateAbilityOption(AbilityConfig ability)
        {
            AbilityOptionCard abilityOptionCard = Instantiate(abilityOptionCardPrefab, abilityOptionParent);
            abilityOptionCard.Initialize(ability);
            abilityOptionCard.onSelectionChanged.AddListener(CardSelectionChanged);
            abilityCards.Add(abilityOptionCard);
        }

        private void CardSelectionChanged(AbilityOptionCard card)
        {
            Debug.Log("card: " + card.IsSelected);
            switch (card.IsSelected)
            {
                case true:
                    selectedCards.Add(card);
                    break;
                case false when selectedCards.Contains(card):
                    selectedCards.Remove(card);
                    break;
            }

            ToggleAddButton();
        }

        private void ToggleAddButton()
        {
            addButton.interactable = selectedCards.Count > 0;
        }
        
        private void OnEnable()
        {
            DataManager.OnAbilitiesParsed += CreateAbilityOptions;
        }

        private void OnDisable()
        {
            DataManager.OnAbilitiesParsed -= CreateAbilityOptions;
        }
    }
}
