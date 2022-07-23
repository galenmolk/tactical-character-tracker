using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.Editing.Sections
{
    public abstract class ControlsSection<TValue> : MonoBehaviour
    {
        public TValue Value { get; private set; }
        
        [SerializeField] private UnityEvent<TValue> onValueModified;

        public void SubscribeToModifiedEvent(UnityAction<TValue> action)
        {
            onValueModified.RemoveListener(action);
            onValueModified.AddListener(action);
        }
        
        public abstract void TrySetValue(TValue newValue);

        protected void ModifyValue(TValue newValue)
        {
            Value = newValue;
            onValueModified?.Invoke(Value);
        }

        private void OnDestroy()
        {
            onValueModified.RemoveAllListeners();
        }
    }
}
