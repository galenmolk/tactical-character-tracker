using System;
using System.Collections.Generic;
using Ebla.Models;
using Ebla.Utils;
using MolkExtras;

namespace Ebla
{
    public class ScopeController : Singleton<ScopeController>
    {
        public static event Action OnScopeChanged;
        
        public FolderConfig CurrentFolder { get; private set; }
        private readonly Dictionary<string, FolderConfig> folderRegistry = new();

        private FolderConfig RootFolder { get; set; }

        private List<BaseConfig> configs;
        private List<FolderConfig> folders;

        public void RegisterFolder(FolderConfig folderConfig)
        {
            folderRegistry.TryAdd(folderConfig.FullPath, folderConfig);
        }

        public bool TryGetFolderForPath(string path, out FolderConfig folderConfig)
        {
            return folderRegistry.TryGetValue(path, out folderConfig);
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            CreateRootFolder();
            // this.ExecuteAfterDelay(1f, () =>
            // {
            //     LoadScope(RootFolder);
            // });
        }
        
        private void CreateRootFolder()
        {
            RootFolder = new FolderConfig(PathUtils.ROOT_NAME);
            RegisterFolder(RootFolder);
            CurrentFolder = RootFolder;
        }

        public void LoadRoot()
        {
            LoadScope(RootFolder);
        }
        
        public void LoadScope(FolderConfig folderConfig)
        {
            CurrentFolder = folderConfig;
            
            OnScopeChanged?.Invoke();

            foreach (BaseConfig config in CurrentFolder.Configs)
            {
                config.InvokeLoadIntoFolder();
            }
        }
    }
}
