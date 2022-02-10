using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MainPanel<EnemySelector>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform enemyOptionParent;
    [SerializeField] private EnemyOptionCard enemyOptionCardPrefab;
    
    private DungeonCard dungeonCard;
    private readonly List<EnemyOptionCard> enemyOptions = new();
    private readonly List<EnemyConfig> selectedTypes = new List<EnemyConfig>();

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
        dungeonCard.Config.enemies.AddRange(GetSelectedTypesAsEnemyConfigList());
        DungeonEditor.Instance.Initialize(dungeonCard);
        DungeonEditor.Instance.Open();
        Close();
    }

    public void Back()
    {
        DungeonEditor.Instance.Open();
        Close();
    }

    public void Initialize(DungeonCard _dungeonCard)
    {
        DestroyAllOptions();
        dungeonCard = _dungeonCard;
        CreateEnemyOptions();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Close();
    }

    private List<EnemyConfig> GetSelectedTypesAsEnemyConfigList()
    {
        selectedTypes.Clear();
        
        foreach (var enemyOptionCard in enemyOptions)
        {
            if (!enemyOptionCard.IsSelected)
                continue;
            
            selectedTypes.Add(new EnemyConfig(enemyOptionCard.Config));
        }

        return selectedTypes;
    }

    private void CreateEnemyOptions()
    {
        Debug.Log("CreateEnemyOptions");
        CharacterListConfig enemies = DataManager.Instance.Enemies;
        Debug.Log("enemies: " + enemies);
        Debug.Log("enemies.Count: " + enemies.characters.Count);

        CharacterListConfig dungeonTypes = dungeonCard.Config.GetEnemyTypes();
        
        for (int i = 0, count = enemies.characters.Count; i < count; i++)
        {
            if (!dungeonTypes.characters.Contains(enemies.characters[i]))
                CreateEnemyOption(enemies.characters[i]);
        }
    }

    private void CreateEnemyOption(CharacterConfig enemyType)
    {
        EnemyOptionCard enemyOptionCard = Instantiate(enemyOptionCardPrefab, enemyOptionParent);
        enemyOptionCard.Initialize(enemyType);
        enemyOptions.Add(enemyOptionCard);
    }

    private void DestroyAllOptions()
    {
        enemyOptionParent.gameObject.DestroyChildrenOfType<EnemyOptionCard>();
        enemyOptions.Clear();
    }
}
