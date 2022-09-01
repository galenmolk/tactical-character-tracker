using System.Collections.Generic;
using UnityEngine;

namespace MolkExtras
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class AppEvent : ScriptableObject
    {
        private const string NAME = nameof(AppEvent);
        private const string MENU = "Custom Assets/" + NAME;

        private readonly HashSet<AppEventListener> listeners = new();

        public void TriggerEvent()
        {
            foreach (AppEventListener listener in listeners)
            {
                listener.RaiseEvent();
            }
        }

        public void Subscribe(AppEventListener listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(AppEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
