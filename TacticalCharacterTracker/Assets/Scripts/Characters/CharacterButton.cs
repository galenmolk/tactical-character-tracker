using TMPro;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;

    private CharacterConfig characterConfig;
    
    public void Initialize(CharacterConfig _characterConfig)
    {
        characterConfig = _characterConfig;
        characterName.text = characterConfig.name;
    }

    public void Select()
    {
        CharacterSelect.Instance.SelectCharacter(characterConfig);
    }
}
