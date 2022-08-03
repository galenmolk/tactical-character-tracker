using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class EnemySlot : ConfigSlot<EnemySlot, EnemyConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
    {
        public override event Action<EnemySlot> OnReleaseObject;
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
