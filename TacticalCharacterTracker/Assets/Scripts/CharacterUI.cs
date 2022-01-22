using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Character testCharacter;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private StatsUI statsUI;
   
    [SerializeField] private AbilityManager abilityManager;
    
    private Character character;
    
    public void LoadCharacter(Character _character)
    {
        character = _character;
        characterName.text = character.name;
        statsUI.LoadStats(character);
        abilityManager.DisplayAbilities(character.abilities);
    }
    
    private void Awake()
    {
        LoadCharacter(testCharacter);
    }
}
