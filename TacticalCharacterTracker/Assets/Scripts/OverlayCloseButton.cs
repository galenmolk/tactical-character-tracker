using UnityEngine;

public class OverlayCloseButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Open()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, true);
    }
    
    public void Close()
    {
        MessageCenter.InvokeOverlayCloseButtonPressed();
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }
    
    private void Awake()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }

    private void OnEnable()
    {
        MessageCenter.SubscribeAbilityInfoButtonPressed(_ => Open());  
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeAbilityInfoButtonPressed(_ => Open());
    }
}
