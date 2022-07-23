using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ebla.UI
{
    [RequireComponent(typeof(RectTransform), typeof(VerticalLayoutGroup))]
    public class ContextMenu : MonoBehaviour
    {
        public event Action<UnityEvent> OnOptionSelected;
        public event Action OnMenuClosed;

        public ContextMenuOption OptionPrefab => optionPrefab;
        [SerializeField] private ContextMenuOption optionPrefab;
        
        [SerializeField] private Transform optionParent;

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        public VerticalLayoutGroup VerticalLayoutGroup
        {
            get
            {
                if (layoutGroup == null)
                    layoutGroup = GetComponent<VerticalLayoutGroup>();

                return layoutGroup;
            }
        }

        private RectTransform rectTransform;
        private VerticalLayoutGroup layoutGroup;
        
        public void Configure(ContextMenuBehaviour behaviour)
        {
            CreateOptions(behaviour.Options);
        }

        public void Close()
        {
            OnMenuClosed?.Invoke();
        }
        
        private void CreateOptions(ContextMenuBehaviour.Option[] options)
        {
            for (int i = 0, length = options.Length; i < length; i++)
            {
                CreateOption(options[i]);
            }
        }

        private void CreateOption(ContextMenuBehaviour.Option option)
        {
            ContextMenuOption newOption = Instantiate(optionPrefab, optionParent);
            newOption.OnOptionSelected += HandleOptionSelected;
            newOption.Configure(option);
        }

        private void HandleOptionSelected(UnityEvent action)
        {
            OnOptionSelected?.Invoke(action);
        }

        private void OnDisable()
        {
            OnOptionSelected = null;
            OnMenuClosed = null;
        }
    }
}
