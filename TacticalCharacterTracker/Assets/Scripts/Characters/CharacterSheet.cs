using UnityEngine;

public class CharacterSheet : Singleton<CharacterSheet>
{
    [SerializeField] private StatsUI statsUI;
    [SerializeField] private AbilityUI abilityUI;
    [SerializeField] private CharacterNameUI characterNameUI;
    
    protected override void OnAwake()
    {
        LoadCharacter(CharacterSelect.Instance.SelectedCharacter);
    }

    public void LoadCharacter(CharacterConfig character)
    {
        MessageCenter.InvokeCharacterLoaded(character);
        characterNameUI.LoadCharacterName(character);
        statsUI.LoadStats(character);
        abilityUI.DisplayAbilities(character);
    }
}
