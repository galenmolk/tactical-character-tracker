using TMPro;
using UnityEngine;

public class DungeonEditor : MainPanel<DungeonEditor>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_InputField dungeonNameText;

    private DungeonCard dungeonCard;
    
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
    }

    public void SetName(string name)
    {
        if (name == dungeonCard.DungeonName)
            return;

        dungeonCard.UpdateName(name);
    }
    
    protected override void OnAwake()
    {
        Close();
        gameObject.SetActive(true);
    }
}
