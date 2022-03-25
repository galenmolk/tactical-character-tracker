using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void TooltipElementEvent(TooltipElementEventArgs eventArgs);

    public static event TooltipElementEvent PointerEnter;
    public static event TooltipElementEvent PointerExit;

    public string Text => text;
    [SerializeField] private string text;

    private TooltipElementEventArgs args;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        args.UpdateEventData(eventData);
        PointerEnter?.Invoke(args);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        args.UpdateEventData(eventData);
        PointerExit?.Invoke(args);
    }

    private void Awake()
    {
        args = new TooltipElementEventArgs(this);
    }
}
