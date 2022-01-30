using UnityEngine;

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
        reselectParameters.SetConfirmationAction(CharacterSelect.Instance.ReselectCharacter);
        LoadCharacter(CharacterSelect.Instance.SelectedCharacter);
    }

    public void LoadCharacter(CharacterConfig character)
    {
        characterNameUI.LoadCharacterName(character);
        statsUI.LoadStats(character);
        abilityUI.DisplayAbilities(character);
    }

    public void TakeDamage(int amount)
    {
        statsUI.TakeDamage(amount);
    }
}
