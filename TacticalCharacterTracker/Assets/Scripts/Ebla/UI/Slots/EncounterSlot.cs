using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class EncounterSlot : ConfigSlot<EncounterSlot, EncounterConfig>
    {
        public override event Action<EncounterSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
