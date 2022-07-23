using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.UI
{
    public class ContextMenu : MonoBehaviour
    {
        public Action<UnityEvent> OnOptionSelected;
        
        [SerializeField] private ContextMenuOption optionPrefab;
        [SerializeField] private Transform optionParent;

        private readonly List<ContextMenuOption> contextMenuOptions = new();

        public void Configure(ContextMenuBehaviour behaviour)
        {
            CreateOptions(behaviour.Options);
        }

        private void CreateOptions(ContextMenuBehaviour.Option[] options)
        {
            for (int i = 0, length = options.Length; i < length; i++)
            {
                contextMenuOptions.Add(CreateOption(options[i]));
            }
        }

        private ContextMenuOption CreateOption(ContextMenuBehaviour.Option option)
        {
            ContextMenuOption newOption = Instantiate(optionPrefab, optionParent);
            newOption.OnOptionSelected += HandleOptionSelected;
            newOption.Configure(option);
            return newOption;
        }

        private void HandleOptionSelected(UnityEvent action)
        {
            OnOptionSelected?.Invoke(action);
        }

        private void OnDisable()
        {
            OnOptionSelected = null;
        }
    }
}
