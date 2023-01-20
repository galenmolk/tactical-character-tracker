using UnityEngine;

namespace Ebla.Utils
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU, order = 0)]
    public class HotKeyMap : ScriptableObject
    {
        private const string NAME = nameof(HotKeyMap);
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        public KeyCode Back => back;
        [SerializeField] private KeyCode back;

        public KeyCode ForceExecute => forceExecute;
        [SerializeField] private KeyCode forceExecute;

        public KeyCode ReduceCooldowns => reduceCooldowns;
        [SerializeField] private KeyCode reduceCooldowns;
    }
}
