using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private RectTransform characterButtonParent;
    [SerializeField] private CharacterUI characterUI;
    
    public void ReturnToCharacterSelectScreen()
    {
        characterUI.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    
    private void PopulateCharacterButtons(CharacterListConfig characterListConfig)
    {
        for (int i = 0, length = characterListConfig.characterList.Count; i < length; i++)
        {
            CharacterButton button = Instantiate(characterButtonPrefab, characterButtonParent);
            button.Initialize(characterListConfig.characterList[i]);
            button.SetClickAction(SelectCharacter);
        }
    }

    private void SelectCharacter(CharacterButton characterButton)
    {
        characterUI.LoadCharacter(characterButton.CharacterConfig);
        gameObject.SetActive(false);
        characterUI.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        MessageCenter.SubscribeCharacterListReceived(PopulateCharacterButtons);
    }
    
    private void OnDisable()
    {
        MessageCenter.UnsubscribeCharacterListReceived(PopulateCharacterButtons);
    }
}
