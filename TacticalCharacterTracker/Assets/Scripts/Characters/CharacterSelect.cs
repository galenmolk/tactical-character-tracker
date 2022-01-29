using UnityEngine;

public class CharacterSelect : Singleton<CharacterSelect>
{
    [SerializeField] private CharacterSelectUI characterSelectUI;

    public CharacterConfig SelectedCharacter { get; private set; }

    public void ReturnToCharacterSelectScreen()
    {
        
    }

    protected override void OnAwake()
    {
        characterSelectUI.PopulateCharacterButtons(CharacterDownloader.Instance.CharacterListConfig);
    }

    public void SelectCharacter(CharacterConfig characterConfig)
    {
        SelectedCharacter = characterConfig;
        SceneLoadManager.Instance.LoadScene(Scenes.CHARACTER_SHEET);
    }
}
