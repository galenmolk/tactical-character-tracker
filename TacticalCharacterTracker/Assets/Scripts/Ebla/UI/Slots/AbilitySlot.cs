using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class AbilitySlot : ConfigSlot<AbilitySlot, AbilityConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
    {
        public override event Action<AbilitySlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
