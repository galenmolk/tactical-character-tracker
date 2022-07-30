using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class FolderSlot : ConfigSlot<FolderSlot, FolderConfig>
    {
        public override event Action<FolderSlot> OnReleaseObject;

        public void OpenFolder()
        {
            ScopeController.Instance.LoadScope(Config);
        }
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
