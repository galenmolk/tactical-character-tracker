using System.Collections;
using MolkExtras;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeselectableMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    [SerializeField] private UnityEvent menuClose;
    
    private bool pointerInRect;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerInRect = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerInRect = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    
    public virtual void OnDeselect(BaseEventData eventData)
    {
        if (pointerInRect)
        {
            return;
        }

        this.ExecuteWhen(() => Input.GetMouseButtonUp(0), () =>
        {
            menuClose.Invoke();
        });
    }
}
