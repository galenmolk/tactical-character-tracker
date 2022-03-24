using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private RectTransform characterButtonParent;
    
    protected void Awake()
    {
        Debug.Log(ActiveSession.AvailableCharacters);
        
        if (ActiveSession.AvailableCharacters == null)
            return;
        
        Debug.Log(ActiveSession.AvailableCharacters);
        Debug.Log(ActiveSession.AvailableCharacters.Count);
        Debug.Log(ActiveSession.AvailableCharacters[0].name);

        PopulateCharacterButtons(ActiveSession.AvailableCharacters);
    }

    private void SelectAndLoadCharacter(CharacterConfig characterConfig)
    {
        ActiveSession.SelectedCharacter = characterConfig;
        SceneManager.LoadScene(SceneKeys.CHARACTER_SHEET);
    }

    private void PopulateCharacterButtons(List<CharacterConfig> characterList)
    {
        Debug.Log("PopulateCharacterButtons");
        for (int i = 0, length = characterList.Count; i < length; i++)
        {
            CharacterButton button = Instantiate(characterButtonPrefab, characterButtonParent);
            button.Initialize(characterList[i], SelectAndLoadCharacter);
        }
    }
}
