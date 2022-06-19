using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent OnLongPress = new UnityEvent();

    [Tooltip("The length of time in seconds the button must be pressed before onLongPress is invoked.")]
    [Min(0f)][SerializeField] private float holdTime = 3f;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke(nameof(InvokeLongPress), holdTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke(nameof(InvokeLongPress));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke(nameof(InvokeLongPress));
    }

    private void InvokeLongPress()
    {
        OnLongPress.Invoke();
    }
}
