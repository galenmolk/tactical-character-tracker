using UnityEngine;
using UnityEngine.Events;

namespace Ebla.Editing.Sections
{
    public abstract class ControlsSection<TValue> : MonoBehaviour
    {
        public TValue Value { get; private set; }
        
        [SerializeField] private UnityEvent<TValue> onValueModified;

        public abstract void TrySetValue(TValue newValue);

        protected void ModifyValue(TValue newValue)
        {
            Value = newValue;
            onValueModified?.Invoke(Value);
        }
    }
}
