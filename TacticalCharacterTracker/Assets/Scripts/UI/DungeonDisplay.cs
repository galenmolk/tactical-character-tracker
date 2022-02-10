using System.Collections.Generic;
using UnityEngine;

public class DungeonDisplay : Singleton<DungeonDisplay>
{
    [SerializeField] private DungeonCard dungeonCardPrefab;
    [SerializeField] private Transform dungeonCardParent;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private List<DungeonCard> dungeonCards = new List<DungeonCard>();
    
    public void Open()
    {
        canvasGroup.SetIsActive(true);
    }
    
    public void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    public void CreateNewDungeon()
    {
        DungeonCard dungeonCard = Instantiate(dungeonCardPrefab, dungeonCardParent);
        dungeonCard.Initialize(GetNewDungeonConfig());
        AddDungeonCard(dungeonCard);
    }

    public void DeleteDungeon(DungeonCard card)
    {
        RemoveDungeonCard(card);
        Destroy(card.gameObject);
    }

    protected override void OnAwake()
    {
        Close();
    }

    private void AddDungeonCard(DungeonCard card)
    {
        dungeonCards.Add(card);
    }

    private void RemoveDungeonCard(DungeonCard card)
    {
        dungeonCards.Remove(card);
    }

    private DungeonConfig GetNewDungeonConfig()
    {
        return new DungeonConfig(GetUniqueDungeonName());
    }

    private string GetUniqueDungeonName()
    {
        List<string> names = GetAllDungeonNames();

        int index = 0;
        string uniqueName;

        do
        {
            index++;
            uniqueName = $"{DungeonConfig.UNTITLED_NAME_PREFIX}{index}";
        }
        while (names.Contains(uniqueName));
        
        return uniqueName;
    }

    private List<string> GetAllDungeonNames()
    {
        List<string> names = new List<string>();

        for (int i = 0, count = dungeonCards.Count; i < count; i++)
            names.Add(dungeonCards[i].DungeonName);

        return names;
    }
}
