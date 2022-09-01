using System.Collections.Generic;
using UnityEngine;

namespace MolkExtras
{
    public class GenericAppEvent<T> : ScriptableObject
    {
        private readonly HashSet<GenericAppEventListener<T>> listeners = new();
        
        public void TriggerEvent(T t)
        {
            foreach (GenericAppEventListener<T> listener in listeners)
            {
                listener.RaiseEvent(t);
            }
        }

        public void Subscribe(GenericAppEventListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(GenericAppEventListener<T> listener)
        {
            listeners.Remove(listener);
        }
    }
}
