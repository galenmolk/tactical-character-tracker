using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class DungeonSlot : ConfigSlot<DungeonSlot, DungeonConfig>
    {
        public override event Action<DungeonSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
