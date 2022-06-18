using System;
using UnityEngine;

namespace JsonFactory
{
    public abstract class PropertyBinding<T> : MonoBehaviour
    {
        public event Action<T> OnValueModified;

        public abstract void SetValue(T value);

        public abstract T GetValue();

        public void ValueModified()
        {
            OnValueModified?.Invoke(GetValue());
        }
    }
}
