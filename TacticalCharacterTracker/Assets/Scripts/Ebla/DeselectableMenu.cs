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
        Debug.Log("OnPointerEnter");
        pointerInRect = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

        pointerInRect = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    
    public virtual void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect");

        if (pointerInRect)
        {
            return;
        }
        Debug.Log("menuClose");

        menuClose.Invoke();

        // StartCoroutine(DelayDeselectInvoke());
    }
    
    private IEnumerator DelayDeselectInvoke()
    {
        yield return YieldRegistry.WaitUntil(() => Input.GetMouseButtonUp(0));
        menuClose.Invoke();
    }
}
