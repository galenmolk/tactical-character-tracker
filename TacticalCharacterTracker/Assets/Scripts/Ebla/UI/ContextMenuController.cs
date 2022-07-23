using UnityEngine;
using UnityEngine.Events;

namespace Ebla.UI
{
    public class ContextMenuController : MonoBehaviour
    {
        [SerializeField] private ContextMenu menuPrefab;
        [SerializeField] private Transform contextMenuParent;
        
        private ContextMenu activeMenuInstance;
        
        private void OnEnable()
        {
            ContextMenuBehaviour.OnContextMenuRequested += HandleContextMenuRequested;
        }

        private void OnDisable()
        {
            ContextMenuBehaviour.OnContextMenuRequested -= HandleContextMenuRequested;
        }

        private void HandleContextMenuRequested(ContextMenuBehaviour behaviour)
        {
            Vector2 position = GetPosition(behaviour);
            activeMenuInstance = Instantiate(menuPrefab, position, Quaternion.identity, contextMenuParent);
            activeMenuInstance.OnOptionSelected += HandleMenuOptionSelected;
            activeMenuInstance.Configure(behaviour);
        }

        private void HandleMenuOptionSelected(UnityEvent action)
        {
            Destroy(activeMenuInstance.gameObject);
            activeMenuInstance = null;
            action?.Invoke();
        }
        
        private static Vector2 GetPosition(ContextMenuBehaviour obj)
        {
            return RectTransformUtility.WorldToScreenPoint(null, obj.RectTransform.position);
        }
    }
}
