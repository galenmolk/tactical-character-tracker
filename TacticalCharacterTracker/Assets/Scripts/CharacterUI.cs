using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private StatsUI statsUI;
   
    [SerializeField] private AbilityManager abilityManager;

    [SerializeField] private string testJson;
    
    private CharacterConfig activeCharacter;
    
    public void LoadCharacter(CharacterConfig characterConfig)
    {
        activeCharacter = characterConfig;
        characterName.text = activeCharacter.name;
        statsUI.LoadStats(activeCharacter);
        abilityManager.DisplayPassiveAbilities(activeCharacter.passiveAbilities);
        abilityManager.DisplayCooldownAbilities(activeCharacter.cooldownAbilities);
    }
    
    private void Awake()
    {
        MessageCenter.SubscribeCharacterListReceived(LoadCharacters);
        // CharacterListConfig list = JsonConvert.DeserializeObject<CharacterListConfig>(testJson);
        // LoadCharacter(list.characterList[0]);
    }

    private void LoadCharacters(CharacterListConfig characterListConfig)
    {
        LoadCharacter(characterListConfig.characterList[0]);
    }
}
