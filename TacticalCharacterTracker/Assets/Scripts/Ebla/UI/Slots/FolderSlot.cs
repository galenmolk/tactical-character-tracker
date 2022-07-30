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
            for (int i = 0, count = Config.Configs.Count; i < count; i++)
            {
                Config.Configs[i].DeleteConfig();
            }
        }
    }
}
