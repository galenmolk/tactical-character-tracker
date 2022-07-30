using UnityEngine;

namespace Ebla.UI
{
    public abstract class ScopeBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            ScopeController.OnScopeChanged += HandleScopeChanged;
        }

        private void OnDestroy()
        {
            ScopeController.OnScopeChanged -= HandleScopeChanged;
        }

        protected abstract void HandleScopeChanged();
    }
}
