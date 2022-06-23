using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Ebla.UI
{
    public class DoubleClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent onDoubleClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
                onDoubleClick?.Invoke();            
        }
    }
}
