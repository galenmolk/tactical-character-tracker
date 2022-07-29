using System;
using Ebla.Models;

namespace Ebla.UI.Slots
{
    public class FolderSlot : ConfigSlot<FolderSlot, FolderConfig>
    {
        public static event Action<FolderSlot> OnOpenFolder;
        
        public override event Action<FolderSlot> OnReleaseObject;

        public void OpenFolder()
        {
            OnOpenFolder?.Invoke(this);
        }
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
    }
}
