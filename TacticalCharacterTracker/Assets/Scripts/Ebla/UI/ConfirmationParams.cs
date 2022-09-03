using Ebla.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.UI
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class ConfirmationParams : ScriptableObject
    {
        private const string NAME = nameof(ConfirmationParams);
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        public string Title => title;
        public Color ConfirmButtonColor => confirmButtonColor;
        public Color DenyButtonColor => denyButtonColor;
        public Color ConfirmButtonTextColor => confirmButtonTextColor;
        public Color DenyButtonTextColor => denyButtonTextColor;
        public string ConfirmButtonText => confirmButtonText;
        public string DenyButtonText => denyButtonText;
        public Sprite Icon => icon;
        public bool IsDenyButtonActive => isDenyButtonActive;

        private readonly UnityEvent actionToConfirm = new();
        
        [Header("Title")]
        [SerializeField] private string title;
        
        [Space(5), Header("Confirm Button")]
        [SerializeField] private string confirmButtonText;
        [SerializeField] private Color confirmButtonTextColor;
        [SerializeField] private Color confirmButtonColor;
        
        [Space(5), Header("Deny Button")]
        [SerializeField] private string denyButtonText;
        [SerializeField] private Color denyButtonTextColor;
        [SerializeField] private Color denyButtonColor;
        
        [Space(5), Header("Icon")]
        [SerializeField] private Sprite icon;

        [SerializeField] private bool isDenyButtonActive;
        
        public void SetTitle(string newTitle)
        {
            title = newTitle;
        }
        
        public void LoadAction(UnityAction action)
        {
            actionToConfirm?.RemoveAllListeners();
            
            if (action != null)
            {
                actionToConfirm?.AddListener(action);
            }
        }

        public void InvokeAction()
        {
            actionToConfirm?.Invoke();
            actionToConfirm?.RemoveAllListeners();
        }

        public void ClearAction()
        {
            actionToConfirm?.RemoveAllListeners();
        }
    }
}
