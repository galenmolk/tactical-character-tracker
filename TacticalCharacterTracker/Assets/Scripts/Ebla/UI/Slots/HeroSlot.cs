using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class HeroSlot : ConfigSlot<HeroSlot, HeroConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public override event Action<HeroSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
