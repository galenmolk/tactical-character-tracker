using UnityEngine;

namespace Ebla.Utils
{
    public class AppEventTrigger : MonoBehaviour
    {
        [SerializeField] private AppEvent appEvent;

        public void Trigger()
        {
            appEvent.TriggerEvent();
        }
    }
}
