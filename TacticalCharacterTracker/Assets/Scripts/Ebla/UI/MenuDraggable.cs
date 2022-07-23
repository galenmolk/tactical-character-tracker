using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDraggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    // Offset for dragging the menu at a specific point.
    private Vector3 dragOffset;
    
    // The menu's starting position.
    private Vector3 startPos;

    private bool isOffsetCached;

    private Transform myTransform;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        CacheDragOffset(eventData);
        isOffsetCached = true;
    }
    
    // OnPointerDown can sometimes be intercepted by other raycastable components so we also cache the offset here.
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isOffsetCached)
            CacheDragOffset(eventData);
        
        myTransform.SetAsLastSibling();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        myTransform.position = Input.mousePosition - dragOffset;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        myTransform.position = Input.mousePosition - dragOffset;
        isOffsetCached = false;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isOffsetCached = false;
    }

    public void ResetPosition()
    {
        myTransform.position = startPos;
    }

    private void CacheDragOffset(PointerEventData eventData)
    {
        dragOffset = (Vector3)eventData.position - myTransform.position;
    }
    
    private void Start()
    {
        myTransform = transform;
        startPos = myTransform.position;
    }
}
