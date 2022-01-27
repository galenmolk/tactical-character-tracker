using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private StatsUI statsUI;
   
    [SerializeField] private AbilityManager abilityManager;
    
    private CharacterConfig activeCharacter;
    
    public void LoadCharacter(CharacterConfig characterConfig)
    {
        MessageCenter.InvokeCharacterLoaded(characterConfig);
        activeCharacter = characterConfig;
        characterName.text = activeCharacter.name;
        statsUI.LoadStats(activeCharacter);
        abilityManager.DisplayAbilities(activeCharacter);
    }
}
