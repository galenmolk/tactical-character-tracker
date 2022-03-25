using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text tooltipText;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float lifetime;
    
    private RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = transform as RectTransform;

            return rectTransform;
        }
    }

    private RectTransform rectTransform;
    
    private TooltipElementEventArgs currentArgs;
    
    private void Activate(TooltipElementEventArgs eventArgs)
    {
        if (currentArgs?.Element == eventArgs.Element)
            return;
        
        currentArgs = eventArgs;
        tooltipText.text = eventArgs.Element.Text;
        this.DelayExecutionUntilEndOfFrame(() =>
        {
            Vector2 size = RectTransform.sizeDelta;
            Debug.Log(size);
            RectTransform.position = eventArgs.EventData.position + size * 0.5f;
            canvasGroup.SetIsVisible(true);
        });
    }

    private void Deactivate(TooltipElementEventArgs eventArgs)
    {
        if (eventArgs?.Element != currentArgs.Element)
            return;

        currentArgs = null;
        canvasGroup.SetIsVisible(false);
    }
    
    private void OnEnable()
    {
        TooltipElement.PointerEnter += Activate;
        TooltipElement.PointerExit += Deactivate;
    }

    private void OnDisable()
    {
        TooltipElement.PointerEnter -= Activate;
        TooltipElement.PointerExit -= Deactivate;
    }
}
