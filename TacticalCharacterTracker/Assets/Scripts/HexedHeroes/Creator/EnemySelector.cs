using System.Collections.Generic;
using HexedHeroes.Utils;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelector : MainPanel<EnemySelector>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform enemyOptionParent;
    [SerializeField] private EnemyOptionCard enemyOptionCardPrefab;
    [SerializeField] private GameObject noEnemiesWindow;
    [SerializeField] private Button addButton;
    
    private DungeonCard dungeonCard;
    private readonly List<EnemyOptionCard> enemyOptions = new();
    private readonly List<EnemyTypeConfig> selectedTypes = new List<EnemyTypeConfig>();

    private List<CharacterConfig> selectedEnemies = new List<CharacterConfig>();
    
    public override void Open()
    {
        canvasGroup.SetIsActive(true);
    }
    
    public override void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    public void AddSelection()
    {
        dungeonCard.Config.enemyTypes.AddRange(GetSelectedTypesAsEnemyConfigList());
        DungeonEditor.Instance.Initialize(dungeonCard);
    }

    public void Back()
    {
        Close();
    }

    public void Initialize(DungeonCard _dungeonCard)
    {
        addButton.interactable = false;
        DestroyAllOptions();
        dungeonCard = _dungeonCard;
        CreateEnemyOptions();
    }

    public void SwitchToEnemyBuilder()
    {
        Debug.Log("Enemy Builder");
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Close();
    }

    private List<EnemyTypeConfig> GetSelectedTypesAsEnemyConfigList()
    {
        selectedTypes.Clear();
        
        foreach (var enemyOptionCard in enemyOptions)
        {
            if (!enemyOptionCard.IsSelected)
                continue;
            
            selectedTypes.Add(new EnemyTypeConfig(enemyOptionCard.Config));
        }

        return selectedTypes;
    }

    private void CreateEnemyOptions()
    {
        List<CharacterConfig> enemies = DataManager.Instance.Characters;
        int enemyCount = enemies.Count;

        if (enemyCount < 1)
        {
            noEnemiesWindow.SetActive(true);
            return;
        }

        List<CharacterConfig> dungeonTypes = dungeonCard.Config.GetEnemyTypes();
        
        for (int i = 0, count = enemies.Count; i < count; i++)
        {
            if (!dungeonTypes.Contains(enemies[i]))
                CreateEnemyOption(enemies[i]);
        }
    }

    private void CreateEnemyOption(CharacterConfig enemyType)
    {
        EnemyOptionCard enemyOptionCard = Instantiate(enemyOptionCardPrefab, enemyOptionParent);
        enemyOptionCard.Initialize(enemyType);
        
        if (selectedEnemies.Contains(enemyType))
            enemyOptionCard.IsSelected = true;
            
        enemyOptionCard.OnSelectionChanged.AddListener(CardSelectionChanged);
        enemyOptions.Add(enemyOptionCard);
    }

    private void CardSelectionChanged(EnemyOptionCard card)
    {
        switch (card.IsSelected)
        {
            case true:
                selectedEnemies.Add(card.Config);
                break;
            case false when selectedEnemies.Contains(card.Config):
                selectedEnemies.Remove(card.Config);
                break;
        }

        addButton.interactable = selectedEnemies.Count > 0;
    }
    
    private void DestroyAllOptions()
    {
        enemyOptionParent.gameObject.DestroyChildrenOfType<EnemyOptionCard>();
        enemyOptions.Clear();
        selectedEnemies.Clear();
    }
}
