using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class DungeonSlot : ConfigSlot<DungeonSlot, DungeonConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public override event Action<DungeonSlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
