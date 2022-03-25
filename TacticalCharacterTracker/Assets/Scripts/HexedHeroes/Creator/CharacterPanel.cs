using UnityEngine;

public class CharacterPanel : MainPanel<CharacterPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    public override void Open()
    {
        Debug.Log("Open");
        DungeonPanel.Instance.Close();
        CharacterDisplay.Instance.Open();
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