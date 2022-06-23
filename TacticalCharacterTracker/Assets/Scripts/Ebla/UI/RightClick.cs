using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Ebla.UI
{
    public class RightClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent onRightClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                onRightClick?.Invoke();            
        }
    }
}
