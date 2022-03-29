using HexedHeroes.Utils;
using UnityEngine;

public class OverlayCloseButton : Singleton<OverlayCloseButton>
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void Open()
    {
        canvasGroup.SetIsActive(true);
    }

    public void Clicked()
    {
        ConfirmationBox.Instance.Close();
        AbilityInfoBox.Instance.CloseAbilityInfo();
        Close();
    }
    
    public void Close()
    {
        canvasGroup.SetIsActive(false);
    }
    
    private void Awake()
    {
        canvasGroup.SetIsActive(false);
    }
}
