using UnityEngine;
using UnityEngine.Events;

namespace MolkExtras
{
    public class GenericAppEventListener<T> : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField] private GenericAppEvent<T> appEvent;
        [SerializeField] private UnityEvent<T> onAppEventInvoked;
        #pragma warning restore CS0649

        public void RaiseEvent(T t)
        {
            onAppEventInvoked?.Invoke(t);
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
