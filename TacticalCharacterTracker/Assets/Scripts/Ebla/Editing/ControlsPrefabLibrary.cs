using UnityEngine;

namespace Ebla.Editing
{
    public class ControlsPrefabLibrary : MonoBehaviour
    {
        public AbilityControls AbilityControls => abilityControls;
        [SerializeField] private AbilityControls abilityControls;
    }
}
