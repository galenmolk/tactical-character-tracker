using TMPro;
using UnityEngine;

public class CharacterNameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    
    public void LoadCharacterName(CharacterConfig characterConfig)
    {
        characterName.text = characterConfig.name;
    }
}
