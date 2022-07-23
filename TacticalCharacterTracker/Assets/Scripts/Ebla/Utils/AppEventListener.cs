using UnityEngine;
using UnityEngine.Events;

namespace Ebla.Utils
{
    public class AppEventListener : MonoBehaviour
    {
        [SerializeField] private AppEvent appEvent;

        [SerializeField] private UnityEvent onAppEventInvoked;

        private void Awake()
        {
            appEvent.ListenForEvent(HandleAppEvent);
        }

        private void HandleAppEvent()
        {
            onAppEventInvoked?.Invoke();
        }
    }
}
