using System.Collections.Generic;
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
        Close();
    }

    public void DeleteEnemyCard(EnemyCard enemyCard)
    {
        dungeonCard.Config.enemies.Remove(enemyCard.Config);
        enemyCards.Remove(enemyCard);
        Destroy(enemyCard.gameObject);
    }
    
    protected override void OnAwake()
    {
        Close();
    }
    
    private void DisplayEnemyTypes()
    {
        for (int i = 0, count = dungeonCard.Config.enemies.Count; i < count; i++)
            AddEnemyCard(dungeonCard.Config.enemies[i]);
    }

    private void AddEnemyCard(EnemyConfig enemyConfig)
    {
        EnemyCard enemyCard = Instantiate(enemyCardPrefab, enemyCardParent);
        enemyCard.Initialize(enemyConfig);
        enemyCards.Add(enemyCard);
    }

    private void ClearEnemyTypes()
    {
        if (enemyCards.Count == 0)
            return;
        
        enemyCards.Clear();
        enemyCardParent.gameObject.DestroyChildrenOfType<EnemyCard>();
    }
}
