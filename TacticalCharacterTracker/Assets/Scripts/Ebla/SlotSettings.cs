using Ebla.Utils;
using UnityEngine;

namespace Ebla
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class SlotSettings : ScriptableObject
    {
        private const string NAME = "Slot Settings";
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        public float DragIconReturnDuration => dragIconReturnDuration;
        [SerializeField, Min(0f)] private float dragIconReturnDuration;
    }
}
