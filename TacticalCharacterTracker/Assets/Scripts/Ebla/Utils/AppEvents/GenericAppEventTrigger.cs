using UnityEngine;

namespace MolkExtras
{
    public class GenericAppEventTrigger<T> : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField] private GenericAppEvent<T> appEvent;
        #pragma warning restore CS0649

        public void Trigger(T t)
        {
            appEvent.TriggerEvent(t);
        }
    }
}
