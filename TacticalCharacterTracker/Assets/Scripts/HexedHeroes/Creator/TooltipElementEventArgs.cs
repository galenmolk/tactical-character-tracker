using System;
using UnityEngine.EventSystems;

public class TooltipElementEventArgs : EventArgs
{
    public TooltipElement Element { get; }
    public PointerEventData EventData { get; private set; }

    public TooltipElementEventArgs(TooltipElement element)
    {
        Element = element;
    }

    public void UpdateEventData(PointerEventData eventData)
    {
        EventData = eventData;
    }
}
