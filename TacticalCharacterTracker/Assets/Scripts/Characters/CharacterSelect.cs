using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private RectTransform characterButtonParent;
    
    protected void Awake()
    {
        if (ActiveSession.AvailableCharacters == null)
            return;
        
        PopulateCharacterButtons(ActiveSession.AvailableCharacters);
    }

    private void SelectAndLoadCharacter(CharacterConfig characterConfig)
    {
        ActiveSession.SelectedCharacter = characterConfig;
        SceneLoadManager.Instance.LoadScene(Scenes.CHARACTER_SHEET);
    }

    private void PopulateCharacterButtons(List<CharacterConfig> characterList)
    {
        for (int i = 0, length = characterList.Count; i < length; i++)
        {
            CharacterButton button = Instantiate(characterButtonPrefab, characterButtonParent);
            button.Initialize(characterList[i], SelectAndLoadCharacter);
        }
    }
}
