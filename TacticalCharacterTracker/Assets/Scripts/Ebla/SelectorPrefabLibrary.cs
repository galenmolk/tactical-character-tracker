using Ebla.Selection;
using UnityEngine;

namespace Ebla
{
    public class SelectorPrefabLibrary : MonoBehaviour
    {
        public AbilitySelector AbilitySelector => abilitySelectorPrefab;
        [SerializeField] private AbilitySelector abilitySelectorPrefab;
    }
}
