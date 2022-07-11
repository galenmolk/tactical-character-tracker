using UnityEngine;

namespace Ebla.Selection
{
    public abstract class FilterElement<TValue> : MonoBehaviour
    {
        public abstract bool IsValid(TValue value);
    }
}
