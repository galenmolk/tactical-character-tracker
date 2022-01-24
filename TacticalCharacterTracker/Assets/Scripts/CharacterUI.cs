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
        activeCharacter = characterConfig;
        characterName.text = activeCharacter.name;
        statsUI.LoadStats(activeCharacter);
        abilityManager.DisplayPassiveAbilities(activeCharacter.passiveAbilities);
        abilityManager.DisplayCooldownAbilities(activeCharacter.cooldownAbilities);
    }
}
