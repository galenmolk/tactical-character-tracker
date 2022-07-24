using System.Collections.Generic;
using Ebla.UI;
using Ebla.Utils;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class BackController : Singleton<BackController>
    {
        [SerializeField] private List<Window> windows = new();

        private bool didGoBackThisPress;
        
        public void RegisterWindow(Window window)
        {
            windows.Add(window);

            if (!enabled)
            {
                enabled = true;
            }
        }

        public void UnregisterWindow(Window window)
        {
            Debug.Log($"UnregisterWindow: {window.gameObject.name}");
            if (!windows.Remove(window))
            {
                Debug.Log($"UnregisterWindow: {window.gameObject.name} failure");
                return;
            }
            
            if (windows.Count == 0)
            {
                Debug.Log($"UnregisterWindow: {window.gameObject.name} disabling");
                enabled = false;
                didGoBackThisPress = false;
            }
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            enabled = false;
        }

        private void Update()
        {
            if (!Input.anyKey)
            {
                didGoBackThisPress = false;
            }

            if (didGoBackThisPress)
            {
                return;
            }

            if (!Input.GetKeyDown(HotKeys.Back))
            {
                return;
            }

            didGoBackThisPress = true;
            GoBack();
        }

        private void GoBack()
        {
            int index = windows.Count - 1;

            Window window = windows[index];

            windows.RemoveAt(index);
            
            if (windows.Count == 0)
            {
                enabled = false;
            }
            
            window.ClearWindow();
        }
    }
}
