using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class AbilitySlot : ConfigSlot<AbilitySlot, AbilityConfig>
    {
        public override event Action<AbilitySlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}