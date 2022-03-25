using UnityEngine;

public class DungeonPanel : MainPanel<DungeonPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    public override void Open()
    {
        DungeonEditor.Instance.Close();
        EnemySelector.Instance.Close();
        CharacterPanel.Instance.Close();
        DungeonDisplay.Instance.Open();
        canvasGroup.SetIsActive(true);
    }

    public override void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    protected override void OnAwake()
    {
        Close();
    }
}