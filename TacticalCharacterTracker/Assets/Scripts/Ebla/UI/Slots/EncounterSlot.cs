using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class EncounterSlot : ConfigSlot<EncounterSlot, EncounterConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public override event Action<EncounterSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
