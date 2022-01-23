using Newtonsoft.Json;
using UnityEngine;

public class JsonCharacterCreator : MonoBehaviour
{
    [SerializeField] private CharacterConfig characterConfig;
    [SerializeField] private CharacterListConfig characterListConfig;
    
    [ContextMenu("LogCharacterAsJson")]
    public void LogCharacterAsJson()
    {
        string characterListJson = JsonConvert.SerializeObject(characterListConfig);
        Debug.Log(characterListJson);
    }
    
    [ContextMenu("SetCharacter")]
    public void SetCharacter()
    {
        characterListConfig.characterList.Add(characterConfig);
    }
}
