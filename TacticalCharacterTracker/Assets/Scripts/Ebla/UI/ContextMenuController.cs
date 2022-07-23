using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
            if (activeMenuInstance != null)
            {
                HandleMenuClosed();
            }
            
            AdjustPivot(behaviour.Options.Length);
            activeMenuInstance = Instantiate(menuPrefab, Input.mousePosition, Quaternion.identity, contextMenuParent);
            activeMenuInstance.OnOptionSelected += HandleMenuOptionSelected;
            activeMenuInstance.OnMenuClosed += HandleMenuClosed;
            activeMenuInstance.Configure(behaviour);
        }

        private void AdjustPivot(int optionCount)
        {
            RectTransform menuRectTransform = menuPrefab.RectTransform;
            Rect menuRect = menuRectTransform.rect;
            
            Vector2 mousePos = Input.mousePosition;
            Debug.Log($"mousePos {mousePos}");

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            
            float xPivot = GetXPivot(menuRect.width, mousePos.x, screenSize.x);
            float yPivot = GetYPivot(optionCount, mousePos.y);

            menuRectTransform.pivot = new Vector2(xPivot, yPivot);
        }

        private float GetXPivot(float menuWidth, float xPos, float screenWidth)
        {
            return xPos + menuWidth > screenWidth ? 1f : 0f;
        }

        private float GetYPivot(int optionCount, float yPos)
        {
            VerticalLayoutGroup layoutGroup = menuPrefab.VerticalLayoutGroup;
            float spacing = layoutGroup.spacing;
            RectOffset padding = layoutGroup.padding;
            float optionHeight = menuPrefab.OptionPrefab.RectTransform.rect.height;
            
            float menuHeight = padding.top + padding.bottom;
            
            // Only add spacing equal to the spaces in between -- so one less than the total.
            for (int i = 0; i < optionCount; i++)
            {
                menuHeight += optionHeight;

                if (i < optionCount - 1)
                {
                    menuHeight += spacing;
                }
            }
            
            Debug.Log($"menuHeight {menuHeight}");

            return yPos - menuHeight < 0 ? 0f : 1f;
        }

        private void HandleMenuOptionSelected(UnityEvent action)
        {
            HandleMenuClosed();
            action?.Invoke();
        }

        private void HandleMenuClosed()
        {
            Destroy(activeMenuInstance.gameObject);
            activeMenuInstance = null;
        }
    }
}
