using UnityEngine;
using UnityEngine.Events;

namespace MolkExtras
{
    public class AppEventListener : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField] private AppEvent appEvent;
        [SerializeField] private UnityEvent onAppEventInvoked;
        #pragma warning restore CS0649
        
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
