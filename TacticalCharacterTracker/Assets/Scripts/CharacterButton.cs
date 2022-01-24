using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CharacterButton : MonoBehaviour
{
    public CharacterConfig CharacterConfig { get; private set; }

    [SerializeField] private TMP_Text characterName;

    private UnityAction<CharacterButton> action;

    public void Initialize(CharacterConfig _characterConfig)
    {
        CharacterConfig = _characterConfig;
        characterName.text = CharacterConfig.name;
    }

    public void SetClickAction(UnityAction<CharacterButton> _action)
    {
        action = _action;
    }

    public void Select()
    {
        action(this);
    }
}
