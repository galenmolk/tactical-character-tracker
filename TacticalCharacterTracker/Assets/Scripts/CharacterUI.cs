using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Character testCharacter;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private StatsUI statsUI;
    [SerializeField] private AbilitySlot abilitySlotPrefab;
    [SerializeField] private Transform abilityParent;
    
    private Character character;
    
    public void LoadCharacter(Character _character)
    {
        character = _character;
        characterName.text = character.name;
        statsUI.LoadStats(character);
        DisplayAbilities(character.abilities);
    }

    private void DisplayAbilities(Ability[] abilities)
    {
        for (int i = 0, length = abilities.Length; i < length; i++)
            DisplayAbility(abilities[i]);
    }

    private void DisplayAbility(Ability ability)
    {
        Instantiate(abilitySlotPrefab, abilityParent).Initialize(ability);
    }
    
    private void Awake()
    {
        LoadCharacter(testCharacter);
    }
}
