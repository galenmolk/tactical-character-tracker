using System;
using Ebla.Models;
using UnityEngine.EventSystems;

namespace Ebla.UI.Slots
{
    public class FolderSlot : ConfigSlot<FolderSlot, FolderConfig>, IBeginDragHandler, IDragHandler, IEndDragHandler
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

        protected override void HandleConfigRemoved(BaseConfig baseConfig)
        {
            RemoveAllConfigs();
            base.HandleConfigRemoved(baseConfig);
        }

        private void RemoveAllConfigs()
        {
            // TODO THIS IS BREAKING WHY IS CONFIGS BEING MODIFIEd
            foreach (BaseConfig baseConfig in Config.Configs)
            {
                baseConfig.DeleteConfig();
            }
        }
    }
}
