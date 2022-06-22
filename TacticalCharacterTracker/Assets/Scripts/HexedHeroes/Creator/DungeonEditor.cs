using System.Collections.Generic;
using HexedHeroes.Creator;
using HexedHeroes.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

public class DungeonEditor : MainPanel<DungeonEditor>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_InputField dungeonNameText;
    [SerializeField] private Transform enemyCardParent;
    [SerializeField] private EnemyCard enemyCardPrefab;
    
    private DungeonCard dungeonCard;

    private readonly List<EnemyCard> enemyCards = new();
    
    public override void Open()
    {
        canvasGroup.SetIsActive(true);
    }
    
    public override void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    public void Back()
    {
        Close();
        DungeonDisplay.Instance.Open();
        EnemySelector.Instance.Close();
    }

    public void Initialize(DungeonCard card)
    {
        dungeonCard = card;
        dungeonNameText.text = card.DungeonName;
        ClearEnemyTypes();
        DisplayEnemyTypes();
    }

    public void SetName(string name)
    {
        if (name == dungeonCard.DungeonName)
            return;

        dungeonCard.UpdateName(name);
    }

    public void AddNewEnemies()
    {
        EnemySelector.Instance.Initialize(dungeonCard);
        EnemySelector.Instance.Open();
        //Close();
    }

    public void DeleteEnemyCard(EnemyCard enemyCard)
    {
        dungeonCard.Config.enemyTypes.Remove(enemyCard.TypeConfig);
        EnemySelector.Instance.Initialize(dungeonCard);
        enemyCards.Remove(enemyCard);
        Destroy(enemyCard.gameObject);
    }
    
    protected override void OnAwake()
    {
        Close();
    }
    
    private void DisplayEnemyTypes()
    {
        for (int i = 0, count = dungeonCard.Config.enemyTypes.Count; i < count; i++)
            AddEnemyCard(dungeonCard.Config.enemyTypes[i]);
    }

    private void AddEnemyCard(EnemyTypeConfig enemyTypeConfig)
    {
        EnemyCard enemyCard = Instantiate(enemyCardPrefab, enemyCardParent);
        enemyCard.Initialize(enemyTypeConfig);
        enemyCards.Add(enemyCard);
        EnemySelector.Instance.Initialize(dungeonCard);
    }

    private void ClearEnemyTypes()
    {
        if (enemyCards.Count == 0)
            return;
        
        enemyCards.Clear();
        enemyCardParent.gameObject.DestroyChildrenOfType<EnemyCard>();
    }
}
