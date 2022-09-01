using UnityEngine;

namespace MolkExtras
{
    public class AppEventTrigger : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField] private AppEvent appEvent;
        #pragma warning restore CS0649

        public void Trigger()
        {
            appEvent.TriggerEvent();
        }
    }
}
