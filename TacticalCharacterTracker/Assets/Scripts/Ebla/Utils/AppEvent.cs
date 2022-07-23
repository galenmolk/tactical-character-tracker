using UnityEngine;
using UnityEngine.Events;

namespace Ebla.Utils
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class AppEvent : ScriptableObject
    {
        private const string NAME = nameof(AppEvent);
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        private UnityEvent appEvent;

        public void ListenForEvent(UnityAction action)
        {
            appEvent.RemoveListener(action);
            appEvent.AddListener(action);
        }

        public void TriggerEvent()
        {
            appEvent?.Invoke();
        }
    }
}
