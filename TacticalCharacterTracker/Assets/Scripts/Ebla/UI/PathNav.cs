using System.Collections.Generic;
using Ebla.Models;
using Ebla.Pooling;
using Ebla.Utils;
using UnityEngine;

namespace Ebla.UI
{
    public class PathNav : ScopeBehaviour
    {
        [SerializeField] private PathNavButtonPool buttonPool;
        
        protected override void HandleScopeChanged()
        {
            List<FolderConfig> folders = PathUtils.GetFolderConfigsInPath(ScopeController.Instance.CurrentFolder);

            for (int i = 0, count = folders.Count; i < count; i++)
            {
                PathNavButton button = buttonPool.Get();
                button.Transform.SetSiblingIndex(i);
                button.Configure(folders[i]);
            }
        }
    }
}
