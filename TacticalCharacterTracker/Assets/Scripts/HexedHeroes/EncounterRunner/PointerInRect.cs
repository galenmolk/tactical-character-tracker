using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerInRect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsPointerInRect { get; private set; }

    [SerializeField] private UnityEvent onPointerEnter;
    [SerializeField] private UnityEvent onPointerExit;
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        IsPointerInRect = true;
        onPointerEnter?.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        IsPointerInRect = false;
        onPointerExit?.Invoke();
    }
}
