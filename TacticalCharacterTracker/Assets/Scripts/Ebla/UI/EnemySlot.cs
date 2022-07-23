using System;
using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla.UI
{
    public class EnemySlot : ConfigSlot<EnemySlot, EnemyConfig>
    {
        public override event Action<EnemySlot> OnReleaseObject;
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
