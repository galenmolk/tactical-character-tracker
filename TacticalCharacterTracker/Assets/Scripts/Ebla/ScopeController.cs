using System;
using System.Collections.Generic;
using Ebla.Models;
using Ebla.UI;
using Ebla.UI.Slots;
using Ebla.Utils;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class ScopeController : Singleton<ScopeController>
    {
        public static event Action OnScopeChanged;
        
        public FolderConfig CurrentFolder { get; private set; }
        public FolderConfig LastFolder { get; private set; }

        [SerializeField] private ConfigBrowser configBrowser;
        
        private FolderConfig RootFolder { get; set; }

        // private Dictionary<string, FolderConfig> folderRegistry = new();

        private List<BaseConfig> configs;
        private List<FolderConfig> folders;

        private readonly Dictionary<string, FolderConfig> folderRegistry = new();

        // [ContextMenu("Create Folder System")]
        // public void CreateFolderSystem()
        // {
        //     folders = new List<FolderConfig>();
        //     configs = new List<BaseConfig>();
        //     configs.AddRange(AbilityLibrarian.Instance.GetAbilities());
        //     Debug.Log($"configs found: {configs.Count}");
        //
        //     // foreach (var config in configs)
        //     // {
        //     //     TryCreateAllFoldersForConfig(config);
        //     // }
        //
        //     string folderLog = string.Empty;
        //     foreach (var folderConfig in folders)
        //     {
        //         folderLog += $"Folder Name {folderConfig.Name}, Files: {folderConfig.Configs.Stringify(file => file.Name)}, " +
        //                      $"Parent: {folderConfig.Parent.Name}\n"; 
        //     }
        //     Debug.Log(folderLog);
        //     
        //     string configLog = string.Empty;
        //     foreach (var bc in configs)
        //     {
        //         configLog += $"Config Name {bc.Name}, Parent: {bc.Parent.Name}\n"; 
        //     }
        //     Debug.Log(configLog);
        // }

        // private void TryCreateAllFoldersForConfig(BaseConfig config)
        // {
        //     string[] parentFolderPaths = config.GetAllParentFolderPathsForConfig();
        //
        //     // If there are no parents, place this config in the root folder.
        //     if (!parentFolderPaths.TryGetLength(out int length))
        //     {
        //         RootFolder.AddConfigToFolder(config);
        //         return;
        //     }
        //
        //     FolderConfig nextParent = null;
        //
        //     // Starting with the root folder, iterate through all parent folders in the path.
        //     for (int i = 0; i < length; i++)
        //     {
        //         string folderPath = parentFolderPaths[i];
        //         
        //         // If a FolderConfig for this path can't be found, create it and add it to the registry. 
        //         if (!folderRegistry.TryGetValue(folderPath, out FolderConfig folderConfig))
        //         {
        //             folderConfig = new FolderConfig(folderPath);
        //             folders.Add(folderConfig);
        //             folderRegistry.Add(folderPath, folderConfig);
        //         }
        //         
        //         // If this folder's parent hasn't been set yet, assign it to the last iteration's FolderConfig
        //         // i.e. the folder above this one. If this is the first iteration, next parent will be null,
        //         // so we place this folder in the root folder.
        //         if (folderConfig.Parent == null)
        //         {
        //             folderConfig.UpdateParent(nextParent ?? RootFolder);
        //         }
        //         
        //         // This folder will be the parent folder for the next iteration.
        //         nextParent = folderConfig;
        //
        //         // If this is the last folder in the path, we place the config inside it.
        //         if (i == length - 1)
        //         {
        //             folderConfig.AddConfigToFolder(config);
        //         }
        //     }
        // }

        public void RegisterFolder(FolderConfig folderConfig)
        {
            Debug.Log($"Registering folder {folderConfig.Path}");
            folderRegistry.TryAdd(folderConfig.FullPath, folderConfig);
        }

        public bool TryGetFolderForPath(string path, out FolderConfig folderConfig)
        {
            return folderRegistry.TryGetValue(path, out folderConfig);
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            FolderSlot.OnOpenFolder += HandleOpenFolder;
            CreateRootFolder();
        }
        
        private void CreateRootFolder()
        {
            RootFolder = new FolderConfig();
            RootFolder.UpdateName(PathUtils.ROOT_NAME);
            RegisterFolder(RootFolder);
            CurrentFolder = RootFolder;
        }

        [UnityEngine.ContextMenu("Open Root Folder")]
        public void OpenRootFolder()
        {
            LoadScope(RootFolder);
        }

        [UnityEngine.ContextMenu("GoBack")]
        public void GoBack()
        {
            if (LastFolder != null)
            {
                LoadScope(LastFolder);
            }
        }
        
        private void HandleOpenFolder(FolderSlot folderSlot)
        {
            if (folderSlot.Config == CurrentFolder)
            {
                Debug.Log("Opening folder that's already open. returning");
                return;
            }

            LoadScope(folderSlot.Config);
        }

        private void LoadScope(FolderConfig folderConfig)
        {
            OnScopeChanged?.Invoke();

            LastFolder = CurrentFolder;
            CurrentFolder = folderConfig;
            
            Debug.Log($"LoadScopt: {CurrentFolder.Name}");

            
            Debug.Log($"config count {CurrentFolder.Configs.Count}");
            foreach (BaseConfig config in CurrentFolder.Configs)
            {
                config.InvokeLoadIntoFolder();
            }
        }
    }
}
