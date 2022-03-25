using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : Singleton<CharacterDisplay>
{
    [SerializeField] private CharacterCard characterCardPrefab;
    [SerializeField] private Transform characterCardParent;
    [SerializeField] private CanvasGroup canvasGroup;

    private readonly List<CharacterCard> characterCards = new();

    public void Open()
    {
        canvasGroup.SetIsActive(true);
    }
    
    public void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    public void DisplayCharacters(List<CharacterConfig> characterConfigs)
    {
        foreach (var characterConfig in characterConfigs)
        {
            CreateCharacterCard(characterConfig);
        }
    }
    
    public void CreateNewCharacter()
    {
        CreateCharacterCard(GetNewCharacterConfig());
    }

    private void CreateCharacterCard(CharacterConfig config)
    {
        CharacterCard characterCard = Instantiate(characterCardPrefab, characterCardParent);
        characterCard.Initialize(config);
        characterCards.Add(characterCard);
    }
    
    public void DeleteCharacter(CharacterCard card)
    {
        characterCards.Remove(card);
        Destroy(card.gameObject);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Close();
    }

    private CharacterConfig GetNewCharacterConfig()
    {
        return new CharacterConfig(GetUniqueCharacterName());
    }
    
    private string GetUniqueCharacterName()
    {
        List<string> names = GetAllCharacterNames();

        var index = 0;
        string uniqueName;

        do
        {
            index++;
            uniqueName = $"{CharacterConfig.UNTITLED_NAME_PREFIX}{index}";
        } 
        while (names.Contains(uniqueName));

        return uniqueName;
    }

    private List<string> GetAllCharacterNames()
    {
        var names = new List<string>();
        
        for (int i = 0, count = characterCards.Count; i < count; i++)
            names.Add(characterCards[i].CharacterName);

        return names;
    }
}
