using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerInRect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsPointerInRect { get; private set; }

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            if (IsPointerInRect && !value)
            {
                SetIsPointerInRect(false);
            }

            isActive = value;
        }
    }

    private bool isActive = true;

    [SerializeField] private UnityEvent onPointerEnter;
    [SerializeField] private UnityEvent onPointerExit;
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsActive)
        {
            return;
        }

        SetIsPointerInRect(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!IsActive)
        {
            return;
        }

        SetIsPointerInRect(false);
    }

    private void SetIsPointerInRect(bool isPointerInRect)
    {
        IsPointerInRect = isPointerInRect;

        UnityEvent unityEvent = isPointerInRect ? onPointerEnter : onPointerExit;
        unityEvent?.Invoke();
    }
}
