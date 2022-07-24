using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class HeroSlot : ConfigSlot<HeroSlot, HeroConfig>
    {
        public override event Action<HeroSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
