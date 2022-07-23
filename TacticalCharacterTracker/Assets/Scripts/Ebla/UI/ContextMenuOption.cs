using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ebla.UI
{
    [RequireComponent(typeof(Button), typeof(RectTransform))]
    public class ContextMenuOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<UnityEvent> OnOptionSelected;

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
        
        [SerializeField] private TMP_Text text;

        private Button button;
        private UnityEvent action;
        
        public void Configure(ContextMenuBehaviour.Option option)
        {
            action = option.Action;
            text.text = option.Label;
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnOptionSelected?.Invoke(action);
        }
        
        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnDisable()
        {
            OnOptionSelected = null;
            button.onClick.RemoveAllListeners();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            text.color = new(1f, 1f, 1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.color = new(0f, 0f, 0f);
        }
    }
}
