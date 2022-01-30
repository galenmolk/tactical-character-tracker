using UnityEngine;

public class OverlayCloseButton : Singleton<OverlayCloseButton>
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void Open()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, true);
    }
    
    public void Close()
    {
        ConfirmationBox.Instance.Close();
        AbilityInfoBox.Instance.CloseAbilityInfo();
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }
    
    private void Awake()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }
}
