using HexedHeroes.Creator;
using TMPro;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameText;
    
    public CharacterConfig Config { get; private set; }

    public string CharacterName => characterNameText.text;
    
    public void Initialize(CharacterConfig config)
    {
        Config = config;
        characterNameText.text = config.name;
    }

    public void TryDelete()
    {
        Debug.Log("Try Delete");
        ConfirmationPanel.Instance.Open(new DeleteCharacterParams(this));
    }

    public void Edit()
    {
        Debug.Log("Edit");
        CharacterEditor.Instance.Initialize(this);
        CharacterEditor.Instance.Open();
        CharacterDisplay.Instance.Close();
    }

    public void UpdateName(string name)
    {
        Debug.Log("UpdateName");
        Config.name = name;
        characterNameText.text = name;
    }
}
