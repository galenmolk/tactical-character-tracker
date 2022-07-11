using MolkExtras;
using UnityEngine;

namespace HexedHeroes.Player
{
    public class CharacterSheet : Singleton<CharacterSheet>
    {
        [SerializeField] private StatsUI statsUI;
        [SerializeField] private AbilityUI abilityUI;
        [SerializeField] private CharacterNameUI characterNameUI;
        [SerializeField] private ConfirmationParameters reselectParameters;

        public void ConfirmReselectCharacter()
        {
            ConfirmationBox.Instance.Open(reselectParameters);
        }
    
        protected override void OnAwake()
        {
            base.OnAwake();
            if (ActiveSession.SelectedCharacter == null)
                return;
        
            LoadCharacter(ActiveSession.SelectedCharacter);
        }

        private void LoadCharacter(CharacterConfig character)
        {
            characterNameUI.LoadCharacterName(character);
            statsUI.LoadStats(character);
            abilityUI.DisplayAbilities(character);
        }

        public void TakeDamage(int amount)
        {
            statsUI.TakeDamage(amount);
        }

        public void GainDefense(int amount)
        {
            statsUI.GainDefense(amount);
        }
    }
}
