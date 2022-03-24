using UnityEngine;

public class CharacterPanel : MainPanel<CharacterPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    public override void Open()
    {
        DungeonEditor.Instance.Close();
        EnemySelector.Instance.Close();
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