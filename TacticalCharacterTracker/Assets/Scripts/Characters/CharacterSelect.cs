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

    private void PopulateCharacterButtons(CharacterListConfig characterListConfig)
    {
        for (int i = 0, length = characterListConfig.characterList.Count; i < length; i++)
        {
            CharacterButton button = Instantiate(characterButtonPrefab, characterButtonParent);
            button.Initialize(characterListConfig.characterList[i], SelectAndLoadCharacter);
        }
    }
}
