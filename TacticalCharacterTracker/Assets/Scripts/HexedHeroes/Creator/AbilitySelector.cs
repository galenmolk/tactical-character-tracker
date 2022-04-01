using System.Collections.Generic;
using HexedHeroes.Utils;
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
        
        private readonly List<AbilityOptionCard> abilityCards = new();
        [SerializeField] private List<AbilityConfig> selectedAbilities = new();
        
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
            characterCard.Config.abilities.AddRange(selectedAbilities);
            CharacterEditor.Instance.Initialize(characterCard);
        }
        
        public void Back()
        {
            Close();
        }

        public void Initialize(CharacterCard _characterCard)
        {
            addButton.interactable = false;
            DestroyAllOptions();
            characterCard = _characterCard;
            CreateAbilityOptions();
        }

        public void SwitchToAbilityCreator()
        {
            Debug.Log("Enemy Builder");
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Close();
        }

        private void CreateAbilityOptions()
        {
            var abilities = DataManager.Instance.Abilities;
            var abilityCount = abilities.Count;

            if (abilityCount < 1)
            {
                noAbilitiesWindow.SetActive(true);
                return;
            }

            var currentAbilities = characterCard.Config.abilities;
        
            for (int i = 0, count = abilities.Count; i < count; i++)
            {
                if (!currentAbilities.Contains(abilities[i]))
                    CreateAbilityOption(abilities[i]);
            }
        }

        private void CreateAbilityOption(AbilityConfig ability)
        {
            AbilityOptionCard abilityOptionCard = Instantiate(abilityOptionCardPrefab, abilityOptionParent);
            abilityOptionCard.Initialize(ability);
        
            if (selectedAbilities.Contains(ability))
                abilityOptionCard.IsSelected = true;
            
            abilityOptionCard.onSelectionChanged.AddListener(CardSelectionChanged);
            abilityCards.Add(abilityOptionCard);
        }

        private void CardSelectionChanged(AbilityOptionCard card)
        {
            Debug.Log("card: " + card.IsSelected);
            switch (card.IsSelected)
            {
                case true:
                    selectedAbilities.Add(card.Config);
                    break;
                case false when selectedAbilities.Contains(card.Config):
                    selectedAbilities.Remove(card.Config);
                    break;
            }

            addButton.interactable = selectedAbilities.Count > 0;
        }
    
        private void DestroyAllOptions()
        {
            abilityOptionParent.gameObject.DestroyChildrenOfType<AbilityOptionCard>();
            abilityCards.Clear();
            selectedAbilities.Clear();
        }
    }
}
