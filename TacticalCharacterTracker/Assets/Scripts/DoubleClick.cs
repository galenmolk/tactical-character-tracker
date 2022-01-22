using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onDoubleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
            onDoubleClick.Invoke();
    }
}
