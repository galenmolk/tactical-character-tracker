using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.Utils
{
    public class AppEventListener : MonoBehaviour
    {
        [SerializeField] private AppEvent appEvent;
        [SerializeField] private UnityEvent onAppEventInvoked;

        public void RaiseEvent()
        {
            onAppEventInvoked?.Invoke();
        }
        
        private void Awake()
        {
            appEvent.Subscribe(this);
        }

        private void OnDestroy()
        {
            appEvent.Unsubscribe(this);
        }
    }
}
