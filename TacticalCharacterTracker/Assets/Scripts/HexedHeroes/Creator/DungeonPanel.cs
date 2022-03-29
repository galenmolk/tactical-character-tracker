using HexedHeroes.Creator;
using HexedHeroes.Utils;
using UnityEngine;

public class DungeonPanel : MainPanel<DungeonPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    public override void Open()
    {
        Close();
        CharacterPanel.Instance.Close();
        DungeonDisplay.Instance.Open();
        canvasGroup.SetIsActive(true);
    }

    public override void Close()
    {
        DungeonDisplay.Instance.Close();
        DungeonEditor.Instance.Close();
        EnemySelector.Instance.Close();
        canvasGroup.SetIsActive(false);
    }

    protected override void OnAwake()
    {
        Close();
    }
}