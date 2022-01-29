using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private CharacterButton characterButtonPrefab;
    [SerializeField] private RectTransform characterButtonParent;

    public void PopulateCharacterButtons(CharacterListConfig characterListConfig)
    {
        for (int i = 0, length = characterListConfig.characterList.Count; i < length; i++)
            Instantiate(characterButtonPrefab, characterButtonParent).Initialize(characterListConfig.characterList[i]);
    }
}
