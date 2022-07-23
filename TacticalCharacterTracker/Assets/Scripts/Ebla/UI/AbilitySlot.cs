using System;
using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla.UI
{
    public class AbilitySlot : ConfigSlot<AbilitySlot, AbilityConfig>
    {
        public override event Action<AbilitySlot> OnReleaseObject;
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }

        protected override void RemoveConfig()
        {
            Librarian.Instance.Remove(Config);
        }
    }
}
